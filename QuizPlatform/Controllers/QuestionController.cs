using Microsoft.AspNetCore.Mvc;
using QuizPlatform.Models;

namespace QuizPlatform.Controllers
{
    public class QuestionController : Controller
    {
        public IActionResult Index()
        {
            var tupleModel = new Tuple<List<Category>, List<Question>>(Database.lstCategory, Database.lstQuestion);
            return View(tupleModel);
        }

        public IActionResult Create()
        {
            return View(new Question());
        }

        [HttpPost]
        public IActionResult SaveQuestion(Question model, string typeSubmit)
        {
            if (ModelState.IsValid == true)
            {
                Database.lstQuestion.Add(model);

                foreach (var question in Database.lstQuestion)
                {
                    Console.WriteLine("Question ID: " + question.ID);
                    Console.WriteLine("Content: " + question.Content);
                    Console.WriteLine("Category ID: " + question.CategoryID);
                    Console.WriteLine("Default Mark: " + question.DefaultMark);

                    Console.WriteLine("Answers:");
                    foreach (var answer in question.Answers)
                    {
                        Console.WriteLine("  Choice: " + answer.choice);
                        Console.WriteLine("  Weight: " + answer.weight);
                    }
                }
                Console.WriteLine("Type: " + typeSubmit);
                if(typeSubmit == "Save")
                    return RedirectToAction("ManageTab", "Home");
                return RedirectToAction("Edit", new { IDQuestion = model.ID });
            }
            else
            {
                ModelState.AddModelError("", "Error when save data!");
                return View(model);
            }
        }

        public IActionResult Edit(string IDQuestion)
        {
            var question = Database.lstQuestion.SingleOrDefault(m => m.ID == IDQuestion);
            var tupleModel = new Tuple<List<Category>, List<Question>, Question>(Database.lstCategory, Database.lstQuestion, question);
            return View(tupleModel);
        }

        [HttpPost]
        public IActionResult Update(Question model)
        {
            var question = Database.lstQuestion.SingleOrDefault(m => m.ID == model.ID);
            question.Content = model.Content;
            question.CategoryID = model.CategoryID;
            question.DefaultMark = model.DefaultMark;

            for(int i=0; i < question.Answers.Count; i++)
            {
                question.Answers[i].choice = model.Answers[i].choice;
                question.Answers[i].weight = model.Answers[i].weight;
            }    

            var tupleModel = new Tuple<List<Category>, List<Question>, Question>(Database.lstCategory, Database.lstQuestion, question);
            return RedirectToAction("Index");
        }

    }
}
