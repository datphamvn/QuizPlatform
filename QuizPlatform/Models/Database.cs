using NuGet.Packaging;

namespace QuizPlatform.Models
{
    public static class Database
    {
        public static List<clsQuiz> lstQuiz = new List<clsQuiz>();
        public static List<Category> lstCategory = new List<Category>();
        //public static List<List<Answer>> lstAnswer = new List<List<Answer>>();
        public static List<Question> lstQuestion = new List<Question>();

        static Database()
        {
            var initQuiz = new List<clsQuiz>
            {
                new clsQuiz { ID = 1,
                Name = "Test",
                Description = "Test",
                openQuiz = new DateTime(2023,1,1),
                closeQuiz = new DateTime(2023, 1, 1),
                Shuffle = true,
                timeLimit = 10 },

                new clsQuiz {ID = 2,
                Name = "Test 2",
                Description = "Test",
                openQuiz = new DateTime(2023,1,1),
                closeQuiz = new DateTime(2023, 1, 1),
                timeLimit = 10},
            };
            lstQuiz.AddRange(initQuiz);

            var initCategory = new List<Category>
            {
                new Category { ID = 1,
                ParentCategory = -1,
                Name = "Category 1",
                CategoryInfo = "Test"},

                new Category { ID = 2,
                ParentCategory = -1,
                Name = "Category 2",
                CategoryInfo = "Test"},

                new Category { ID = 3,
                ParentCategory = 2,
                Name = "Category 2.2",
                CategoryInfo = "Test"},

                new Category { ID = 4,
                ParentCategory = 3,
                Name = "Category 2.2.3",
                CategoryInfo = "Test"},
            };
            lstCategory.AddRange(initCategory);

            var answers1 = new List<Answer>()
            {
                new Answer { choice = "A", weight = 0.5f },
                new Answer { choice = "B", weight = 0.25f },
                new Answer { choice = "C", weight = 0.25f }
            };

            var answers2 = new List<Answer>()
            {
                new Answer { choice = "Yes", weight = 1 },
                new Answer { choice = "No", weight = 2 }
            };
 

            var initQuestion = new List<Question>
            {
                new Question { ID = "1",
                Content = "Question 1",
                Answers = answers1,
                CategoryID = 1,
                DefaultMark = 1},

                new Question { ID = "2",
                Content = "Question 2",
                Answers = answers1,
                CategoryID = 1,
                DefaultMark = 1},

                new Question { ID = "3",
                Content = "Question 3",
                Answers = answers2,
                CategoryID = 3,
                DefaultMark = 1},

            };
            lstQuestion.AddRange(initQuestion);
        }
    }
}
