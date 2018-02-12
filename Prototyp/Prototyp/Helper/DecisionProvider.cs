using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.Xml.Serialization;

namespace Helper
{
	class DecisionProvider : IDecisionProvider
	{
		static string D_PATH = HostingEnvironment.MapPath(@"~/Content/Decisions.xml");

		static DecisionProvider provider = null;

		List<Decision> decisions;
        List<QuestionAnswer> questionAnswer = new List<QuestionAnswer>();
        List<int> rating = new List<int>();
        List<string> argument = new List<string>();
        private string description;
        private string titel;
        private int Rating;
        private int question = 1;
       


		private DecisionProvider()
		{
			decisions = new List<Decision>();

			if (File.Exists(D_PATH))
			{
				try
				{
					decisions = FromXML(File.ReadAllText(D_PATH));
				}
				catch
				{

				}
			}
		}
        public void Reload() => provider = new DecisionProvider();

        public static DecisionProvider GetDecisionProvider()
		{
			if (provider == null)
			{
				provider = new DecisionProvider();
			}

			return provider;
		}

		public List<Decision> GetAll()
		{
			return decisions;
		}
        public void setTitel(string t){
            this.titel = t;
        }
        public void riseQuestion(){
            this.question++;
        }
        public int getQuestion(){
            return this.question;
        }
       public void setQuestion(int q)
        {
            this.question = q;
        }   
        public void setRating(int rating){
            this.Rating = rating;
        }
      
        public void addRatingtoList(int r){
            this.rating.Add(r);
        }
        public void addArgumetnToList(string s){
            this.argument.Add(s);
        }

        public void setDescription(string d){
            this.description = d;
        }
		
		public void Delete(int id)
		{
			for (int i = 0; i < decisions.Count(); i++)
			{
				if (decisions.ElementAt(i).Id.Equals(id))
				{
					decisions.RemoveAt(i);
				}
			}

		}
        public void setArguments(int id, int weight, string argument){
            this.questionAnswer.ElementAt(id).Arguments = argument;
            this.questionAnswer.ElementAt(id).Rating= weight;
        }
        public void addQuestonAnswer(QuestionAnswer q){
            this.questionAnswer.Add(q);
        }
        public void clearQuestionAnswer(){
            Decision decision = new Decision();
            decision.Title = this.titel;
            decision.Description = this.description;
            for (int i = 0; i < this.questionAnswer.Count; i++){
                this.questionAnswer.ElementAt(i).Rating= this.rating.ElementAt(i);
                this.questionAnswer.ElementAt(i).Arguments= this.argument.ElementAt(i);
            }
            decision.Answers = this.questionAnswer;
           
            this.Add(decision);
           
        }
		public void Add(Decision decision)
		{
			decision.Id = decisions.Count == 0 ? 0 : decisions.Max(d => d.Id) + 1;
			decisions.Add(decision);
            this.questionAnswer = new List<QuestionAnswer>();
            this.rating = new List<int>();
            this.argument = new List<string>();
			File.Delete(D_PATH);
			File.WriteAllText(D_PATH, ToXML(decisions));
		}

		public void Save(Decision decision)
		{
			decisions.Remove(decisions.FirstOrDefault(d => d.Id == decision.Id));
			decisions.Add(decision);

			File.Delete(D_PATH);
			File.WriteAllText(D_PATH, ToXML(decisions));
		}

		private static string ToXML(List<Decision> decisions)
		{
			using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Decision>));
				xmlSerializer.Serialize(stringWriter, decisions);
				return stringWriter.ToString();
			}
		}

		private static List<Decision> FromXML(string xml)
		{
			using (StringReader stringReader = new StringReader(xml))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(List<Decision>));
				return (List<Decision>)serializer.Deserialize(stringReader);
			}
		}
	}
}
