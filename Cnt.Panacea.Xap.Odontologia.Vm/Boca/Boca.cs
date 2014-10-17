using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Entities.Parametrizacion;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Boca
{
    public class Boca : ViewModelBase, IDisposable
    {
        public Boca()
        {
            if (IsInDesignMode)
            {
            }
            else
            {
                ClickCommand = new RelayCommand(click);
                Elemento = new Odontograma.Odontograma();

                oirNuevoOdontograma();
                oirDiagnosticoProcedimientoSeleccionado();
                oirNivelSeveridad();
                oirTipoOdontograma();
                oirDesHacer();
            }
        }

        private void oirNuevoOdontograma()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Activar_Elementos>(this, elemento =>
            {
                Elemento.DiagnosticoProcedimiento.Boca.Clear();   
            });
        }

        private void oirNivelSeveridad()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<NivelSeveridadDXEntity>(this, nivel =>
            {
                Elemento.NivelSeveridadSeleccionado = nivel;
            });
        }

        private void oirDesHacer()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Estado_DesHacer>(this, Estado =>
            {
                Elemento.Estado_Deshacer = Estado;
            });
        }

        private void oirTipoOdontograma()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Cambiar_Tipo_Odontograma>(this, item =>
            {
                Elemento.Tipo_Odontograma_Actual = item;
                Elemento.pintarSuperficiesPlanTratamiento(item);
            });
        }

        private void oirDiagnosticoProcedimientoSeleccionado()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<ConfigurarDiagnosticoProcedimOtraEntity>(this, diagnostico =>
            {
                Elemento.Paleta_Selecionado = diagnostico;
            });
        }

        private void click()
        {
            if (Elemento.Estado_Deshacer == null)
            {
                Elemento.Estado_Deshacer = new Estado_DesHacer() { Estado = false };
            }

            if (Elemento.Tipo_Odontograma_Actual == null)
            {
                Elemento.Tipo_Odontograma_Actual = new Cambiar_Tipo_Odontograma();
                Elemento.Tipo_Odontograma_Actual.Tipo_Odontograma = Tipo_Odontograma.Inicial;
            }

            Elemento.codigoPiezaDental = 99;
            Elemento.codigoSPiezaDental = "99";

            Elemento.click("Boca");
        }

        private Odontograma.Odontograma elemento = new Odontograma.Odontograma() { Estado_Deshacer = new Estado_DesHacer() { Estado = false } };

        public Odontograma.Odontograma Elemento
        {
            get { return elemento; }
            set { elemento = value; RaisePropertyChanged("Elemento"); }
        }


        public RelayCommand ClickCommand { get; set; }

        public void Dispose()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<ConfigurarDiagnosticoProcedimOtraEntity>(this);            
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Cambiar_Tipo_Odontograma>(this);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Estado_DesHacer>(this);            
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<NivelSeveridadDXEntity>(this);                        
        }
    }
}
