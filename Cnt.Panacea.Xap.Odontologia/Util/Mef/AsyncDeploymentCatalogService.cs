using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Net;
using System.Threading.Tasks;
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
    public interface IAsyncDeploymentCatalogService
    {
         Task<AggregateCatalog> AddXap(string uri, Action<AsyncCompletedEventArgs> completedAction = null);
         void RemoveXap(string uri);
    }

    
    [Export(typeof(IAsyncDeploymentCatalogService))]
    public class AsyncDeploymentCatalogServiceOdontologia : IAsyncDeploymentCatalogService
    {
        private static AggregateCatalog _aggregateCatalog;
        Dictionary<string, DeploymentCatalog> _catalogs;

        public AsyncDeploymentCatalogServiceOdontologia()
        {
            _catalogs = new Dictionary<string, DeploymentCatalog>();
        }

        public static void Initialize()
        {
            _aggregateCatalog = new AggregateCatalog();
            _aggregateCatalog.Catalogs.Add(new DeploymentCatalog());
            
            //Sobra xq ya se inicializo en el dinamico
            //CompositionHost.Initialize(_aggregateCatalog);
        }

        public async Task<AggregateCatalog> AddXap(string uri, Action<AsyncCompletedEventArgs> completedAction = null)
        {
            var tcs = new TaskCompletionSource<AggregateCatalog>();

            DeploymentCatalog catalog;
            if (!_catalogs.TryGetValue(uri, out catalog))
            {
                catalog = new DeploymentCatalog(uri);
               

                if (completedAction != null)
                {                    
                    catalog.DownloadProgressChanged += catalog_DownloadProgressChanged;
                }
                else
                {                 
                    catalog.DownloadProgressChanged += catalog_DownloadProgressChanged;
                }

                await catalog.DownloadAsyncasTask();
                _catalogs[uri] = catalog;
                _aggregateCatalog.Catalogs.Add(catalog);
            }

            return _aggregateCatalog;
        }

        void catalog_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(e.ProgressPercentage.ToString(), "Mef");
        }        

        public void RemoveXap(string uri)
        {            
            DeploymentCatalog catalog;
            if (_catalogs.TryGetValue(uri, out catalog))
            {
                _aggregateCatalog.Catalogs.Remove(catalog);
                _catalogs.Remove(uri);
            }
        }
    }
}
