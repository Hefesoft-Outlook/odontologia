using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Hefesoft.Usuario.Interfaces;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Usuario
{
    public class Locator
    {
        public static bool isRegistered;

        static Locator()
        {
            if (!isRegistered)
            {
                isRegistered = true;
                ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

                if (ViewModelBase.IsInDesignModeStatic)
                {
                    //SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
                }
                else
                {
                    //SimpleIoc.Default.Register<IDataService, DataService>();
                }

                SimpleIoc.Default.Register<ViewModel.Usuarios>();
                if (!SimpleIoc.Default.IsRegistered<IUsuarios>())
                {
                    //Aca seleccionamos a que sevicio queremos conectarnos
                    SimpleIoc.Default.Register<IUsuarios>(() => new Data.Usuarios(),true);
                }
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
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

        public ViewModel.Usuarios vmUsuarios
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ViewModel.Usuarios>();
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
