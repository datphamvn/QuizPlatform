using Microsoft.CodeAnalysis.Options;
using Newtonsoft.Json.Linq;
using QuizPlatform.Models;
using System.Text;

namespace QuizPlatform.Utils
{
    public static class HtmlHelpers
    {

        public static int CountQuestionsInCategory(List<Question> questions, int categoryID)
        {
            return questions.Count(q => q.CategoryID == categoryID);
        }

        public static string RenderChildren(IEnumerable<Category> categories, List<Question> questions, int level)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var category in categories)
            {
                string indent = "";
                for (int i = 0; i < level * 4; i++)
                {
                    indent += "&nbsp;";
                }

                int numQ = CountQuestionsInCategory(questions, category.ID);

                for (int i = 0; i < level; i++) { }
                if(numQ != 0)
                    sb.AppendFormat("<option value=\"{0}\">{1}{2} ({3})</option>", category.ID, indent, category.Name, numQ);
                else
                    sb.AppendFormat("<option value=\"{0}\">{1}{2}</option>", category.ID, indent, category.Name);
                if (category.Children.Any())
                {
                    sb.Append(RenderChildren(category.Children, questions, level + 1));
                }
            }
            return sb.ToString();
        }


    }
}
