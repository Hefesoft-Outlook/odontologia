using Cnt.Panacea.Entities.Odontologia;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace Cnt.Panacea.Xap.Odontologia.Util.Messenger.Mapa_Dental.Imagenes
{
    public class listado_Imagenes_Cargadas
    {
        public Action<List<TratamientoImagenEntity>> listadoImagenesCargadas { get; set; }
    }
}
