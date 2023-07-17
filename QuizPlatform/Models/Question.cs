namespace QuizPlatform.Models
{
    public class Answer
    {
        public string choice { get; set; }
        public float weight { get; set; }
    }
    public class Question
    {
        public string ID { get; set; }
        public string Content { get; set; }
        public List<Answer> Answers { get; set; }
        public int CategoryID { get; set; }
        public int DefaultMark { get; set; }

    }
}
