using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Hefesoft.Util.W8.UI.Assets.BusyBox
{
    public sealed partial class BusyBox : UserControl
    {
        public BusyBox()
        {
            this.InitializeComponent();
        }


        #region Texto (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public string Texto
        {
            get { return (string)GetValue(TextoProperty); }
            set { SetValue(TextoProperty, value); }
        }
        public static readonly DependencyProperty TextoProperty =
            DependencyProperty.Register("Texto", typeof(string), typeof(BusyBox),
            new PropertyMetadata("Cargando....", new PropertyChangedCallback(OnTextoChanged)));

        private static void OnTextoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BusyBox)d).OnTextoChanged(e);
        }

        private void OnTextoChanged(DependencyPropertyChangedEventArgs e)
        {
            var item = e.NewValue.ToString();
            TxtBlckCargando.Text = item;
        }

        
        #endregion
        

        #region IsBusy (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }
        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(BusyBox),
            new PropertyMetadata(false, new PropertyChangedCallback(OnIsBusyChanged)));

        private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BusyBox)d).OnIsBusyChanged(e);
        }

        private void OnIsBusyChanged(DependencyPropertyChangedEventArgs e)
        {
            var item = (bool)e.NewValue;
            if (item)
            {
                GrdBusy.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                GrdBusy.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }            
        }


        #endregion
        
    }
}
