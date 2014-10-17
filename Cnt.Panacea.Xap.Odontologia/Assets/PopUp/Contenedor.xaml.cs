using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Xap.Odontologia.Clases;
using Cnt.Panacea.Xap.Odontologia.PopUp;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Evolucion.Util;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Linq;

namespace Cnt.Panacea.Xap.Odontologia.Assets.PopUp
{
    //Este control se encargara de oir cuando se necesite un tipo de pop up y generarlo
    public partial class Contenedor : UserControl, IDisposable
    {
        public Contenedor()
        {
            InitializeComponent();
            oirPedidoVentana();
        }

        private void oirPedidoVentana()
        {
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana>(this, generarVentana);
        }

        private void generarVentana(Vm.Messenger.Pop_Up.Mostrar_Ventana obj)
        {
            if (obj.Nombre == "Convenio")
            {
                ventanaConvenio(obj);
            }
            else if (obj.Nombre == "Supernumerario")
            {
                supernumerario(obj);
            }
            else if (obj.Nombre == "Evolucion")
            {
                Evolucion();
            }
            else if (obj.Nombre == "Tratamientos")
            {
                verTratamientos(obj);
            }
            else if (obj.Nombre == "Plan tratamiento")
            {
                planTratamiento(obj);
            }
            else if (obj.Nombre == "Listado odontograma inicial")
            {
                listadoOdontogramaInicial();
            }
            else if (obj.Nombre == "Listado imagenes")
            {
                listadoImagenes(obj);
            }
            else if (obj.Nombre == "Observaciones Evolucion")
            {
                Odontologia.PopUp.Observaciones Observacion = new Odontologia.PopUp.Observaciones();
                Observacion.Closing += (m, n) =>
                    {
                        obj.Resultado(Observacion.DialogResult);
                    };
                Observacion.Show();
            }
            else if (obj.Nombre == "Aprueba plan tratamiento")
            {
                Odontologia.PopUp.Aprueba_Plan_Tratamiento Aprueba = new Odontologia.PopUp.Aprueba_Plan_Tratamiento();
                Aprueba.Closing += (m, p) =>
                {
                    obj.Resultado(Aprueba.DialogResult);                    
                };
                Aprueba.Show();
            }
            else if (obj.Nombre == "Generar plan tratamiento")
            {
                generarPlan(obj);
            }
            else if (obj.Nombre == "Cotizacion")
            {
                cotizacion(obj);
            }
            else if (obj.Nombre == "Bodega")
            {
                bodega(obj);
            }
            else if (obj.Nombre == "Descargar imagenes")
            {
                Odontologia.PopUp.Descargar descargar = new Descargar();
                descargar.Imagen = obj.Propiedad_Adicional.Tratamiento;
                descargar.Show();
            }
            else if (obj.Nombre == "Sobreescribir Procedimiento")
            {
                SobreEscribirAdicionar confirmacion = new SobreEscribirAdicionar();
                confirmacion.Closed += (m, n) =>
                {
                    obj.Resultado(confirmacion.EstadoSobreEscribirAdicionar);                    
                };
                confirmacion.Show();
            }
            else if (obj.Nombre == "Descargar imagen")
            {
                Odontologia.PopUp.Descargar descargar = new Descargar();
                descargar.Imagen = obj.Propiedad_Adicional;
                descargar.Show();
            }
            else if (obj.Nombre == "Reporte")
            {   
                SeleccionReporte popReporte = new SeleccionReporte();
                popReporte.Show();
                popReporte.OKButton.Click += (sender, e) =>
                {
                    ListBoxItem listItem = popReporte.LstBoxReportes.SelectedItem as ListBoxItem;
                    var nombreInforme = listItem.Tag.ToString();
                    GenerarInforme(nombreInforme);
                };
            }
            else if (obj.Nombre == "Procedimientos evolucion")
            {
                //Antes modificacion
                //Odontologia.PopUp.ProcedimientosEvolucion Child = new Odontologia.PopUp.ProcedimientosEvolucion();
                //Child.DataContext = (this.DataContext as ViewModels.Odontograma).PacienteOdontogramaViewModel;
                //Child.OdontogramaSeleccionado = ((System.Windows.FrameworkElement)(sender)).DataContext as Clases.Odontograma;
                //Child.Show();
            }
            else if (obj.Nombre == "Requiere clasificador")
            {
                MensajeRequiereClasificador RequiereClasificador = new MensajeRequiereClasificador();
                RequiereClasificador.Show();
            }
            else if (obj.Nombre == "Mostrar Procedimientos")
            {
                //Mensaje mostrar ventana pacientes
                ////Se muestran los procedimientos
                var procedimientos = new Odontologia.Views.PacienteTratamientos();
                procedimientos.inicializar();
                Generico popUp = new Generico();
                //En que fila lo queremos ubicar
                Grid.SetRow(procedimientos, 0);
                popUp.LayoutRoot.Children.Add(procedimientos);
                popUp.OKButton.Visibility = Visibility.Collapsed;
                popUp.CancelButton.Visibility = Visibility.Collapsed;
                popUp.Width = 1100;
                popUp.Height = 500;
                popUp.Show();
            }
            else if (obj.Nombre == "Finalizar tratamiento")
            {
                Aprueba_Cerrar_Tratamiento cerrar = new Aprueba_Cerrar_Tratamiento();
                cerrar.Show();
                cerrar.Closing += (sender, e) =>
                {
                    if (obj.Resultado != null)
                    {
                        obj.Resultado(cerrar.DialogResult);
                    }
                };
            }
            else if (obj.Nombre == "Autorizacion") 
            {
                var context = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm.Evolucion>();
                Autorizacion autorizacion = new Autorizacion();
                autorizacion.DataContext = context;
                autorizacion.Show();
            }
            else if (obj.Nombre == "Finaliza Plan Tratamiento")
            {
                finalizarTratamiento(obj);
            }
            else if (obj.Nombre == "Ver imagen")
            {
                Descargar mostrarImagen = new Descargar() { Imagen = obj.Propiedad_Adicional };
                mostrarImagen.Show();
            }
        }

