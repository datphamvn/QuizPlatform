using Microsoft.AspNetCore.Mvc;
using QuizPlatform.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using SautinSoft.Document;
using NuGet.DependencyResolver;
using QuizPlatform.Utils;

namespace QuizPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var lstQuiz = Database.lstQuiz;
            return View(lstQuiz);
        }

        public IActionResult ManageTab()
        {
            var tupleModel = new Tuple<List<Category>, List<Question>>(Database.lstCategory, Database.lstQuestion);
            return View(tupleModel);
        }

        [HttpPost]
        public IActionResult SaveCategory(Category model)
        {
            Database.lstCategory.Add(model);
            return RedirectToAction("ManageTab");
        }

        [HttpPost]
        public async Task<IActionResult> upload(IFormFile file)
        {
            StringBuilder strbuild = new StringBuilder();
            try
            {
                if (file == null || file.Length == 0)
                {
                    TempData["mess"] = "File is not selected or has zero length!";
                    return RedirectToAction("ManageTab");
                }

                var fileName = Path.GetFileName(file.FileName);

                // Check if the file extension is valid
                var fileExtension = Path.GetExtension(fileName);
                if (fileExtension != ".txt" && fileExtension != ".doc" && fileExtension != ".docx")
                {
                    TempData["mess"] = "Invalid file type. Only text files are allowed.";
                    return RedirectToAction("ManageTab");
                }

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Document", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                if (!string.IsNullOrEmpty(filePath))
                {
                    if (fileExtension == ".txt")
                    {
                        using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                        {
                            strbuild.Append(await sr.ReadToEndAsync());
                        }
                    }
                    else
                    {
                        DocumentCore dc = DocumentCore.Load(filePath);
                        // Get all Run elements from document.
                        foreach (Run run in dc.GetChildElements(true, ElementType.Run))
                        {
                            strbuild.Append(run.Text);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["mess"] = ex.Message;
                return RedirectToAction("ManageTab");
            }

            string getStatus = new ProcessAnkinFormat().checkFormatInput(strbuild.ToString());
            TempData["mess"] = getStatus;

            if (getStatus[0] == 'S')
            {
                List<Question> lstParse = new ProcessAnkinFormat().ParseQuestions(strbuild.ToString());
                foreach (Question question in lstParse)
                {
                    Database.lstQuestion.Add(question);
                }
            }    
            

            return RedirectToAction("ManageTab");

            //return new JsonResult(new { result = strbuild.ToString(), mess = new ProcessAnkinFormat().checkFormatInput(strbuild.ToString()) });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}