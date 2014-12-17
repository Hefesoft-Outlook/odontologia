using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Hefesoft.Standard.BusyBox;
using Microsoft.Practices.ServiceLocation;
using Hefesoft.Standard.Util.Collection.Observables;
using Hefesoft.Periodontograma.Elastic.Entidades;

namespace Hefesoft.Periodontograma.Elastic.ViewModel
{
    public partial class Periodontograma : ViewModelBase
    {
        private void limpiarSinRecargarImagenes()
        {
            Hefesoft.Periodontograma.Elastic.Util.LimpiarDatos.limpiarListado(LstPeriodontogramaParte1);
            Hefesoft.Periodontograma.Elastic.Util.LimpiarDatos.limpiarListado(LstPeriodontogramaParte2);
            Hefesoft.Periodontograma.Elastic.Util.LimpiarDatos.limpiarListado(LstPeriodontogramaParte3);
            Hefesoft.Periodontograma.Elastic.Util.LimpiarDatos.limpiarListado(LstPeriodontogramaParte4);
            Hefesoft.Periodontograma.Elastic.Util.LimpiarDatos.limpiarListado(LstPeriodontogramaParte5);
            Hefesoft.Periodontograma.Elastic.Util.LimpiarDatos.limpiarListado(LstPeriodontogramaParte6);
            Hefesoft.Periodontograma.Elastic.Util.LimpiarDatos.limpiarListado(LstPeriodontogramaParte7);
            Hefesoft.Periodontograma.Elastic.Util.LimpiarDatos.limpiarListado(LstPeriodontogramaParte8);
        }

        private async void cargarPeriodontogramaSeleccionado(Data.Periodontograma obj)
        {
            BusyBox.UserControlCargando(true);
            var data = await Hefesoft.Standard.Util.Blob.CrudBlob.getBlobByPartitionAndRowKey(obj, obj.RowKey);
            if (data != null && data.Listado.Any())
            {
                periodontogramaAListadoUi(data.Listado);
            }
            BusyBox.UserControlCargando(false);
        }

        private async void listarPorPaciente()
        {
            BusyBox.UserControlCargando(true);
            var vmPaciente = ServiceLocator.Current.GetInstance<Hefesoft.Pacientes.Elastic.ViewModel.Pacientes>();
            Data.Crud crud = new Data.Crud();

            var query = new Hefesoft.Periodontograma.Elastic.Data.Periodontograma
            {
                PartitionKey = vmPaciente.Paciente.RowKey
            };

            var result = await crud.getPorPaciente(query);
            listadoPeriodontogramasPorPaciente = result.ToObservableCollection();
            RaisePropertyChanged("listadoPeriodontogramasPorPaciente");
            BusyBox.UserControlCargando(false);
        }

        public async void save()
        {
            var vmPaciente = ServiceLocator.Current.GetInstance<Hefesoft.Pacientes.Elastic.ViewModel.Pacientes>();
            var vmUsuario = ServiceLocator.Current.GetInstance<Hefesoft.Usuario.ViewModel.Usuarios>();

            if (vmPaciente.Paciente != null)
            {
                BusyBox.UserControlCargando(true, "Guardando...");
                Data.Periodontograma periodontograma = new Data.Periodontograma();
                periodontograma.Paciente = vmPaciente.Paciente;
                periodontograma.PartitionKey = vmPaciente.Paciente.RowKey;
                periodontograma.Listado.AddRange(LstPeriodontogramaParte1);
                periodontograma.Listado.AddRange(LstPeriodontogramaParte2);
                periodontograma.Listado.AddRange(LstPeriodontogramaParte3);
                periodontograma.Listado.AddRange(LstPeriodontogramaParte4);
                periodontograma.Listado.AddRange(LstPeriodontogramaParte5);
                periodontograma.Listado.AddRange(LstPeriodontogramaParte6);
                periodontograma.Listado.AddRange(LstPeriodontogramaParte7);
                periodontograma.Listado.AddRange(LstPeriodontogramaParte8);

                Data.Crud crud = new Data.Crud();
                await crud.guardar(periodontograma);

                //UI para agregar el que se acaba de guardar
                listadoPeriodontogramasPorPaciente.Add(periodontograma);
                BusyBox.UserControlCargando(false);
            }
            else
            {
                // Mensaje seleccione un usuario
            }
        }

