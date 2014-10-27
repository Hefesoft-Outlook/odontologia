using Cnt.Panacea.Entities.Parametrizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm
{
    public partial class Plan_Tratamiento 
    {
        public void createSampleData()
        {
            if (TiposTratamiento == null)
            {
                TiposTratamiento = new System.Collections.ObjectModel.ObservableCollection<Entities.Odontologia.TipoTratamientoEntity>();
            }

            TiposTratamiento.Add(new Entities.Odontologia.TipoTratamientoEntity() 
            {
                Identificador = 1,
                Estado = true,
                EstadoRegistro = Std.EstadosEntidad.Modificado,
                Nombre = "Nuevo",
                Ips = 4
            });


            TiposTratamiento.Add(new Entities.Odontologia.TipoTratamientoEntity()
            {
                Identificador = 2,
                Estado = true,
                EstadoRegistro = Std.EstadosEntidad.Modificado,
                Nombre = "Garantia",
                Ips = 4
            });

            TiposTratamiento.Add(new Entities.Odontologia.TipoTratamientoEntity()
            {
                Identificador = 3,
                Estado = true,
                EstadoRegistro = Std.EstadosEntidad.Modificado,
                Nombre = "Retratamiento",
                Ips = 4
            });

            //Descomentarear esta linea para llenar con datos de prueba
            //TiposTratamiento.ToObservableCollection().fillTables(new Hefesoft.Entities.Odontologia.Tratamiento.TipoTratamientoEntity());
        }

        public void datosPruebaEspecialidades()
        {
            List<ProcedimientoEspecialidadSedeEntity> lstEspecialidadesData = new List<ProcedimientoEspecialidadSedeEntity>();

            lstEspecialidadesData.Add(new ProcedimientoEspecialidadSedeEntity()
            {
                Identificador = 1,
                CodigoProcedimiento = "1",
                Especialidad = new EspecialidadEntity()
                {
                    Codigo = "1",
                    Nombre = "Especialidad 1",
                    Identificador = 1
                },
                NombreProcedimiento = "Procedimiento 1",
                TipoProcedimiento = new TiposProcedimiento()
                {
                },
                Sede = new SedeEntity()
                {
                    Identificador = 1,
                    Descripcion = "Nombre sede"
                }
            });

            lstEspecialidadesData.Add(new ProcedimientoEspecialidadSedeEntity()
            {
                Identificador = 2,
                CodigoProcedimiento = "2",
                Especialidad = new EspecialidadEntity()
                {
                    Codigo = "2",
                    Nombre = "Especialidad 2",
                    Identificador = 2
                },
                NombreProcedimiento = "Procedimiento 2",
                TipoProcedimiento = new TiposProcedimiento()
                {
                },
                Sede = new SedeEntity()
                {
                    Identificador = 2,
                    Descripcion = "Nombre sede"
                }
            });

            lstEspecialidadesData.ToObservableCollection()
                .fillTables(new Hefesoft.Entities.Odontologia.Especialidades.ProcedimientoEspecialidadSedeEntity());
        }
    }
}
