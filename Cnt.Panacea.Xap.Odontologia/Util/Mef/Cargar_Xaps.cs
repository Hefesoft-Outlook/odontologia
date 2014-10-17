using Cnt.Panacea.Xap.Dinamico.Controles;
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
using System.Linq;

namespace Cnt.Panacea.Xap.Odontologia.Util.Mef
{
    public class Cargar_Xaps :  IPartImportsSatisfiedNotification
    {
        #region Variables

        private string urlKey = "";
        public event EventHandler Cargado;

        private void onCargado(object sender, EventArgs e)
        {
            if (Cargado != null)
            {
                Cargado(sender, e);
            }
        }

        public Cargar_Xaps()
	    {
            CompositionInitializer.SatisfyImports(this);               
	    }


        public void cargarXap(string uri)
        {
            urlKey = uri;
            CatalogService.AddXap(uri);
            loadControl();
        }

        #endregion


        #region Propiedades

       
        [Import]
        public IDeploymentCatalogService CatalogService { get; set; }

        [ImportMany(AllowRecomposition = true)]
        public Lazy<UserControl>[] MEFModuleList { get; set; }

        #endregion

        #region Metodos
       
        public void OnImportsSatisfied()
        {
            loadControl();
        }

        private void loadControl()
        {
            if (MEFModuleList != null && MEFModuleList.Length > 0)
            {
                var elemento = MEFModuleList.FirstOrDefault(a => a.Value.Tag.ToString() == urlKey);

                if (elemento != null)
                {
                    var control = elemento.Value as UserControl;
                    onCargado(control, EventArgs.Empty);
                }
            }
        }

        #endregion
    }
}