        private static void finalizarTratamiento(Vm.Messenger.Pop_Up.Mostrar_Ventana obj)
        {
            FinalizarTratamiento finalizar = new FinalizarTratamiento();
            if (!obj.Propiedad_Adicional.FinalizaCumplimientoProcedimientos)
            {
                finalizar.rbtnAbandonado.Visibility = Visibility.Visible;
            }

            finalizar.OKButton.Click += (sender, e) =>
            {
                if ((bool)finalizar.rbtnAbandonado.IsChecked)
                {
                    obj.Propiedad_Adicional.TratamientoPadre.EstadoTratamiento = EstadoTratamiento.Abandono;
                    obj.Propiedad_Adicional.TratamientoPadre.FechaFinal = DateTime.Now;
                    obj.Propiedad_Adicional.TratamientoPadre.ObservacionesTerminacion = finalizar.txtObservaciones.Text;
                    if (obj.Propiedad_Adicional.UsuarioFinaliza != "")
                    {
                        obj.Propiedad_Adicional.TratamientoPadre.Usuario = obj.Propiedad_Adicional.UsuarioFinaliza;
                    }
                    else
                    {
                        obj.Propiedad_Adicional.TratamientoPadre.Usuario = Variables_Globales.UsuarioActual;
                    }
                }
                else
                {
                    obj.Propiedad_Adicional.TratamientoPadre.EstadoTratamiento = EstadoTratamiento.Terminacion;
                    obj.Propiedad_Adicional.TratamientoPadre.FechaFinal = DateTime.Now;
                    obj.Propiedad_Adicional.TratamientoPadre.ObservacionesTerminacion = finalizar.txtObservaciones.Text;
                }
                obj.Resultado(null);
            };
        }

        /// <summary>
        /// Generars the informe.
        /// </summary>
        /// <param name="nombreInforme">The nombre informe.</param>
        private void GenerarInforme(string nombreInforme)
        {   
            long idTratamientoAux = Variables_Globales.IdTratamientoActivo;
            //LFDO para q no se envie el tratamiento cuando el reporte no lo requiere
            if (nombreInforme == "InfConfProcedDiagnos")
            { idTratamientoAux = 0; }

            string appuri = HtmlPage.Document.DocumentUri.ToString().Replace("Historia_Clinica/RegistrarAtencionDatosForm.aspx", "ReportesForm.aspx");
            string url = string.Format(CultureInfo.CurrentCulture, "{0}?reporte={1}&idtratamiento={2}",
                appuri, nombreInforme, idTratamientoAux);

            HtmlPage.Window.Invoke("MostarVentanaInformes", url);
        }

