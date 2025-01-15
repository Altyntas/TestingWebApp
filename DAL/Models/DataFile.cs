namespace DAL.Models
{
    public class DataFile
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string ContentType { get; set; }
        public DateTime UploadDate { get; set; }
        public Byte[] Data { get; set; }

        public virtual ICollection<DataFileColumns> Columns { get; set; }
        public virtual ICollection<DataSetTable> DataSetTables { get; set; }
    }
}
