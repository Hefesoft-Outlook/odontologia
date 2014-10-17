using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
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
    public interface IDeploymentCatalogServiceOdontologia
    {    
        void AddXap(string uri, Action<AsyncCompletedEventArgs> completedAction = null);
        void RemoveXap(string uri);
    }

    
    [Export(typeof(IDeploymentCatalogServiceOdontologia))]
    public class DeploymentCatalogServiceOdontologia : IDeploymentCatalogServiceOdontologia
    {
        private static AggregateCatalog _aggregateCatalog;
        Dictionary<string, DeploymentCatalog> _catalogs;

        public DeploymentCatalogServiceOdontologia()
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

        public void AddXap(string uri, Action<AsyncCompletedEventArgs> completedAction = null)
        {   
            DeploymentCatalog catalog;
            if (!_catalogs.TryGetValue(uri, out catalog))
            {
                catalog = new DeploymentCatalog(uri);

                if (completedAction != null)
                {
                    catalog.DownloadCompleted += (s, e) => completedAction(e);
                    catalog.DownloadProgressChanged += catalog_DownloadProgressChanged;
                }
                else
                {
                    catalog.DownloadCompleted += catalog_DownloadCompleted;
                    catalog.DownloadProgressChanged += catalog_DownloadProgressChanged;
                }

                catalog.DownloadAsync();
                _catalogs[uri] = catalog;
                _aggregateCatalog.Catalogs.Add(catalog);
            }
        }

        void catalog_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(e.ProgressPercentage.ToString(), "Mef");
        }

        void catalog_DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send("Cargado", "Mef");     
            if (e.Error != null)
            {
                throw e.Error;
            }
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
