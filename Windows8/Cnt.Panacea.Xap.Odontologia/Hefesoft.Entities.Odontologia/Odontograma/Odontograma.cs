using Dto;
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

        public List<OdontogramaEntity> odontograma { get; set; }

        public List<TratamientoImagenEntity> adjuntosImagen { get; set; }

        public Odontograma()
        {
            odontograma = new List<OdontogramaEntity>();
            adjuntosImagen = new List<TratamientoImagenEntity>();
        }
    
    }
}
