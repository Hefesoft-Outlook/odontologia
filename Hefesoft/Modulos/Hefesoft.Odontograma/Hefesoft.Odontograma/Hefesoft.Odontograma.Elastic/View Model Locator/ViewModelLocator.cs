using Cnt.Panacea.Xap.Odontologia.Recursos.ViewModelLocator;
using Cnt.Panacea.Xap.Odontologia.Vm.Contexto;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace Cnt.Panacea.Xap.Odontologia.Vm.View_Model_Locator
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                //SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
            }
            else
            {
                //SimpleIoc.Default.Register<IDataService, DataService>();
            }


            SimpleIoc.Default.Register<MainViewModel>();

            if (!SimpleIoc.Default.IsRegistered<IContexto_Odontologia>())
            {
                //Aca seleccionamos a que sevicio queremos conectarnos
                SimpleIoc.Default.Register<IContexto_Odontologia>(() => new Cnt.Panacea.Xap.Odontologia.Vm.Contexto.Sample_data.Contexto_Odontologia());
                
                //Conexion a wcf
                //SimpleIoc.Default.Register<IContexto_Odontologia>(() => new Cnt.Panacea.Xap.Odontologia.Vm.Contexto.Wcf.Contexto_Odontologia());

                //Implementacion hefesoft
                SimpleIoc.Default.Register<Hefesoft.Usuario.Interfaces.IUsuarios>(() => new Hefesoft.Usuario.Data.Usuarios());
            }

            //View model donde se muestra el indice de placa bacteriana y demas
            SimpleIoc.Default.Register<Cnt.Panacea.Xap.Odontologia.Assets.Pieza_Dental.Pieza_Seleccionada.vm.vm>();
            SimpleIoc.Default.Register<Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm>();
            SimpleIoc.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Paleta.Paleta>();
            SimpleIoc.Default.Register<Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm.Odontograma_Inicial>();
            SimpleIoc.Default.Register<Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm.Plan_Tratamiento>();
            SimpleIoc.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Evolucion.Grid_Evolucion>();
            SimpleIoc.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Inicial.vm>();
            SimpleIoc.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Plan_tratamiento.GridPlanTratamientoProcedimientosWizard>();
            SimpleIoc.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Mapa_Dental.UserControlGuardarPlanTratamiento>();
            SimpleIoc.Default.Register<Cnt.Panacea.Xap.ViewModels.PacienteTratamientosViewModel>();
            SimpleIoc.Default.Register<Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm.Evolucion>();
            SimpleIoc.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Contenedor.vm>();
            SimpleIoc.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Adjuntar.vm.vm>();
            SimpleIoc.Default.Register<Cnt.Panacea.Xap.Odontologia.Assets.PopUp.vm.Bodega>();
            SimpleIoc.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Plan_tratamiento.Listado_Procedimientos>();
            SimpleIoc.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Pieza_Seleccionada.Seleccionado>();
            SimpleIoc.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Boca.Boca>();

            //Hefesoft Implementacion 
            SimpleIoc.Default.Register<Hefesoft.Usuario.ViewModel.Usuarios>();
            SimpleIoc.Default.Register<Hefesoft.Usuario.ViewModel.Pacientes.Pacientes>();         
            SimpleIoc.Default.Register<Hefesoft.Standard.BusyBox.Busy>();
            
            
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public Cnt.Panacea.Xap.Odontologia.Assets.Pieza_Dental.Pieza_Seleccionada.vm.vm Pieza_Dental_Seleccionada
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Assets.Pieza_Dental.Pieza_Seleccionada.vm.vm>();
            }
        }

        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm Mapa_dental
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public Cnt.Panacea.Xap.Odontologia.Vm.Paleta.Paleta Paleta
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Paleta.Paleta>();
            }
        }

         [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm.Odontograma_Inicial Odontograma_Inicial
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm.Odontograma_Inicial>();
            }
        }

         [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm.Plan_Tratamiento Plan_Tratamiento
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm.Plan_Tratamiento>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
         public Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Evolucion.Grid_Evolucion Grid_Evolucion
         {
             get
             {
                 return ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Evolucion.Grid_Evolucion>();
             }
         }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Inicial.vm Grilla_Odontograma_Inicial
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Inicial.vm>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Plan_tratamiento.GridPlanTratamientoProcedimientosWizard Wizard_Plan_Tratamiento
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Plan_tratamiento.GridPlanTratamientoProcedimientosWizard>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public Cnt.Panacea.Xap.Odontologia.Vm.Mapa_Dental.UserControlGuardarPlanTratamiento Grilla_Plan_Tratamiento
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Mapa_Dental.UserControlGuardarPlanTratamiento>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
           "CA1822:MarkMembersAsStatic",
           Justification = "This non-static member is needed for data binding purposes.")]
        public Cnt.Panacea.Xap.ViewModels.PacienteTratamientosViewModel Procedimentos_Paciente
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.ViewModels.PacienteTratamientosViewModel>();
            }
        }

          [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
         public Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm.Evolucion Evolucion
         {
             get
             {
                 return ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm.Evolucion>();
             }
         }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
          public Cnt.Panacea.Xap.Odontologia.Vm.Contenedor.vm Contenedor
         {
             get
             {
                 return ServiceLocator.Current.GetInstance <Cnt.Panacea.Xap.Odontologia.Vm.Contenedor.vm>();
             }
         }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
            public Cnt.Panacea.Xap.Odontologia.Vm.Adjuntar.vm.vm Listar_Imagenes
         {
             get
             {
                 return ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Adjuntar.vm.vm>();
             }
         }        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
            public Cnt.Panacea.Xap.Odontologia.Assets.PopUp.vm.Bodega Bodega
         {
             get
             {
                 return ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Assets.PopUp.vm.Bodega>();
             }
         }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
            public Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Plan_tratamiento.Listado_Procedimientos Listado_Procedimientos
         {
             get
             {
                 return ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Plan_tratamiento.Listado_Procedimientos>();
             }
         }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public Cnt.Panacea.Xap.Odontologia.Vm.Pieza_Seleccionada.Seleccionado Seleccionado
         {
             get
             {
                 return ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Pieza_Seleccionada.Seleccionado>();
             }
         }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public Cnt.Panacea.Xap.Odontologia.Vm.Boca.Boca Boca
         {
             get
             {
                 return ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Boca.Boca>();
             }
         }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
    "CA1822:MarkMembersAsStatic",
    Justification = "This non-static member is needed for data binding purposes.")]
        public Hefesoft.Usuario.ViewModel.Usuarios Usuarios
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Hefesoft.Usuario.ViewModel.Usuarios>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
    "CA1822:MarkMembersAsStatic",
    Justification = "This non-static member is needed for data binding purposes.")]
        public Hefesoft.Usuario.ViewModel.Pacientes.Pacientes Pacientes
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Hefesoft.Usuario.ViewModel.Pacientes.Pacientes>();
            }
        }


        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
    "CA1822:MarkMembersAsStatic",
    Justification = "This non-static member is needed for data binding purposes.")]
        public Hefesoft.Standard.BusyBox.Busy Busy
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Hefesoft.Standard.BusyBox.Busy>();
            }
        }

                
         
        

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {

        }
    }
}
