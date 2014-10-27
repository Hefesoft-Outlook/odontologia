using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Especialidades
{
    public class EspecialidadEntity
    {
        
        public string Acronimo { get; set; }
        
        public string Codigo { get; set; }
        //public IEntidadesCollection ColeccionCiclicaHijos { get; }
        
        //public EspecialidadesCollection EspecialidadesAfines { get; set; }
        
        public bool Estado { get; set; }
        
        public bool GeneraConsultas { get; set; }
        
        public bool GenerarOrdenIngreso { get; set; }
        
        //public EspecialidadHistoricosCollection Historial { get; set; }
        
        public short Identificador { get; set; }
        
        public Guid IdentificadorBitacora { get; set; }
        
        public short? IdPadre { get; set; }
        
        public short? Ips { get; set; }
        
        public string Nombre { get; set; }
        public double Opacidad { get; set; }
        
        //public OrdenEspecialidadArticulosCollection OrdenArticulos { get; set; }
        
        //public OrdenEspecialidadProcedimientosCollection OrdenProcedimientos { get; set; }
        
        public bool PrestaUrgencias { get; set; }
        
        //public ProcedimientosServiciosCollection Procedimientos { get; set; }
        
        public bool RequiereDisponibilidad { get; set; }
        
        //public EspecialidadesCollection SubEspecialidades { get; set; }
        public string TipoEntidadBitacora { get; set; }
        
        public decimal Valor { get; set; }

    }
}
