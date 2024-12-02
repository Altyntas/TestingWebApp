namespace DAL.Models
{
    public class Queue
    {
        public Guid Id { get; set; }
        public Guid? EntityId { get; set; }
        public int QueueType { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        public bool IsCompleted { get; set; }
        public Guid? DatasetTableGuid { get ; set; }
    }
}
