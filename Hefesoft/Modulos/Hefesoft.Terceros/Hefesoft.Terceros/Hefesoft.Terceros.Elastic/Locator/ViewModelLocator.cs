
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Hefesoft.Terceros.Elastic.Locator
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
                SimpleIoc.Default.Register<Hefesoft.Terceros.Elastic.ViewModel.Terceros>();
                SimpleIoc.Default.Register<Hefesoft.Terceros.Elastic.ViewModel.Odontologo>();
                SimpleIoc.Default.Register<Hefesoft.Terceros.Elastic.ViewModel.Higienista>();
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

        public Hefesoft.Terceros.Elastic.ViewModel.Terceros Terceros
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Hefesoft.Terceros.Elastic.ViewModel.Terceros>();
            }
        }

        public Hefesoft.Terceros.Elastic.ViewModel.Odontologo Odontologo
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Hefesoft.Terceros.Elastic.ViewModel.Odontologo>();
            }
        }

        public Hefesoft.Terceros.Elastic.ViewModel.Higienista Higienista
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Hefesoft.Terceros.Elastic.ViewModel.Higienista>();
            }
        }

        



        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}