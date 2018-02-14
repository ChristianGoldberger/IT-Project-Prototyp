using Helper;
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

        //string title;
        //string description;

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

                DecisionProvider.GetDecisionProvider().setTitel(d.Title);
                DecisionProvider.GetDecisionProvider().setDescription(d.Description);

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

                return RedirectToAction("New", new
                {
                    q = 1

                });
            }
            return View();
        }

        public ActionResult CheckRadio(FormCollection frm)
        {

            DecisionProvider.GetDecisionProvider().addRatingtoList(int.Parse(frm["Entscheidung"].ToString()));
            DecisionProvider.GetDecisionProvider().addArgumetnToList(frm["TextFeldArgument"].ToString());


            return RedirectToAction("New", new
            {
                q = DecisionProvider.GetDecisionProvider().getQuestion()

            });
        }

    



        public ActionResult New(int? q)
        {
            int pageNumber;
            int pageSize;

            if (q.HasValue && q > QuestionProvider.GetQuestionProvider().GetQuestions().Count)
            {

                DecisionProvider.GetDecisionProvider().setQuestion(1);
                DecisionProvider.GetDecisionProvider().clearQuestionAnswer();
                return RedirectToAction("Show");
            }

            //Ab hier weiter mit QuestionAnswer arbeiten (enthält Rating und Argumente)
            qAnswers = (List<QuestionAnswer>)Session["qAnswers"];
            QuestionAnswer myQ = qAnswers.ElementAt(q - 1 ?? 0);

            //ToDo: Rating von den RadioButtons bekommen!?!?!?!?!?
            //myQ.Rating = DecisionProvider.GetDecisionProvider().getRating();



            DecisionProvider.GetDecisionProvider().addQuestonAnswer(myQ);
            ViewBag.myQ = myQ;



            pageSize = 1;
            pageNumber = (q ?? 1);

            DecisionProvider.GetDecisionProvider().riseQuestion();

            return View(qAnswers.ToPagedList(pageNumber, pageSize));


        }
        /* public ActionResult Safe(int id)
        {
            Decision dec = DecisionProvider.GetDecisionProvider().GetAll().FirstOrDefault(d => d.Id == id);
           var checkBox =  


            //decision.ActualPerformance = Convert.ToInt32()
        }*/

        [HttpPost]
        public ActionResult Upload()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/"), fileName);
                    file.SaveAs(path);
                }
            }

            Show();
            return View("Show");
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
			int i = 0;
            Decision dec = DecisionProvider.GetDecisionProvider().GetAll().FirstOrDefault(d => d.Id == id);
			Session["ID"] = id;
            ViewBag.QuestionAnswer = dec.Answers;
            return View();
        }

        public ActionResult DetailsPerformed(FormCollection frm)
        {
			int id = int.Parse(Session["ID"].ToString());
			int actualPerformance = int.Parse(frm["Entscheidung"].ToString());
			//Decision dec = DecisionProvider.GetDecisionProvider().GetAll().FirstOrDefault(d => d.Id == id);
			//Decision decision = DecisionProvider.GetDecisionProvider().GetAll().Single(s => (s.Id == id));

			return View(decision);
        }

        public ActionResult Delete(int id)
        {

            DecisionProvider.GetDecisionProvider().Delete(id);
            return RedirectToAction("Show");

        }
    }


}