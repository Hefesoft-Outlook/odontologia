using Hefesoft.Usuario.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hefesoft.Usuario.Interfaces
{
    public interface IUsuarios
    {
        Task<IUsuario> crearUsuario(IUsuario usuario);
    }
}
