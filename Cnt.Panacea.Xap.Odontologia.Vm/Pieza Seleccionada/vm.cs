using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Net;
using System.Windows;
using System.Linq;
using Cnt.Panacea.Xap.Odontologia.Clases;
using Cnt.Panacea.Xap.Odontologia.Util.Messenger;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Odontograma;

namespace Cnt.Panacea.Xap.Odontologia.Assets.Pieza_Dental.Pieza_Seleccionada.vm
{
    public class vm : ViewModelBase , IDisposable
    {
        public vm()
        {
            if (IsInDesignMode)
            {
            }
            else
            {
                oirCambioIndices();
                oirIndicePlacaBacteriana();
                oirNuevoTratamiento();
            }
        }

        private void oirNuevoTratamiento()
        {
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Activar_Elementos>(this, nuevo =>
            {
                if (nuevo.valor == "Nuevo")
                {
                    CEO = 0;
                    COP = 0;
                    Indice_Placa_Bacteriana = 0;
                    
                    RaisePropertyChanged("CEO");
                    RaisePropertyChanged("COP");
                    RaisePropertyChanged("Indice_Placa_Bacteriana");
                }
            });
        }

        private void oirIndicePlacaBacteriana()
        {
            // El segundo parametro es un token que garantiza que este msj solo llegue a las clases que lo tengan
            Messenger.Default.Register<double>(this, "Numero Piezas Dentales", piezasDentales => 
            {
                numero_piezas_presentes = piezasDentales;
                Variables_Globales.Numero_Piezas_Dentales = piezasDentales;

                if (piezasDentales > 0)
                {
                    solicitarSuperficiesPintadas();
                }                
            });
        }

        private void oirCambioIndices()
        {
            //Los elementos del odontograma le cuentn a este view model que se acaba de agregar un diagnostico
            //Que cambia los indices que se deben mostrar
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Util.Messenger.Indices.Recalcular_Indices>(this, calculo => 
            {
                solicitarSuperficiesPintadas();
            });
        }

        private void solicitarSuperficiesPintadas()
        {
            Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Guardar()
            {
                Tipo_Datos_Solicitar = Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Tipo_Datos_Solicitar.Clase_Odontograma,
                lstOdontogramas = listadoOdontogramasCalculo
            });
        }

        
         /*   1.	COP (C=Dientes Cariados, O=Dientes Obturados, P=Dientes Perdidos) 
         *      para dientes permanentes (los dientes de afuera), conteo de los diagnósticos parametrizados con Índice COP.  

            2.	CEO (C=Dientes Cariados, E=Dientes Extraídos, O=Dientes Obturados) 
         *      para dientes temporales (los dientes de adentro), conteo de los diagnósticos parametrizados con Índice CEO.  

                Para COP y CEO Se debe contar por cada diente, es decir si en un solo diente se marcan las 7 superficies, solo se cuenta 1.

            3.	Placa bacteriana: superficies marcadas con placa (solo se debe tomar 4 superficies por cada diente máximo) * 100 / piezas presentes * 4.
        */
        private void listadoOdontogramasCalculo(System.Collections.Generic.List<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> obj)
        {
            COP = obj.Where(a => a.Tipo_Diente_Permanente_Temporal == Tipo_Diente_Permanente_Temporal.Permanente && a.sumaIndice).Count();
            CEO = obj.Where(a => a.Tipo_Diente_Permanente_Temporal == Tipo_Diente_Permanente_Temporal.Temporal && a.sumaIndice).Count();

            if (numero_piezas_presentes > 0)
            {
                Indice_Placa_Bacteriana = ((obj.Sum(a => a.indicePlacaBacteriana) * 100) / numero_piezas_presentes) * 4;
            }

            Variables_Globales.COP = COP;
            Variables_Globales.CEO = CEO;

            RaisePropertyChanged("COP");
            RaisePropertyChanged("CEO");
            RaisePropertyChanged("Indice_Placa_Bacteriana");
        }

        public void Dispose()
        {
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Util.Messenger.Indices.Recalcular_Indices>(this);
        }
        

        public int CEO { get; set; }

        public int COP { get; set; }

        public double Indice_Placa_Bacteriana { get; set; }

        public double numero_piezas_presentes { get; set; }
    }
}
