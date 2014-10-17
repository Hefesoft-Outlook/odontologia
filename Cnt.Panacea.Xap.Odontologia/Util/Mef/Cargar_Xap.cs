using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Cnt.Panacea.Xap.Odontologia.Util.Mef
{
    public class Cargar_Xap: IPartImportsSatisfiedNotification
    {
        public Cargar_Xap()
        {
            AsyncDeploymentCatalogServiceOdontologia.Initialize();
            //Llama al deployment catalog
                                   
        }

        internal async void cargarXap(string ruta)
        {
            CompositionInitializer.SatisfyImports(this); 
            await CatalogService.AddXap(ruta, result);           
        }

        private void result(System.ComponentModel.AsyncCompletedEventArgs obj)
        {
            
        }


        [Import]
        public IAsyncDeploymentCatalogService CatalogService { get; set; }

        [ImportMany(AllowRecomposition = true, RequiredCreationPolicy = CreationPolicy.NonShared)]
        public Lazy<UserControl>[] MEFModuleList { get; set; }

        public void OnImportsSatisfied()
        {
            
        }
    }
}
