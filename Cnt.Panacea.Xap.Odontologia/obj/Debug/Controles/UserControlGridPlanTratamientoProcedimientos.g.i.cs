﻿#pragma checksum "C:\CNT\Version 1.2\DEV\Xap\Cnt.Panacea.Xap.OdontologiaDev\Cnt.Panacea.Xap.Odontologia\Cnt.Panacea.Xap.Odontologia\Controles\UserControlGridPlanTratamientoProcedimientos.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "10BA7BAC163FC756175CA809AFA8B789"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18408
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Cnt.Panacea.Xap.Odontologia {
    
    
    public partial class UserControlGridPlanTratamientoProcedimientos : System.Windows.Controls.UserControl {
        
        internal System.Windows.Media.Animation.Storyboard StoryboardComprobantesCerrar;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Infragistics.Controls.Grids.XamGrid gridTratamientos;
        
        internal System.Windows.Controls.TextBlock TxtBlokTotalPaciente;
        
        internal System.Windows.Controls.TextBlock TxtBlokTotalProcedimiento;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Cnt.Panacea.Xap.Odontologia;component/Controles/UserControlGridPlanTratamientoPr" +
                        "ocedimientos.xaml", System.UriKind.Relative));
            this.StoryboardComprobantesCerrar = ((System.Windows.Media.Animation.Storyboard)(this.FindName("StoryboardComprobantesCerrar")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.gridTratamientos = ((Infragistics.Controls.Grids.XamGrid)(this.FindName("gridTratamientos")));
            this.TxtBlokTotalPaciente = ((System.Windows.Controls.TextBlock)(this.FindName("TxtBlokTotalPaciente")));
            this.TxtBlokTotalProcedimiento = ((System.Windows.Controls.TextBlock)(this.FindName("TxtBlokTotalProcedimiento")));
        }
    }
}

