using GalaSoft.MvvmLight;
using Hefesoft.Standard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Usuario.Entidades.Pacientes
{
    public class Paciente : ViewModelBase, IEntidadBase, IUsuario
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }        
        public bool generarIdentificador { get; set; }
        public string id { get; set; }        

        private string _nombre;
        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; RaisePropertyChanged("nombre"); }
        }
        

        public string correo { get; set; }
        public string telefono { get; set; }
        public string telefono2 { get; set; }
        
        private string _imagenRuta;
        public string imagenRuta
        {
            get { return _imagenRuta; }
            set { _imagenRuta = value; RaisePropertyChanged("imagenRuta"); }
        }
        

    }
}
