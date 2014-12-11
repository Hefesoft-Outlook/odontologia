using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Printing;

namespace Hefesoft.Odontograma.Util.Reportes
{
    public partial class Reportes_Template : FrameworkElement
    {

        public event EventHandler<PrintingEventArgs> BeginBuildReportItem;
        protected void OnBeginBuildReportItem(PrintingEventArgs args)
        {
            if (BeginBuildReportItem != null)
                BeginBuildReportItem(this, args);
        }

        public event EventHandler<PrintingEventArgs> BeginBuildReportFooter;
        protected void OnBeginBuildReportFooter(PrintingEventArgs args)
        {
            if (BeginBuildReportFooter != null)
                BeginBuildReportFooter(this, args);
        }

        //private void PrintDocumentBeginPrintHandler(object sender, BeginPrintEventArgs e)
        //{
        //    OnBeginPrint(EventArgs.Empty);
        //}

        //private void PrintDocumentEndPrintHandler(object sender, EndPrintEventArgs e)
        //{
        //    OnEndPrint(EventArgs.Empty);

        //    // remove event handlers so we don't have any references hanging around
        //    _printDocument.PrintPage -= PrintDocumentPrintPageHandler;
        //    _printDocument.BeginPrint -= PrintDocumentBeginPrintHandler;
        //    _printDocument.EndPrint -= PrintDocumentEndPrintHandler;
        //}

        //public void Print()
        //{
        //    Print(null);
        //}

        //public void Print(PrinterFallbackSettings settings)
        //{
        //    CurrentPageNumber = 0;
        //    _pageTreeIndex = 0;

        //    _printDocument.PrintPage += PrintDocumentPrintPageHandler;
        //    _printDocument.BeginPrint += PrintDocumentBeginPrintHandler;
        //    _printDocument.EndPrint += PrintDocumentEndPrintHandler;

        //    if (settings != null)
        //        _printDocument.Print(Title, settings);
        //    else
        //        _printDocument.Print(Title);
        //}


        /// <summary>
        /// Gets or sets the grid.
        /// </summary>
        /// <value>The grid.</value>
        public Grid Grid
        {
            get { return (Grid)GetValue(GridProperty); }
            set { SetValue(GridProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty GridProperty =
            DependencyProperty.Register("Grid", typeof(Grid), typeof(Reportes_Template), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Report"/> is texto.
        /// </summary>
        /// <value><c>true</c> if texto; otherwise, <c>false</c>.</value>
        public bool Texto
        {
            get { return (bool)GetValue(TextoProperty); }
            set { SetValue(TextoProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TextoProperty =
            DependencyProperty.Register("Texto", typeof(bool), typeof(Reportes_Template), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Report"/> is imagen.
        /// </summary>
        /// <value><c>true</c> if imagen; otherwise, <c>false</c>.</value>
        public bool Imagen
        {
            get { return (bool)GetValue(ImagenProperty); }
            set { SetValue(ImagenProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ImagenProperty =
            DependencyProperty.Register("Imagen", typeof(bool), typeof(Reportes_Template), new PropertyMetadata(null));


        /// <summary>
        /// Gets or sets the imagen odontograma.
        /// </summary>
        /// <value>The imagen odontograma.</value>
        public Image ImagenOdontograma
        {
            get { return (Image)GetValue(ImagenOdontogramaProperty); }
            set { SetValue(ImagenOdontogramaProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ImagenOdontogramaProperty =
            DependencyProperty.Register("ImagenOdontograma", typeof(Image), typeof(Reportes_Template), new PropertyMetadata(null));



        public DataTemplate PageHeaderTemplate
        {
            get { return (DataTemplate)GetValue(PageHeaderTemplateProperty); }
            set { SetValue(PageHeaderTemplateProperty, value); }
        }

        public static readonly DependencyProperty PageHeaderTemplateProperty =
            DependencyProperty.Register("PageHeaderTemplate", typeof(DataTemplate), typeof(Reportes_Template), new PropertyMetadata(null));


        public DataTemplate PageFooterTemplate
        {
            get { return (DataTemplate)GetValue(PageFooterTemplateProperty); }
            set { SetValue(PageFooterTemplateProperty, value); }
        }

        public static readonly DependencyProperty PageFooterTemplateProperty =
            DependencyProperty.Register("PageFooterTemplate", typeof(DataTemplate), typeof(Reportes_Template), new PropertyMetadata(null));


        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(Reportes_Template), new PropertyMetadata(null));




        public DataTemplate ReportFooterTemplate
        {
            get { return (DataTemplate)GetValue(ReportFooterTemplateProperty); }
            set { SetValue(ReportFooterTemplateProperty, value); }
        }

        public static readonly DependencyProperty ReportFooterTemplateProperty =
            DependencyProperty.Register("ReportFooterTemplate", typeof(DataTemplate), typeof(Reportes_Template), new PropertyMetadata(null));



        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(Reportes_Template), new PropertyMetadata(null));




        //public ItemCollection ItemsOnCurrentPage
        //{
        //    get { return (ItemCollection)GetValue(ItemsOnCurrentPageProperty); }
        //    private set { SetValue(ItemsOnCurrentPageProperty, value); }
        //}

        //public static readonly DependencyProperty ItemsOnCurrentPageProperty =
        //    DependencyProperty.Register("ItemsOnCurrentPage", typeof(ItemCollection), typeof(Reportes_Template), new PropertyMetadata(null));


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(Reportes_Template), new PropertyMetadata("Report"));




        public int CurrentPageNumber
        {
            get { return (int)GetValue(CurrentPageNumberProperty); }
            private set { SetValue(CurrentPageNumberProperty, value); }
        }

        public static readonly DependencyProperty CurrentPageNumberProperty =
            DependencyProperty.Register("CurrentPageNumber", typeof(int), typeof(Reportes_Template), new PropertyMetadata(0));



        public int TotalPageCount
        {
            get { return (int)GetValue(TotalPageCountProperty); }
            private set { SetValue(TotalPageCountProperty, value); }
        }

        public static readonly DependencyProperty TotalPageCountProperty =
            DependencyProperty.Register("TotalPageCount", typeof(int), typeof(Reportes_Template), new PropertyMetadata(0));



        private PrintDocument _printDocument = new PrintDocument();
        public PrintDocument PrintDocument
        {
            get { return _printDocument; }
        }


        public event EventHandler EndPrint;
        protected void OnEndPrint(EventArgs args)
        {
            if (EndPrint != null)
                EndPrint(this, args);
        }

        public event EventHandler BeginPrint;
        protected void OnBeginPrint(EventArgs args)
        {
            if (BeginPrint != null)
                BeginPrint(this, args);
        }

        public event EventHandler EndBuildReport;
        protected void OnEndBuildReport(EventArgs args)
        {
            if (EndBuildReport != null)
                EndBuildReport(this, args);
        }

        public event EventHandler BeginBuildReport;
        protected void OnBeginBuildReport(EventArgs args)
        {
            if (BeginBuildReport != null)
                BeginBuildReport(this, args);
        }

        public event EventHandler EndBuildReportItem;
        protected void OnEndBuildReportItem(EventArgs args)
        {
            if (EndBuildReportItem != null)
                EndBuildReportItem(this, args);
        }
    }
}
