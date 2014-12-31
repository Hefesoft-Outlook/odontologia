using System;
using System.Net;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using Cnt.Panacea.Entities.Odontologia;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Cnt.Panacea.Xap.Odontologia.Util.Messenger;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using Cnt.Panacea.Entities.Parametrizacion;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using Cnt.Panacea.Xap.Odontologia.Vm.Extensiones.Clases;
using Cnt.Panacea.Xap.Odontologia.Vm.Util;
using Microsoft.Practices.ServiceLocation;
using Hefesoft.Odontograma.Elastic.Util.Extenciones;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Odontograma
{
    public class DiagnosticoProcedimiento : ViewModelBase
    {
        public DiagnosticoProcedimiento()
        {
            lst = new Lista_Hefesoft<DiagnosticoProcedimiento_Extend>();
            lst.OnAdd += (m, n) =>
            {
                var elemento = (DiagnosticoProcedimiento_Extend)m;
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<DiagnosticoProcedimiento_Extend>(elemento, "Diagnostico procedimiento agregado");
            };

            lst.OnRemove += (m, n) =>
            {
                var elemento = (DiagnosticoProcedimiento_Extend)m;

                if (elemento.Superficie == "Superficie1")
                {
                    Superficie1.Remove(elemento.ConfigurarDiagnosticoProcedimOtraEntity);
                }
                else if (elemento.Superficie == "Superficie2")
                {
                    Superficie2.Remove(elemento.ConfigurarDiagnosticoProcedimOtraEntity);
                }
                else if (elemento.Superficie == "Superficie3")
                {
                    Superficie3.Remove(elemento.ConfigurarDiagnosticoProcedimOtraEntity);
                }
                else if (elemento.Superficie == "Superficie4")
                {
                    Superficie4.Remove(elemento.ConfigurarDiagnosticoProcedimOtraEntity);
                }
                else if (elemento.Superficie == "Superficie5")
                {
                    Superficie5.Remove(elemento.ConfigurarDiagnosticoProcedimOtraEntity);
                }
                else if (elemento.Superficie == "Superficie6")
                {
                    Superficie6.Remove(elemento.ConfigurarDiagnosticoProcedimOtraEntity);
                }
                else if (elemento.Superficie == "Superficie7")
                {
                    Superficie7.Remove(elemento.ConfigurarDiagnosticoProcedimOtraEntity);
                }
                else if (elemento.Superficie == "Superficie8")
                {
                    Superficie8.Remove(elemento.ConfigurarDiagnosticoProcedimOtraEntity);
                }
            }; 

            this.habilitarSuperficies();

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Pedir_Pintar_Datos>(this, elemento => 
            {
                cambiarRealizadosFalso(elemento);
                if (elemento.Limpiar_Datos)
                {
                    limpiarSuperficies(elemento);
                }
            });
        }

        private void cambiarRealizadosFalso(Pedir_Pintar_Datos elemento)
        {
            // Si se pide un nuevo odontograma 
            // Hay que devolver cada superficie al estado original
            if (elemento.nuevoOdontograma)
            {
                Realizado_Boca = false;
                Realizado_Pieza_Completa = false;
                Realizado_Superficie1 = false;
                Realizado_Superficie2 = false;
                Realizado_Superficie3 = false;
                Realizado_Superficie4 = false;
                Realizado_Superficie5 = false;
                Realizado_Superficie6 = false;
                Realizado_Superficie7 = false;
                Realizado_Superficie8 = false;
            }
        }

        private void limpiarSuperficies(Pedir_Pintar_Datos elemento)
        {
            if(Superficie1.Any())
            {
                Superficie1.Clear();
            }
            if (Superficie2.Any())
            {
                Superficie2.Clear();
            }
            if (Superficie3.Any())
            {
                Superficie3.Clear();
            }
            if (Superficie4.Any())
            {
                Superficie4.Clear();
            }
            if (Superficie5.Any())
            {
                Superficie5.Clear();
            }
            if (Superficie6.Any())
            {
                Superficie6.Clear();
            }
            if (Superficie7.Any())
            {
                Superficie7.Clear();
            }
            if (Superficie8.Any())
            {
                Superficie8.Clear();
            }
            if (PiezaCompleta.Any())
            {
                PiezaCompleta.Clear();
            }
            if (Boca.Any())
            {
                Boca.Clear();
            }

            if (lst.Any())
            {
                lst.Clear();
            }

        }

        private ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> superficie1 = new ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>();

        public ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> Superficie1
        {
            get { return superficie1; }
            set { superficie1 = value; RaisePropertyChanged("Superficie1"); }
        }

        private ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> superficie2 = new ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>();

        public ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> Superficie2
        {
            get { return superficie2; }
            set { superficie2 = value; RaisePropertyChanged("Superficie2"); }
        }

        private ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> superficie3 = new ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>();

        public ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> Superficie3
        {
            get { return superficie3; }
            set { superficie3 = value; RaisePropertyChanged("Superficie3"); }
        }

        private ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> superficie4 = new ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>();

        public ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> Superficie4
        {
            get { return superficie4; }
            set { superficie4 = value; RaisePropertyChanged("Superficie4"); }
        }

        private ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> superficie5 = new ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>();

        public ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> Superficie5
        {
            get { return superficie5; }
            set { superficie5 = value; RaisePropertyChanged("Superficie5"); }
        }

        private ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> superficie6 = new ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>();

        public ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> Superficie6
        {
            get { return superficie6; }
            set { superficie6 = value; RaisePropertyChanged("Superficie6"); }
        }

        private ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> superficie7 = new ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>();

        public ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> Superficie7
        {
            get { return superficie7; }
            set { superficie7 = value; RaisePropertyChanged("Superficie7"); }
        }

        private ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> superficie8 = new ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>();

        public ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> Superficie8
        {
            get { return superficie8; }
            set { superficie8 = value; RaisePropertyChanged("Superficie8"); }
        }

        private ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> piezaCompleta = new ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>();

        public ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> PiezaCompleta
        {
            get { return piezaCompleta; }
            set { piezaCompleta = value; RaisePropertyChanged("PiezaCompleta"); }
        }

        private ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> boca = new ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>();

        public ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> Boca
        {
            get { return boca; }
            set { boca = value; RaisePropertyChanged("Boca"); }
        }

        //listado Diagnosticos procedimientos guardados
        public Lista_Hefesoft<DiagnosticoProcedimiento_Extend> lst;

        #region OdontogramaInicial
        internal DiagnosticoProcedimiento_Validaciones agregarOdontogramaInicial(ConfigurarDiagnosticoProcedimOtraEntity item, Cambiar_Tipo_Odontograma Tipo_Odontograma_Actual, string SuperficieSeleccionada, NivelSeveridadDXEntity Nivel,int CodigoPiezaDental) 
        {
            var Estado = validaEstado(item, Tipo_Odontograma_Actual, SuperficieSeleccionada, Nivel, CodigoPiezaDental);
            if (Estado == DiagnosticoProcedimiento_Validaciones.No_Existe_En_La_Lista)
            {
                pintarDiagnosticos(item, SuperficieSeleccionada);
            }
            return Estado;
        }

        public void pintarDiagnosticos(ConfigurarDiagnosticoProcedimOtraEntity item, string SuperficieSeleccionada)
        {
            if (SuperficieSeleccionada == "Superficie1")
            {
                Superficie1.Add(item);
            }
            else if (SuperficieSeleccionada == "Superficie2")
            {
                Superficie2.Add(item);
            }
            else if (SuperficieSeleccionada == "Superficie3")
            {
                Superficie3.Add(item);
            }
            else if (SuperficieSeleccionada == "Superficie4")
            {
                Superficie4.Add(item);
            }
            else if (SuperficieSeleccionada == "Superficie5")
            {
                Superficie5.Add(item);
            }
            else if (SuperficieSeleccionada == "Superficie6")
            {
                Superficie6.Add(item);
            }
            else if (SuperficieSeleccionada == "Superficie7")
            {
                Superficie7.Add(item);
            }
            else if (SuperficieSeleccionada == "Boca")
            {
                //Se busca el viewmodel de la boca y a ese se le agrega el diagnostico o procedimiento
                //Esto para cuando se da click en un diente y no en la boca                
                var vmBoca = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Boca.Boca>();
                vmBoca.Elemento.DiagnosticoProcedimiento.Boca.Add(item);
            }
            else if (SuperficieSeleccionada == "Pieza_Completa")
            {
                PiezaCompleta.Add(item);
            }
        }

        private DiagnosticoProcedimiento_Validaciones validaEstado(ConfigurarDiagnosticoProcedimOtraEntity item, Cambiar_Tipo_Odontograma Tipo_Odontograma_Actual, string SuperficieSeleccionada, NivelSeveridadDXEntity Nivel, int codigoPiezaDental)
        {
            if (lst.Any(a => a.Superficie == SuperficieSeleccionada || a.Superficie == "Pieza_Completa"))
            {
                if (lst.Any(a => a.ConfigurarDiagnosticoProcedimOtraEntity == item && a.Superficie == SuperficieSeleccionada))
                {
                    return DiagnosticoProcedimiento_Validaciones.Existe;
                }
                else
                {
                    return DiagnosticoProcedimiento_Validaciones.Ya_posee_elementos;
                }
            }            
            else
            {
                // Nivel de severidad
                if (Nivel == null)
                {
                    lst.Add(new DiagnosticoProcedimiento_Extend() { Superficie = SuperficieSeleccionada, Tipo_Odontograma_Actual = Tipo_Odontograma_Actual.Tipo_Odontograma, ConfigurarDiagnosticoProcedimOtraEntity = item, Codigo_Pieza_Dental = codigoPiezaDental });
                }
                else
                {
                    lst.Add(new DiagnosticoProcedimiento_Extend() { Superficie = SuperficieSeleccionada, Tipo_Odontograma_Actual = Tipo_Odontograma_Actual.Tipo_Odontograma, ConfigurarDiagnosticoProcedimOtraEntity = item, Nivel_Severidad = Nivel, Codigo_Pieza_Dental = codigoPiezaDental });
                }

                return DiagnosticoProcedimiento_Validaciones.No_Existe_En_La_Lista;
            }
        }
        #endregion

        #region Odontograma Plan tratamiento
        internal DiagnosticoProcedimiento_Validaciones agregarOdontogramaPlanTratamiento(ConfigurarDiagnosticoProcedimOtraEntity item, Cambiar_Tipo_Odontograma Tipo_Odontograma_Actual, string SuperficieSeleccionada, int CodigoPiezaDental)
        {
            if (SuperficieSeleccionada == "Superficie1")
            {
                //Valida que la superficie ya tengo un plan de tratamiento para mostrar el menu adicionaro sobreescribir superficie
                if (!lst.Any(a => a.Tipo_Odontograma_Actual == Tipo_Odontograma.Plan_Tratamiento && (a.Superficie == SuperficieSeleccionada || SuperficieSeleccionada == "Pieza_Completa")))
                {
                    
                    Superficie1.Clear();
                    lst.Add(new DiagnosticoProcedimiento_Extend() { Superficie = SuperficieSeleccionada, Tipo_Odontograma_Actual = Tipo_Odontograma_Actual.Tipo_Odontograma, ConfigurarDiagnosticoProcedimOtraEntity = item, Codigo_Pieza_Dental = CodigoPiezaDental });
                    Superficie1.Add(item);
                    return DiagnosticoProcedimiento_Validaciones.Agregado;
                }
                else
                {
                    return DiagnosticoProcedimiento_Validaciones.Ya_posee_elementos;
                }
            }
            else if (SuperficieSeleccionada == "Superficie2")
            {
                if (!lst.Any(a => a.Tipo_Odontograma_Actual == Tipo_Odontograma.Plan_Tratamiento && (a.Superficie == SuperficieSeleccionada || SuperficieSeleccionada == "Pieza_Completa")))
                {
                    Superficie2.Clear();
                    lst.Add(new DiagnosticoProcedimiento_Extend() { Superficie = SuperficieSeleccionada, Tipo_Odontograma_Actual = Tipo_Odontograma_Actual.Tipo_Odontograma, ConfigurarDiagnosticoProcedimOtraEntity = item, Codigo_Pieza_Dental = CodigoPiezaDental });
                    Superficie2.Add(item);
                    return DiagnosticoProcedimiento_Validaciones.Agregado;
                }                
                else
                {
                    return DiagnosticoProcedimiento_Validaciones.Ya_posee_elementos;
                }
            }
            else if (SuperficieSeleccionada == "Superficie3")
            {
                if (!lst.Any(a => a.Tipo_Odontograma_Actual == Tipo_Odontograma.Plan_Tratamiento && (a.Superficie == SuperficieSeleccionada || SuperficieSeleccionada == "Pieza_Completa")))
                {
                    Superficie3.Clear();
                    lst.Add(new DiagnosticoProcedimiento_Extend() { Superficie = SuperficieSeleccionada, Tipo_Odontograma_Actual = Tipo_Odontograma_Actual.Tipo_Odontograma, ConfigurarDiagnosticoProcedimOtraEntity = item, Codigo_Pieza_Dental = CodigoPiezaDental });
                    Superficie3.Add(item);
                    return DiagnosticoProcedimiento_Validaciones.Agregado;
                }
                else
                {
                    return DiagnosticoProcedimiento_Validaciones.Ya_posee_elementos;
                }
            }
            else if (SuperficieSeleccionada == "Superficie4")
            {
                if (!lst.Any(a => a.Tipo_Odontograma_Actual == Tipo_Odontograma.Plan_Tratamiento && (a.Superficie == SuperficieSeleccionada || SuperficieSeleccionada == "Pieza_Completa")))
                {
                    Superficie4.Clear();
                    lst.Add(new DiagnosticoProcedimiento_Extend() { Superficie = SuperficieSeleccionada, Tipo_Odontograma_Actual = Tipo_Odontograma_Actual.Tipo_Odontograma, ConfigurarDiagnosticoProcedimOtraEntity = item, Codigo_Pieza_Dental = CodigoPiezaDental });
                    Superficie4.Add(item);
                    return DiagnosticoProcedimiento_Validaciones.Agregado;
                }
                else
                {
                    return DiagnosticoProcedimiento_Validaciones.Ya_posee_elementos;
                }
            }
            else if (SuperficieSeleccionada == "Superficie5")
            {
                if (!lst.Any(a => a.Tipo_Odontograma_Actual == Tipo_Odontograma.Plan_Tratamiento && (a.Superficie == SuperficieSeleccionada || SuperficieSeleccionada == "Pieza_Completa")))
                {
                    Superficie5.Clear();
                    lst.Add(new DiagnosticoProcedimiento_Extend() { Superficie = SuperficieSeleccionada, Tipo_Odontograma_Actual = Tipo_Odontograma_Actual.Tipo_Odontograma, ConfigurarDiagnosticoProcedimOtraEntity = item, Codigo_Pieza_Dental = CodigoPiezaDental });
                    Superficie5.Add(item);
                    return DiagnosticoProcedimiento_Validaciones.Agregado;
                }
                else
                {
                    return DiagnosticoProcedimiento_Validaciones.Ya_posee_elementos;
                }
            }
            else if (SuperficieSeleccionada == "Superficie6")
            {
                if (!lst.Any(a => a.Tipo_Odontograma_Actual == Tipo_Odontograma.Plan_Tratamiento && (a.Superficie == SuperficieSeleccionada || SuperficieSeleccionada == "Pieza_Completa")))
                {
                    Superficie6.Clear();
                    lst.Add(new DiagnosticoProcedimiento_Extend() { Superficie = SuperficieSeleccionada, Tipo_Odontograma_Actual = Tipo_Odontograma_Actual.Tipo_Odontograma, ConfigurarDiagnosticoProcedimOtraEntity = item, Codigo_Pieza_Dental = CodigoPiezaDental });
                    Superficie6.Add(item);
                    return DiagnosticoProcedimiento_Validaciones.Agregado;
                }
                else
                {
                    return DiagnosticoProcedimiento_Validaciones.Ya_posee_elementos;
                }
            }
            else if (SuperficieSeleccionada == "Superficie7")
            {
                if (!lst.Any(a => a.Tipo_Odontograma_Actual == Tipo_Odontograma.Plan_Tratamiento && (a.Superficie == SuperficieSeleccionada || SuperficieSeleccionada == "Pieza_Completa")))
                {
                    Superficie7.Clear();
                    lst.Add(new DiagnosticoProcedimiento_Extend() { Superficie = SuperficieSeleccionada, Tipo_Odontograma_Actual = Tipo_Odontograma_Actual.Tipo_Odontograma, ConfigurarDiagnosticoProcedimOtraEntity = item, Codigo_Pieza_Dental = CodigoPiezaDental });
                    Superficie7.Add(item);
                    return DiagnosticoProcedimiento_Validaciones.Agregado;
                }
                else
                {
                    return DiagnosticoProcedimiento_Validaciones.Ya_posee_elementos;
                }
            }
            else if (SuperficieSeleccionada == "Boca")
            {
                //Se busca el viewmodel de la boca y a ese se le agrega el diagnostico o procedimiento
                //Esto para cuando se da click en un diente y no en la boca
                var vmBoca = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Boca.Boca>();
                var lstBoca = vmBoca.Elemento.DiagnosticoProcedimiento.lst;

                if (!lstBoca.Any(a => a.Tipo_Odontograma_Actual == Tipo_Odontograma.Plan_Tratamiento && a.Superficie == SuperficieSeleccionada))
                {
                    vmBoca.Elemento.DiagnosticoProcedimiento.Boca.Clear();
                    vmBoca.Elemento.DiagnosticoProcedimiento.lst.Add(new DiagnosticoProcedimiento_Extend() { Superficie = SuperficieSeleccionada, Tipo_Odontograma_Actual = Tipo_Odontograma_Actual.Tipo_Odontograma, ConfigurarDiagnosticoProcedimOtraEntity = item, Codigo_Pieza_Dental = 99 });
                    vmBoca.Elemento.DiagnosticoProcedimiento.Boca.Add(item);
                    return DiagnosticoProcedimiento_Validaciones.Agregado;
                }
                else
                {
                    return DiagnosticoProcedimiento_Validaciones.Ya_posee_elementos;
                }
            }
            else if (SuperficieSeleccionada == "Pieza_Completa")
            {
                if (!lst.Any(a => a.Tipo_Odontograma_Actual == Tipo_Odontograma.Plan_Tratamiento && a.Superficie == SuperficieSeleccionada))
                {
                    PiezaCompleta.Clear();
                    lst.Add(new DiagnosticoProcedimiento_Extend() { Superficie = SuperficieSeleccionada, Tipo_Odontograma_Actual = Tipo_Odontograma_Actual.Tipo_Odontograma, ConfigurarDiagnosticoProcedimOtraEntity = item, Codigo_Pieza_Dental = CodigoPiezaDental });
                    PiezaCompleta.Add(item);
                    return DiagnosticoProcedimiento_Validaciones.Agregado;
                }
                else
                {
                    return DiagnosticoProcedimiento_Validaciones.Ya_posee_elementos;
                }
            }

            return DiagnosticoProcedimiento_Validaciones.No_Opcion_Encontrada;
        }
        #endregion

        internal DiagnosticoProcedimiento_Validaciones desHacerOdontograma(ConfigurarDiagnosticoProcedimOtraEntity item, Cambiar_Tipo_Odontograma Tipo_Odontograma_Actual, string SuperficieSeleccionada)
        {
            SuperficieSeleccionada = lst.Last().Superficie;
            item = lst.Last().ConfigurarDiagnosticoProcedimOtraEntity;

            if (SuperficieSeleccionada == "Superficie1")
            {
                if (lst.Any(a => a.Superficie == SuperficieSeleccionada && a.Tipo_Odontograma_Actual == Tipo_Odontograma_Actual.Tipo_Odontograma && a.ConfigurarDiagnosticoProcedimOtraEntity == item))
                {
                    var elemento = lst.First(a => a.Superficie == SuperficieSeleccionada && a.Tipo_Odontograma_Actual == Tipo_Odontograma_Actual.Tipo_Odontograma && a.ConfigurarDiagnosticoProcedimOtraEntity == item);
                    superficie1.Remove(elemento.ConfigurarDiagnosticoProcedimOtraEntity);
                    lst.Remove(elemento);
                }
                else
                {
                    return DiagnosticoProcedimiento_Validaciones.No_Opcion_Encontrada;
                }
            }
            else if (SuperficieSeleccionada == "Superficie2")
            {
                if (lst.Any(a => a.Superficie == SuperficieSeleccionada && a.Tipo_Odontograma_Actual == Tipo_Odontograma_Actual.Tipo_Odontograma && a.ConfigurarDiagnosticoProcedimOtraEntity == item))
                {
                    var elemento = lst.First(a => a.Superficie == SuperficieSeleccionada && a.Tipo_Odontograma_Actual == Tipo_Odontograma_Actual.Tipo_Odontograma && a.ConfigurarDiagnosticoProcedimOtraEntity == item);
                    superficie2.Remove(elemento.ConfigurarDiagnosticoProcedimOtraEntity);
                    lst.Remove(elemento);
                }
                else
                {
                    return DiagnosticoProcedimiento_Validaciones.No_Opcion_Encontrada;
                }
            }
            else if (SuperficieSeleccionada == "Superficie3")
            {
                if (lst.Any(a => a.Superficie == SuperficieSeleccionada && a.Tipo_Odontograma_Actual == Tipo_Odontograma_Actual.Tipo_Odontograma && a.ConfigurarDiagnosticoProcedimOtraEntity == item))
                {
                    var elemento = lst.First(a => a.Superficie == SuperficieSeleccionada && a.Tipo_Odontograma_Actual == Tipo_Odontograma_Actual.Tipo_Odontograma && a.ConfigurarDiagnosticoProcedimOtraEntity == item);
                    superficie3.Remove(elemento.ConfigurarDiagnosticoProcedimOtraEntity);
                    lst.Remove(elemento);
                }
                else
                {
                    return DiagnosticoProcedimiento_Validaciones.No_Opcion_Encontrada;
                }
            }
            else if (SuperficieSeleccionada == "Superficie4")
            {
                if (lst.Any(a => a.Superficie == SuperficieSeleccionada && a.Tipo_Odontograma_Actual == Tipo_Odontograma_Actual.Tipo_Odontograma && a.ConfigurarDiagnosticoProcedimOtraEntity == item))
                {
                    var elemento = lst.First(a => a.Superficie == SuperficieSeleccionada && a.Tipo_Odontograma_Actual == Tipo_Odontograma_Actual.Tipo_Odontograma && a.ConfigurarDiagnosticoProcedimOtraEntity == item);
                    superficie4.Remove(elemento.ConfigurarDiagnosticoProcedimOtraEntity);
                    lst.Remove(elemento);
                }
                else
                {
                    return DiagnosticoProcedimiento_Validaciones.No_Opcion_Encontrada;
                }
            }
            else if (SuperficieSeleccionada == "Superficie5")
            {
                if (lst.Any(a => a.Superficie == SuperficieSeleccionada && a.Tipo_Odontograma_Actual == Tipo_Odontograma_Actual.Tipo_Odontograma && a.ConfigurarDiagnosticoProcedimOtraEntity == item))
                {
                    var elemento = lst.First(a => a.Superficie == SuperficieSeleccionada && a.Tipo_Odontograma_Actual == Tipo_Odontograma_Actual.Tipo_Odontograma && a.ConfigurarDiagnosticoProcedimOtraEntity == item);
                    superficie5.Remove(elemento.ConfigurarDiagnosticoProcedimOtraEntity);
                    lst.Remove(elemento);
                }
                else
                {
                    return DiagnosticoProcedimiento_Validaciones.No_Opcion_Encontrada;
                }
            }
            else if (SuperficieSeleccionada == "Superficie6")
            {
                if (lst.Any(a => a.Superficie == SuperficieSeleccionada && a.Tipo_Odontograma_Actual == Tipo_Odontograma_Actual.Tipo_Odontograma && a.ConfigurarDiagnosticoProcedimOtraEntity == item))
                {
                    var elemento = lst.First(a => a.Superficie == SuperficieSeleccionada && a.Tipo_Odontograma_Actual == Tipo_Odontograma_Actual.Tipo_Odontograma && a.ConfigurarDiagnosticoProcedimOtraEntity == item);
                    superficie6.Remove(elemento.ConfigurarDiagnosticoProcedimOtraEntity);
                    lst.Remove(elemento);
                }
                else
                {
                    return DiagnosticoProcedimiento_Validaciones.No_Opcion_Encontrada;
                }
            }
            else if (SuperficieSeleccionada == "Superficie7")
            {
                if (lst.Any(a => a.Superficie == SuperficieSeleccionada && a.Tipo_Odontograma_Actual == Tipo_Odontograma_Actual.Tipo_Odontograma && a.ConfigurarDiagnosticoProcedimOtraEntity == item))
                {
                    var elemento = lst.First(a => a.Superficie == SuperficieSeleccionada && a.Tipo_Odontograma_Actual == Tipo_Odontograma_Actual.Tipo_Odontograma && a.ConfigurarDiagnosticoProcedimOtraEntity == item);
                    superficie7.Remove(elemento.ConfigurarDiagnosticoProcedimOtraEntity);
                    lst.Remove(elemento);
                }
                else
                {
                    return DiagnosticoProcedimiento_Validaciones.No_Opcion_Encontrada;
                }
            }
            else if (SuperficieSeleccionada == "Boca")
            {
                var vmBoca = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Boca.Boca>();
                var lstBoca = vmBoca.Elemento.DiagnosticoProcedimiento.lst;

                if (lstBoca.Any(a => a.Superficie == SuperficieSeleccionada && a.Tipo_Odontograma_Actual == Tipo_Odontograma_Actual.Tipo_Odontograma && a.ConfigurarDiagnosticoProcedimOtraEntity == item))
                {
                    var elemento = vmBoca.Elemento.DiagnosticoProcedimiento.lst.First(a => a.Superficie == SuperficieSeleccionada && a.Tipo_Odontograma_Actual == Tipo_Odontograma_Actual.Tipo_Odontograma && a.ConfigurarDiagnosticoProcedimOtraEntity == item);
                    vmBoca.Elemento.DiagnosticoProcedimiento.Boca.Remove(elemento.ConfigurarDiagnosticoProcedimOtraEntity);
                    vmBoca.Elemento.DiagnosticoProcedimiento.lst.Remove(elemento);
                }
                else
                {
                    return DiagnosticoProcedimiento_Validaciones.No_Opcion_Encontrada;
                }
            }
            else if (SuperficieSeleccionada == "Pieza_Completa")
            {
                if (lst.Any(a => a.Superficie == SuperficieSeleccionada && a.Tipo_Odontograma_Actual == Tipo_Odontograma_Actual.Tipo_Odontograma && a.ConfigurarDiagnosticoProcedimOtraEntity == item))
                {
                    var elemento = lst.First(a => a.Superficie == SuperficieSeleccionada && a.Tipo_Odontograma_Actual == Tipo_Odontograma_Actual.Tipo_Odontograma && a.ConfigurarDiagnosticoProcedimOtraEntity == item);
                    PiezaCompleta.Remove(elemento.ConfigurarDiagnosticoProcedimOtraEntity);
                    lst.Remove(elemento);
                }
                else
                {
                    return DiagnosticoProcedimiento_Validaciones.No_Opcion_Encontrada;
                }
            }

            return DiagnosticoProcedimiento_Validaciones.No_Opcion_Encontrada;
        }

        internal void adicionar(ConfigurarDiagnosticoProcedimOtraEntity item, Cambiar_Tipo_Odontograma Tipo_Odontograma_Actual, string SuperficieSeleccionada)
        {
            if (SuperficieSeleccionada == "Superficie1")
            {
                lst.Add(new DiagnosticoProcedimiento_Extend() { Superficie = SuperficieSeleccionada, Tipo_Odontograma_Actual = Tipo_Odontograma_Actual.Tipo_Odontograma, ConfigurarDiagnosticoProcedimOtraEntity = item });
                Superficie1.Add(item);
            }
            else if (SuperficieSeleccionada == "Superficie2")
            {
                lst.Add(new DiagnosticoProcedimiento_Extend() { Superficie = SuperficieSeleccionada, Tipo_Odontograma_Actual = Tipo_Odontograma_Actual.Tipo_Odontograma, ConfigurarDiagnosticoProcedimOtraEntity = item });
                Superficie2.Add(item);
            }
            else if (SuperficieSeleccionada == "Superficie3")
            {
                lst.Add(new DiagnosticoProcedimiento_Extend() { Superficie = SuperficieSeleccionada, Tipo_Odontograma_Actual = Tipo_Odontograma_Actual.Tipo_Odontograma, ConfigurarDiagnosticoProcedimOtraEntity = item });
                Superficie3.Add(item);
            }
            else if (SuperficieSeleccionada == "Superficie4")
            {
                lst.Add(new DiagnosticoProcedimiento_Extend() { Superficie = SuperficieSeleccionada, Tipo_Odontograma_Actual = Tipo_Odontograma_Actual.Tipo_Odontograma, ConfigurarDiagnosticoProcedimOtraEntity = item });
                Superficie4.Add(item);
            }
            else if (SuperficieSeleccionada == "Superficie5")
            {
                lst.Add(new DiagnosticoProcedimiento_Extend() { Superficie = SuperficieSeleccionada, Tipo_Odontograma_Actual = Tipo_Odontograma_Actual.Tipo_Odontograma, ConfigurarDiagnosticoProcedimOtraEntity = item });
                Superficie5.Add(item);
            }
            else if (SuperficieSeleccionada == "Superficie6")
            {
                lst.Add(new DiagnosticoProcedimiento_Extend() { Superficie = SuperficieSeleccionada, Tipo_Odontograma_Actual = Tipo_Odontograma_Actual.Tipo_Odontograma, ConfigurarDiagnosticoProcedimOtraEntity = item });
                Superficie6.Add(item);
            }
            else if (SuperficieSeleccionada == "Superficie7")
            {
                lst.Add(new DiagnosticoProcedimiento_Extend() { Superficie = SuperficieSeleccionada, Tipo_Odontograma_Actual = Tipo_Odontograma_Actual.Tipo_Odontograma, ConfigurarDiagnosticoProcedimOtraEntity = item });
                Superficie7.Add(item);
            }
            else if (SuperficieSeleccionada == "Boca")
            {
                var vmBoca = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Boca.Boca>();                
                vmBoca.Elemento.DiagnosticoProcedimiento.lst.Add(new DiagnosticoProcedimiento_Extend() { Superficie = SuperficieSeleccionada, Tipo_Odontograma_Actual = Tipo_Odontograma_Actual.Tipo_Odontograma, ConfigurarDiagnosticoProcedimOtraEntity = item, Codigo_Pieza_Dental = 99 });
                vmBoca.Elemento.DiagnosticoProcedimiento.Boca.Add(item);
            }
            else if (SuperficieSeleccionada == "Pieza_Completa")
            {
                lst.Add(new DiagnosticoProcedimiento_Extend() { Superficie = SuperficieSeleccionada, Tipo_Odontograma_Actual = Tipo_Odontograma_Actual.Tipo_Odontograma, ConfigurarDiagnosticoProcedimOtraEntity = item });
                PiezaCompleta.Add(item);
            }
        }

        internal void sobreEscribir(ConfigurarDiagnosticoProcedimOtraEntity item, Cambiar_Tipo_Odontograma Tipo_Odontograma_Actual, string SuperficieSeleccionada)
        {
            var listadoEliminar = lst.Where(a => (a.Superficie == SuperficieSeleccionada || a.Superficie == "Pieza_Completa") && a.Tipo_Odontograma_Actual == Tipo_Odontograma_Actual.Tipo_Odontograma).ToList();

            if (listadoEliminar.Any())
            {
                // Se usa asi para evitar excepciones del tipo
                // Collection was modified; enumeration operation may not execute
                // Basicamente lo que se hace es recorrer la coleccion a la inversa
                for(int i = listadoEliminar.Count() - 1; i >= 0; i--) {
                    lst.RemoveAt(i);
                }
            }            

            lst.Add(new DiagnosticoProcedimiento_Extend() { Superficie = SuperficieSeleccionada, Tipo_Odontograma_Actual = Tipo_Odontograma_Actual.Tipo_Odontograma, ConfigurarDiagnosticoProcedimOtraEntity = item });
            
            if (SuperficieSeleccionada == "Superficie1")
            {   
                Superficie1.Clear();
                Superficie1.Add(item);
            }
            else if (SuperficieSeleccionada == "Superficie2")
            {            
                Superficie2.Clear();
                Superficie2.Add(item);
            }
            else if (SuperficieSeleccionada == "Superficie3")
            {             
                Superficie3.Clear();
                Superficie3.Add(item);
            }
            else if (SuperficieSeleccionada == "Superficie4")
            {             
                Superficie4.Clear();
                Superficie4.Add(item);
            }
            else if (SuperficieSeleccionada == "Superficie5")
            {             
                Superficie5.Clear();
                Superficie5.Add(item);
            }
            else if (SuperficieSeleccionada == "Superficie6")
            {             
                Superficie6.Clear();
                Superficie6.Add(item);
            }
            else if (SuperficieSeleccionada == "Superficie7")
            {
                Superficie7.Clear();
                Superficie7.Add(item);
            }
            else if (SuperficieSeleccionada == "Boca")
            {
                var vmBoca = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Boca.Boca>();
                vmBoca.Elemento.DiagnosticoProcedimiento.Boca.Clear();
                vmBoca.Elemento.DiagnosticoProcedimiento.Boca.Add(item);
            }
            else if (SuperficieSeleccionada == "Pieza_Completa")
            {             
                PiezaCompleta.Clear();
                PiezaCompleta.Add(item);
            }

            //Validar si hay que limpiar pieza completa
            if(listadoEliminar.Any(a=>a.Superficie == "Pieza_Completa"))
            {
                PiezaCompleta.Clear();
            }
        }

        #region Habilitados
        private bool habilitado_Superficie1 = true;

        public bool Habilitado_Superficie1
        {
            get { return habilitado_Superficie1; }
            set { habilitado_Superficie1 = value; RaisePropertyChanged("Habilitado_Superficie1"); }
        }

        private bool habilitado_Superficie2 = true;

        public bool Habilitado_Superficie2
        {
            get { return habilitado_Superficie2; }
            set { habilitado_Superficie2 = value; RaisePropertyChanged("Habilitado_Superficie2"); }
        }

        private bool habilitado_Superficie3 = true;

        public bool Habilitado_Superficie3
        {
            get { return habilitado_Superficie3; }
            set { habilitado_Superficie3 = value; RaisePropertyChanged("Habilitado_Superficie3"); }
        }

        private bool habilitado_Superficie4 = true;

        public bool Habilitado_Superficie4
        {
            get { return habilitado_Superficie4; }
            set { habilitado_Superficie4 = value; RaisePropertyChanged("Habilitado_Superficie4"); }
        }

        private bool habilitado_Superficie5 = true;

        public bool Habilitado_Superficie5
        {
            get { return habilitado_Superficie5; }
            set { habilitado_Superficie5 = value; RaisePropertyChanged("Habilitado_Superficie5"); }
        }

        private bool habilitado_Superficie6 = true;

        public bool Habilitado_Superficie6
        {
            get { return habilitado_Superficie6; }
            set { habilitado_Superficie6 = value; RaisePropertyChanged("Habilitado_Superficie6"); }
        }

        private bool habilitado_Superficie7 = true;

        public bool Habilitado_Superficie7
        {
            get { return habilitado_Superficie7; }
            set { habilitado_Superficie7 = value; RaisePropertyChanged("Habilitado_Superficie7"); }
        }

        private bool habilitado_Superficie8 = true;

        public bool Habilitado_Superficie8
        {
            get { return habilitado_Superficie8; }
            set { habilitado_Superficie8 = value; RaisePropertyChanged("Habilitado_Superficie8"); }
        }


        private bool habilitado_Boca;

        public bool Habilitado_Boca
        {
            get { return habilitado_Boca; }
            set { habilitado_Boca = value; RaisePropertyChanged("Habilitado_Boca"); }
        }


        private bool habilitado_Pieza_Completa;

        public bool Habilitado_Pieza_Completa
        {
            get { return habilitado_Pieza_Completa; }
            set { habilitado_Pieza_Completa = value; RaisePropertyChanged("Habilitado_Pieza_Completa"); }
        }
        #endregion

        #region realizado
        private bool realizado_Superficie1;

        public bool Realizado_Superficie1
        {
            get { return realizado_Superficie1; }
            set { realizado_Superficie1 = value; RaisePropertyChanged("Realizado_Superficie1"); }
        }

        private bool realizado_Superficie2;

        public bool Realizado_Superficie2
        {
            get { return realizado_Superficie2; }
            set { realizado_Superficie2 = value; RaisePropertyChanged("Realizado_Superficie2"); }
        }

        private bool realizado_Superficie3;

        public bool Realizado_Superficie3
        {
            get { return realizado_Superficie3; }
            set { realizado_Superficie3 = value; RaisePropertyChanged("Realizado_Superficie3"); }
        }

        private bool realizado_Superficie4;

        public bool Realizado_Superficie4
        {
            get { return realizado_Superficie4; }
            set { realizado_Superficie4 = value; RaisePropertyChanged("Realizado_Superficie4"); }
        }

        private bool realizado_Superficie5;

        public bool Realizado_Superficie5
        {
            get { return realizado_Superficie5; }
            set { realizado_Superficie5 = value; RaisePropertyChanged("Realizado_Superficie5"); }
        }

        private bool realizado_Superficie6;

        public bool Realizado_Superficie6
        {
            get { return realizado_Superficie6; }
            set { realizado_Superficie6 = value; RaisePropertyChanged("Realizado_Superficie6"); }
        }

        private bool realizado_Superficie7;

        public bool Realizado_Superficie7
        {
            get { return realizado_Superficie7; }
            set { realizado_Superficie7 = value; RaisePropertyChanged("Realizado_Superficie7"); }
        }

        private bool realizado_Superficie8;

        public bool Realizado_Superficie8
        {
            get { return realizado_Superficie8; }
            set { realizado_Superficie8 = value; RaisePropertyChanged("Realizado_Superficie8"); }
        }


        private bool realizado_Boca;

        public bool Realizado_Boca
        {
            get { return realizado_Boca; }
            set { realizado_Boca = value; RaisePropertyChanged("Realizado_Boca"); }
        }


        private bool realizado_Pieza_Completa;

        public bool Realizado_Pieza_Completa
        {
            get { return realizado_Pieza_Completa; }
            set { realizado_Pieza_Completa = value; RaisePropertyChanged("Realizado_Pieza_Completa"); }
        }
        #endregion
    }
}
