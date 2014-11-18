using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Input;
using Cnt.Panacea.Entities.Odontologia;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace Cnt.Panacea.Xap.Odontologia
{
	public partial class SuperficieCompleta : UserControl
	{
		public SuperficieCompleta()
		{
			// Required to initialize variables
			InitializeComponent();
		}


        #region realizadoPiezaCompleta (DependencyProperty)
        /// <summary>
        /// A description of the property.
        /// </summary>
        public bool realizadoPiezaCompleta
        {
            get { return (bool)GetValue(realizadoPiezaCompletaProperty); }
            set { SetValue(realizadoPiezaCompletaProperty, value); }
        }
        public static readonly DependencyProperty realizadoPiezaCompletaProperty =
            DependencyProperty.Register("realizadoPiezaCompleta", typeof(bool), typeof(SuperficieCompleta),
            new PropertyMetadata(false, new PropertyChangedCallback(OnrealizadoPiezaCompletaChanged)));

        private static void OnrealizadoPiezaCompletaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SuperficieCompleta)d).OnrealizadoPiezaCompletaChanged(e);
        }

        protected virtual void OnrealizadoPiezaCompletaChanged(DependencyPropertyChangedEventArgs e)
        {
            var item = (bool)e.NewValue;

            if (item)
            {
                VisualStateManager.GoToState(piezaCompleta, "VisualStateRealizado", item);
            }
            else
            {
                VisualStateManager.GoToState(piezaCompleta, "VisualState", item);
            }
        }

        #endregion    
        

	}
}