        private static void bodega(Vm.Messenger.Pop_Up.Mostrar_Ventana obj)
        {
            Odontologia.PopUp.Bodega Bodega = new Odontologia.PopUp.Bodega();
            ParametrosInsumos parametros = new ParametrosInsumos();

            var item = (ProcedimientosGrillaEvolucion)obj.Propiedad_Adicional;
            if (item.PlanTratamientoEntity.Articulos != null && item.PlanTratamientoEntity.Articulos.Any() && item.PlanTratamientoEntity.AplicacionesCanasta != null)
            {
                parametros.EstadoControl = Std.EstadosEntidad.Original;
                parametros.AplicacionesCanasta = obj.Propiedad_Adicional.PlanTratamientoEntity.AplicacionesCanasta;
            }
            else
            {
                parametros.AplicacionesCanasta = new Entities.Parametrizacion.AplicacionesCanastaDtoCollection();
                parametros.IdBodega = Variables_Globales.IdBodega;
                parametros.IdIps = Variables_Globales.IdIps;
                parametros.EstadoControl = Std.EstadosEntidad.Creado;
                parametros.IdPaciente = Variables_Globales.IdPaciente;
                parametros.IdPx = obj.Propiedad_Adicional.ProcedimientoEntity.Procedimiento.Identificador;
            }
            parametros.IdPlanTratamiento = obj.Propiedad_Adicional.PlanTratamientoEntity.Identificador;
            Bodega.ctrlBodega.DataContext = parametros;
            Bodega.CerrarVentanaEvento();
            Bodega.Show();
        }

        private static void cotizacion(Vm.Messenger.Pop_Up.Mostrar_Ventana obj)
        {
            var idCotizacion = obj.Propiedad_Adicional.e[0];
            Odontologia.PopUp.Mensaje_Plan_Tratamiento GenerarPlan = new Odontologia.PopUp.Mensaje_Plan_Tratamiento();
            GenerarPlan.txtIdCotizacion.Text = obj.Propiedad_Adicional.idCotizacion.ToString();

            GenerarPlan.Closing += (m, p) =>
            {
                MessageBox.Show(Cnt.Panacea.Xap.Odontologia.Recursos.Mensajes.Guardar_Odontograma);
            };
            GenerarPlan.IdTratamiento = Variables_Globales.IdTratamientoActivo.ToString();
            GenerarPlan.NombreTratamiento = obj.Propiedad_Adicional.TratamientoPadre.TipoTratamiento.Nombre;
            GenerarPlan.MensajeCotizacion = Visibility.Visible;
            GenerarPlan.CargarDatos();
            GenerarPlan.Show();
        }

        private static void generarPlan(Vm.Messenger.Pop_Up.Mostrar_Ventana obj)
        {
            Odontologia.PopUp.Mensaje_Plan_Tratamiento GenerarPlan = new Odontologia.PopUp.Mensaje_Plan_Tratamiento();
            GenerarPlan.IdTratamiento = Variables_Globales.IdTratamientoActivo.ToString();
            //GenerarPlan.NombreTratamiento = TratamientoPadre.TipoTratamiento.Nombre;//DFCF BUG 16105 
            GenerarPlan.NombreTratamiento = obj.Propiedad_Adicional.Descripcion;//DFCF BUG 16105 
            GenerarPlan.MensajeCotizacion = Visibility.Collapsed;
            GenerarPlan.CargarDatos();
            GenerarPlan.Show();
        }

        private static void listadoImagenes(Vm.Messenger.Pop_Up.Mostrar_Ventana obj)
        {
            Generico popUp = new Generico();
            var imagenes = new Cnt.Panacea.Xap.Odontologia.Assets.Adjuntar.Ver_Imagenes.Listado_Imagenes();
            Grid.SetRow(imagenes, 0);
            popUp.LayoutRoot.Children.Add(imagenes);

            popUp.OKButton.Visibility = Visibility.Collapsed;
            popUp.CancelButton.Content = "Cerrar";
            popUp.Width = 300;
            popUp.Height = 500;
            popUp.Show();
        }

        private static void listadoOdontogramaInicial()
        {
            Generico popUp = new Generico();
            popUp.Title = "Diagnosticos y Procedimientos odontograma inicial";
            var grilla = new Assets.Grillas.Inicial.vm.UserControlDiagnosticosOdontogramaInicial();
            grilla.inicializarElementos();

            Grid.SetRow(grilla, 0);
            popUp.LayoutRoot.Children.Add(grilla);
            popUp.Width = 900;
            popUp.Height = 500;
            popUp.Show();
        }

