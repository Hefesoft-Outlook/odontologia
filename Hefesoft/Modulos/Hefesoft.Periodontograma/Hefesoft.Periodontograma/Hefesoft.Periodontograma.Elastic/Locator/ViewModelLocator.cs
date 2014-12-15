
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Hefesoft.Periodontograma.Elastic.Locator
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

                if (!SimpleIoc.Default.IsRegistered<Hefesoft.Standard.BusyBox.Busy>())
                {
                    SimpleIoc.Default.Register<Hefesoft.Standard.BusyBox.Busy>();
                }

                SimpleIoc.Default.Register<Hefesoft.Periodontograma.Elastic.ViewModel.Periodontograma>();
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

        public Hefesoft.Periodontograma.Elastic.ViewModel.Periodontograma Periodontograma
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Hefesoft.Periodontograma.Elastic.ViewModel.Periodontograma>();
            }
        }



        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}