namespace TestingWebApp.ViewModel
{
    public class DatasetsVM
    {
        public DatasetsVM()
        {
            Columns = new List<ColumnsVM>();
        }

        public string? Id { get; set; }
        public string? Name { get; set; }

        public ICollection<ColumnsVM> Columns { get; set; }
    }
}
