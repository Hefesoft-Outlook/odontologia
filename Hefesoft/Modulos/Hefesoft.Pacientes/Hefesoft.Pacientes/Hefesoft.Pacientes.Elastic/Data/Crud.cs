using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hefesoft.Standard.Util.table;
using Hefesoft.Standard.Util.Blob;

namespace Hefesoft.Pacientes.Elastic.Data
{
    internal class Crud
    {

        internal async Task<bool> insert(Hefesoft.Pacientes.Elastic.Entidades.Pacientes entidad)
        {
            await entidad.postBlob();
            await entidad.postTable();
            return true;
        }

        internal async Task<List<Hefesoft.Pacientes.Elastic.Entidades.Pacientes>> getAllTableStorage(Hefesoft.Pacientes.Elastic.Entidades.Pacientes query)
        {
            return await query.getTableByPartition();
        }

        internal async Task<List<Entidades.Pacientes>> getAllBlob(Hefesoft.Pacientes.Elastic.Entidades.Pacientes query)
        {
            return await query.getBlobByPartition();
        }

    }
}
