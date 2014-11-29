﻿using Hefesoft.Standard.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Hefesoft.Odontologia.Periodontograma.Data
{
    public class Periodontograma : IEntidadBase
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }
        public bool generarIdentificador { get; set; }
        public string id { get; set; }

        private List<Entidades.PeriodontogramaEntity> listado = new List<Entidades.PeriodontogramaEntity>();
        public List<Entidades.PeriodontogramaEntity> Listado
        {
            get { return listado; }
            set { listado = value; }
        }
    }
}
