using Hefesoft.Standard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Usuario.Entidades
{
    public class Outlook : IEntidadBase, IUsuario
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }
        public bool generarIdentificador { get; set; }
        public string id { get; set; }
        public string nombre { get; set; }
        public string imagenRuta { get; set; }
    }
}
