namespace QuizPlatform.Models
{
    public class clsQuiz
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime openQuiz { get; set; }
        public DateTime closeQuiz { get; set;}
        public int timeLimit { get; set; }
        public string unitTimeLimit { get; set; }
        public int maxGrade { get; set; }
        public bool Shuffle { get; set; } = false;

        public List<string> lstIDQuestion { get; set; } = new List<string>();

        /*
        public List<clsQuiz> initQuiz()
        {
            List<clsQuiz> lstQuiz = new List<clsQuiz>();
            
            lstQuiz.Add(new clsQuiz()
            {
                ID = 1,
                Name = "Test",
                Description = "Test",
                openQuiz = new DateTime(2023,1,1),
                closeQuiz = new DateTime(2023, 1, 1),
                timeLimit = 10
            });

            lstQuiz.Add(new clsQuiz()
            {
                ID = 2,
                Name = "Test 2",
                Description = "Test 2",
                openQuiz = new DateTime(2023, 1, 1),
                closeQuiz = new DateTime(2023, 1, 1),
                timeLimit = 10
            });
            
            return lstQuiz;
        }*/

    }
}
