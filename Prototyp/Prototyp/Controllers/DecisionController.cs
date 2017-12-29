﻿using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;

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
            //Ab hier weiter mit QuestionAnswer arbeiten (enthält Rating und Argumente)
            qAnswers = (List<QuestionAnswer>)Session["qAnswers"];
            QuestionAnswer myQ = qAnswers.ElementAt(q - 1 ?? 0);
            ViewBag.myQ = myQ;

            int pageSize = 1;
            int pageNumber = (q ?? 1);

            return View(qAnswers.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Show()
        {
            return View();
        }
    }
}