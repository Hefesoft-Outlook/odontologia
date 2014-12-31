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

        //El nombre de la superficie Ej: Mesial, Oclusal etc
        public Superficie Superficie_Enumerador { get; set; }
        public string Nombre_Superficie { get; set; }

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

            manejoNombreSuperficie();
        }

        private void manejoNombreSuperficie()
        {
            Superficie_Enumerador = this.Superficie.superficieNomenclatura();
            if (Superficie_Enumerador == Entities.Odontologia.Superficie.Distal)
            {
                Nombre_Superficie = "Distal";
            }
            else if (Superficie_Enumerador == Entities.Odontologia.Superficie.IncisalOclusal)
            {
                Nombre_Superficie = "Incisal Oclusal";
            }
            else if (Superficie_Enumerador == Entities.Odontologia.Superficie.LingualPalatinaInferior)
            {
                Nombre_Superficie = "Lingual palatina inferior";
            }
            else if (Superficie_Enumerador == Entities.Odontologia.Superficie.LingualPalatinaSuperior)
            {
                Nombre_Superficie = "Lingual palatina superior";
            }
            else if (Superficie_Enumerador == Entities.Odontologia.Superficie.Mesial)
            {
                Nombre_Superficie = "Mesial";
            }
            else if (Superficie_Enumerador == Entities.Odontologia.Superficie.SuperficieTotal)
            {
                Nombre_Superficie = "Superficie Total";
            }
            else if (Superficie_Enumerador == Entities.Odontologia.Superficie.VestibularInferior)
            {
                Nombre_Superficie = "Vestibular inferior";
            }
            else if (Superficie_Enumerador == Entities.Odontologia.Superficie.VestibularSuperior)
            {
                Nombre_Superficie = "Vestibular superior";
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
