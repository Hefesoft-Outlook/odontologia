using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hefesoft.Standard.Util.table;
using Hefesoft.Standard.Util.Blob;

namespace Hefesoft.Autentication.Elastic.Data
{
    internal class Crud
    {

        internal async Task<bool> insert(Hefesoft.Autentication.Elastic.Entidades.Autentication entidad)
        {
            await entidad.postBlob();
            await entidad.postTable();
            return true;
        }

        internal async Task<List<Hefesoft.Autentication.Elastic.Entidades.Autentication>> getAllTableStorage(Hefesoft.Autentication.Elastic.Entidades.Autentication query)
        {
            return await query.getTableByPartition();
        }

        internal async Task<List<Entidades.Autentication>> getAllBlob(Hefesoft.Autentication.Elastic.Entidades.Autentication query)
        {
            return await query.getBlobByPartition();
        }

    }
}
