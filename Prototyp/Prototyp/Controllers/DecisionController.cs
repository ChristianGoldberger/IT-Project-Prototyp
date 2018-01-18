﻿using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Net.Http.Headers;
using Prototyp.Helper;

namespace Prototyp.Controllers
{
	public class DecisionController : Controller
	{
		//List<QuestionControl> qControls = new List<QuestionControl>();
		List<QuestionAnswer> qAnswers = new List<QuestionAnswer>();
		List<Question> questions;

		string title;
		string description;

		Decision decision = null;
		

		public ActionResult Index()
		{
			return View();
		}

		public JsonResult SaveDecision(string weight, string questionKey)
		{
			int check = 0; //0...Entscheidung erfolgreich gespeichert 

			//ToDo: Frage speichern





			//string desc = description.Trim(' '); //manchmal wird " ssadasd" gespeichert, deshalb trim
			//if (title.Equals("") && desc.Equals(""))
			//{
			//	return Json(3, JsonRequestBehavior.AllowGet);
			//}
			//if (title.Equals("")) return Json(1, JsonRequestBehavior.AllowGet);
			//if (desc.Equals("")) return Json(2, JsonRequestBehavior.AllowGet);

			//if (this.decision == null)
			//{
			//	questions = QuestionProvider.GetQuestionProvider().GetQuestions();
			//}
			//else
			//{
			//	questions = QuestionProvider.GetQuestionProvider().GetQuestions().Where(qq => this.decision.Answers.FirstOrDefault(dd => dd.QuestionKey == qq.Key) != null).ToList();
			//}

			////Question in QuestionAnswer wandeln
			//foreach (Question qqq in questions)
			//{
			//	QuestionAnswer qa = new QuestionAnswer();
			//	qa.QuestionKey = qqq.Text;
			//	qAnswers.Add(qa);
			//}
			//Session["qAnswers"] = qAnswers;

			return Json(check, JsonRequestBehavior.AllowGet);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Index([Bind(Include = "Title,Description")] Decision d)
		{
			if (ModelState.IsValid)
			{
				title = d.Title;
				description = d.Description;

				if (this.decision == null)
				{
					questions = QuestionProvider.GetQuestionProvider().GetQuestions();
				}
				else
				{
					questions = QuestionProvider.GetQuestionProvider().GetQuestions().Where(qq => this.decision.Answers.FirstOrDefault(dd => dd.QuestionKey == qq.Key) != null).ToList();
				}

				//Question in QuestionAnswer wandeln
				foreach (Question qqq in questions)
				{
					QuestionAnswer qa = new QuestionAnswer();
					qa.QuestionKey = qqq.Text;
					qAnswers.Add(qa);
				}
				Session["qAnswers"] = qAnswers;

				return RedirectToAction("New");
			}
			return View();
		}

		public ActionResult New(int? q)
		{
			int pageNumber;
			int pageSize;

			if (q.HasValue && q > 12)
			{
				q = 1;


				Decision decisionToSave = DecisionProvider.GetDecisionProvider().getDecisionHelper();
				decisionToSave.Answers = ViewBag.myQ;
				decisionToSave.Id = DecisionProvider.GetDecisionProvider().GetId();
				DecisionProvider.GetDecisionProvider().Add(decisionToSave);
				DecisionProvider.GetDecisionProvider().clearRadioValue();



				return RedirectToAction("Show");
			}

			//Ab hier weiter mit QuestionAnswer arbeiten (enthält Rating und Argumente)
			qAnswers = (List<QuestionAnswer>)Session["qAnswers"];
			QuestionAnswer myQ = qAnswers.ElementAt(q - 1 ?? 0);
			myQ.Rating = 1;
			ViewBag.myQ = myQ;


			pageSize = 1;
			pageNumber = (q ?? 1);

			return View(qAnswers.ToPagedList(pageNumber, pageSize));

			////Ab hier weiter mit QuestionAnswer arbeiten (enthält Rating und Argumente)
			//qAnswers = (List<QuestionAnswer>)Session["qAnswers"];
			//QuestionAnswer myQ = qAnswers.ElementAt(q - 1 ?? 0);
			//ViewBag.myQ = myQ;

			//int pageSize = 1;
			//int pageNumber = (q ?? 1);

			//return View(qAnswers.ToPagedList(pageNumber, pageSize));
		}

        public ActionResult DownloadFile()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~") + "Content/Decisions.xml");
            string fileName = "Decisions.xml";
            return base.File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

		public ActionResult Show()
		{
            var decisions = DecisionProvider.GetDecisionProvider().GetAll();
            ViewBag.Decisions = decisions;

			return View();
		}

        public ActionResult Details(int id)
        {
            Decision dec = DecisionProvider.GetDecisionProvider().GetAll().FirstOrDefault(d => d.Id == id);
			List<Answer> answers = new List<Answer>();
			ViewBag.Questions = QuestionProvider.GetQuestionProvider().GetQuestions();
			foreach (var s in dec.Answers)
			{
				Question q = QuestionProvider.GetQuestionProvider().GetQuestions().Single(x => (x.Key == s.QuestionKey));
				answers.Add(new Answer { Rating = s.Rating, Text = q.Text, Annotation =  s.Arguments});
			}
            ViewBag.Decisions = dec;
			ViewBag.Answers = answers;
            return View();
        }
		public ActionResult Delete(int id)
		{

			DecisionProvider.GetDecisionProvider().Delete(id);
			return RedirectToAction("Show");

		}
	}
}