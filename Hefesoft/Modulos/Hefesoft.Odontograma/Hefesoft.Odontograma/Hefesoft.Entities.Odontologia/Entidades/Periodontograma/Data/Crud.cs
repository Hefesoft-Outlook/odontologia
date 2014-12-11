using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hefesoft.Standard.Util.Blob;
using Hefesoft.Standard.Util.table;

namespace Hefesoft.Odontologia.Periodontograma.Data
{
    public class Crud
    {
        public async Task<Periodontograma> guardar(Periodontograma periodontograma)
        {
            var resultTable = await Hefesoft.Standard.Util.table.crudTable.postTable(periodontograma);
            var result = await Hefesoft.Standard.Util.Blob.CrudBlob.postBlob(periodontograma);
            return result;
        }

        public async Task<List<Periodontograma>> get()
        {
            var result = await Hefesoft.Standard.Util.Blob.CrudBlob.getBlobByPartition(new Periodontograma());
            return result;
        }

        public async Task<List<Periodontograma>> getPorPaciente(Periodontograma entidad)
        {
            var result = await Hefesoft.Standard.Util.table.crudTable.getTableByPartition(entidad);
            return result;
        }
    }
}
