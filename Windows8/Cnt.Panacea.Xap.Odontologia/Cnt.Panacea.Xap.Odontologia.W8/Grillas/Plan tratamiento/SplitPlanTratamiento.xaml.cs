using App2.Common;
using App2.Hub_Partial;
using Cnt.Panacea.Xap.Odontologia;
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
        }
    }
}