        private static void planTratamiento(Vm.Messenger.Pop_Up.Mostrar_Ventana obj)
        {
            Generico popUp = new Generico();
            var guardarPlanTratamientoControl = new UserControlGuardarPlanTratamiento();
            Grid.SetRow(guardarPlanTratamientoControl, 0);
            popUp.LayoutRoot.Children.Add(guardarPlanTratamientoControl);
            popUp.Title = "Plan de tratamiento";

            popUp.Closed += (m, n) =>
            {
                if (popUp.DialogResult == true)
                {
                    //Sacamos el datacontext del popup para tener los datos que debemos guardar
                    var dataContext = ((Cnt.Panacea.Xap.Odontologia.UserControlGuardarPlanTratamiento)(popUp.LayoutRoot.Children[2])).DataContext;
                    //Le enviamos el resultado al que envio el mensaje
                    obj.Resultado(dataContext);
                }
            };

            popUp.OKButton.Visibility = Visibility.Collapsed;
            popUp.CancelButton.Content = "Cancelar";
            popUp.Width = 1200;
            popUp.Height = 600;
            popUp.Show();
        }

        private static void verTratamientos(Vm.Messenger.Pop_Up.Mostrar_Ventana obj)
        {
            Generico popUp = new Generico();
            popUp.Title = "Diagnosticos y Procedimientos odontograma plan de tratamiento";

            //Se pasa el tratamiento padre
            var grilla = new Cnt.Panacea.Xap.Odontologia.UserControlGridPlanTratamiento();
            grilla.inicializarElementos();
            Grid.SetRow(grilla, 0);
            popUp.LayoutRoot.Children.Add(grilla);

            popUp.Width = 900;
            popUp.Height = 400;
            popUp.Show();
        }

        private static void Evolucion()
        {
            Generico popUp = new Generico();
            var grillaEvolucion = new Cnt.Panacea.Xap.Odontologia.Assets.Grillas.Evolucion.UserControlEvolucionProcedimientos();
            grillaEvolucion.inicializar();
            Grid.SetRow(grillaEvolucion, 0);
            popUp.LayoutRoot.Children.Add(grillaEvolucion);
            popUp.OKButton.Content = "Guardar";
            popUp.OKButton.Click += (m, n) =>
            {
                //Le enviamos el mensaje al formulario responsable que debe guardar
                Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Guardar() { }, "Evolucion");
            };

            popUp.Width = 1200;
            popUp.Height = 600;
            popUp.Show();
        }

        private static void supernumerario(Vm.Messenger.Pop_Up.Mostrar_Ventana obj)
        {
            Generico popUp = new Generico();
            //Como este viewmodel es el encargado de gestionar los supernumerarios a este le vamos a setear el contexto de datos
            var control = new Cnt.Panacea.Xap.Odontologia.Assets.Pieza_Dental.Supernumerarios.Agregar_Supernumerario() { DataContext = obj.DataContext };

            //Si es supernumerario no se debe dejar agregar otro al lado de el
            if (obj.Propiedad_Adicional.Supernumerario)
            {
                control.StckPnlIzquierdaDercha.Visibility = Visibility.Collapsed;
            }
            else
            {
                control.HyprlnkBttnEliminar.Visibility = Visibility.Collapsed;
            }


            Grid.SetRow(control, 0);
            popUp.LayoutRoot.Children.Add(control);
            popUp.Title = "Supernumerarios";
            popUp.OKButton.Visibility = Visibility.Collapsed;
            popUp.CancelButton.Visibility = Visibility.Collapsed;
            popUp.Width = 400;
            popUp.Height = 100;
            popUp.Show();
        }

        private static void ventanaConvenio(Vm.Messenger.Pop_Up.Mostrar_Ventana obj)
        {
            Generico popUp = new Generico();
            var convenios = new Convenios.Cambiar_Convenio();
            Grid.SetRow(convenios, 0);
            popUp.LayoutRoot.Children.Add(convenios);
            popUp.Closed += (m, n) =>
            {
                if (popUp.DialogResult == true && convenios.vm.ConvenioSeleccionado != null)
                {
                    obj.Resultado(Convert.ToInt16(convenios.vm.ConvenioSeleccionado.ConvenioId));
                }
            };
            popUp.Width = 400;
            popUp.Height = 400;
            popUp.Show();
        }

        public void Dispose()
        {
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Util.Messenger.Mostrar_Cargando>(this);
        }
    }
}
