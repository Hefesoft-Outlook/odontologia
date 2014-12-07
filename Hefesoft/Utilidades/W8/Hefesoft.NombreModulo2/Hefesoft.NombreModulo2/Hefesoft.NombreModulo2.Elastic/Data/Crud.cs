using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hefesoft.Standard.Util.table;
using Hefesoft.Standard.Util.Blob;

namespace Hefesoft.NombreModulo2.Elastic.Data
{
    internal class Crud
    {

        internal async Task<bool> insert(Hefesoft.NombreModulo2.Elastic.Entidades.NombreModulo2 entidad)
        {
            await entidad.postBlob();
            await entidad.postTable();
            return true;
        }

        internal async Task<List<Hefesoft.NombreModulo2.Elastic.Entidades.NombreModulo2>> getAllTableStorage(Hefesoft.NombreModulo2.Elastic.Entidades.NombreModulo2 query)
        {
            return await query.getTableByPartition();
        }

        internal async Task<List<Entidades.NombreModulo2>> getAllBlob(Hefesoft.NombreModulo2.Elastic.Entidades.NombreModulo2 query)
        {
            return await query.getBlobByPartition();
        }

    }
}
