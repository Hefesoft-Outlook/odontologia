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
        static Locator()
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
            

            if (!SimpleIoc.Default.IsRegistered<IUsuarios>())
            {
                //Aca seleccionamos a que sevicio queremos conectarnos
                SimpleIoc.Default.Register<IUsuarios>(() => new Usuario.Data.Usuarios());                
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

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {

        }
    }
}
