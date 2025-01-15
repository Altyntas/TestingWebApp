namespace DAL.Models
{
    public class ColumnTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DataFileColumns> Columns { get; set; }
    }
}
