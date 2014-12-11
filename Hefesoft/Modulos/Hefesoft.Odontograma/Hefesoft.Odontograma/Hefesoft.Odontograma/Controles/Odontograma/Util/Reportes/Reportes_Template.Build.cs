using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App2.Util.Reportes
{
    public partial class Reportes_Template : FrameworkElement    
    {

        //private int _pageTreeIndex = 0;
        //private void PrintDocumentPrintPageHandler(object sender, PrintPageEventArgs e)
        //{
        //    if (_pageTreeIndex == 0)
        //    {
        //        //BuildReport(e.PrintableArea);
        //        TotalPageCount = _pageTrees.Count;
        //        CurrentPageNumber = 0;
        //    }

        //    if (_pageTrees.Count > 0)
        //    {
        //        CurrentPageNumber++;
        //        e.PageVisual = _pageTrees[_pageTreeIndex];
        //    }

        //    e.HasMorePages = _pageTreeIndex < _pageTrees.Count - 1;

        //    _pageTreeIndex++;
        //}




        public List<UIElement> _pageTrees = new List<UIElement>();

        // Pre-calculates pages. This allows for total page count
        // as well as better handling of page breaks, and eventual
        // inclusion of groups and whatnot. If this takes too long, though
        // it'll exceed the timeout and make the printing fail in a 
        // sandboxed silverlight application. Alternative would be to make
        // printing a two-step operation: First, build report, Second, user 
        // clicks button to do the actual printing. That approach 
        // introduces issues with data changing in-between the steps.
        public void BuildReport(Size printableArea)
        {
            _pageTrees.Clear();
            CurrentPageNumber = 0;

            OnBeginBuildReport(EventArgs.Empty);

            if (ItemsSource == null)
                return;

            IEnumerable reportItems = ItemsSource;
            IEnumerator reportItemsEnumerator = reportItems.GetEnumerator();

            reportItemsEnumerator.Reset();

            StackPanel itemsPanel = null;
            Grid pagePanel = null;
            Size itemsPanelMaxSize = new Size();

            // create first page
            pagePanel = GetNewPage(printableArea, out itemsPanel, out itemsPanelMaxSize);

            // add the items
            while (reportItemsEnumerator.MoveNext())
            {
                object currentItem = reportItemsEnumerator.Current;

                PrintingEventArgs args = new PrintingEventArgs();
                args.DataContext = currentItem;
                OnBeginBuildReportItem(args);

                // create row. Set data context.
                FrameworkElement row = ItemTemplate.LoadContent() as FrameworkElement;
                row.DataContext = args.DataContext;

                // ContentPresenter approach was tried to see if it helped resolve Run
                // inlines with binding. No go.
                //ContentPresenter row = new ContentPresenter();
                //row.ContentTemplate = ItemTemplate;
                //row.Content = currentItem;

                row.Measure(printableArea);

                // create a new page if we're out of room here
                if (row.DesiredSize.Height + itemsPanel.DesiredSize.Height > itemsPanelMaxSize.Height)
                {
                    pagePanel = GetNewPage(printableArea, out itemsPanel, out itemsPanelMaxSize);
                }

                itemsPanel.Children.Add(row);
                itemsPanel.Measure(printableArea);
            }

            // create report footer. This goes at the very end of the report and typically
            // includes totals and other summary information
            if (ReportFooterTemplate != null)
            {
                PrintingEventArgs reportFooterEventArgs = new PrintingEventArgs();
                reportFooterEventArgs.DataContext = this;
                OnBeginBuildReportFooter(reportFooterEventArgs);

                FrameworkElement reportFooter = ReportFooterTemplate.LoadContent() as FrameworkElement;
                if (reportFooter != null)
                {
                    reportFooter.DataContext = reportFooterEventArgs.DataContext;
                    reportFooter.Measure(printableArea);

                    // fit the footer into the report
                    if (reportFooter.DesiredSize.Height + itemsPanel.DesiredSize.Height > itemsPanelMaxSize.Height)
                    {
                        pagePanel = GetNewPage(printableArea, out itemsPanel, out itemsPanelMaxSize);
                    }

                    itemsPanel.Children.Add(reportFooter);
                }
            }

            OnEndBuildReport(EventArgs.Empty);

        }


        private Grid GetNewPage(Size printableArea, out StackPanel itemsPanel, out Size itemsPanelMaxSize)
        {
            CurrentPageNumber++;

            int x = -1;

            Grid pagePanel = new Grid();

            RowDefinition Encabezado = new RowDefinition();
            Encabezado.Height = GridLength.Auto;

            RowDefinition Odontograma = new RowDefinition();
            Odontograma.Height = GridLength.Auto;

            RowDefinition headerRow = new RowDefinition();
            headerRow.Height = GridLength.Auto;

            RowDefinition itemsRow = new RowDefinition();
            itemsRow.Height = new GridLength(1, GridUnitType.Star);

            RowDefinition footerRow = new RowDefinition();
            footerRow.Height = GridLength.Auto;

            pagePanel.RowDefinitions.Add(Encabezado);
            pagePanel.RowDefinitions.Add(Odontograma);
            pagePanel.RowDefinitions.Add(headerRow);
            pagePanel.RowDefinitions.Add(itemsRow);
            pagePanel.RowDefinitions.Add(footerRow);


            x = x + 1;
            Grid.SetRow(Grid, x);
            pagePanel.Children.Add(Grid);

            if (Imagen == true)
            {
                try
                {
                    FrameworkElement a = ImagenOdontograma;
                    x = x + 1;
                    Grid.SetRow(a, x);
                    pagePanel.Children.Add(a);
                }
                catch { }
            }


            FrameworkElement header = PageHeaderTemplate.LoadContent() as FrameworkElement;
            header.DataContext = this;
            if (Texto == true)
            {
                x = x + 1;
                Grid.SetRow(header, x);
                pagePanel.Children.Add(header);

                header.Measure(new Size(printableArea.Width, printableArea.Height));
            }

            itemsPanel = new StackPanel();
            itemsPanel.Orientation = Orientation.Vertical;
            itemsPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
            itemsPanel.VerticalAlignment = VerticalAlignment.Top;

            if (Texto == true)
            {
                x = x + 1;
                Grid.SetRow(itemsPanel, x);
                pagePanel.Children.Add(itemsPanel);
            }


            FrameworkElement footer = PageFooterTemplate.LoadContent() as FrameworkElement;
            footer.DataContext = this;
            x = x + 1;
            Grid.SetRow(footer, x);
            pagePanel.Children.Add(footer);
            footer.Measure(new Size(printableArea.Width, printableArea.Height));

            itemsPanelMaxSize = new Size(printableArea.Width, printableArea.Height - footer.DesiredSize.Height - header.DesiredSize.Height);

            _pageTrees.Add(pagePanel);

            return pagePanel;
        }

    }
}
