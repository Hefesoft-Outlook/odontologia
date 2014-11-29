using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hefesoft.Standard.Util.Blob;

namespace Hefesoft.Odontologia.Periodontograma.Data
{
    public class Crud
    {
        public async Task<Periodontograma> guardar(Periodontograma periodontograma)
        {
            var result = await Hefesoft.Standard.Util.Blob.CrudBlob.postBlob(periodontograma);
            return result;
        }

        public async Task<List<Periodontograma>> get()
        {
            var result = await Hefesoft.Standard.Util.Blob.CrudBlob.getBlobByPartition(new Periodontograma());
            return result;
        }
    }
}
