
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Hefesoft.Usuario.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace Hefesoft.Autentication.Elastic.Locator
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public static bool isRegistered;
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            if (!isRegistered)
            {
                isRegistered = true;
                SimpleIoc.Default.Register<MainViewModel>();
                SimpleIoc.Default.Register<Hefesoft.Standard.BusyBox.Busy>();
                SimpleIoc.Default.Register<Hefesoft.Autentication.Elastic.ViewModel.Autentication>();
                SimpleIoc.Default.Register<Hefesoft.Usuario.ViewModel.Usuarios>();

                if (!SimpleIoc.Default.IsRegistered<IUsuarios>())
                {
                    //Aca seleccionamos a que sevicio queremos conectarnos
                    SimpleIoc.Default.Register<IUsuarios>(() => new Usuario.Data.Usuarios());
                }            

            }
        }

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
        public Hefesoft.Standard.BusyBox.Busy Busy
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Hefesoft.Standard.BusyBox.Busy>();
            }
        }

        public Hefesoft.Autentication.Elastic.ViewModel.Autentication Autentication
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Hefesoft.Autentication.Elastic.ViewModel.Autentication>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public IUsuarios Usuarios
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IUsuarios>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public Hefesoft.Usuario.ViewModel.Usuarios vmUsuarios
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Hefesoft.Usuario.ViewModel.Usuarios>();
            }
        }

        

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}