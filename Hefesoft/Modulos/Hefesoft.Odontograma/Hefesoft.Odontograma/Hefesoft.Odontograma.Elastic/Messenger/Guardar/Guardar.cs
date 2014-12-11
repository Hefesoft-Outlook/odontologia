using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Input;
using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar
{
    public class Guardar
    {
        // Se inicializa por defecto en un valor aunque es el valor por defecto
        // se inicializa para hacer mas legible el codigo
        public Guardar()
        {
            Tipo_Datos_Solicitar = Messenger.Guardar.Tipo_Datos_Solicitar.Odontograma_Entity;
        }

        /// <summary>
        /// Esta clase se devuelve transformada casi lista para ser guardada
        /// Se una en odontograma inicial, plan de tratamiento
        /// </summary>
        public Action<List<OdontogramaEntity>> lstGuardar { get; set; }
        
        //Esta lista se envia como la fuente de datos
        //Se usa para calculos como el de la placa bacteriana ceo...
        public Action<List<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma>> lstOdontogramas { get; set; }

        public Tipo_Odontograma Pedir_Tipos_Guardar { get; set; }

        //Si es falso usa el modo actual del modulo
        //Ej el modulo esta en modo plan de tratamiento, 
        //La respuesta solo enviara los procedimientos
        //Si es true deja sobrecargarlo para pedir el tipo deseado Eje Inicial
        //o evolucion en modo plan de tratamiento
        public bool utilizar_ModoActual { get; set; }

        public bool CargarTodos { get; set; }

        public Tipo_Datos_Solicitar Tipo_Datos_Solicitar { get; set; }
        
    }

    public class Guardar_Barra_Comando
    {        
    }

    public enum Tipo_Datos_Solicitar
    {
        Odontograma_Entity = 0,
        Clase_Odontograma = 1,
    }
}
