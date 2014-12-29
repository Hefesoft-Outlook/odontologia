using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Entities.Parametrizacion;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using Cnt.Panacea.Xap.Odontologia.Vm.Odontograma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Extensiones.Clases
{
    public class DiagnosticoProcedimiento_Extend
    {
        public int Codigo_Pieza_Dental { get; set; }
        public referenciaSupernumerario Referencia_SuperNumerario { get; set; }
        public bool Es_Supernumerario { get; set; }
        public string Superficie { get; set; }        

        private ConfigurarDiagnosticoProcedimOtraEntity configurarDiagnosticoProcedimOtraEntity;

        public ConfigurarDiagnosticoProcedimOtraEntity ConfigurarDiagnosticoProcedimOtraEntity
        {
            get 
            { 
                return configurarDiagnosticoProcedimOtraEntity; 
            }
            
            set 
            { 
                configurarDiagnosticoProcedimOtraEntity = value;
                procesarValor(value);
            }
        }

        private void procesarValor(Entities.Odontologia.ConfigurarDiagnosticoProcedimOtraEntity value)
        {
            if (value.TipoPanel == TipoPanel.Diagnostico)
            {
                Descripcion = value.Descripcion;
            }
            else if (value.TipoPanel == TipoPanel.Procedimiento)
            {
                Descripcion = value.Descripcion;
            }
        }
        

        public Tipo_Odontograma Tipo_Odontograma_Actual { get; set; }
        public NivelSeveridadDXEntity Nivel_Severidad { get; set; }
        public OdontogramaEntity OdontogramaEntity { get; set; }
    
public  string Descripcion { get; set; }}

    public enum DiagnosticoProcedimiento_Validaciones
    {
        Existe = 0,
        Ya_posee_elementos = 1,
        No_Existe_En_La_Lista = 2,
        Agregado = 3,
        No_Opcion_Encontrada = 4
    }
}
