using Hefesoft.Usuario.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Hefesoft.Autentication.Util.Storage
{
    public class Usuario
    {
        public Usuario()
        {
            
        }

        static internal Hefesoft.Usuario.Entidades.Usuario obtenerUsuario()
        {
            var usuario = new Hefesoft.Usuario.Entidades.Usuario();

            var valores = ApplicationData.Current.LocalSettings.Values;


            if (valores.Any(a => a.Key == "Identificador"))
            {
                usuario.id = valores["Identificador"].ToString();
            }

            if (valores.Any(a => a.Key == "Nombre"))
            {
                usuario.nombre = valores["Nombre"].ToString();
            }

            if (valores.Any(a => a.Key == "Imagen_Ruta"))
            {
                usuario.imagenRuta = valores["Imagen_Ruta"].ToString();
            }
            return usuario;
        }

        internal static void guardarUsuario(IUsuario Usuario)
        {
            ApplicationData.Current.LocalSettings.Values["Identificador"] = Usuario.id;
            ApplicationData.Current.LocalSettings.Values["Nombre"] = Usuario.nombre;
            ApplicationData.Current.LocalSettings.Values["Imagen_Ruta"] = Usuario.imagenRuta;
        }
    }
}
