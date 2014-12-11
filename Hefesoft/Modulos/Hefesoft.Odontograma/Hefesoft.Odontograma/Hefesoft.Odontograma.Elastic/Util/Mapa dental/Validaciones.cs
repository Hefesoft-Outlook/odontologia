using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Extensiones.Clases;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Util.Mapa_dental
{
    public class Validaciones
    {
        // Valida que existan procedimiento para guardar del tipo actual del odontograma)
        public bool validarcadaDiagnosticoTieneProcedimientos(Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm Mapa)
        {
            var lista1 = Mapa.LstOdontogramaParte1.validaCadaSuperficieConDiagnosticoTieneProcedimiento();
            var lista2 = Mapa.LstOdontogramaParte2.validaCadaSuperficieConDiagnosticoTieneProcedimiento();
            var lista3 = Mapa.LstOdontogramaParte3.validaCadaSuperficieConDiagnosticoTieneProcedimiento();
            var lista4 = Mapa.LstOdontogramaParte4.validaCadaSuperficieConDiagnosticoTieneProcedimiento();
            var lista5 = Mapa.LstOdontogramaParte5.validaCadaSuperficieConDiagnosticoTieneProcedimiento();
            var lista6 = Mapa.LstOdontogramaParte6.validaCadaSuperficieConDiagnosticoTieneProcedimiento();
            var lista7 = Mapa.LstOdontogramaParte7.validaCadaSuperficieConDiagnosticoTieneProcedimiento();
            var lista8 = Mapa.LstOdontogramaParte8.validaCadaSuperficieConDiagnosticoTieneProcedimiento();

            // Devuelve el resultado de la validacion
            return (lista1 && lista2 && lista3 && lista4 && lista5 && lista6 && lista7 && lista8);
        }




        // Valida que existan procedimiento para guardar del tipo actual del odontograma)
        public bool validarTieneElementos(Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm Mapa, Tipo_Odontograma Tipo_Odontograma)
        {
            var lista1 = Mapa.LstOdontogramaParte1.Any(a => a.DiagnosticoProcedimiento.lst.Any(b => b.Tipo_Odontograma_Actual == Tipo_Odontograma));
            var lista2 = Mapa.LstOdontogramaParte2.Any(a => a.DiagnosticoProcedimiento.lst.Any(b => b.Tipo_Odontograma_Actual == Tipo_Odontograma));
            var lista3 = Mapa.LstOdontogramaParte3.Any(a => a.DiagnosticoProcedimiento.lst.Any(b => b.Tipo_Odontograma_Actual == Tipo_Odontograma));
            var lista4 = Mapa.LstOdontogramaParte4.Any(a => a.DiagnosticoProcedimiento.lst.Any(b => b.Tipo_Odontograma_Actual == Tipo_Odontograma));
            var lista5 = Mapa.LstOdontogramaParte5.Any(a => a.DiagnosticoProcedimiento.lst.Any(b => b.Tipo_Odontograma_Actual == Tipo_Odontograma));
            var lista6 = Mapa.LstOdontogramaParte6.Any(a => a.DiagnosticoProcedimiento.lst.Any(b => b.Tipo_Odontograma_Actual == Tipo_Odontograma));
            var lista7 = Mapa.LstOdontogramaParte7.Any(a => a.DiagnosticoProcedimiento.lst.Any(b => b.Tipo_Odontograma_Actual == Tipo_Odontograma));
            var lista8 = Mapa.LstOdontogramaParte8.Any(a => a.DiagnosticoProcedimiento.lst.Any(b => b.Tipo_Odontograma_Actual == Tipo_Odontograma));

            return (lista1 || lista2 || lista3 || lista4 || lista5 || lista6 || lista7 || lista8);
        }

        public List<Cnt.Panacea.Entities.Odontologia.OdontogramaEntity> convertirAEntidadesOdontogramaEntity(Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm Mapa, Tipo_Odontograma tipo_Odontograma)
        {
            List<Cnt.Panacea.Entities.Odontologia.OdontogramaEntity> lst = new List<Entities.Odontologia.OdontogramaEntity>();
            List<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> lstTemp = new List<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma>();

            var ls1 = Mapa.LstOdontogramaParte1.Where(a => a.DiagnosticoProcedimiento.lst.Any() && a.DiagnosticoProcedimiento.lst.Any(b => b.Tipo_Odontograma_Actual == tipo_Odontograma)).ToList();
            var ls2 = Mapa.LstOdontogramaParte2.Where(a => a.DiagnosticoProcedimiento.lst.Any() && a.DiagnosticoProcedimiento.lst.Any(b => b.Tipo_Odontograma_Actual == tipo_Odontograma)).ToList();
            var ls3 = Mapa.LstOdontogramaParte3.Where(a => a.DiagnosticoProcedimiento.lst.Any() && a.DiagnosticoProcedimiento.lst.Any(b => b.Tipo_Odontograma_Actual == tipo_Odontograma)).ToList();
            var ls4 = Mapa.LstOdontogramaParte4.Where(a => a.DiagnosticoProcedimiento.lst.Any() && a.DiagnosticoProcedimiento.lst.Any(b => b.Tipo_Odontograma_Actual == tipo_Odontograma)).ToList();
            var ls5 = Mapa.LstOdontogramaParte5.Where(a => a.DiagnosticoProcedimiento.lst.Any() && a.DiagnosticoProcedimiento.lst.Any(b => b.Tipo_Odontograma_Actual == tipo_Odontograma)).ToList();
            var ls6 = Mapa.LstOdontogramaParte6.Where(a => a.DiagnosticoProcedimiento.lst.Any() && a.DiagnosticoProcedimiento.lst.Any(b => b.Tipo_Odontograma_Actual == tipo_Odontograma)).ToList();
            var ls7 = Mapa.LstOdontogramaParte7.Where(a => a.DiagnosticoProcedimiento.lst.Any() && a.DiagnosticoProcedimiento.lst.Any(b => b.Tipo_Odontograma_Actual == tipo_Odontograma)).ToList();
            var ls8 = Mapa.LstOdontogramaParte8.Where(a => a.DiagnosticoProcedimiento.lst.Any() && a.DiagnosticoProcedimiento.lst.Any(b => b.Tipo_Odontograma_Actual == tipo_Odontograma)).ToList();

            lstTemp.AddRange(ls1);
            lstTemp.AddRange(ls2);
            lstTemp.AddRange(ls3);
            lstTemp.AddRange(ls4);
            lstTemp.AddRange(ls5);
            lstTemp.AddRange(ls6);
            lstTemp.AddRange(ls7);
            lstTemp.AddRange(ls8);

            lst = extraerProcedimientosDiagnosticos(lstTemp);

            return lst;
        }

        //Carga todos sin impportar el tipo de odontograma
        public List<Cnt.Panacea.Entities.Odontologia.OdontogramaEntity> convertirAEntidadesOdontogramaEntityTodos(Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm Mapa)
        {
            List<Cnt.Panacea.Entities.Odontologia.OdontogramaEntity> lst = new List<Entities.Odontologia.OdontogramaEntity>();
            List<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> lstTemp = new List<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma>();

            var ls1 = Mapa.LstOdontogramaParte1.Where(a => a.DiagnosticoProcedimiento.lst.Any()).ToList();
            var ls2 = Mapa.LstOdontogramaParte2.Where(a => a.DiagnosticoProcedimiento.lst.Any()).ToList();
            var ls3 = Mapa.LstOdontogramaParte3.Where(a => a.DiagnosticoProcedimiento.lst.Any()).ToList();
            var ls4 = Mapa.LstOdontogramaParte4.Where(a => a.DiagnosticoProcedimiento.lst.Any()).ToList();
            var ls5 = Mapa.LstOdontogramaParte5.Where(a => a.DiagnosticoProcedimiento.lst.Any()).ToList();
            var ls6 = Mapa.LstOdontogramaParte6.Where(a => a.DiagnosticoProcedimiento.lst.Any()).ToList();
            var ls7 = Mapa.LstOdontogramaParte7.Where(a => a.DiagnosticoProcedimiento.lst.Any()).ToList();
            var ls8 = Mapa.LstOdontogramaParte8.Where(a => a.DiagnosticoProcedimiento.lst.Any()).ToList();

            lstTemp.AddRange(ls1);
            lstTemp.AddRange(ls2);
            lstTemp.AddRange(ls3);
            lstTemp.AddRange(ls4);
            lstTemp.AddRange(ls5);
            lstTemp.AddRange(ls6);
            lstTemp.AddRange(ls7);
            lstTemp.AddRange(ls8);

            lst = extraerProcedimientosDiagnosticos(lstTemp);
            return lst;
        }

        private List<OdontogramaEntity> extraerProcedimientosDiagnosticos(List<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> lstTemp)
        {
            List<OdontogramaEntity> lst = new List<OdontogramaEntity>();
            listadoProcedimientos(lstTemp, lst);
            return lst;
        }

        // Obtiene y formatea los procedimientos en el odontograma
        private static void listadoProcedimientos(List<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> lstTemp, List<OdontogramaEntity> lst)
        {
            //Extraer procedimientos Y diagnosticos dela boca
            extraeBoca(ref lst);
            
            //Supernumerarios
            agregarSupernumerarios(lstTemp, ref lst);

            // Manejo dientes normales sacando los supernumerarios y la boca que ya se agregaron
            foreach (var item in lstTemp.Where(a => a.codigoPiezaDental != 100 && a.codigoPiezaDental != 99))
            {
                foreach (var itemB in item.DiagnosticoProcedimiento.lst)
                {
                    OdontogramaEntity Odontologia = null;

                    if (itemB.OdontogramaEntity != null)
                    {
                        Odontologia = itemB.OdontogramaEntity;
                    }
                    else
                    {
                        Odontologia = new OdontogramaEntity();
                    }                        
                        
                    Odontologia.Diente = new DientesEntity() { Identificador = item.codigoPiezaDental };
                    Odontologia.Superficie = itemB.Superficie.superficieNomenclatura();
                    extraerCaracteristicas(itemB, Odontologia);
                                        

                    if (itemB.ConfigurarDiagnosticoProcedimOtraEntity.TipoPanel == TipoPanel.Diagnostico)
                    {
                        Odontologia.Diagnostico.Identificador = Convert.ToInt32(itemB.ConfigurarDiagnosticoProcedimOtraEntity.Identificador);
                        Odontologia.Diagnostico.Diagnostico.Identificador = Convert.ToInt32(itemB.ConfigurarDiagnosticoProcedimOtraEntity.Diagnostico);
                        Odontologia.Procedimiento = null;
                        Odontologia.Diagnostico.Diagnostico.NombreAlterno = itemB.ConfigurarDiagnosticoProcedimOtraEntity.Descripcion;                        
                    }
                    else if (itemB.ConfigurarDiagnosticoProcedimOtraEntity.TipoPanel == TipoPanel.Procedimiento)
                    {
                        Odontologia.Procedimiento.Identificador = Convert.ToInt32(itemB.ConfigurarDiagnosticoProcedimOtraEntity.Identificador);
                        Odontologia.Procedimiento.Procedimiento.Identificador = Convert.ToInt32(itemB.ConfigurarDiagnosticoProcedimOtraEntity.Procedimiento);
                        Odontologia.Procedimiento.Procedimiento.NombreAlterno = itemB.ConfigurarDiagnosticoProcedimOtraEntity.Descripcion;
                        Odontologia.Diagnostico = null;
                    }

                    Odontologia.FechaRealizacion = DateTime.Now;
                    Odontologia.NivelSeveridad = new Entities.Parametrizacion.NivelSeveridadDXEntity();
                    Odontologia.NivelSeveridad = itemB.Nivel_Severidad;

                    Odontologia.Inicial = true;
                    Odontologia.IPOrigen = Variables_Globales.IpCliente;
                    Odontologia.IdIps = Variables_Globales.IdIps;
                    Odontologia.Usuario = Variables_Globales.UsuarioActual;
                    lst.Add(Odontologia);
                }
            }
        }

        private static void extraerCaracteristicas(DiagnosticoProcedimiento_Extend itemB, OdontogramaEntity Odontologia)
        {
            //Para la version de windows 8 si se deben guardar datos como color letra etc
            //En la version de cnt solo se guarda el codigo del diagnostico ye n wcf se cargan por un
            // foreach
            if (itemB.ConfigurarDiagnosticoProcedimOtraEntity.TipoPanel == TipoPanel.Diagnostico)
            {
                Odontologia.Diagnostico = Convertir_Observables.ConvertirEntidades(itemB.ConfigurarDiagnosticoProcedimOtraEntity, Odontologia.Diagnostico);
            }
            else if (itemB.ConfigurarDiagnosticoProcedimOtraEntity.TipoPanel == TipoPanel.Procedimiento)
            {             
                Odontologia.Procedimiento = Convertir_Observables.ConvertirEntidades(itemB.ConfigurarDiagnosticoProcedimOtraEntity, Odontologia.Procedimiento);
            }
        }

        private static void extraeBoca(ref List<OdontogramaEntity> lst)
        {
            var boca = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Boca.Boca>();

            foreach (var itemB in boca.Elemento.DiagnosticoProcedimiento.lst)
            {
                OdontogramaEntity Odontologia = new OdontogramaEntity();
                Odontologia.Diente = new DientesEntity() { Identificador = 99};
                Odontologia.Superficie = itemB.Superficie.superficieNomenclatura();
                extraerCaracteristicas(itemB, Odontologia);

                if (itemB.ConfigurarDiagnosticoProcedimOtraEntity.TipoPanel == TipoPanel.Diagnostico)
                {
                    Odontologia.Diagnostico.Identificador = Convert.ToInt32(itemB.ConfigurarDiagnosticoProcedimOtraEntity.Identificador);
                    Odontologia.Diagnostico.Diagnostico.Identificador = Convert.ToInt32(itemB.ConfigurarDiagnosticoProcedimOtraEntity.Diagnostico);
                    Odontologia.Procedimiento = null;
                    Odontologia.Diagnostico.Diagnostico.NombreAlterno = itemB.ConfigurarDiagnosticoProcedimOtraEntity.Descripcion;
                }
                else if (itemB.ConfigurarDiagnosticoProcedimOtraEntity.TipoPanel == TipoPanel.Procedimiento)
                {
                    Odontologia.Procedimiento.Identificador = Convert.ToInt32(itemB.ConfigurarDiagnosticoProcedimOtraEntity.Identificador);
                    Odontologia.Procedimiento.Procedimiento.Identificador = Convert.ToInt32(itemB.ConfigurarDiagnosticoProcedimOtraEntity.Procedimiento);
                    Odontologia.Procedimiento.Procedimiento.NombreAlterno = itemB.ConfigurarDiagnosticoProcedimOtraEntity.Descripcion;
                    Odontologia.Diagnostico = null;
                }

                Odontologia.FechaRealizacion = DateTime.Now;
                Odontologia.NivelSeveridad = new Entities.Parametrizacion.NivelSeveridadDXEntity();
                Odontologia.NivelSeveridad = itemB.Nivel_Severidad;

                Odontologia.Inicial = true;
                Odontologia.IPOrigen = Variables_Globales.IpCliente;
                Odontologia.IdIps = Variables_Globales.IdIps;
                Odontologia.Usuario = Variables_Globales.UsuarioActual;
                lst.Add(Odontologia);
            }
        }


        //Manejo para salvar un supernumerario en la bd
        private static List<OdontogramaEntity> agregarSupernumerarios(List<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> lstTemp, ref List<OdontogramaEntity> lst)
        {
            //El 100 es el codigo que por defecto le asigna el mapa dental al odontograma
            foreach (var pivote in lstTemp.Where(a => a.codigoPiezaDental == 100))
            {
                Cnt.Panacea.Entities.Odontologia.OdontogramaEntity OdontologiaSupernumerario = new OdontogramaEntity();
                OdontologiaSupernumerario.Diente = new DientesEntity() { Identificador = 98 };
                OdontologiaSupernumerario.Superficie = Superficie.SuperficieTotal;
                OdontologiaSupernumerario.FechaRealizacion = DateTime.Now;


                //Cuando se agrega el supernumerario como tal van en nulo
                OdontologiaSupernumerario.Diagnostico = null;
                OdontologiaSupernumerario.Procedimiento = null;

                if (pivote.Referencia != null)
                {
                    if (pivote.Referencia.Esta_A_la_Derecha)
                    {
                        OdontologiaSupernumerario.DienteReferencia1 = new DientesEntity() { Identificador = pivote.Referencia.Diente_Referencia };
                    }
                    else
                    {
                        OdontologiaSupernumerario.DienteReferencia2 = new DientesEntity() { Identificador = pivote.Referencia.Diente_Referencia };
                    }
                }


                OdontologiaSupernumerario.NivelSeveridad = null;
                OdontologiaSupernumerario.Inicial = true;
                OdontologiaSupernumerario.IPOrigen = Variables_Globales.IpCliente;
                OdontologiaSupernumerario.IdIps = Variables_Globales.IdIps;
                OdontologiaSupernumerario.Usuario = Variables_Globales.UsuarioActual;

                lst.Add(OdontologiaSupernumerario);
            }

            return lst;
        }

        /// <summary>
        /// Se incluye validacion para cuando sea un supernumerario no saca el diente
        /// Si no el que esta al lado izquierdo o derecho segun el caso
        /// </summary>
        /// <param name="Mapa"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma obtenerElementoEnElOdontograma(Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm Mapa, DiagnosticoProcedimiento_Extend item)
        {
            
            Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma elemento = new Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma("");

            //Validacion para el supernumerario
            //Cuando es supernumerario no se busca la referencia del diente si no la del que esta al lado
            // Y al final se le suma o se le resta uno dependiendo si es a la izquierda o a la derecha
            if (item.Es_Supernumerario)
            {
                item.Codigo_Pieza_Dental = item.Referencia_SuperNumerario.Diente_Referencia;
            }

            if (Mapa.LstOdontogramaParte1.Any(a => a.codigoPiezaDental == item.Codigo_Pieza_Dental))
            {
                elemento = validarSupernumerario(Mapa.LstOdontogramaParte1, item);
            }
            else if (Mapa.LstOdontogramaParte2.Any(a => a.codigoPiezaDental == item.Codigo_Pieza_Dental))
            {
                elemento = validarSupernumerario(Mapa.LstOdontogramaParte2, item);
            }
            else if (Mapa.LstOdontogramaParte3.Any(a => a.codigoPiezaDental == item.Codigo_Pieza_Dental))
            {
                elemento = validarSupernumerario(Mapa.LstOdontogramaParte3, item);
            }
            else if (Mapa.LstOdontogramaParte4.Any(a => a.codigoPiezaDental == item.Codigo_Pieza_Dental))
            {
                elemento = validarSupernumerario(Mapa.LstOdontogramaParte4, item);
            }
            else if (Mapa.LstOdontogramaParte5.Any(a => a.codigoPiezaDental == item.Codigo_Pieza_Dental))
            {
                elemento = validarSupernumerario(Mapa.LstOdontogramaParte5, item);
            }
            else if (Mapa.LstOdontogramaParte6.Any(a => a.codigoPiezaDental == item.Codigo_Pieza_Dental))
            {
                elemento = validarSupernumerario(Mapa.LstOdontogramaParte6, item);
            }
            else if (Mapa.LstOdontogramaParte7.Any(a => a.codigoPiezaDental == item.Codigo_Pieza_Dental))
            {
                elemento = validarSupernumerario(Mapa.LstOdontogramaParte7, item);
            }
            else if (Mapa.LstOdontogramaParte8.Any(a => a.codigoPiezaDental == item.Codigo_Pieza_Dental))
            {
                elemento = validarSupernumerario(Mapa.LstOdontogramaParte8, item);
            }

            if (elemento != null && item.Es_Supernumerario)
            {

            }

            return elemento;

        }

        private Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma validarSupernumerario(ObservableCollection<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> listado, DiagnosticoProcedimiento_Extend item)
        {
            Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma elemento = null;
            if (item.Es_Supernumerario)
            {
                elemento = listado.First(a => a.codigoPiezaDental == item.Codigo_Pieza_Dental);
                var indexElemento = listado.IndexOf(elemento);

                if (item.Referencia_SuperNumerario.Esta_A_la_Derecha)
                {
                    elemento = listado.ElementAt(indexElemento + 1);
                }
                else
                {
                    elemento = listado.ElementAt(indexElemento - 1);
                }
            }
            else
            {
                elemento = listado.First(a => a.codigoPiezaDental == item.Codigo_Pieza_Dental);
            }
            return elemento;
        }



        public Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma obtenerElementoEnElOdontogramaCodigoDiente(Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm Mapa, int Codigo_Pieza_Dental)
        {
            Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma elemento = new Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma("");

            if (Mapa.LstOdontogramaParte1.Any(a => a.codigoPiezaDental == Codigo_Pieza_Dental))
            {
                elemento = Mapa.LstOdontogramaParte1.First(a => a.codigoPiezaDental == Codigo_Pieza_Dental);
            }
            else if (Mapa.LstOdontogramaParte2.Any(a => a.codigoPiezaDental == Codigo_Pieza_Dental))
            {
                elemento = Mapa.LstOdontogramaParte2.First(a => a.codigoPiezaDental == Codigo_Pieza_Dental);
            }
            else if (Mapa.LstOdontogramaParte3.Any(a => a.codigoPiezaDental == Codigo_Pieza_Dental))
            {
                elemento = Mapa.LstOdontogramaParte3.First(a => a.codigoPiezaDental == Codigo_Pieza_Dental);
            }
            else if (Mapa.LstOdontogramaParte4.Any(a => a.codigoPiezaDental == Codigo_Pieza_Dental))
            {
                elemento = Mapa.LstOdontogramaParte4.First(a => a.codigoPiezaDental == Codigo_Pieza_Dental);
            }
            else if (Mapa.LstOdontogramaParte5.Any(a => a.codigoPiezaDental == Codigo_Pieza_Dental))
            {
                elemento = Mapa.LstOdontogramaParte5.First(a => a.codigoPiezaDental == Codigo_Pieza_Dental);
            }
            else if (Mapa.LstOdontogramaParte6.Any(a => a.codigoPiezaDental == Codigo_Pieza_Dental))
            {
                elemento = Mapa.LstOdontogramaParte6.First(a => a.codigoPiezaDental == Codigo_Pieza_Dental);
            }
            else if (Mapa.LstOdontogramaParte7.Any(a => a.codigoPiezaDental == Codigo_Pieza_Dental))
            {
                elemento = Mapa.LstOdontogramaParte7.First(a => a.codigoPiezaDental == Codigo_Pieza_Dental);
            }
            else if (Mapa.LstOdontogramaParte8.Any(a => a.codigoPiezaDental == Codigo_Pieza_Dental))
            {
                elemento = Mapa.LstOdontogramaParte8.First(a => a.codigoPiezaDental == Codigo_Pieza_Dental);
            }

            return elemento;

        }

    }
}
