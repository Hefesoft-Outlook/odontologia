using System;
namespace Cnt.Panacea.Xap.Odontologia.Vm.Contexto
{
    public interface IContexto_Odontologia
    {
        System.Threading.Tasks.Task<bool> ActualizarPlanesTratamiento(Cnt.Panacea.Entities.Odontologia.TratamientoEntity Tratamiento, Cnt.Panacea.Entities.Odontologia.PlanesTratamientoCollection Planes);
        System.Threading.Tasks.Task<bool> Actualizartratamiento(Cnt.Panacea.Entities.Odontologia.TratamientoEntity Tratamiento);
        System.Threading.Tasks.Task<Cnt.Panacea.Entities.Parametrizacion.ConvenioEntity> consultarConvenio(short id);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Odontologia.ConfigurarDiagnosticoProcedimOtraEntity>> ConsultarDiagnosticosConfigurados(short idIps);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Parametrizacion.ProcedimientoEspecialidadSedeEntity>> ConsultarEspecialidadesPorProcedimiento(short idSede, string Procedimientos);
        System.Threading.Tasks.Task<Cnt.Panacea.Entities.Odontologia.TratamientoImagenEntity> ConsultarImagenTratamiento(int idImagenTratamiento);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Parametrizacion.NivelSeveridadDXEntity>> ConsultarNivelSeveridad();
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Odontologia.OdontogramaEntity>> ConsultarOdontogramaPaciente(long idOdontogramaPaciente, short idIps);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Odontologia.ConfigurarDiagnosticoProcedimOtraEntity>> ConsultarOtrasCaracteristicasConfig(short idIps);
        System.Threading.Tasks.Task<Cnt.Panacea.Entities.Parametrizacion.TerceroEntity> ConsultarPrestador(int IdPrestador);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Odontologia.ConfigurarDiagnosticoProcedimOtraEntity>> ConsultarProcedimientosConfigurados(short idIps);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Odontologia.ConfigurarDiagnosticoProcedimOtraEntity>> ConsultarProcedimientosConvenio(int idPaciente, short idConvenio, short idIps);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Odontologia.ConfiguracionProcedimientoOdontologiaDto>> ConsultarProcedimientosOdontologiaLigero(short idIps);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Odontologia.RelaDiagnosticoProcedEntity>> ConsultarProcedimientosPorDiagnostico(short idIps, string Diagnosticos);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Odontologia.SesionesPlanTratamientoEntity>> ConsultarSesionesTratamiento(long IdTratamiento);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Odontologia.TipoTratamientoEntity>> ConsultarTiposTratamiento(short IdIps);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Odontologia.TratamientoEntity>> ConsultarTratamientosPaciente(short idIps, int idPaciente);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Odontologia.PlanTratamientoEntity>> ConsultarValoresProcedimiento(short IdIps, int idPaciente, Cnt.Panacea.Entities.Odontologia.PlanesTratamientoCollection Planes, bool gestante, short IdConvenio, short idEspecialidad);
        System.Threading.Tasks.Task<bool> GuardarImagenTratamiento(long idTratamientoPaciente, System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Odontologia.TratamientoImagenEntity> adjuntoImagenes);
        System.Threading.Tasks.Task<long> GuardarOdontogramaInicial(Cnt.Panacea.Entities.Odontologia.TratamientoEntity tratamiento, Cnt.Panacea.Entities.Odontologia.OdontogramasPacienteEntity odontogramaPaciente, System.Collections.Generic.List<Cnt.Panacea.Entities.Odontologia.OdontogramaEntity> odontograma, System.Collections.Generic.List<Cnt.Panacea.Entities.Odontologia.TratamientoImagenEntity> adjuntosImagen, short idIps);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<long>> GuardarPlanTratamiento(Cnt.Panacea.Entities.Odontologia.TratamientoEntity TratamientoEntity, short idTipoTratamiento, Cnt.Panacea.Entities.Odontologia.OdontogramasPacienteEntity OdontogramasPacienteEntity, System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Odontologia.OdontogramaEntity> odontogramaTratamiento, bool Cotizacion, Cnt.Panacea.Entities.Parametrizacion.PacienteEntity Paciente, Cnt.Panacea.Entities.Parametrizacion.ComprobanteEntity Comprobante, short idIps, string usuario, short idSede, short idConvenio);
        void inicializarContexto();
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Parametrizacion.ComprobanteEntity>> ListarComprobantes(short idIps, string usuario, short idSede);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Parametrizacion.PacienteConvenioEntity>> ListarConveniosPaciente(short idIps, int idPaciente);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Parametrizacion.FinalidadProcedimientoEntity>> ListarFinalidadesProcedimiento(short idIps);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Parametrizacion.TerceroEntity>> ListarHigienistasPorIps(short IdIps);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Odontologia.OdontogramaEntity>> ListarHistorial(long idTratamiento, short idIps);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Odontologia.OdontogramasPacienteEntity>> ListarOdontogramasSinTratamiento(int IdPaciente, short IdIps);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Odontologia.OdontogramaEntity>> ListarOdontogramaTratamiento(long idOdontogramaPaciente, short idIps);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Parametrizacion.TerceroEntity>> ListarOdontologosPorIps(short IdIps);
        System.Threading.Tasks.Task<Cnt.Panacea.Entities.Odontologia.ParametroOdontologicoBodegaEntity> ListarParametrosBodega(short idIps);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Cnt.Panacea.Entities.Odontologia.ParametroOdontologicoConvenioEntity>> ListarParametrosConvenio(short IdIps, short idConvenio);
        System.Threading.Tasks.Task<long> ReciboCajaTratamiento(long idTratamiento);
        System.Threading.Tasks.Task<Cnt.Panacea.Entities.Odontologia.TratamientoEntity> SeleccionarTratamientoActivo(long idTratamiento);
        System.Threading.Tasks.Task<Cnt.Panacea.Entities.Odontologia.OdontogramasPacienteEntity> SelecionarOdontogramaPaciente(long idOdontogramaPaciente);
        System.Threading.Tasks.Task<bool> ValidarusuarioCierraTratamiento(string idUsuario, string Clave);
        System.Threading.Tasks.Task<decimal> ValorPagoTratamiento(long idTratamiento);

        void binding(dynamic valor);
        void url(string url);

        dynamic servicio(dynamic cliente);

    }
}
