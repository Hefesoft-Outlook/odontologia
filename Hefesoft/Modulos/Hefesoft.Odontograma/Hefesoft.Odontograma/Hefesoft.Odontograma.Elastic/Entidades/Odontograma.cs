using Hefesoft.Standard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Odontograma.Elastic.Entidades
{
    public class Odontograma : IEntidadBase
    {
        /*
            Propiedades minimas de una entidad para este framework
         */
        /**************************************/
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public bool generarIdentificador { get; set; }

        public string nombreTabla { get; set; }
        /**************************************/

        public int Codigo { get; set; }
        public string Descripcion { get; set; }
    }
}
