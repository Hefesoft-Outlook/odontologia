using Hefesoft.Standard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Usuario.Entidades
{
    public interface IUsuario : IEntidadBase
    {
        string id { get; set; }
        string nombre { get; set; }
        string imagenRuta { get; set; }
    }
}
