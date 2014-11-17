using Hefesoft.Usuario.Interfaces;
using Hefesoft.Standard.Util.Convertir_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hefesoft.Standard.Util.table;

namespace Hefesoft.Usuario.Data
{
    public class Usuarios : IUsuarios
    {
        public async Task<Entidades.IUsuario> crearUsuario(Entidades.IUsuario usuario)
        {
            //Lo que se inserta en la clase usuario de hefesoft
            var usuarioInsertar = Convertir_Entidades.ConvertirEntidades(usuario, new Entidades.Usuario());
            var result = await usuarioInsertar.postTable();
            return result;
        }


        public async Task<IEnumerable<Entidades.Usuario>> listarUsuarios(string partitionKey = "", string rowKey = "", string nombreTabla = "")
        {
            var entidad = new Hefesoft.Usuario.Entidades.Usuario();

            if(!string.IsNullOrEmpty(partitionKey))
            {
                entidad.PartitionKey = partitionKey;
            }

            if (!string.IsNullOrEmpty(rowKey))
            {
                entidad.RowKey = rowKey;
            }

            if (!string.IsNullOrEmpty(nombreTabla))
            {
                entidad.nombreTabla = nombreTabla;
            }

            var result = await entidad.getTableByPartition();
            return result;
        }
    }
}
