using System;
using System.Collections.Generic;
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
using System.ComponentModel;
using Contracts;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.IO;
using System.Windows.Resources;
using System.Xml.Linq;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.ComponentModel.Composition.Primitives;

namespace The_Windmill_Project
{
    [Export(typeof(IDeploymentCatalogService))]
    public class DeploymentCatalogService : IDeploymentCatalogService
    {
        #region Variables
        public static AggregateCatalog _aggregateCatalog;
        public Dictionary<string, DeploymentCatalog> _catalogs;
        DeploymentCatalog catalog;
        string Url = "";
        string absoluteUrl = "";
        Uri uriDownloadXap;
        public string VersionXap;
        

        private readonly List<Assembly> _assemblies = new List<Assembly>();

        private static readonly List<string> _parts = new List<string>();

        #endregion

        #region Constructor
        public DeploymentCatalogService()
        {   
            _catalogs = new Dictionary<string, DeploymentCatalog>();
        }
        #endregion

        #region Methods
        public static void Initialize()
        {
            _aggregateCatalog = new AggregateCatalog();
            _aggregateCatalog.Catalogs.Add(new DeploymentCatalog());
            CompositionHost.Initialize(_aggregateCatalog);
        }

        public void AddXap(string uri, Action<AsyncCompletedEventArgs> completedAction = null)
        {
            Url = uri;
            absoluteUrl = uri;          

            if (!_catalogs.TryGetValue(uri, out catalog))
            {
                DownloadXap(uri, completedAction);
            }
            else
            {
                onFinalizadoXap(null, null);
            }
        }

        private void DownloadXap(string uri, Action<AsyncCompletedEventArgs> completedAction)
        {            
            catalog = WriteCatalog(uri, catalog);
            catalog.DownloadCompleted -= catalog_DownloadCompleted;

            catalog.DownloadProgressChanged += new EventHandler<DownloadProgressChangedEventArgs>(catalog_DownloadProgressChanged);

            if (completedAction != null)
            {
                catalog.DownloadCompleted += (s, e) => completedAction(e);
            }
            else
            {
                catalog.DownloadCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(catalog_DownloadCompleted);
            }

            catalog.DownloadAsync();
            _catalogs[uri] = catalog;

            if (_aggregateCatalog == null)
            {
                _aggregateCatalog = new AggregateCatalog();
            }

            _aggregateCatalog.Catalogs.Add(catalog);
        }

