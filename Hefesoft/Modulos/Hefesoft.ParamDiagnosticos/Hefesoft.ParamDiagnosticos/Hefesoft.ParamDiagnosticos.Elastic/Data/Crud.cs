using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hefesoft.Standard.Util.table;
using Hefesoft.Standard.Util.Blob;

namespace Hefesoft.ParamDiagnosticos.Elastic.Data
{
    internal class Crud
    {

        internal async Task<bool> insert(Hefesoft.ParamDiagnosticos.Elastic.Entidades.ParamDiagnosticos entidad)
        {
            await entidad.postBlob();
            await entidad.postTable();
            return true;
        }

        internal async Task<List<Hefesoft.ParamDiagnosticos.Elastic.Entidades.ParamDiagnosticos>> getAllTableStorage(Hefesoft.ParamDiagnosticos.Elastic.Entidades.ParamDiagnosticos query)
        {
            return await query.getTableByPartition();
        }

        internal async Task<List<Entidades.ParamDiagnosticos>> getAllBlob(Hefesoft.ParamDiagnosticos.Elastic.Entidades.ParamDiagnosticos query)
        {
            return await query.getBlobByPartition();
        }

    }
}
