using Hefesoft.Standard.Interfaces;
using Hefesoft.Entities.Odontologia.Tratamiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Odontograma
{
    public class Odontograma : IEntidadBase
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }

        public short idIps { get; set; }

        public TratamientoEntity tratamiento  { get; set; }

        public OdontogramasPacienteEntity odontogramaPaciente { get; set; }

        public List<OdontogramaEntity> odontogramaInicial { get; set; }

        public List<OdontogramaEntity> odontogramaPlanTratamiento { get; set; }

        public List<OdontogramaEntity> odontogramaEvolucion { get; set; }

        public List<TratamientoImagenEntity> adjuntosImagen { get; set; }      

        public bool generarIdentificador { get; set; }

        public Odontograma()
        {
            odontogramaInicial = new List<OdontogramaEntity>();
            odontogramaPlanTratamiento = new List<OdontogramaEntity>();
            odontogramaEvolucion = new List<OdontogramaEntity>();
            adjuntosImagen = new List<TratamientoImagenEntity>();
        }


        public string urlPreviewOdontograma { get; set; }
    }
}