        private bool ValidateExistFile()
        {
            string UrlStorage = "";

            if (Url.Contains("http"))
            {
                UrlStorage = Url.Split('/').Last();
            }
            else
            {
                UrlStorage = Url;
            }


            using (var iso = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (iso.FileExists(UrlStorage))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private static DeploymentCatalog WriteCatalog(string uri, DeploymentCatalog catalog)
        {
            if (uri.Contains("http"))
            {
                Uri direccion = new Uri(uri, UriKind.Absolute);
                catalog = new DeploymentCatalog(direccion);
            }
            else
            {
                catalog = new DeploymentCatalog(uri);
            }
            return catalog;
        }

        void catalog_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            onCargarXap(sender, e);
        }

        public void RemoveXap(string uri)
        {
            DeploymentCatalog catalog;
            if (_catalogs.TryGetValue(uri, out catalog))
            {
                _aggregateCatalog.Catalogs.Remove(catalog);
            }
        }

        public void limpiarMef()
        {
            var algo = _aggregateCatalog.Catalogs;

            

            foreach (var item in algo)
            {
                try
                {
                    if (item.Parts.Count() > 0)
                    {
                        if (item.ToString() != "TypeCatalog (Types='WindMillWelcome.MainPage').")
                        {
                            _aggregateCatalog.Catalogs.Remove(item);
                        }
                    }
                }
                catch
                {
                }
            }

            _ReadFromIso();

            _aggregateCatalog.Catalogs.Count();
        }

        private void onFinalizadoXap(object sender, EventArgs e)
        {
            if (FinalizadoXap != null)
            {
                FinalizadoXap(sender, e);
                FinalizadoXap = null;
                CargandoXap = null;                
                DownloadAsync();

            }
        }

        private void onCargarXap(object sender, EventArgs e)
        {
            if (CargandoXap != null)
            {
                CargandoXap(sender, e);
            }
        }

        public void DownloadAsync()
        {
            if (Url.Contains("http"))
            {
                uriDownloadXap = new Uri(Url, UriKind.Absolute);
            }
            else
            {
                uriDownloadXap = new Uri(Url, UriKind.Relative);
            }

            Debug.WriteLine("Begin async download of XAP {0}", Url);
            var webClient = new WebClient();
            webClient.OpenReadCompleted += new OpenReadCompletedEventHandler(WebClientOpenReadCompleted);
            webClient.OpenReadAsync(uriDownloadXap);

        }

        private void WebClientOpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {

            Debug.WriteLine("Download of xap {0} completed.", uriDownloadXap);

            if (e.Error != null)
            {
                // will try to read from ISO as a fallback 
                Debug.WriteLine("Catalog load failed: {0}", e.Error.Message);
            }
            else
            {
                if (Url.Contains("http"))
                {
                    Url = Url.Split('/').Last();
                }

                if (Url.Contains("?overwride"))
                {
                    Url = Url.Replace("?overwride", "");
                }

                var isoName = Url;

                Debug.WriteLine("Attempting to store XAP {0} to local file {1}", Url, isoName);

                using (var iso = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (var br = new BinaryReader(e.Result))
                    {
                        using (var bw = new BinaryWriter(iso.OpenFile(isoName, FileMode.Create, FileAccess.ReadWrite)))
                        {
                            bw.Write(br.ReadBytes((int)e.Result.Length));
                        }
                        br.Close();
                    }
                }
            }
        }


        
        

        public bool _ReadFromIso()
        {
            Debug.WriteLine("Attempting to retrieve XAP {0} from isolated storage.", uriDownloadXap);

            string UrlStorage = "";

            if (Url.Contains("http"))
            {
                UrlStorage = Url.Split('/').Last();
            }
            else
            {
                UrlStorage = Url;
            }

            using (var iso = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (iso.FileExists(UrlStorage))
                {
                    _ProcessXap(iso.OpenFile(UrlStorage, FileMode.Open, FileAccess.Read));
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void _ProcessXap(Stream stream)
        {
            var manifestStr = new
                StreamReader(
                Application.GetResourceStream(new StreamResourceInfo(stream, null),
                                                new Uri("AppManifest.xaml", UriKind.Relative))
                    .Stream).ReadToEnd();

            var deploymentRoot = XDocument.Parse(manifestStr).Root;

            if (deploymentRoot == null)
            {
                Debug.WriteLine("Unable to find manifest for XAP {0}", uriDownloadXap);
                //if (DownloadCompleted != null)
                //{
                //    DownloadCompleted(this,
                //                        new AsyncCompletedEventArgs(new Exception("Could not find manifest root in XAP"),
                //                                                    false, null));
                //}
                return;
            }

            var parts = (from p in deploymentRoot.Elements().Elements() select p).ToList();

            foreach (var src in
                from part in parts
                select part.Attribute("Source")
                    into srcAttr
                    where srcAttr != null
                    select srcAttr.Value)
            {
                _ProcessPart(src, stream);
            }

            foreach (var assembly in _assemblies)
            {
                try
                {
                    catalog = WriteCatalog(absoluteUrl, catalog);
                    _catalogs[absoluteUrl] = catalog;
                    _aggregateCatalog.Catalogs.Add(new TypeCatalog(assembly.GetTypes()));
                }
                catch (ReflectionTypeLoadException ex)
                {
                    Debug.WriteLine("Exception encountered loading types: {0}", ex.Message);

                    if (Debugger.IsAttached)
                    {
                        foreach (var item in ex.LoaderExceptions)
                        {
                            Debug.WriteLine("With exception: {0}", item.Message);
                        }
                    }

                    throw;
                }
            }
            Debug.WriteLine("Xap file {0} successfully loaded and processed.", uriDownloadXap);
            stream.Close();
            onFinalizadoXap(null, null);
        }

        private void _ProcessPart(string src, Stream stream)
        {
            Debug.WriteLine("Offline catalog is parsing assembly part {0}", src);

            var assemblyPart = new AssemblyPart();

            var srcInfo = Application.GetResourceStream(new StreamResourceInfo(stream, "application/binary"),
                                                        new Uri(src, UriKind.Relative));

            lock (((ICollection)_parts).SyncRoot)
            {
                if (_parts.Contains(src))
                {
                    return;
                }

                _parts.Add(src);

                if (src.EndsWith(".dll"))
                {
                    var assembly = assemblyPart.Load(srcInfo.Stream);
                    _assemblies.Add(assembly);
                }
                else
                {
                    assemblyPart.Load(srcInfo.Stream);
                }
            }
        }

        #endregion

        #region Events
        public void catalog_DownloadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                throw new Exception(e.Error.Message, e.Error);
            }
            else
            {
                onFinalizadoXap(sender, e);
            }
        }
        #endregion
        public event EventHandler FinalizadoXap;
        public event EventHandler CargandoXap;


        public void updateModule(bool version)
        {
           //Funcionalidad que se usa solo en hefesoft
        }
    }

}
