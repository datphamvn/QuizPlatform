using QuizPlatform.Models;

namespace QuizPlatform.Utils
{
    public class ProcessAnkinFormat
    {
        public static int CheckQuestionFormat(string s)
        {
            // Split the question string into lines
            string[] lines = s.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length < 4) return 0;

            // Check that the last line starts with "ANSWER:"
            string lastLine = lines[lines.Length - 1].Trim();
            if (!lastLine.StartsWith("ANSWER:", StringComparison.OrdinalIgnoreCase))
            {
                return lines.Length - 1;
            }

            // Check that the answer is a single letter (A, B, C, etc.)
            string answer = lastLine.Substring(7).Trim();
            if (answer.Length != 1 || !char.IsLetter(answer[0]))
            {
                return lines.Length - 1;
            }

            // Check that all the other lines are of the form "letter. answer"
            for (int i = 1; i < lines.Length - 1; i++)
            {
                string line = lines[i].Trim();
                if (!line.Contains(".") || line.Length < 3 || !char.IsLetter(line[0]) || line[1] != '.')
                {
                    return i;
                }
            }

            // All checks passed
            return -1;
        }

        public static int getNumberLine(string s)
        {
            string[] lines = s.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return lines.Length;
        }

        public string checkFormatInput(string s)
        {
            // Split the question string into lines
            string[] lines = s.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            int startLine = 1;

            // For over the questiong
            for (int i = 0; i < lines.Length; i++)
            {
                int lineError = CheckQuestionFormat(lines[i]);
                if (lineError >= 0)
                {
                    return "Error at " + (startLine + lineError);
                }
                startLine += getNumberLine(lines[i]) + 1;
            }

            // All checks passed
            return "Success import " + lines.Length + " question(s)!";
        }

        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static Question ParseQuestion(string s)
        {
            Question question = new Question();

            // Split the question string into lines
            string[] lines = s.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            // Get question
            question.ID = GenerateRandomString(10);
            question.Content = lines[0];
            question.CategoryID = -1;
            question.DefaultMark = 1;
            question.Answers = new List<Answer>();

            // Get true answer is a single letter (A, B, C, etc.)
            string lastLine = lines[lines.Length - 1].Trim();
            string trueAnswer = lastLine.Substring(7).Trim();

            // Get answers
            for (int i = 1; i < lines.Length - 1; i++)
            {
                string line = lines[i].Trim();
                Answer answer = new Answer();
                answer.choice = line;
                if (line[0] == trueAnswer[0])
                {
                    answer.weight = 1;
                }
                else answer.weight = 0;

                question.Answers.Add(answer);
            }

            return question;
        }

        public List<Question> ParseQuestions(string s)
        {
            List<Question> questions = new List<Question>();

            // Split the question string into lines
            string[] lines = s.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            // For over the questiong
            for (int i = 0; i < lines.Length; i++)
            {
                Question question = ParseQuestion(lines[i]);
                questions.Add(question);
            }

            return questions;
        }

    }
}
