namespace QuizPlatform.Models
{
    public class Category
    {
        public int ID { get; set; }
        public int ParentCategory { get; set; }
        public string Name { get; set; }
        public string CategoryInfo { get; set; } 
        public List<Category> Children { get; set; } = new List<Category>();
    }
}
