using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Cnt.Panacea.Entities.Odontologia;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Cnt.Panacea.Xap.Odontologia.Util.Messenger;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Indices__Ceo__Ceop_;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using Cnt.Panacea.Xap.Odontologia.Clases;
using System.Collections.ObjectModel;


namespace Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm
{
    public partial class Odontograma_Inicial : ViewModelBase, IDisposable
    {
        public Odontograma_Inicial()
        {
            if (IsInDesignMode)
            {
            }
            else
            {
                mostrarGrillaTratamientosCommand = new RelayCommand(mostrarGrillaTratamientos);
                //Cuando algun evento dentro del modulo le pide que por favor cargue
                //El odontograma inicial dado una variable global
                oirCargarOdontogramaInicial();
                
                //Implementacion alternativa
                oirCargadoOdontogramaInicial();

                //Cuando se le indica que debe guardar entonces, este pide los datos al mapa dental (user control)
                //Este le devuelve un listado de entidades que el odontograma guardara
                oirGuardar();
            }
        }

        

        private void mostrarGrillaTratamientos()
        {
            Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana() 
            {
                Nombre = "Listado odontograma inicial"
            });
        }

        private void oirGuardar()
        {
            //Eliminar el evento si esta vivo
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Guardar_Barra_Comando>(this);

            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Guardar_Barra_Comando>(this, "Inicial", elemento => 
            {
                guardar();
            });
        }

        private void oirCargarOdontogramaInicial()
        {
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Util.Messenger.Cargar_Odontograma>(this, item =>
            {
                if (item.Odontogra_Cargar == "Inicial")
                {
                    CargarOdontograma();
                }
                else if (item.Odontogra_Cargar == "Nuevo")
                {
                    Messenger.Default.Send(new Pedir_Pintar_Datos() { nuevoOdontograma = true });
                }
            });
        }

