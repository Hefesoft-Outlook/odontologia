using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Xap.Odontologia.Vm.Odontograma;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace Cnt.Panacea.Xap.Odontologia.Util.Messenger
{
    public class Pedir_Pintar_Datos
    {
        public Action<List<Odontograma>> Resultado { get; set; }
        public ObservableCollection<OdontogramaEntity> lst { get; set; }

        // Bit para indicarle al control mapa dental que debe pintar un nuevo odontograma sin datos en el
        public bool nuevoOdontograma { get; set; }

        private bool limpiarDatos = true;

        public bool Limpiar_Datos
        {
            get { return limpiarDatos; }
            set { limpiarDatos = value; }
        }
        
    }
}
