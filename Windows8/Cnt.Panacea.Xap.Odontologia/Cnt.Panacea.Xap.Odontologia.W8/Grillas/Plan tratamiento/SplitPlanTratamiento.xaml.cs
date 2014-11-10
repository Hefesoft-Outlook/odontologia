using App2.Common;
using App2.Hub_Partial;
using Cnt.Panacea.Xap.Odontologia;
using Cnt.Panacea.Xap.Odontologia.Vm.Util.Plan_Tratamiento;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace App2.Grillas.Plan_tratamiento
{
    public sealed partial class SplitPlanTratamiento : UserControl
    {
        public SplitPlanTratamiento()
        {
            this.InitializeComponent();
            var vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Plan_tratamiento.GridPlanTratamientoProcedimientosWizard>();
            vm.pintarProcedimientosColoresPiezadental();
            itemListView.SelectionChanged += itemListView_SelectionChanged;
        }

        /// <summary>
        /// Se usa para dejar el numero de sesiones predeterminado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void itemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lst = sender as ListView;

            if (lst.SelectedItem != null)
            {
                var item = lst.SelectedItem as ProcedimientosGrillaPlanTratamiento;
                CmbBxNumeroSesiones.SelectedIndex = item.numeroSesion - 1;
                CmbBxTipoSesion.SelectedIndex = (int)item.OpcionesTratamientoValor;
            }
        }

        private void HyprlnkBaseBttn_Click(object sender, RoutedEventArgs e)
        {
            var hp = sender as HyperlinkButton;
            var item = hp.DataContext as ProcedimientosGrillaPlanTratamiento;

            var vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Plan_tratamiento.GridPlanTratamientoProcedimientosWizard>();
            vm.tomarElementoComoBase(item);
        }
    }
}
