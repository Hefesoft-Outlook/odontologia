using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Cnt.Std;
using Cnt.Panacea.Entities.Parametrizacion;

namespace Cnt.Panacea.Xap.Odontologia.Clases
{
    /// <summary>
    /// Parametros que se usan para el control de insumos
    /// </summary>
    public class ParametrosInsumos
    {

        
        AplicacionesCanastaDtoCollection aplicacionesCanasta;
        public event EventHandler GetPlanesValores;

        #region Propiedades
        public int IdPaciente { get; set; }

        public int IdPx { get; set; }

        public int IdBodega { get; set; }

        public EstadosEntidad EstadoControl { get; set; }

        public int IdIps { get; set; }

        public long IdPlanTratamiento { get; set; }

        public AplicacionesCanastaDtoCollection AplicacionesCanasta
        {
            get
            {

                return aplicacionesCanasta;
            }
            set
            {
                aplicacionesCanasta = value;
                if (aplicacionesCanasta !=null && aplicacionesCanasta.Count >0)
                OnGetPlanesValores();
            } 
             }


        private void OnGetPlanesValores()
        {
            if (GetPlanesValores != null)
            {
                GetPlanesValores(this, new EventArgs());
            }
        }

        #endregion
    }
}
