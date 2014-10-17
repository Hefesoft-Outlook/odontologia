using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Entities.Parametrizacion;
using Cnt.Panacea.Xap.Dinamico.Controles;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Cnt.Panacea.Xap.Odontologia
{
    //Se mueve la clase por orden para hacer legible la clase principal
    public partial class MainPage : UserControl, IControlesAtencion
    {
        #region Propiedad Dependiente PlantillaOrdenSeleccionada
        /// <summary>
        /// Obtener o asignar el PlantillaOrdenSeleccionada.
        /// </summary>
        /// <value>El PlantillaOrdenSeleccionada.</value>
        public int PrestadorSeleccionado
        {
            get { return (int)GetValue(PrestadorSeleccionadoProperty); }
            set { SetValue(PrestadorSeleccionadoProperty, value); }
        }
        /// <summary>
        /// DependencyProperty como el alamcenamiento de PlantillaOrdenSeleccionada.  
        /// </summary>
        public static readonly DependencyProperty PrestadorSeleccionadoProperty =
            DependencyProperty.Register("PrestadorSeleccionado", typeof(int), typeof(MainPage),
            new PropertyMetadata(0, new PropertyChangedCallback(OnPrestadorSeleccionadoChanged)));

        private static void OnPrestadorSeleccionadoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainPage instancia = d as MainPage;
            int valor = (int)e.NewValue;
            Variables_Globales.IdPrestador = valor;

        }
        #endregion

        #region Propiedad idConvenio
        /// <summary>
        /// Obtener o asignar el PlantillaOrdenSeleccionada.
        /// </summary>
        /// <value>El PlantillaOrdenSeleccionada.</value>
        public short IdConvenio
        {
            get { return (short)GetValue(IdConvenioProperty); }
            set { SetValue(IdConvenioProperty, value); }
        }
        /// <summary>
        /// DependencyProperty como el alamcenamiento de PlantillaOrdenSeleccionada.  
        /// </summary>
        public static readonly DependencyProperty IdConvenioProperty =
            DependencyProperty.Register("IdConvenio", typeof(short), typeof(MainPage),
            new PropertyMetadata((short)0, new PropertyChangedCallback(OnIdConvenioPropertySeleccionadoChanged)));

        private static void OnIdConvenioPropertySeleccionadoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainPage instancia = d as MainPage;
            short valor = (short)e.NewValue;
            Variables_Globales.IdConvenio = valor;

        }
        #endregion

        #region Clasificador Odontologico PlantillaOrdenSeleccionada
        /// <summary>
        /// Obtener o asignar el PlantillaOrdenSeleccionada.
        /// </summary>
        /// <value>El PlantillaOrdenSeleccionada.</value>
        public bool ClasificadorOdontologicoSeleccionado
        {
            get { return (bool)GetValue(ClasificadorOdontologicoProperty); }
            set { SetValue(ClasificadorOdontologicoProperty, value); }
        }
        /// <summary>
        /// DependencyProperty como el alamcenamiento de PlantillaOrdenSeleccionada.  
        /// </summary>
        public static readonly DependencyProperty ClasificadorOdontologicoProperty =
            DependencyProperty.Register("ClasificadorOdontologicoSeleccionado", typeof(bool), typeof(MainPage),
            new PropertyMetadata(false, new PropertyChangedCallback(OnClasificadorOdontologicoChanged)));

        private static void OnClasificadorOdontologicoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainPage instancia = d as MainPage;
            bool valor = (bool)e.NewValue;
            Variables_Globales.ClasificadorOdontologico = valor;
        }
        #endregion

        #region Propiedad Id Paciente
        /// <summary>
        /// Obtener o asignar el PlantillaOrdenSeleccionada.
        /// </summary>
        /// <value>El PlantillaOrdenSeleccionada.</value>
        /// 
        // Esta es una de int
        public int IdPacienteSeleccionado
        {
            get { return (int)GetValue(IdPacienteProperty); }
            set { SetValue(IdPacienteProperty, value); }
        }
        /// <summary>
        /// DependencyProperty como el alamcenamiento de PlantillaOrdenSeleccionada.  
        /// </summary>
        public static readonly DependencyProperty IdPacienteProperty =
            DependencyProperty.Register("IdPacienteSeleccionado", typeof(int), typeof(MainPage),
            new PropertyMetadata(0, new PropertyChangedCallback(OnIdPacienteChanged)));

        private static void OnIdPacienteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainPage instancia = d as MainPage;
            int valor = (int)e.NewValue;
            Variables_Globales.IdPaciente = valor;
        }
        #endregion

        #region Propiedad Boton Cancelar


        public bool Cancelar
        {
            get { return (bool)GetValue(CancelarProperty); }
            set { SetValue(CancelarProperty, value); }
        }
        /// <summary>
        /// DependencyProperty como el alamcenamiento de PlantillaOrdenSeleccionada.  
        /// </summary>
        public static readonly DependencyProperty CancelarProperty =
            DependencyProperty.Register("Cancelar", typeof(bool), typeof(MainPage), new PropertyMetadata(false, new PropertyChangedCallback(OnCancelarChanged)));

        private static void OnCancelarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainPage instancia = d as MainPage;
            bool valor = (bool)e.NewValue;
        }


        #endregion

        #region Propiedad IdFinalidad
        /// <summary>
        /// Obtener o asignar el PlantillaOrdenSeleccionada.
        /// </summary>
        /// <value>El PlantillaOrdenSeleccionada.</value>
        /// 
        // Esta es una de int
        public int IdFinalidadProcedimiento
        {
            get { return (int)GetValue(IdFinalidadProcedimientoProperty); }
            set { SetValue(IdFinalidadProcedimientoProperty, value); }
        }
        /// <summary>
        /// DependencyProperty como el alamcenamiento de PlantillaOrdenSeleccionada.  
        /// </summary>
        public static readonly DependencyProperty IdFinalidadProcedimientoProperty =
            DependencyProperty.Register("IdFinalidadProcedimiento", typeof(int), typeof(MainPage),
            new PropertyMetadata(0, new PropertyChangedCallback(OnIdFinalidadChanged)));

        private static void OnIdFinalidadChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainPage instancia = d as MainPage;
            int valor = (int)e.NewValue;
            Variables_Globales.IdFinalidadProcedimiento = valor;
        }
        #endregion

        #region Propiedad IdAtencion
        /// <summary>
        /// Obtener o asignar el PlantillaOrdenSeleccionada.
        /// </summary>
        /// <value>El PlantillaOrdenSeleccionada.</value>
        /// 
        // Esta es una de int
        public long IdAtencion
        {
            get { return (long)GetValue(IdAtencionProperty); }
            set { SetValue(IdAtencionProperty, value); }
        }
        /// <summary>
        /// DependencyProperty como el alamcenamiento de PlantillaOrdenSeleccionada.  
        /// </summary>
        public static readonly DependencyProperty IdAtencionProperty =
            DependencyProperty.Register("IdAtencion", typeof(long), typeof(MainPage),
            new PropertyMetadata((long)-1, new PropertyChangedCallback(OnIdAtencionChanged)));


        private static void OnIdAtencionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainPage instancia = d as MainPage;
            long valor = (long)e.NewValue;
            Variables_Globales.IdAtencion = valor;
            ((MainPage)d).OnMyIdAtencionChanged(e);
            
        }

        protected virtual void OnMyIdAtencionChanged(DependencyPropertyChangedEventArgs e)
        {
            if ((long)e.NewValue > 0)
            {
                cargarOdontograma();
            }
        }

        #endregion




        #region Propiedad IdUsuario

        public string IdUsuario
        {
            get { return (string)GetValue(IdUsuarioProperty); }
            set { SetValue(IdUsuarioProperty, value); }
        }
        /// <summary>
        /// DependencyProperty como el alamcenamiento de PlantillaOrdenSeleccionada.  
        /// </summary>

        private static void OnIdUsuarioPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainPage instancia = d as MainPage;
            string valor = (string)e.NewValue;
            Variables_Globales.IdUsuario = valor;
        }


        public static readonly DependencyProperty IdUsuarioProperty =
           DependencyProperty.RegisterAttached("string", typeof(string), typeof(MainPage), new PropertyMetadata("", OnIdUsuarioPropertyChanged));


        #endregion Propiedad IdUsuario



        #region Propiedad IpCliente





        #region Propiedad IdTratamiento

        public string IdTratamiento
        {
            get { return (string)GetValue(IdTratamientoProperty); }
            set { SetValue(IdTratamientoProperty, value); }
        }
        /// <summary>
        /// DependencyProperty como el alamcenamiento de PlantillaOrdenSeleccionada.  
        /// </summary>

        private static void IdTratamientoPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainPage instancia = d as MainPage;
            string valor = (string)e.NewValue;
        }


        public static readonly DependencyProperty IdTratamientoProperty =
           DependencyProperty.RegisterAttached("string", typeof(string), typeof(MainPage), new PropertyMetadata("", IdTratamientoPropertyChanged));


        #endregion Propiedad IdTratamiento



        #region Propiedad Lista Ordenes





        public List<PlanTratamientoEntity> OrdenesSiguienteSesion
        {

            get { return (List<PlanTratamientoEntity>)GetValue(OrdenesSiguienteSesionProperty); }

            set { SetValue(OrdenesSiguienteSesionProperty, value); }

        }



        public static readonly DependencyProperty OrdenesSiguienteSesionProperty =

        DependencyProperty.Register("OrdenesSiguienteSesion", typeof(List<PlanTratamientoEntity>), typeof(MainPage), null);



        #endregion Propiedad Ordenes


        #region ModeloAtencion (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public AtencionHistoriaClinicaDto ModeloAtencion
        {
            get { return (AtencionHistoriaClinicaDto)GetValue(ModeloAtencionProperty); }
            set { SetValue(ModeloAtencionProperty, value); }
        }
        public static readonly DependencyProperty ModeloAtencionProperty =
            DependencyProperty.Register("ModeloAtencion", typeof(AtencionHistoriaClinicaDto), typeof(MainPage),
            new PropertyMetadata(null, new PropertyChangedCallback(OnModeloAtencionChanged)));
        private AtencionHistoriaClinicaDto Atencion;

        private static void OnModeloAtencionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MainPage)d).OnModeloAtencionChanged(e);
        }

        protected virtual void OnModeloAtencionChanged(DependencyPropertyChangedEventArgs e)
        {
            Atencion = (AtencionHistoriaClinicaDto)e.NewValue;
            Variables_Globales.IdAtencion = Atencion.Identificador;
            Variables_Globales.IdPrestador = Atencion.Prestador.Identificador;
            Variables_Globales.IdConvenio = Atencion.IdConvenio;
            Variables_Globales.ClasificadorOdontologico = Atencion.Prestador.ClasificadorOdontologico;

            //Ya se esta cargando por el token
            //Variables_Globales.Paciente = Atencion.Paciente.Identificador;

        }

        #endregion
        


       


        public void ConfigurarBindings()
        {

            this.SetBinding(ModeloAtencionProperty, new Binding("ModeloAtencion")
            {
                Mode = BindingMode.OneWay
            });

            this.SetBinding(PrestadorSeleccionadoProperty, new Binding("ModeloAtencion.Prestador.Identificador")
            {
                Mode = BindingMode.OneWay
            });
            this.SetBinding(IdConvenioProperty, new Binding("ModeloAtencion.IdConvenio")
            {
                Mode = BindingMode.OneWay
            });
            this.SetBinding(ClasificadorOdontologicoProperty, new Binding("ModeloAtencion.Prestador.ClasificadorOdontologico")
            {
                Mode = BindingMode.TwoWay
            });
            this.SetBinding(IdPacienteProperty, new Binding("ModeloAtencion.Paciente.Identificador")
            {
                Mode = BindingMode.TwoWay
            });

            this.SetBinding(IdUsuarioProperty, new Binding("Valor")
            {
                Mode = BindingMode.TwoWay
            });

            this.SetBinding(IdTratamientoProperty, new Binding("Valor")
            {
                Mode = BindingMode.TwoWay
            });

            this.SetBinding(OrdenesSiguienteSesionProperty, new Binding("ListaPlanTratamiento")
            {
                Mode = BindingMode.TwoWay
            });

            this.SetBinding(IdAtencionProperty, new Binding("ModeloAtencion.Identificador")
            {
                Mode = BindingMode.OneWay
            });

            this.SetBinding(CancelarProperty, new Binding("CancelarOdontologia")
            {
                Mode = BindingMode.TwoWay
            });


        }
        #endregion
    }
}
