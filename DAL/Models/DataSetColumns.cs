using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class DataSetColumns
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string? Description { get; set; }
        public int? Length { get; set; }
        public Guid DataSetTableId { get; set; }

        public virtual DataSetTable DataSetTable { get; set; }
    }
}
