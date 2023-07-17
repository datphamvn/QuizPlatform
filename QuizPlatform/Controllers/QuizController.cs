using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using QuizPlatform.Models;
using Rotativa;
using Rotativa.AspNetCore;
using System.Reflection;

namespace QuizPlatform.Controllers
{
    public class QuizController : Controller
    {
        public IActionResult Index(int idQuiz)
        {
            var quiz = Database.lstQuiz.SingleOrDefault(m => m.ID == idQuiz);
            return View(quiz);
        }

        public IActionResult Create()
        {
            return View(new clsQuiz());
        }


        [HttpPost]
        public IActionResult SaveQuiz(clsQuiz model)
        {
            Database.lstQuiz.Add(model);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Editing(int idQuiz)
        {
            var quiz = Database.lstQuiz.SingleOrDefault(m => m.ID == idQuiz);
            var tupleModel = new Tuple<List<Category>, List<Question>, clsQuiz>(Database.lstCategory, Database.lstQuestion, quiz);
            return View(tupleModel);
        }

        public IActionResult Start(int idQuiz)
        {
            var quiz = Database.lstQuiz.SingleOrDefault(m => m.ID == idQuiz);
            var tupleModel = new Tuple<List<Question>, clsQuiz>(Database.lstQuestion, quiz);
            return View(tupleModel);
        }

        public IActionResult Result(int idQuiz)
        {
            var quiz = Database.lstQuiz.SingleOrDefault(m => m.ID == idQuiz);
            var tupleModel = new Tuple<List<Question>, clsQuiz>(Database.lstQuestion, quiz);
            return View(tupleModel);
        }

        public IActionResult ExportPDF(int idQuiz)
        {
            var quiz = Database.lstQuiz.SingleOrDefault(m => m.ID == idQuiz);
            var tupleModel = new Tuple<List<Question>, clsQuiz>(Database.lstQuestion, quiz);
            return View(tupleModel);
        }

        [HttpPost]
        public IActionResult UpdateQuesionOfQuiz(clsQuiz model)
        {
            var quiz = Database.lstQuiz.SingleOrDefault(m => m.ID == model.ID);
            quiz.lstIDQuestion = model.lstIDQuestion;
            return RedirectToAction("Editing", new { idQuiz = model.ID });
        }

        [HttpPost]
        public IActionResult UpdateSettingsQuiz(clsQuiz model)
        {
            var quiz = Database.lstQuiz.SingleOrDefault(m => m.ID == model.ID);
            quiz.maxGrade = model.maxGrade;
            quiz.Shuffle = model.Shuffle;
            return RedirectToAction("Index", new { idQuiz = model.ID });
        }
    }
}
