using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hefesoft.Standard.Util.table;
using Hefesoft.Standard.Util.Blob;

namespace Hefesoft.Terceros.Elastic.Data
{
    internal class Crud
    {

        internal async Task<bool> insert(Hefesoft.Terceros.Elastic.Entidades.Terceros entidad)
        {
            await entidad.postBlob();
            await entidad.postTable();
            return true;
        }

        internal async Task<List<Hefesoft.Terceros.Elastic.Entidades.Terceros>> getAllTableStorage(Hefesoft.Terceros.Elastic.Entidades.Terceros query)
        {
            return await query.getTableByPartition();
        }

        internal async Task<List<Entidades.Terceros>> getAllBlob(Hefesoft.Terceros.Elastic.Entidades.Terceros query)
        {
            return await query.getBlobByPartition();
        }

    }
}
