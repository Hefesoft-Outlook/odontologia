using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hefesoft.Standard.Util.table;
using Hefesoft.Standard.Util.Blob;

namespace Hefesoft.NombreModulo1.Elastic.Data
{
    internal class Crud
    {

        internal async Task<bool> insert(Hefesoft.NombreModulo1.Elastic.Entidades.NombreModulo1 entidad)
        {
            await entidad.postBlob();
            await entidad.postTable();
            return true;
        }

        internal async Task<List<Hefesoft.NombreModulo1.Elastic.Entidades.NombreModulo1>> getAllTableStorage(Hefesoft.NombreModulo1.Elastic.Entidades.NombreModulo1 query)
        {
            return await query.getTableByPartition();
        }

        internal async Task<List<Entidades.NombreModulo1>> getAllBlob(Hefesoft.NombreModulo1.Elastic.Entidades.NombreModulo1 query)
        {
            return await query.getBlobByPartition();
        }

    }
}
