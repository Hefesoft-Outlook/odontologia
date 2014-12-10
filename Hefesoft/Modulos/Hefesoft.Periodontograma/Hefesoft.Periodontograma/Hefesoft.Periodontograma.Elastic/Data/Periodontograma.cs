using Hefesoft.Standard.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Hefesoft.Periodontograma.Elastic.Data
{
    public class Periodontograma : IEntidadBase
    {
        public Periodontograma()
        {
            Fecha_Ultima_Modificacion = DateTime.Now;
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }
        public bool generarIdentificador { get; set; }
        public string id { get; set; }
        public string idPaciente { get; set; }

        private Hefesoft.Usuario.Entidades.Usuario paciente = new Usuario.Entidades.Usuario();

        public Hefesoft.Usuario.Entidades.Usuario  Paciente
        {
            get { return paciente; }
            set 
            { 
                paciente = value;
                PacienteIdentificador = value.id;
                PacienteNombre = value.nombre;
                PacienteImagen = value.imagenRuta;
            }
        }
        

        public DateTime Fecha_Ultima_Modificacion { get; set; }

        private List<Entidades.PeriodontogramaEntity> listado = new List<Entidades.PeriodontogramaEntity>();
        public List<Entidades.PeriodontogramaEntity> Listado
        {
            get { return listado; }
            set { listado = value; }
        }

        public string PacienteIdentificador { get; set; }

        public string PacienteNombre { get; set; }

        public string PacienteImagen { get; set; }
    }
}
