using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Cnt.Panacea.Entities.Odontologia;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Cnt.Panacea.Xap.Odontologia.Util.Messenger;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Indices__Ceo__Ceop_;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using Cnt.Panacea.Xap.Odontologia.Clases;
using System.Collections.ObjectModel;

namespace Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm
{
    /// <summary>
    /// Clase que se crea inicialmente para la implementacion de windows 8
    /// </summary>
    public partial class Odontograma_Inicial
    {

        private void oirCargadoOdontogramaInicial()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<ObservableCollection<OdontogramaEntity>>(this, "Odontograma cargado", odontogramas);
        }

        private async void odontogramas(ObservableCollection<OdontogramaEntity> obj)
        {
            Busy.UserControlCargando();
            var resultado = obj;

            //Pedimos al control Mapa dental que pinte los datos que llegaron de la bd
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Pedir_Pintar_Datos() { lst = resultado });

            var convenio = await Contexto_Odontologia.obtenerContexto().consultarConvenio(Variables_Globales.IdConvenio);
            ConvenioAtencion = convenio;
            RaisePropertyChanged("ConvenioAtencion");
            Busy.UserControlCargando(false);

            if (Variables_Globales.OdontogramaPacientetity != 0)
            {

                var Prestador = await Contexto_Odontologia.obtenerContexto().ConsultarPrestador(Variables_Globales.IdPrestador);//LFDO Bug 16006
                PrestadorAtencion = Prestador;
                RaisePropertyChanged("PrestadorAtencion");
                //cargarProcedimientosAsociadosADiagnosticos(resultado);
                TipoOdontograma.Inicial = false;
                Odontologia.Clases.TipoOdontograma.InicialCargandoDatos = true;
                //await tratamientoActivo(Variables_Globales.IdTratamientoActivo);
                var OdontogramaPaciente = await Contexto_Odontologia.obtenerContexto().SelecionarOdontogramaPaciente(Variables_Globales.OdontogramaPacientetity);

                Messenger.Default.Send(new Carga_Odontograma_Inicial() { Cantidad_Dientes = OdontogramaPaciente.CantidadDientes });
                Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paleta.Paleta() { NumeroPiezasDentales = OdontogramaPaciente.CantidadDientes });
            }
            else
            {
                Odontologia.Clases.TipoOdontograma.Inicial = true;
                Odontologia.Clases.TipoOdontograma.InicialCargandoDatos = false;
            }
        }

    }
}
