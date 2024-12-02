using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class DataSetTable
    {
        public DataSetTable()
        {
            Columns = new List<DataSetColumns>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public ICollection<DataSetColumns> Columns { get; set; }
    }
}
