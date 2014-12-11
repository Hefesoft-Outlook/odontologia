using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.PopUp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Util.Mapa_dental
{
    public class Agregar_Supernumerario
    {

        public void clickDerechoContenedorPiezaDental(Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm vm, Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma Odontograma)
        {
            if (Odontograma.TieneDienteSupernumerarioIzquierda && Odontograma.TieneDienteSupernumerarioDerecha)
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                {
                    Mensaje = "El elemento ya posee supernumerarios asociados a izquierda y derecha"
                });
            }
            else
            {
                vm.OdontogramaSeleccionadoSupernumerario = Odontograma;
                mostrarVentanaSupernumerario(vm);
            }
        }

        // Ya que los supernumerarios son un caso excepcional se le delega este manejo a una clase
        // Para hacer el codigo mas legible
        public void adicionarSupernumerario(Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm vm, Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma Odontograma, bool derecha)
        {
            int codigoSupernumerario = 100;
            if (Odontograma.codigoPiezaDental < 100)
            {
                int izquierdaDerecha = 0;
                var valido = true;
                //Para saber donde esta el supernumerario
                if (derecha)
                {
                    //Valida que no se traten de adicionar dos supernumerarios seguidos en la bd
                    //Ya que nos quedariamos sin referencia de como pintarlos
                    // La clase odontograma tiene una propiedad que indica si el elemento
                    // Ya tiene un supernumerario asociado a el
                    //Con ese se hace la validacion
                    valido = !Odontograma.TieneDienteSupernumerarioDerecha;
                    if (valido)
                    {
                        Odontograma.TieneDienteSupernumerarioDerecha = true;
                        izquierdaDerecha = 1;
                    }
                }
                else
                {
                    //Valida que no se traten de adicionar dos supernumerarios seguidos en la bd
                    //Ya que nos quedariamos sin referencia de como pintarlos
                    valido = !Odontograma.TieneDienteSupernumerarioIzquierda;
                    if (valido)
                    {
                        Odontograma.TieneDienteSupernumerarioIzquierda = true;
                        izquierdaDerecha = 0;
                    }
                }

                //Si es valido agreguelo a la lista
                if (valido)
                {
                    Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma Supernumerario = new Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma("S") { codigoSPiezaDental = "S", codigoPiezaDental = codigoSupernumerario++, UbicacionOdontograma = Odontograma.UbicacionOdontograma, Supernumerario = true, colorCodigoSPiezaDental = "#FF000000", CodigoSuperficie1 = Entities.Odontologia.Superficie.IncisalOclusal, CodigoSuperficie2 = Entities.Odontologia.Superficie.Mesial, CodigoSuperficie3 = Entities.Odontologia.Superficie.Distal, CodigoSuperficie4 = Entities.Odontologia.Superficie.LingualPalatinaInferior, CodigoSuperficie5 = Entities.Odontologia.Superficie.LingualPalatinaSuperior, CodigoSuperficie6 = Entities.Odontologia.Superficie.VestibularSuperior, CodigoSuperficie7 = Entities.Odontologia.Superficie.VestibularInferior };
                    Supernumerario.Referencia = new Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.referenciaSupernumerario() { Diente_Referencia = Odontograma.codigoPiezaDental };
                    Supernumerario.Referencia.Esta_A_la_Derecha = derecha;

                    int index = 0;

                    if (derecha)
                    {
                        index = vm.lstOdontograma.IndexOf(Odontograma) + izquierdaDerecha;
                    }
                    else
                    {
                        index = vm.lstOdontograma.IndexOf(Odontograma) + izquierdaDerecha;
                    }


                    // El odontograma esta dividido en varios listview
                    // Con esto se sabe a cual listview se le debe agregar
                    if (Odontograma.UbicacionOdontograma == 1)
                    {
                        int indexListaEspecifica = vm.LstOdontogramaParte1.IndexOf(Odontograma) + izquierdaDerecha;
                        vm.LstOdontogramaParte1.Insert(indexListaEspecifica, Supernumerario);
                    }
                    else if (Odontograma.UbicacionOdontograma == 2)
                    {
                        int indexListaEspecifica = vm.LstOdontogramaParte2.IndexOf(Odontograma) + izquierdaDerecha;
                        vm.LstOdontogramaParte2.Insert(indexListaEspecifica, Supernumerario);
                    }
                    else if (Odontograma.UbicacionOdontograma == 3)
                    {
                        int indexListaEspecifica = vm.LstOdontogramaParte3.IndexOf(Odontograma) + izquierdaDerecha;
                        vm.LstOdontogramaParte3.Insert(indexListaEspecifica, Supernumerario);
                    }
                    else if (Odontograma.UbicacionOdontograma == 4)
                    {
                        int indexListaEspecifica = vm.LstOdontogramaParte4.IndexOf(Odontograma) + izquierdaDerecha;
                        vm.LstOdontogramaParte4.Insert(indexListaEspecifica, Supernumerario);
                    }
                    else if (Odontograma.UbicacionOdontograma == 5)
                    {
                        int indexListaEspecifica = vm.LstOdontogramaParte5.IndexOf(Odontograma) + izquierdaDerecha;
                        vm.LstOdontogramaParte5.Insert(indexListaEspecifica, Supernumerario);
                    }
                    else if (Odontograma.UbicacionOdontograma == 6)
                    {
                        int indexListaEspecifica = vm.LstOdontogramaParte6.IndexOf(Odontograma) + izquierdaDerecha;
                        vm.LstOdontogramaParte6.Insert(indexListaEspecifica, Supernumerario);
                    }
                    else if (Odontograma.UbicacionOdontograma == 7)
                    {
                        int indexListaEspecifica = vm.LstOdontogramaParte7.IndexOf(Odontograma) + izquierdaDerecha;
                        vm.LstOdontogramaParte7.Insert(indexListaEspecifica, Supernumerario);
                    }
                    vm.lstOdontograma.Insert(index, Supernumerario);
                }
            }
        }

        public void mostrarVentanaSupernumerario(Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm vm)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Vm.Messenger.Pop_Up.Mostrar_Ventana()
            {
                Nombre = "Supernumerario",
                DataContext = this,
                Propiedad_Adicional = vm.OdontogramaSeleccionadoSupernumerario
            });
        }

        /// <summary>
        /// Dependiendo la opcion seleccionada por el usuario realiza una accion u otra
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="Odontograma"></param>
        /// <param name="derecha"></param>
        public void menuSupernumerario(Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm vm, object elemento)
        {
            //Este mensaje cierra el childwindow abierto
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cerrar_Pop_Up_Generico());

            if (elemento.ToString().Equals("Izquierda"))
            {
                adicionarSupernumerario(vm, vm.OdontogramaSeleccionadoSupernumerario, false);
            }
            else if (elemento.ToString().Equals("Derecha"))
            {
                adicionarSupernumerario(vm, vm.OdontogramaSeleccionadoSupernumerario, true);
            }
            else if (elemento.ToString().Equals("Eliminar"))
            {
                eliminarSupernumerario(vm, vm.OdontogramaSeleccionadoSupernumerario);
            }
        }

        public void eliminarSupernumerario(Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm vm, Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma Odontograma)
        {

            int index = vm.lstOdontograma.IndexOf(Odontograma);

            if (Odontograma.UbicacionOdontograma == 1)
            {
                int indexListaEspecifica = vm.LstOdontogramaParte1.IndexOf(Odontograma);
                vm.LstOdontogramaParte1.RemoveAt(indexListaEspecifica);
            }
            else if (Odontograma.UbicacionOdontograma == 2)
            {
                int indexListaEspecifica = vm.LstOdontogramaParte2.IndexOf(Odontograma);
                vm.LstOdontogramaParte2.RemoveAt(indexListaEspecifica);

            }
            else if (Odontograma.UbicacionOdontograma == 3)
            {
                int indexListaEspecifica = vm.LstOdontogramaParte3.IndexOf(Odontograma);
                vm.LstOdontogramaParte3.RemoveAt(indexListaEspecifica);
            }
            else if (Odontograma.UbicacionOdontograma == 4)
            {
                int indexListaEspecifica = vm.LstOdontogramaParte4.IndexOf(Odontograma);
                vm.LstOdontogramaParte4.RemoveAt(indexListaEspecifica);
            }
            else if (Odontograma.UbicacionOdontograma == 5)
            {
                int indexListaEspecifica = vm.LstOdontogramaParte5.IndexOf(Odontograma);
                vm.LstOdontogramaParte5.RemoveAt(indexListaEspecifica);
            }
            else if (Odontograma.UbicacionOdontograma == 6)
            {
                int indexListaEspecifica = vm.LstOdontogramaParte6.IndexOf(Odontograma);
                vm.LstOdontogramaParte6.RemoveAt(indexListaEspecifica);
            }
            else if (Odontograma.UbicacionOdontograma == 7)
            {
                int indexListaEspecifica = vm.LstOdontogramaParte7.IndexOf(Odontograma);
                vm.LstOdontogramaParte7.RemoveAt(indexListaEspecifica);
            }

            vm.lstOdontograma.RemoveAt(index);
            vm.SupernumerariosActuales = vm.SupernumerariosActuales - 1;

            //Vamos a buscar el diente al que se hace referencia en el supernumerario y decirle que ya no tiene un supernumerario a sulado
            var elemento = vm.lstOdontograma.FirstOrDefault(a => a.codigoPiezaDental == Odontograma.Referencia.Diente_Referencia);

            if (elemento != null)
            {
                if (Odontograma.Referencia.Esta_A_la_Derecha)
                {
                    elemento.TieneDienteSupernumerarioDerecha = false;
                }
                else
                {
                    elemento.TieneDienteSupernumerarioIzquierda = false;
                }
            }

        }

        public void agregarSupernumerariosListado(Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm vm, ObservableCollection<Entities.Odontologia.OdontogramaEntity> resultado)
        {
            if (resultado.Any(a => a.Diente.Identificador == 98))
            {
                //Agregar supernumerarios al odontograma al cargarlo
                var listadoSupernumerarios = resultado.Where(a => a.Diente.Identificador == 98);
                var buscadorElementosOdontograma = new Validaciones();
                var elementoOdontograma = new Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma();

                foreach (var item in listadoSupernumerarios)
                {
                    if (item.DienteReferencia1 != null && item.DienteReferencia1.Identificador != 0)
                    {
                        elementoOdontograma = buscadorElementosOdontograma.obtenerElementoEnElOdontogramaCodigoDiente(vm, item.DienteReferencia1.Identificador);
                        adicionarSupernumerario(vm, elementoOdontograma, true);
                    }
                    else if (item.DienteReferencia2 != null && item.DienteReferencia2.Identificador != 0)
                    {
                        elementoOdontograma = buscadorElementosOdontograma.obtenerElementoEnElOdontogramaCodigoDiente(vm, item.DienteReferencia2.Identificador);
                        adicionarSupernumerario(vm, elementoOdontograma, false);
                    }
                }
            }
        }

        public void limpiarSuperNumerarios(Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm vm)
        {
            var supernumerarios = vm.lstOdontograma.Where(a => a.codigoPiezaDental == 100 || a.codigoPiezaDental == 98);

            foreach (var item in supernumerarios)
            {
                if (item.UbicacionOdontograma == 1)
                {
                    vm.LstOdontogramaParte1.Remove(item);
                }
                else if (item.UbicacionOdontograma == 2)
                {
                    vm.LstOdontogramaParte2.Remove(item);
                }
                else if (item.UbicacionOdontograma == 3)
                {
                    vm.LstOdontogramaParte3.Remove(item);
                }
                else if (item.UbicacionOdontograma == 4)
                {
                    vm.LstOdontogramaParte4.Remove(item);
                }
                else if (item.UbicacionOdontograma == 5)
                {
                    vm.LstOdontogramaParte5.Remove(item);
                }
                else if (item.UbicacionOdontograma == 6)
                {
                    vm.LstOdontogramaParte6.Remove(item);
                }
                else if (item.UbicacionOdontograma == 7)
                {
                    vm.LstOdontogramaParte7.Remove(item);
                }
                else if (item.UbicacionOdontograma == 8)
                {
                    vm.LstOdontogramaParte8.Remove(item);
                }
            }
        }
    }
}
