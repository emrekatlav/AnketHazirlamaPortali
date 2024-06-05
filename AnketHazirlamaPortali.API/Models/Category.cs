namespace AnketHazirlamaPortali.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<Survey> Surveys { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