        public async void CargarOdontograma()
        {
            Busy.UserControlCargando();            
            var resultado = await Contexto_Odontologia.obtenerContexto().ConsultarOdontogramaPaciente(Variables_Globales.IdTratamientoActivo, Variables_Globales.IdIps);

            if (resultado.Count > 0)
            {
                //Pedimos al control Mapa dental que pinte los datos que llegaron de la bd
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Pedir_Pintar_Datos() { lst = resultado });

                var convenio = await Contexto_Odontologia.obtenerContexto().consultarConvenio(Variables_Globales.IdConvenio);
                ConvenioAtencion = convenio;
                RaisePropertyChanged("ConvenioAtencion");
                Busy.UserControlCargando(false);

                if (Variables_Globales.OdontogramaPacientetity != 0)
                {

                    var Prestador = await Contexto_Odontologia.obtenerContexto().ConsultarPrestador(Variables_Globales.IdPrestador);//LFDO Bug 16006
                    PrestadorAtencion = Prestador;
                    RaisePropertyChanged("PrestadorAtencion");
                    //cargarProcedimientosAsociadosADiagnosticos(resultado);
                    TipoOdontograma.Inicial = false;
                    Odontologia.Clases.TipoOdontograma.InicialCargandoDatos = true;
                    //await tratamientoActivo(Variables_Globales.IdTratamientoActivo);
                    var OdontogramaPaciente = await Contexto_Odontologia.obtenerContexto().SelecionarOdontogramaPaciente(Variables_Globales.OdontogramaPacientetity);

                    Messenger.Default.Send(new Carga_Odontograma_Inicial() { Cantidad_Dientes = OdontogramaPaciente.CantidadDientes });
                    Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paleta.Paleta() { NumeroPiezasDentales = OdontogramaPaciente.CantidadDientes });
                }
                else
                {
                    Odontologia.Clases.TipoOdontograma.Inicial = true;
                    Odontologia.Clases.TipoOdontograma.InicialCargandoDatos = false;
                }
            }
        }        

        private void guardar()
        {
            // Le pedimos al mapa dental que nos devuelva el listado de elementos para guardar
            Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Guardar() { Pedir_Tipos_Guardar = Tipo_Odontograma.Inicial, lstGuardar = guardar });
        }

        //La respuesta del mapa dental
        private async void guardar(List<Entities.Odontologia.OdontogramaEntity> OdontogramaEntity)
        {
            Busy.UserControlCargando(true, "Guardando...");
            TratamientoEntity tratamientoPadre = new TratamientoEntity();            
            bool primerInicial = false;

            if (Variables_Globales.IdTratamientoActivo != 0)
            {
                tratamientoPadre.Identificador = Variables_Globales.IdTratamientoActivo;
            }
            else
            {
                tratamientoPadre = new TratamientoEntity();
                tratamientoPadre.TipoTratamiento = null;
                tratamientoPadre.EstadoTratamiento = EstadoTratamiento.ValoracionInicial;
                primerInicial = true;
            }

            tratamientoPadre.Sede = Variables_Globales.IdSede;
            tratamientoPadre.Paciente = Variables_Globales.IdPaciente;
            tratamientoPadre.EstadoTratamiento = EstadoTratamiento.ValoracionInicial;
            tratamientoPadre.AtencionInicial = Variables_Globales.IdAtencion;
            tratamientoPadre.Convenio = new Entities.Parametrizacion.ConvenioEntity();
            tratamientoPadre.Convenio.Identificador = Variables_Globales.IdConvenio;
            tratamientoPadre.FechaRegistro = DateTime.Now;
            tratamientoPadre.IdIps = Variables_Globales.IdIps;
            tratamientoPadre.Usuario = Variables_Globales.UsuarioActual;
            tratamientoPadre.IPOrigen = Variables_Globales.IpCliente;
            

            var odontogramaPaciente = new OdontogramasPacienteEntity();            
            odontogramaPaciente.Descripcion = Cnt.Panacea.Xap.Odontologia.Recursos.Mensajes.Odontograma_Inicial;            
            odontogramaPaciente.CantidadDientes = Convert.ToInt32(Variables_Globales.Numero_Piezas_Dentales);//JICR 17886
            odontogramaPaciente.Usuario = Variables_Globales.UsuarioActual;
            odontogramaPaciente.IPOrigen = Variables_Globales.IpCliente;

            int i = 0;

            foreach (var item in OdontogramaEntity)
            {
                item.Identificador = i + 1;
                item.EstadoRegistro = Std.EstadosEntidad.Creado;
                item.Usuario = Variables_Globales.UsuarioActual;
                item.Inicial = true;
                item.PrimerInicial = primerInicial;
                i = i + 1;
            }
            

            await guardarOdontograma
                (
                    tratamientoPadre,
                    OdontogramaEntity,
                    odontogramaPaciente
                );

            Busy.UserControlCargando(false);
        }

        private async Task guardarOdontograma(TratamientoEntity tratamientoPadre, List<OdontogramaEntity> observableCollection, OdontogramasPacienteEntity odontogramaPaciente)
        {
            //Valida que no este guardando ya contra el servicio
            //if (!Contexto_Odontologia.obtenerContexto().guardado_En_Proceso)
            //{
                var numeroOdontograma = await Contexto_Odontologia.obtenerContexto().GuardarOdontogramaInicial(tratamientoPadre, odontogramaPaciente, observableCollection, null, Variables_Globales.IdIps);

                Busy.UserControlCargando();
                //Este numero se usara para guardar tambien plan de tratamiento y evolucion
                Variables_Globales.IdTratamientoActivo = numeroOdontograma;
                
                //Unificar estos dos
                //*********************************************/*
                Variables_Globales.TratamientosPadre = await Contexto_Odontologia.obtenerContexto().SeleccionarTratamientoActivo(Variables_Globales.IdTratamientoActivo);
                Variables_Globales.IdTratamientoActivo = Variables_Globales.TratamientosPadre.Identificador;
                //*********************************************/*

                Busy.UserControlCargando(false);

                if (numeroOdontograma > 0)
                {
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                    {
                        Mensaje = string.Format("Odontograma guradado correctamente con id {0}", numeroOdontograma)
                    });

                    Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Activar_Elementos() { valor = "Plan Tratamiento" });
                }
            //}
        }

        public Entities.Parametrizacion.ConvenioEntity ConvenioAtencion { get; set; }

        public Entities.Parametrizacion.TerceroEntity PrestadorAtencion { get; set; }

        public void Dispose()
        {
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Util.Messenger.Cargar_Odontograma>(this);
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Guardar_Barra_Comando>(this);
            Messenger.Default.Unregister<ObservableCollection<OdontogramaEntity>>(this, "Odontograma cargado");
        }

        public RelayCommand mostrarGrillaTratamientosCommand { get; set; }
    }
}
