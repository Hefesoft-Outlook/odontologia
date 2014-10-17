using Cnt.Panacea.Xap.Odontologia.Util.Messenger;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paleta;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Globalization;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Contenedor
{
    public class vm : ViewModelBase
    {
        public vm()
        {
            if (IsInDesignMode)
            {
            }
            else
            {
                IdentificadorPaciente = Variables_Globales.IdPaciente;

                InformesCommand = new RelayCommand<string>(CargarInformes);
                odontogramaInicialCommand = new RelayCommand(odontogramaInicial);
                odontogramaPlanTratamientoCommand = new RelayCommand(odontogramaPlanTratamiento);
                odontogramaEvolucionCommand = new RelayCommand(odontogramaEvolucion);
                procedimientosCommand = new RelayCommand(procedimientos);
                nuevoTratamientoCommand = new RelayCommand(nuevoTratamiento);
                guardarCommand = new RelayCommand(guardar);
                finalizarTratamientoCommand = new RelayCommand(finalizar);

                incializarElementos();

                
            }
        }

        private void finalizar()
        {
            //Mensaje que oye el formulario de evolucion para cerrar el tratamiento
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Finalizar.Finalizar());
        }

        private void incializarElementos()
        {
            procedimientos();
        }

        private void guardar()
        {
            //Funcionalidad para indicarle a cual formulario debe disparar el guardar
            if (Variables_Globales.Tipo_Odontograma_Activo == Tipo_Odontograma.Inicial)
            {
                // El segundo parametro es un token que oye el formulario Assets\Tipos de odontograma\Odontograma_Inicial
                // Para que solo ese formulario ejecute la accion de guardado
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Guardar_Barra_Comando(), "Inicial");
            }
        }

        private void nuevoTratamiento()
        {
            // Indicarle a los formularios que deben estar en estado  odontograma inicial
            Variables_Globales.IdTratamientoActivo = 0;
            Variables_Globales.Tipo_Odontograma_Activo = Tipo_Odontograma.Inicial;
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cambiar_Tipo_Odontograma() { Tipo_Odontograma = Tipo_Odontograma.Inicial });
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Pedir_Pintar_Datos() { nuevoOdontograma = true });
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new cargar_Diagnosticos_Procedimientos() { tipo = Tipo.todos });
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Activar_Elementos() { valor = "Nuevo" });            
        }

        private void procedimientos()
        {
            ventanaProcedimientos();
        }

        internal void ventanaProcedimientos()
        {
            //Le envia un mensaje al contenedor indicandole que debe mostrar la ventana de procedimientos
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana() { Nombre = "Mostrar Procedimientos" });
        }

        private void odontogramaEvolucion()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cambiar_Tipo_Odontograma() { Tipo_Odontograma = Tipo_Odontograma.Evolucion });
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Activar_Elementos() { valor = "Evolucion" });
        }

        private void odontogramaPlanTratamiento()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cambiar_Tipo_Odontograma() { Tipo_Odontograma = Tipo_Odontograma.Plan_Tratamiento });
        }

        private void odontogramaInicial()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cambiar_Tipo_Odontograma() { Tipo_Odontograma = Tipo_Odontograma.Inicial });
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new cargar_Diagnosticos_Procedimientos() { tipo = Tipo.todos });
        }

        private void CargarInformes(string obj)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana()
            {
                Nombre = "Reporte"
            });
        }


        public RelayCommand<string> InformesCommand { get; set; }

        public string nombreInforme { get; set; }

        public RelayCommand odontogramaInicialCommand { get; set; }

        public RelayCommand odontogramaPlanTratamientoCommand { get; set; }

        public RelayCommand odontogramaEvolucionCommand { get; set; }

        public RelayCommand procedimientosCommand { get; set; }

        public RelayCommand nuevoTratamientoCommand { get; set; }

        public RelayCommand guardarCommand { get; set; }

        private long identificadorPaciente;

        public long IdentificadorPaciente
        {
            get { return identificadorPaciente; }
            set { identificadorPaciente = value; RaisePropertyChanged("IdentificadorPaciente"); }
        }



        public RelayCommand finalizarTratamientoCommand { get; set; }
    }
}
