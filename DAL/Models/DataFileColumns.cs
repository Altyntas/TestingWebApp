using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class DataFileColumns
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid DataFileId { get; set; }
        public int ColumnTypeId { get; set; }

        public virtual ColumnTypes ColumnTypes { get; set; }
        public virtual DataFile DataFile { get; set; }
    }
}
