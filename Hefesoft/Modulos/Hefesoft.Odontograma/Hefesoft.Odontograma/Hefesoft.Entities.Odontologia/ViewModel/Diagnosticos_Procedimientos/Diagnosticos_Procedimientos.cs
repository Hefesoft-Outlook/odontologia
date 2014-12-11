using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Hefesoft.Entities.Odontologia.Diagnostico;
using Hefesoft.Entities.Odontologia.Entidades.Diagnostico;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Hefesoft.Entities.Odontologia.Util;
using Hefesoft.Standard.BusyBox;
using Hefesoft.Standard.Util.Blob;
using Hefesoft.Standard.Util.table;


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


                TipoFuente.Add(new Tipo_Fuente() { Codigo = 0, Descripcion = "Webdings" });
                TipoFuente.Add(new Tipo_Fuente() { Codigo = 1, Descripcion = "Wingdings 1" });
                TipoFuente.Add(new Tipo_Fuente() { Codigo = 2, Descripcion = "Wingdings 2" });
                TipoFuente.Add(new Tipo_Fuente() { Codigo = 3, Descripcion = "Wingdings 3" });

                TipoElemento.Add(new Tipo_Elemento() { Codigo = 1, Descripcion = "Solo color" });
                TipoElemento.Add(new Tipo_Elemento() { Codigo = 2, Descripcion = "Color y simbolo" });

                listarElementos();
                ingresoTextoCommand = new RelayCommand<string>(ingresoTexto);
                descripcionCommand = new RelayCommand<string>(descripcion);
                guardarCommand = new RelayCommand(guardar);
                nuevoCommand = new RelayCommand(nuevo);
                deleteCommand = new RelayCommand<Cnt.Panacea.Entities.Odontologia.ConfigurarDiagnosticoProcedimOtraEntity>(delete);
            }
        }

        private async void delete(Cnt.Panacea.Entities.Odontologia.ConfigurarDiagnosticoProcedimOtraEntity elemento)
        {
            if (elemento != null)
            {
                BusyBox.UserControlCargando(true, "Eliminando...");
                var entidad = Convertir_Observables.ConvertirEntidades(elemento, new ConfigurarDiagnosticoProcedimOtraEntity());

                entidad.nombreTabla = "configurardiagnosticoprocedimotraentity";
                entidad.PartitionKey = "cnt.panacea.entities.odontologia.configurardiagnosticoprocedimotraentity";
                entidad.Identificador = elemento.Identificador;
                entidad.RowKey = elemento.Identificador.ToString();
                entidad.Activo = false;

                await entidad.postBlob();
                Listado.Remove(elemento);
                BusyBox.UserControlCargando(false);
            }
        }

        private void nuevo()
        {
            Identificador = 0;
            diagnosticoProcedimiento = null;
            _diagnosticoOdontologiaCnt = null;

            diagnosticoProcedimiento = new ConfigurarDiagnosticoProcedimOtraEntity();
            _diagnosticoOdontologiaCnt = new Cnt.Panacea.Entities.Odontologia.ConfigurarDiagnosticoProcedimOtraEntity();
            
            Letra_Simbolo = "";
            Descripcion = "";
            tipoSeleccionado = null;
            aplicaSeleccionado = null;
            tipoFuenteSeleccionado = null;
            tipoElementoSeleccionado = null;

            RaisePropertyChanged("Letra_Simbolo");
            RaisePropertyChanged("Descripcion");
            RaisePropertyChanged("TipoSeleccionado");
            RaisePropertyChanged("AplicaSeleccionado");
            RaisePropertyChanged("TipoFuenteSeleccionado");
            RaisePropertyChanged("TipoElementoSeleccionado");
            RaisePropertyChanged("DiagnosticoProcedimiento");
            RaisePropertyChanged("diagnosticoOdontologiaCnt");
        }

        public async void listarElementos()
        {
            BusyBox.UserControlCargando(true, "Cargando...");
            List<DiagnosticoProcedimiento> blob = await new DiagnosticoProcedimiento().getBlobByPartition("configurardiagnosticoprocedimotraentity", "cnt.panacea.entities.odontologia.configurardiagnosticoprocedimotraentity");
            var resultado = blob.Where(a => (a.Activo == null || a.Activo == true));
            Listado = Convertir_Observables.ConvertirIEnumerable(resultado, new List<Cnt.Panacea.Entities.Odontologia.ConfigurarDiagnosticoProcedimOtraEntity>()).ToObservableCollection();
            RaisePropertyChanged("Listado");
            BusyBox.UserControlCargando(false);
        }

        private void descripcion(string obj)
        {
            DiagnosticoProcedimiento.Descripcion = obj;
            diagnosticoOdontologiaCnt.Descripcion = obj;

            forceUiDiagnosticoProcedimiento();
        }

        private async void guardar()
        {
            BusyBox.UserControlCargando(true, "Guardando...");
            DiagnosticoProcedimiento.nombreTabla = "configurardiagnosticoprocedimotraentity";
            DiagnosticoProcedimiento.PartitionKey = "cnt.panacea.entities.odontologia.configurardiagnosticoprocedimotraentity";

            // Modo Insercion
            if (Identificador == 0)
            {
                DiagnosticoProcedimiento.Activo = true;
                DiagnosticoProcedimiento.RowKey = new Random().Next().ToString();
                DiagnosticoProcedimiento.Identificador = Convert.ToInt32(DiagnosticoProcedimiento.RowKey);
                Listado.Add(diagnosticoOdontologiaCnt);
                await DiagnosticoProcedimiento.postBlob();
            }
            else
            {
                var item = Listado.FirstOrDefault(a => a.Identificador == diagnosticoOdontologiaCnt.Identificador);

                if (item != null)
                {                    
                    item = Convertir_Observables.ConvertirEntidadesComptabilidad(DiagnosticoProcedimiento, new Cnt.Panacea.Entities.Odontologia.ConfigurarDiagnosticoProcedimOtraEntity());
                    item.Identificador = Identificador;

                    DiagnosticoProcedimiento.Identificador = Identificador;
                    DiagnosticoProcedimiento.RowKey = Identificador.ToString();

                    await DiagnosticoProcedimiento.postBlob();

                    var index = Listado.IndexOf(item);
                    Listado.Remove(item);
                    Listado.Insert(index,item);

                    nuevo();
                }
            }

            BusyBox.UserControlCargando(false);
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
                diagnosticoOdontologiaCnt.Letra = obj;
                diagnosticoOdontologiaCnt.Simbolo = obj;
            }

            forceUiDiagnosticoProcedimiento();
        }

        public void forceUiDiagnosticoProcedimiento()
        {
            //Al final estoy hay que cambiarlo xq solo debe quedar la implementacion de hefesoft
            var elemento = Convertir_Observables.ConvertirEntidadesComptabilidad(DiagnosticoProcedimiento, new Cnt.Panacea.Entities.Odontologia.ConfigurarDiagnosticoProcedimOtraEntity());
            elemento.Identificador = diagnosticoOdontologiaCnt.Identificador;
            diagnosticoOdontologiaCnt = elemento;
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

        

        private Cnt.Panacea.Entities.Odontologia.ConfigurarDiagnosticoProcedimOtraEntity _diagnosticoOdontologiaCnt;

        public Cnt.Panacea.Entities.Odontologia.ConfigurarDiagnosticoProcedimOtraEntity diagnosticoOdontologiaCnt
        {
            get { return _diagnosticoOdontologiaCnt; }
            set 
            {
                _diagnosticoOdontologiaCnt = value;
                RaisePropertyChanged("diagnosticoOdontologiaCnt");                
                DiagnosticoProcedimiento = Convertir_Observables.ConvertirEntidadesComptabilidad(value, new ConfigurarDiagnosticoProcedimOtraEntity());
                Identificador = value.Identificador;                

                fijarElementosModoEdicion();
            }
        }

        private void fijarElementosModoEdicion()
        {
            if (!string.IsNullOrEmpty(DiagnosticoProcedimiento.Letra))
            {
                Letra_Simbolo = DiagnosticoProcedimiento.Letra;
                tipoElementoSeleccionado = TipoElemento.FirstOrDefault(a => a.Codigo == 2);
            }
            else if (!string.IsNullOrEmpty(DiagnosticoProcedimiento.Simbolo))
            {
                Letra_Simbolo = DiagnosticoProcedimiento.Simbolo;
                tipoElementoSeleccionado = TipoElemento.FirstOrDefault(a => a.Codigo == 2);
            }
            else
            {
                tipoElementoSeleccionado = TipoElemento.FirstOrDefault(a => a.Codigo == 1);
            }
            
            if (!string.IsNullOrEmpty(DiagnosticoProcedimiento.Descripcion))
            {
                Descripcion = DiagnosticoProcedimiento.Descripcion;
            }

            if(DiagnosticoProcedimiento.TipoPanel == TipoPanel.Diagnostico)
            {
                tipoSeleccionado = Tipos.FirstOrDefault(a => a.Codigo == 1);
            }
            else if (DiagnosticoProcedimiento.TipoPanel == TipoPanel.Procedimiento)
            {
                tipoSeleccionado = Tipos.FirstOrDefault(a => a.Codigo == 2);
            }

            if(DiagnosticoProcedimiento.AplicaTratamiento == 1)
            {
                aplicaSeleccionado = Aplica_A.FirstOrDefault(a => a.Codigo == 1);
            }
            else if (DiagnosticoProcedimiento.AplicaTratamiento == 2)
            {
                aplicaSeleccionado = Aplica_A.FirstOrDefault(a => a.Codigo == 2);
            }
            else if (DiagnosticoProcedimiento.AplicaTratamiento == 3)
            {
                aplicaSeleccionado = Aplica_A.FirstOrDefault(a => a.Codigo == 2);
            }

            tipoFuenteSeleccionado = TipoFuente.FirstOrDefault(a => a.Descripcion == DiagnosticoProcedimiento.Fuente);

            RaisePropertyChanged("Letra_Simbolo");
            RaisePropertyChanged("Descripcion");
            RaisePropertyChanged("TipoSeleccionado");
            RaisePropertyChanged("AplicaSeleccionado");
            RaisePropertyChanged("TipoFuenteSeleccionado");
            RaisePropertyChanged("TipoElementoSeleccionado");
        }

        public string Letra_Simbolo { get; set; }
        public string Descripcion { get; set; }


        public ObservableCollection<Cnt.Panacea.Entities.Odontologia.ConfigurarDiagnosticoProcedimOtraEntity> Listado { get; set; }


        public RelayCommand nuevoCommand { get; set; }

        public int Identificador { get; set; }

        public RelayCommand<Cnt.Panacea.Entities.Odontologia.ConfigurarDiagnosticoProcedimOtraEntity> deleteCommand { get; set; }
    }
}
