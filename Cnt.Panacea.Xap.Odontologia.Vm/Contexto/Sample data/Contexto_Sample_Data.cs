using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Entities.Parametrizacion;
using Dto.Extension;
using Hefesoft.Entities.Odontologia.Diagnostico;
using Proxy;
using NivelSeveridadDXEntity = Cnt.Panacea.Entities.Parametrizacion.NivelSeveridadDXEntity;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using System.Linq;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Contexto.Sample_data
{
    public class Contexto_Odontologia : IContexto_Odontologia
    {
        public dynamic binding;
        private OdontologiaServicioClient cliente;
        public bool guardado_En_Proceso = false;
        public string url;

        public void inicializarContexto()
        {
        }

        public dynamic servicio(dynamic cliente)
        {
            return null;
        }

        public async Task<bool> ActualizarPlanesTratamiento(TratamientoEntity Tratamiento, PlanesTratamientoCollection Planes)
        {   
            foreach (PlanTratamientoEntity pivot in Planes)
            {                
                if (pivot.Articulos != null)
                {
                    for (int i = 0; i <= pivot.Articulos.Count - 1; i++)
                    {
                        pivot.Articulos[i].Identificador = i;
                    }
                }
            }

            // Actualiza el odontograma con el plan de tratamiento despues de
            // Hacer los cambios en evolucion
            foreach (var item in Variables_Globales.PCL.PlanTratamiento.odontogramaEvolucion)
            {
                var existe = Planes.Any(a => a.Identificador == item.Identificador);
                if(existe)
                {
                    var elemento = Planes.First(a => a.Identificador == item.Identificador);
                    item.PlanTratamiento = Convertir_Observables.ConvertirEntidades(elemento, new Hefesoft.Entities.Odontologia.Odontograma.PlanTratamientoEntity());
                }
            }
                        
            //Se guarda lo mismo que en plan de tratamiento pero con el plan actualizado
            //Es decir hoy se le realiza una calsa entonces se actualiza eso
            var odontogramaInsertar = new Hefesoft.Entities.Odontologia.Odontograma.Odontograma();            
            odontogramaInsertar = Variables_Globales.PCL.PlanTratamiento;
            
            Hefesoft.Entities.Odontologia.Odontograma.Odontograma result = await odontogramaInsertar.postBlob();

            //Se guarda el tratamiento en una tabla aparte para que el listado inicial cargue rapido
            //Se actualizan los valores
            Variables_Globales.PCL.PlanTratamiento.tratamiento.RowKey = Variables_Globales.PCL.PlanTratamiento.RowKey;
            var tratamientoTableStorage = await Variables_Globales.PCL.PlanTratamiento.tratamiento.postTable();            
            
            return true;
        }

        public Task<bool> Actualizartratamiento(TratamientoEntity Tratamiento)
        {
            var tcs = new TaskCompletionSource<bool>();

            cliente.ActualizarTratamientoCompleted += (s, e) =>
            {
                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(e.Result);
            };
            cliente.ActualizarTratamientoAsync(Tratamiento);

            return tcs.Task;
        }

        public Task<ConvenioEntity> consultarConvenio(short id)
        {
            //Validar
            var tcs = new TaskCompletionSource<ConvenioEntity>();
            tcs.TrySetResult(new ConvenioEntity());
            return tcs.Task;
        }


        /// <summary>
        ///     Lista los tratamientos activos d eun paciente
        /// </summary>
        public async Task<ObservableCollection<TratamientoEntity>> ConsultarTratamientosPaciente(short idIps,
            int idPaciente)
        {
            
            var lst = new ObservableCollection<TratamientoEntity>();

            var result = await new Hefesoft.Entities.Odontologia.Tratamiento.TratamientoEntity().getTableByPartition();

            //List<Hefesoft.Entities.Odontologia.Odontograma.Odontograma> blob =
            //    await new Hefesoft.Entities.Odontologia.Odontograma.Odontograma().getBlobByPartition();

            // Saca una propiedad de un listado y devuelve una propiedad de la misma
            // Ademas llena el rowkey con el identificador
            //List<Hefesoft.Entities.Odontologia.Tratamiento.TratamientoEntity> lstTratamientos =
            //    blob.obtenerListadoDadoPropiedad
            //        <Hefesoft.Entities.Odontologia.Odontograma.Odontograma,
            //            Hefesoft.Entities.Odontologia.Tratamiento.TratamientoEntity>("tratamiento");
            //lstTratamientos.ToObservableCollection().ConvertirObservables(lst);

            lst = Convertir_Observables.ConvertirObservables(result.ToObservableCollection(), lst);

            return lst;
        }

        public Task<ObservableCollection<ConfiguracionProcedimientoOdontologiaDto>>
            ConsultarProcedimientosOdontologiaLigero(short idIps)
        {
            inicializarContexto();
            var tcs = new TaskCompletionSource<ObservableCollection<ConfiguracionProcedimientoOdontologiaDto>>();
            cliente.ConsultarProcedimientosOdontologiaLigeroCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    tcs.TrySetException(e.Error);
                }
                else if (e.Cancelled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetResult(e.Result);
                }
            };
            cliente.ConsultarProcedimientosOdontologiaLigeroAsync(idIps);

            return tcs.Task;
        }

        public async Task<ObservableCollection<TipoTratamientoEntity>> ConsultarTiposTratamiento(short IdIps)
        {
            List<Hefesoft.Entities.Odontologia.Tratamiento.TipoTratamientoEntity> result =
                await  new Hefesoft.Entities.Odontologia.Tratamiento.TipoTratamientoEntity().getBlobByPartition("tipotratamientoentity", "cnt.panacea.entities.odontologia.tipotratamientoentity");
            ObservableCollection<TipoTratamientoEntity> lstTiposTratamiento = result.ToObservableCollection().ConvertirObservables(new ObservableCollection<TipoTratamientoEntity>());
            return lstTiposTratamiento;
        }

        public Task<ObservableCollection<RelaDiagnosticoProcedEntity>> ConsultarProcedimientosPorDiagnostico(
            short idIps, String Diagnosticos)
        {
            //Validar si se puedo hacer
            //La idea es trear los procedimientos asociados en el diagnostico inicial
            var tcs = new TaskCompletionSource<ObservableCollection<RelaDiagnosticoProcedEntity>>();
            tcs.TrySetResult(new ObservableCollection<RelaDiagnosticoProcedEntity>());
            return tcs.Task;
        }

        public async Task<ObservableCollection<ProcedimientoEspecialidadSedeEntity>> ConsultarEspecialidadesPorProcedimiento(
            short idSede, string Procedimientos)
        {
            //Validar como se haria esto
            //Se deben cargar las especialidades asociadas a cada procedimiento
            List<Hefesoft.Entities.Odontologia.Especialidades.ProcedimientoEspecialidadSedeEntity> result = await CrudBlob.getBlobByPartition(new Hefesoft.Entities.Odontologia.Especialidades.ProcedimientoEspecialidadSedeEntity(), "procedimientoespecialidadsedeentity", "cnt.panacea.entities.parametrizacion.procedimientoespecialidadsedeentity");
            var resultadoDevolver = Convertir_Observables.ConvertirObservables(result.ToObservableCollection(), new ObservableCollection<ProcedimientoEspecialidadSedeEntity>());
            return resultadoDevolver;
        }

        public Task<TratamientoImagenEntity> ConsultarImagenTratamiento(int idImagenTratamiento)
        {
            inicializarContexto();
            var tcs = new TaskCompletionSource<TratamientoImagenEntity>();

            cliente.ConsultarImagenTratamientoCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    tcs.TrySetException(e.Error);
                }
                else if (e.Cancelled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetResult(e.Result);
                }
            };
            cliente.ConsultarImagenTratamientoAsync(idImagenTratamiento);

            return tcs.Task;
        }

        public async Task<ObservableCollection<NivelSeveridadDXEntity>> ConsultarNivelSeveridad()
        {
            var result = new ObservableCollection<NivelSeveridadDXEntity>();
            List<Hefesoft.Entities.Odontologia.Diagnostico.NivelSeveridadDXEntity> blob = await new Hefesoft.Entities.Odontologia.Diagnostico.NivelSeveridadDXEntity().getBlobByPartition(
                        "nivelseveridaddxentity", "cnt.panacea.entities.parametrizacion.nivelseveridaddxentity");
            blob.ToObservableCollection().ConvertirObservables(result);
            return result;
        }

        //No tiene uso aca porque se carga en SeleccionarTratamientoActivo
        public Task<ObservableCollection<OdontogramaEntity>> ConsultarOdontogramaPaciente(Int64 idOdontogramaPaciente,
            short idIps)
        {
            var tcs = new TaskCompletionSource<ObservableCollection<OdontogramaEntity>>();
            return tcs.Task;
        }

        public Task<ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>> ConsultarOtrasCaracteristicasConfig(
            short idIps)
        {
            inicializarContexto();
            var tcs = new TaskCompletionSource<ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>>();
            cliente.ConsultarOtrasCaracteristicasConfigCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    tcs.TrySetException(e.Error);
                }
                else if (e.Cancelled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetResult(e.Result);
                }
            };
            cliente.ConsultarOtrasCaracteristicasConfigAsync(idIps);

            return tcs.Task;
        }

        //Validar
        public Task<TerceroEntity> ConsultarPrestador(int IdPrestador) //LFDO Bug 16006
        {
            var tcs = new TaskCompletionSource<TerceroEntity>();
            tcs.TrySetResult(new TerceroEntity());
            return tcs.Task;
        }


        public Task<ObservableCollection<SesionesPlanTratamientoEntity>> ConsultarSesionesTratamiento(long IdTratamiento)
        {
            //Validar
            var tcs = new TaskCompletionSource<ObservableCollection<SesionesPlanTratamientoEntity>>();

            var odontogramas = Variables_Globales.PCL.PlanTratamiento.odontogramaPlanTratamiento;
            var listadoSesiones = new List<SesionesPlanTratamientoEntity>();

            //Se convierte de las entidades de hefesoft a las de cnt
            foreach (var item in odontogramas)
            {
                foreach (var itemB in item.PlanTratamiento.SesionesPlanTratamiento.ToList())
                {
                    var itemInsertar = Convertir_Observables.ConvertirEntidades(itemB, new SesionesPlanTratamientoEntity());
                    listadoSesiones.Add(itemInsertar);
                }
            }


            tcs.TrySetResult(listadoSesiones.ToObservableCollection());
            return tcs.Task;
        }

        public Task<ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>> ConsultarProcedimientosConvenio(
            int idPaciente, short idConvenio, short idIps)
        {
            inicializarContexto();
            var tcs = new TaskCompletionSource<ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>>();

            cliente.ConsultarProcedimientosConvenioCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    tcs.TrySetException(e.Error);
                }
                else if (e.Cancelled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetResult(e.Result);
                }
            };
            cliente.ConsultarProcedimientosConvenioAsync(idPaciente, idConvenio, idIps);
            return tcs.Task;
        }


        public Task<ObservableCollection<PlanTratamientoEntity>> ConsultarValoresProcedimiento(short IdIps,
            int idPaciente, PlanesTratamientoCollection Planes, bool gestante, short IdConvenio, short idEspecialidad)
        {
            inicializarContexto();
            var tcs = new TaskCompletionSource<ObservableCollection<PlanTratamientoEntity>>();

            cliente.ConsultarValoresProcedimientoCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    tcs.TrySetException(e.Error);
                }
                else if (e.Cancelled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetResult(e.Result);
                }
            };
            cliente.ConsultarValoresProcedimientoAsync(IdIps, idPaciente, gestante, idEspecialidad, IdConvenio,
                Planes.ToObservableCollection());

            return tcs.Task;
        }


        public async Task<long> GuardarOdontogramaInicial(TratamientoEntity tratamiento,
            OdontogramasPacienteEntity odontogramaPaciente, List<OdontogramaEntity> odontograma,
            List<TratamientoImagenEntity> adjuntosImagen, short idIps)
        {
            guardado_En_Proceso = true;
            foreach (OdontogramaEntity pivot in odontograma)
            {
                if (pivot.DienteReferencia1 != null)
                {
                    if (pivot.DienteReferencia1.Identificador == 0)
                    {
                        pivot.DienteReferencia1 = null;
                    }
                }
                if (pivot.DienteReferencia2 != null)
                {
                    if (pivot.DienteReferencia2.Identificador == 0)
                    {
                        pivot.DienteReferencia2 = null;
                    }
                }
                if (pivot.Diagnostico != null)
                {
                    //if (pivot.Diagnostico.Identificador == 0)
                    //{
                    //    pivot.Diagnostico = null;
                    //}
                }

                if (pivot.Procedimiento != null)
                {
                    //if (pivot.Procedimiento.Identificador == 0)
                    //{
                    //    pivot.Procedimiento = null;
                    //}
                }
            }
            odontogramaPaciente.IdIps = idIps;
            var tcs = new TaskCompletionSource<long>();


            var odontogramaInsertar = new Hefesoft.Entities.Odontologia.Odontograma.Odontograma();
            odontogramaInsertar.idIps = idIps;
            odontogramaInsertar.tratamiento = tratamiento.ConvertirEntidades<Hefesoft.Entities.Odontologia.Tratamiento.TratamientoEntity, TratamientoEntity>();

            //En la implementacion de hefesoft esto deberia cambiar para evitar conflictos como que se repitan caracteres
            odontogramaInsertar.RowKey = new Random().Next(1, 1000000000).ToString();
            odontogramaInsertar.tratamiento.RowKey = odontogramaInsertar.RowKey;
            odontogramaInsertar.tratamiento.Identificador = long.Parse(odontogramaInsertar.RowKey);

            odontogramaInsertar.odontogramaPaciente =odontogramaPaciente.ConvertirEntidades<Hefesoft.Entities.Odontologia.Odontograma.OdontogramasPacienteEntity, OdontogramasPacienteEntity>();

            //La entidad que tiene la lista se usa para llenar la que se persistira en el blob
            odontogramaInsertar.odontogramaInicial = odontograma.ConvertirIEnumerable(odontogramaInsertar.odontogramaInicial);
            odontogramaInsertar.adjuntosImagen = adjuntosImagen.ConvertirIEnumerable(odontogramaInsertar.adjuntosImagen);
            Hefesoft.Entities.Odontologia.Odontograma.Odontograma result = await odontogramaInsertar.postBlob();
            
            //Se guarda el tratamiento en una tabla aparte para que el listado inicial cargue rapido
            var tratamientoTableStorage = await odontogramaInsertar.tratamiento.postTable();

            //Cuando se guarda un odontograma inicial agregarlo a listado inicial sin hacer otra llamada al servicio
            var elementoAgregarListado = Convertir_Observables.ConvertirEntidades(odontogramaInsertar.tratamiento, new TratamientoEntity());
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(elementoAgregarListado, "Agregar Listado inicial");

            long id = Convert.ToInt64(odontogramaInsertar.RowKey);
            return id;
        }

        public async Task<ObservableCollection<long>> GuardarPlanTratamiento(
            TratamientoEntity TratamientoEntity,
            short idTipoTratamiento,
            OdontogramasPacienteEntity OdontogramasPacienteEntity,
            ObservableCollection<OdontogramaEntity> odontogramaTratamiento,
            bool Cotizacion,
            PacienteEntity Paciente,
            ComprobanteEntity Comprobante,
            short idIps,
            string usuario,
            short idSede,
            short idConvenio
            )
        {

            // 1 forma
            Variables_Globales.PCL.PlanTratamiento.tratamiento = Convertir_Observables.ConvertirEntidades(TratamientoEntity, new Hefesoft.Entities.Odontologia.Tratamiento.TratamientoEntity());
            Variables_Globales.PCL.PlanTratamiento.odontogramaPaciente = OdontogramasPacienteEntity.ConvertirEntidades<Hefesoft.Entities.Odontologia.Odontograma.OdontogramasPacienteEntity, OdontogramasPacienteEntity>();

            // 2 forma
            Variables_Globales.PCL.PlanTratamiento.odontogramaPlanTratamiento = odontogramaTratamiento.ConvertirIEnumerable(Variables_Globales.PCL.PlanTratamiento.odontogramaPlanTratamiento);
            Hefesoft.Entities.Odontologia.Odontograma.Odontograma result = await Variables_Globales.PCL.PlanTratamiento.postBlob();

            //Se guarda el tratamiento en una tabla aparte para que el listado inicial cargue rapido
            //Se actualizan los valores
            Variables_Globales.PCL.PlanTratamiento.tratamiento.RowKey = Variables_Globales.PCL.PlanTratamiento.RowKey;
            var tratamientoTableStorage = await Variables_Globales.PCL.PlanTratamiento.tratamiento.postTable();

            return new ObservableCollection<long>();
        }


        public Task<bool> GuardarImagenTratamiento(long idTratamientoPaciente,
            ObservableCollection<TratamientoImagenEntity> adjuntoImagenes)
        {
            inicializarContexto();
            var tcs = new TaskCompletionSource<bool>();

            for (int i = 0; i <= adjuntoImagenes.Count - 1; i++)
            {
                adjuntoImagenes[i].Identificador = i;
            }

            cliente.guardarImagenesTratamientoCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    tcs.TrySetException(e.Error);
                }
                else if (e.Cancelled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetResult(e.Result);
                }
            };
            cliente.guardarImagenesTratamientoAsync(idTratamientoPaciente, adjuntoImagenes);
            return tcs.Task;
        }

        public async Task<ObservableCollection<ComprobanteEntity>> ListarComprobantes(short idIps, string usuario,
            short idSede)
        {
            //Validar
            List<Hefesoft.Entities.Odontologia.Comprobantes.ComprobanteEntity> result = await CrudBlob.getBlobByPartition(new Hefesoft.Entities.Odontologia.Comprobantes.ComprobanteEntity(), "comprobanteentity", "cnt.panacea.entities.parametrizacion.comprobanteentity");
            var resultadoDevolver = Convertir_Observables.ConvertirObservables(result.ToObservableCollection(), new ObservableCollection<ComprobanteEntity>());
            return resultadoDevolver;
        }

        public Task<ObservableCollection<OdontogramaEntity>> ListarHistorial(long idTratamiento, short idIps)
        {
            inicializarContexto();
            var tcs = new TaskCompletionSource<ObservableCollection<OdontogramaEntity>>();

            cliente.ListarHistorialOdontogramaCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    tcs.TrySetException(e.Error);
                }
                else if (e.Cancelled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetResult(e.Result);
                }
            };

            cliente.ListarHistorialOdontogramaAsync(idTratamiento, idIps);
            return tcs.Task;
        }

        public async Task<ObservableCollection<TerceroEntity>> ListarHigienistasPorIps(short IdIps)
        {
            List<Hefesoft.Entities.Odontologia.Convenio.TerceroEntity> result = await CrudBlob.getBlobByPartition(new Hefesoft.Entities.Odontologia.Convenio.TerceroEntity(), "terceroentity", "cnt.panacea.entities.parametrizacion.terceroentity");
            var resultadoDevolver = Convertir_Observables.ConvertirObservables(result.ToObservableCollection(), new ObservableCollection<TerceroEntity>());
            return resultadoDevolver;
        }

        public async Task<ObservableCollection<TerceroEntity>> ListarOdontologosPorIps(short IdIps)
        {
            List<Hefesoft.Entities.Odontologia.Convenio.TerceroEntity> result = await CrudBlob.getBlobByPartition(new Hefesoft.Entities.Odontologia.Convenio.TerceroEntity(), "terceroentity", "cnt.panacea.entities.parametrizacion.terceroentity");
            var resultadoDevolver = Convertir_Observables.ConvertirObservables(result.ToObservableCollection(), new ObservableCollection<TerceroEntity>());
            return resultadoDevolver;
        }

        public async Task<ObservableCollection<OdontogramaEntity>> ListarOdontogramaTratamiento(Int64 idOdontogramaPaciente,
            short idIps)
        {
            ObservableCollection<OdontogramaEntity> lstOdontogramas = new ObservableCollection<OdontogramaEntity>();

            //Cuando se crea un odontograma inicial y se navega a el de tratamiento
            //No se encontraran registros por esto es necesario validar este caso
            if (Variables_Globales.PCL.PlanTratamiento != null && Variables_Globales.PCL.PlanTratamiento.RowKey != null && Variables_Globales.PCL.PlanTratamiento.odontogramaPlanTratamiento.Any())
            { 
                Variables_Globales.TratamientosPadre = Convertir_Observables.ConvertirEntidades(Variables_Globales.PCL.PlanTratamiento.tratamiento, new TratamientoEntity());
                Variables_Globales.IdTratamientoActivo = Variables_Globales.TratamientosPadre.Identificador;

                TratamientoEntity entidadConvertida = Convertir_Observables.ConvertirEntidades(Variables_Globales.PCL.PlanTratamiento.tratamiento, new TratamientoEntity());
                lstOdontogramas = Variables_Globales.PCL.PlanTratamiento.odontogramaPlanTratamiento.ToObservableCollection().ConvertirObservables(new ObservableCollection<OdontogramaEntity>());
            }
            else
            {
                //No encontrado
            }

            return lstOdontogramas;
        }

        public Task<ObservableCollection<OdontogramasPacienteEntity>> ListarOdontogramasSinTratamiento(int IdPaciente,
            short IdIps)
        {
            var tcs = new TaskCompletionSource<ObservableCollection<OdontogramasPacienteEntity>>();
            var lst = new ObservableCollection<OdontogramasPacienteEntity>();
            tcs.SetResult(lst);
            return tcs.Task;
        }


        public async Task<ObservableCollection<FinalidadProcedimientoEntity>> ListarFinalidadesProcedimiento(short idIps)
        {
            List<Hefesoft.Entities.Odontologia.Finalidad.FinalidadProcedimientoEntity> result = await CrudBlob.getBlobByPartition(new Hefesoft.Entities.Odontologia.Finalidad.FinalidadProcedimientoEntity(), "finalidadprocedimientoentity", "cnt.panacea.entities.parametrizacion.finalidadprocedimientoentity");
            var resultadoDevolver = Convertir_Observables.ConvertirObservables(result.ToObservableCollection(), new ObservableCollection<FinalidadProcedimientoEntity>());            
            return resultadoDevolver;
        }

        public Task<ObservableCollection<PacienteConvenioEntity>> ListarConveniosPaciente(short idIps, int idPaciente)
        {
            inicializarContexto();
            var tcs = new TaskCompletionSource<ObservableCollection<PacienteConvenioEntity>>();

            cliente.ListarConveniosPacienteCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    tcs.TrySetException(e.Error);
                }
                else if (e.Cancelled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetResult(e.Result);
                }
            };
            cliente.ListarConveniosPacienteAsync(idIps, idPaciente);

            return tcs.Task;
        }


        public async Task<ObservableCollection<ParametroOdontologicoConvenioEntity>> ListarParametrosConvenio(
            short IdIps, short idConvenio)
        {
            var tcs = new TaskCompletionSource<ObservableCollection<ParametroOdontologicoConvenioEntity>>();
            var lst = new ObservableCollection<ParametroOdontologicoConvenioEntity>();
            tcs.SetResult(lst);
            return lst;
        }

        public Task<ParametroOdontologicoBodegaEntity> ListarParametrosBodega(short idIps)
        {
            inicializarContexto();
            var tcs = new TaskCompletionSource<ParametroOdontologicoBodegaEntity>();

            cliente.ListarParametrosBodegaCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    tcs.TrySetException(e.Error);
                }
                else if (e.Cancelled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetResult(e.Result);
                }
            };
            cliente.ListarParametrosBodegaAsync(idIps);
            return tcs.Task;
        }

        public async Task<TratamientoEntity> SeleccionarTratamientoActivo(long idTratamiento)
        {            
            Hefesoft.Entities.Odontologia.Odontograma.Odontograma result = await new Hefesoft.Entities.Odontologia.Odontograma.Odontograma().getBlobByPartitionAndRowKey(idTratamiento.ToString());
            result.tratamiento.RowKey = result.RowKey;
            
            Variables_Globales.PCL.PlanTratamiento = result;
            
            TratamientoEntity entidadConvertida = Convertir_Observables.ConvertirEntidades(result.tratamiento, new TratamientoEntity());
            ObservableCollection<OdontogramaEntity> lstOdontogramas = result.odontogramaInicial.ToObservableCollection().ConvertirObservables(new ObservableCollection<OdontogramaEntity>());
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(lstOdontogramas, "Odontograma cargado");
            return entidadConvertida;
        }

        public Task<OdontogramasPacienteEntity> SelecionarOdontogramaPaciente(long idOdontogramaPaciente)
        {
            inicializarContexto();
            var tcs = new TaskCompletionSource<OdontogramasPacienteEntity>();

            cliente.SeleccionarOdontogramaPacienteCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    tcs.TrySetException(e.Error);
                }
                else if (e.Cancelled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetResult(e.Result);
                }
            };
            cliente.SeleccionarOdontogramaPacienteAsync(idOdontogramaPaciente);

            return tcs.Task;
        }


        public Task<long> ReciboCajaTratamiento(long idTratamiento)
        {   
            var tcs = new TaskCompletionSource<long>();
            tcs.TrySetResult(1);            
            return tcs.Task;
        }

        public Task<decimal> ValorPagoTratamiento(long idTratamiento)
        {            
            var tcs = new TaskCompletionSource<decimal>();
            tcs.TrySetResult(100000);
            return tcs.Task;
        }

        public Task<bool> ValidarusuarioCierraTratamiento(string idUsuario, string Clave)
        {
            inicializarContexto();
            var tcs = new TaskCompletionSource<bool>();

            cliente.ValidarUsuarioCierraTratamientoCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    tcs.TrySetException(e.Error);
                }
                else if (e.Cancelled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetResult(e.Result);
                }
            };
            cliente.ValidarUsuarioCierraTratamientoAsync(idUsuario, Clave);
            return tcs.Task;
        }

        public Task<ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>> ConsultarProcedimientosConfigurados(
            short idIps)
        {
            var tcs = new TaskCompletionSource<ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>>();
            tcs.SetResult(new ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>());
            return tcs.Task;
        }

        public async Task<ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>>
            ConsultarDiagnosticosConfigurados(short idIps)
        {
            var tcs = new TaskCompletionSource<ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>>();
            var result = new ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>();

            List<DiagnosticoProcedimiento> blob =
                await
                    new DiagnosticoProcedimiento().getBlobByPartition("configurardiagnosticoprocedimotraentity",
                        "cnt.panacea.entities.odontologia.configurardiagnosticoprocedimotraentity");
            blob.ToObservableCollection().ConvertirObservables(result);

            return result;
        }


        void IContexto_Odontologia.binding(dynamic valor)
        {
        }

        void IContexto_Odontologia.url(string url)
        {
        }
        
    }
}