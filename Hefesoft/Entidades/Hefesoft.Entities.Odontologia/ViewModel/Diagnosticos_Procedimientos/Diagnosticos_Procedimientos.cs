using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Hefesoft.Entities.Odontologia.Diagnostico;
using Hefesoft.Entities.Odontologia.Entidades.Diagnostico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.ViewModel.Diagnosticos_Procedimientos
{
    public class Diagnosticos_Procedimientos : ViewModelBase
    {
        public Diagnosticos_Procedimientos()
        {
            if(IsInDesignMode)
            {
                var diagnosticoProcedimiento = new ConfigurarDiagnosticoProcedimOtraEntity()
                {
                    Identificador = 2,
                    AplicaTratamiento = 1,//AplicaTratamiento.Diente,
                    Codigo = "K022",//Codigo Diag
                    Descripcion = "Caries del cemento",
                    Diagnostico = 3777,
                    //Config Simbolo Aplica tambien para Px,Otros
                    Simbolo = "E",
                    ColorAdicional = 6750207,
                    Fuente = "Wingdings 3",
                    //
                    //Solo para Dx
                    IndiceCOP = true,
                    IndiceCEO = true,
                    IndicePlacaBacteriana = true,
                    Severidad = true,//Si es true debe exitir el comob de Severidad, si es false no es obligatorio
                };
            }
            else
            {
                Tipos.Add(new Tipo_Diagnostico_Procedimiento() { Codigo = 1, Descripcion = "Diagnostico" });
                Tipos.Add(new Tipo_Diagnostico_Procedimiento() { Codigo = 2, Descripcion = "Procedimiento" });

                Aplica_A.Add(new Aplica_A() { Codigo = 3, Descripcion = "Boca" });
                Aplica_A.Add(new Aplica_A() { Codigo = 2, Descripcion = "Superficies" });
                Aplica_A.Add(new Aplica_A() { Codigo = 1, Descripcion = "Pieza completa" });

                TipoFuente.Add(new Tipo_Fuente() { Codigo = 1, Descripcion = "Wingdings 1" });
                TipoFuente.Add(new Tipo_Fuente() { Codigo = 2, Descripcion = "Wingdings 2" });
                TipoFuente.Add(new Tipo_Fuente() { Codigo = 3, Descripcion = "Wingdings 3" });

                TipoElemento.Add(new Tipo_Elemento() { Codigo = 1, Descripcion = "Solo color" });
                TipoElemento.Add(new Tipo_Elemento() { Codigo = 2, Descripcion = "Color y simbolo" });

                listarElementos();
                ingresoTextoCommand = new RelayCommand<string>(ingresoTexto);
                descripcionCommand = new RelayCommand<string>(descripcion);
                guardarCommand = new RelayCommand(guardar);
            }
        }

        public async void listarElementos()
        {
            List<DiagnosticoProcedimiento> blob = await new DiagnosticoProcedimiento().getBlobByPartition("configurardiagnosticoprocedimotraentity", "cnt.panacea.entities.odontologia.configurardiagnosticoprocedimotraentity");
            Listado = Convertir_Observables.ConvertirIEnumerable(blob, new List<Cnt.Panacea.Entities.Odontologia.ConfigurarDiagnosticoProcedimOtraEntity>());
            RaisePropertyChanged("Listado");
        }

        private void descripcion(string obj)
        {
            DiagnosticoProcedimiento.Descripcion = obj;
            forceUiDiagnosticoProcedimiento();
        }

        private async void guardar()
        {
            DiagnosticoProcedimiento.nombreTabla = "configurardiagnosticoprocedimotraentity";
            DiagnosticoProcedimiento.PartitionKey = "cnt.panacea.entities.odontologia.configurardiagnosticoprocedimotraentity";
            DiagnosticoProcedimiento.RowKey = new Random().Next().ToString();

            await DiagnosticoProcedimiento.postBlob();
        }

        private void ingresoTexto(string obj)
        {
            if(TipoElementoSeleccionado.Codigo == 1)
            {
                //No va 
            }
            else if (TipoElementoSeleccionado.Codigo == 2)
            {
                DiagnosticoProcedimiento.Letra = obj;
                DiagnosticoProcedimiento.Simbolo = obj;                
            }

            forceUiDiagnosticoProcedimiento();
        }

        public void forceUiDiagnosticoProcedimiento()
        {
            var elemento = diagnosticoProcedimiento;
            DiagnosticoProcedimiento = null;
            diagnosticoProcedimiento = elemento;
            RaisePropertyChanged("DiagnosticoProcedimiento");

            //Para compatibilidad con cnt
            diagnosticoOdontologia = new Cnt.Panacea.Entities.Odontologia.ConfigurarDiagnosticoProcedimOtraEntity();
            diagnosticoOdontologia = Convertir_Observables.ConvertirEntidadesComptabilidad(DiagnosticoProcedimiento, new Cnt.Panacea.Entities.Odontologia.ConfigurarDiagnosticoProcedimOtraEntity());
            RaisePropertyChanged("diagnosticoOdontologia");
        }

        private ConfigurarDiagnosticoProcedimOtraEntity diagnosticoProcedimiento = new ConfigurarDiagnosticoProcedimOtraEntity();

        public ConfigurarDiagnosticoProcedimOtraEntity DiagnosticoProcedimiento
        {
            get { return diagnosticoProcedimiento; }
            set 
            { 
                diagnosticoProcedimiento = value;
                RaisePropertyChanged("DiagnosticoProcedimiento"); 
            }
        }

        private List<Tipo_Diagnostico_Procedimiento> tipos = new List<Tipo_Diagnostico_Procedimiento>();

        public List<Tipo_Diagnostico_Procedimiento> Tipos
        {
            get { return tipos; }
            set 
            { 
                tipos = value;
                RaisePropertyChanged("Tipos");
            }
        }

        private Tipo_Diagnostico_Procedimiento tipoSeleccionado;

        public Tipo_Diagnostico_Procedimiento TipoSeleccionado
        {
            get { return tipoSeleccionado; }
            set 
            { 
                tipoSeleccionado = value; 

                if(value.Codigo == 1)
                {
                    DiagnosticoProcedimiento.TipoPanel = TipoPanel.Diagnostico;
                }
                else if (value.Codigo == 2)
                {
                    DiagnosticoProcedimiento.TipoPanel = TipoPanel.Procedimiento;
                }

                forceUiDiagnosticoProcedimiento();
                RaisePropertyChanged("TipoSeleccionado"); 
            }
        }
        


        private List<Aplica_A> aplica = new List<Aplica_A>();

        public List<Aplica_A> Aplica_A
        {
            get { return aplica; }
            set 
            { 
                aplica = value; 
                RaisePropertyChanged("Aplica_A"); 
            }
        }

        private Aplica_A aplicaSeleccionado;

        public Aplica_A AplicaSeleccionado
        {
            get { return aplicaSeleccionado; }
            set 
            { 
                aplicaSeleccionado = value;
                DiagnosticoProcedimiento.AplicaTratamiento = value.Codigo;
                forceUiDiagnosticoProcedimiento();
                RaisePropertyChanged("AplicaSeleccionado");            
            }
        }

        private List<Tipo_Fuente> tipoFuente = new List<Tipo_Fuente>();

        public List<Tipo_Fuente> TipoFuente
        {
            get { return tipoFuente; }
            set { tipoFuente = value; RaisePropertyChanged("TipoFuente"); }
        }

        private Tipo_Fuente tipoFuenteSeleccionado;

        public Tipo_Fuente TipoFuenteSeleccionado
        {
            get { return tipoFuenteSeleccionado; }
            set 
            { 
                tipoFuenteSeleccionado = value;
                DiagnosticoProcedimiento.Fuente = value.Descripcion;
                forceUiDiagnosticoProcedimiento();
                RaisePropertyChanged("TipoFuenteSeleccionado"); 
            }
        }

        private List<Tipo_Elemento> tipoElemento = new List<Tipo_Elemento>();

        public List<Tipo_Elemento> TipoElemento
        {
            get { return tipoElemento; }
            set { tipoElemento = value; RaisePropertyChanged("TipoElemento"); }
        }

        private Tipo_Elemento tipoElementoSeleccionado;

        public Tipo_Elemento TipoElementoSeleccionado
        {
            get { return tipoElementoSeleccionado; }
            set 
            { 
                tipoElementoSeleccionado = value;

                if(value.Codigo == 1)
                {
                    DiagnosticoProcedimiento.Letra = null;
                    DiagnosticoProcedimiento.Simbolo = null;
                    DiagnosticoProcedimiento.Descripcion = null;
                    forceUiDiagnosticoProcedimiento();
                }

                RaisePropertyChanged("TipoElementoSeleccionado"); 
            }
        }

        public RelayCommand<string> ingresoTextoCommand { get; set; }
        public RelayCommand guardarCommand { get; set; }

        public RelayCommand<string> descripcionCommand { get; set; }

        

        private Cnt.Panacea.Entities.Odontologia.ConfigurarDiagnosticoProcedimOtraEntity _diagnosticoOdontologia;

        public Cnt.Panacea.Entities.Odontologia.ConfigurarDiagnosticoProcedimOtraEntity diagnosticoOdontologia
        {
            get { return _diagnosticoOdontologia; }
            set 
            {
                _diagnosticoOdontologia = value;
                RaisePropertyChanged("diagnosticoOdontologia");

                DiagnosticoProcedimiento = Convertir_Observables.ConvertirEntidadesComptabilidad(value, new ConfigurarDiagnosticoProcedimOtraEntity());
            }
        }


        public List<Cnt.Panacea.Entities.Odontologia.ConfigurarDiagnosticoProcedimOtraEntity> Listado { get; set; }
        
    }
}
