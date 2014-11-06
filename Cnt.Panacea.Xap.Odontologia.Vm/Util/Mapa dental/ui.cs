using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM;
using System.Collections.Generic;
using Cnt.Panacea.Entities.Odontologia;
using GalaSoft.MvvmLight.Messaging;
using Cnt.Panacea.Xap.Odontologia.Vm.Extensiones.Clases;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar;
using Microsoft.Practices.ServiceLocation;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Util.Mapa_dental
{
    public class ui
    {
       
        internal List<DiagnosticoProcedimiento_Extend> ConvertirInterfazOdontograma(Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm vm, ObservableCollection<Entities.Odontologia.OdontogramaEntity> resultado)
        {
            //Si hay supernumerarios en el listado los agrega al mapa dental
            new Cnt.Panacea.Xap.Odontologia.Vm.Supernumerario.Agregar_Supernumerario().agregarSupernumerariosListado(vm, resultado);


            var lst = new List<DiagnosticoProcedimiento_Extend>();

            foreach (OdontogramaEntity pivote in resultado)
            {
                // Transforma el odontograma entity en lo que el odontograma puede entender
                // Y lo agrega a la lista
                lst.Add(pivote.odontogramaEntityToDiagnosticoProcedimiento_Extend());
            }

            return lst;
        }

        internal List<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> pintarDatos(Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm vm, List<DiagnosticoProcedimiento_Extend> listado)
        {
            //Esta lista se utiliza para enviarlesa a los formularios que la necesiten para interactuar
            //Con el odontograma indirectamente
            List<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> listadoElementos = new List<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma>();


            var buscadorElementosOdontograma = new Validaciones();

            // Traiga todos menos los de la boca
            foreach (var item in listado.Where(a=>a.Codigo_Pieza_Dental !=99))
            {                
                // Obtiene el el elemento dentro de los list que estan pintados dentro del ui
                var elementoEncontrado = buscadorElementosOdontograma.obtenerElementoEnElOdontograma(vm, item);
                if (elementoEncontrado != null)
                {
                    elementoEncontrado.DiagnosticoProcedimiento.lst.Add(item);
                    elementoEncontrado.DiagnosticoProcedimiento.pintarDiagnosticos(item.ConfigurarDiagnosticoProcedimOtraEntity, item.Superficie);
                }
                listadoElementos.Add(elementoEncontrado);
            }

            //Solo los de la boca
            var vmBoca = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Boca.Boca>();
            foreach (var item in listado.Where(a=>a.Codigo_Pieza_Dental ==99))
            {
                vmBoca.Elemento.DiagnosticoProcedimiento.lst.Add(item);
                vmBoca.Elemento.Paleta_Selecionado = item.ConfigurarDiagnosticoProcedimOtraEntity;
                vmBoca.Elemento.click("Boca");

            }

            // Mensaje para calcular los indices de placa bacteriana, ceo cop
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Util.Messenger.Indices.Recalcular_Indices());
            return listadoElementos;
        }

        internal void datosTransformadosOdontogramaEntity(Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm vm, Guardar peticion)
        {
            var validaciones = new Validaciones();

            //Carga todos los que se encuentren en el mapa dental
            if (peticion.CargarTodos)
            {
                var valido = validaciones.validarTieneElementos(vm, vm.Modo.Tipo_Odontograma);
                var elementos = validaciones.convertirAEntidadesOdontogramaEntityTodos(vm);
                // Envia la respuesta a la peticion
                peticion.lstGuardar(elementos);
            }
            else if (!peticion.utilizar_ModoActual)
            {
                var valido = validaciones.validarTieneElementos(vm, vm.Modo.Tipo_Odontograma);
                var elementos = validaciones.convertirAEntidadesOdontogramaEntity(vm, vm.Modo.Tipo_Odontograma);
                // Envia la respuesta a la peticion
                peticion.lstGuardar(elementos);
            }
            else
            {
                var valido = validaciones.validarTieneElementos(vm, peticion.Pedir_Tipos_Guardar);
                var elementos = validaciones.convertirAEntidadesOdontogramaEntity(vm, peticion.Pedir_Tipos_Guardar);
                // Envia la respuesta a la peticion
                peticion.lstGuardar(elementos);
            }
        }

    }
}