        private void inicializarDatos()
        {
            BusyBox.UserControlCargando(true, "Inicializando datos");

            limpiarListados();

            Hefesoft.Periodontograma.Elastic.Util.Generar_datos datos = new Util.Generar_datos();
            var data = datos.inicializarDatos();
            LstPeriodontogramaParte1 = data.Where(a => a.Parte == Enumeradores.Parte.parte1).ToObservableCollection();
            LstPeriodontogramaParte2 = data.Where(a => a.Parte == Enumeradores.Parte.parte2).ToObservableCollection();
            LstPeriodontogramaParte3 = data.Where(a => a.Parte == Enumeradores.Parte.parte3).ToObservableCollection();
            LstPeriodontogramaParte4 = data.Where(a => a.Parte == Enumeradores.Parte.parte4).ToObservableCollection();
            LstPeriodontogramaParte5 = data.Where(a => a.Parte == Enumeradores.Parte.parte5).ToObservableCollection();
            LstPeriodontogramaParte6 = data.Where(a => a.Parte == Enumeradores.Parte.parte6).ToObservableCollection();
            LstPeriodontogramaParte7 = data.Where(a => a.Parte == Enumeradores.Parte.parte7).ToObservableCollection();
            LstPeriodontogramaParte8 = data.Where(a => a.Parte == Enumeradores.Parte.parte8).ToObservableCollection();

            actualizarListadosUi();

            BusyBox.UserControlCargando(false);
        }

        /// <summary>
        /// Carga todos los blobs que exitan en periodontograma es bastante pesado
        /// </summary>
        private async void obtenerDatos()
        {
            BusyBox.UserControlCargando(true);
            Data.Crud crud = new Data.Crud();
            var result = await crud.get();

            if (result.Any())
            {
                var data = result.First().Listado;
                periodontogramaAListadoUi(data);
            }

            BusyBox.UserControlCargando(false);
        }

        private void periodontogramaAListadoUi(List<PeriodontogramaEntity> data)
        {
            limpiarSinRecargarImagenes();
            actualizarValor(LstPeriodontogramaParte1, data);
            actualizarValor(LstPeriodontogramaParte2, data);
            actualizarValor(LstPeriodontogramaParte3, data);
            actualizarValor(LstPeriodontogramaParte4, data);
            actualizarValor(LstPeriodontogramaParte5, data);
            actualizarValor(LstPeriodontogramaParte6, data);
            actualizarValor(LstPeriodontogramaParte7, data);
            actualizarValor(LstPeriodontogramaParte8, data);
            actualizarListadosUi();
        }

        private void actualizarValor(IEnumerable<PeriodontogramaEntity> lst, List<PeriodontogramaEntity> data)
        {
            foreach (var item in lst)
            {
                var elemento = data.FirstOrDefault(a => a.Numero == item.Numero);
                item.actualizarElemento(elemento);
            }
        }

        private void actualizarListadosUi()
        {
            RaisePropertyChanged("LstPeriodontogramaParte1");
            RaisePropertyChanged("LstPeriodontogramaParte2");
            RaisePropertyChanged("LstPeriodontogramaParte3");
            RaisePropertyChanged("LstPeriodontogramaParte4");
            RaisePropertyChanged("LstPeriodontogramaParte5");
            RaisePropertyChanged("LstPeriodontogramaParte6");
            RaisePropertyChanged("LstPeriodontogramaParte7");
            RaisePropertyChanged("LstPeriodontogramaParte8");
        }

        private void limpiarListados()
        {
            LstPeriodontogramaParte1 = null;
            LstPeriodontogramaParte2 = null;
            LstPeriodontogramaParte3 = null;
            LstPeriodontogramaParte4 = null;
            LstPeriodontogramaParte5 = null;
            LstPeriodontogramaParte6 = null;
            LstPeriodontogramaParte7 = null;
            LstPeriodontogramaParte8 = null;
        }
    }
}
