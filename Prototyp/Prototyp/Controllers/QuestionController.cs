using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Prototyp.Controllers
{
    public class QuestionController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Questions = QuestionProvider.GetQuestionProvider().GetQuestions();

            return View();
        }
		public ActionResult Edit(string id)
		{
			if (id == null)
			{
				return View(new Question()); //New Question
			}
			Question q = QuestionProvider.GetQuestionProvider().GetQuestions().Single(s => (s.Key == id));
			if (q == null)
			{
				return HttpNotFound();
			}
			return View(q);
		}

        public ActionResult Delete(string key){
            QuestionProvider.GetQuestionProvider().deleteQuestion(key);

            return RedirectToAction("Index");

        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Key,Text,Weight,Effect")] Question q)
		{
			if (ModelState.IsValid)
			{
				QuestionProvider.GetQuestionProvider().Save(q);
				return RedirectToAction("Index");
			}
			return View(q);
		}
	}

   
}