﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace Helper {
    class QuestionProvider : IQuestionProvider {
        static string Q_PATH = "~/Content/Questions.xml";

        static QuestionProvider provider = null;

        List<Question> questions;

        private QuestionProvider() {
            questions = new List<Question>();

            if (File.Exists(Q_PATH)) {
                try {
                    questions = FromXML(File.ReadAllText(Q_PATH));
                }
                catch {
                    questions = GetDefaultQuestions();
                }
            }
            else {
                questions = GetDefaultQuestions();
            }
        }

        public static QuestionProvider GetQuestionProvider() {
            if (provider == null) {
                provider = new QuestionProvider();
            }

            return provider;
        }

        public List<Question> GetQuestions() {
            return questions;
        }

        public void DeleteFileFromFolder(string StrFilename)
        {
            string strPhysicalFolder = HttpContext.Current.Server.MapPath("..\\");
            string strFileFullPath = strPhysicalFolder + StrFilename;
            if (File.Exists(strFileFullPath))
            {
                File.Delete(strFileFullPath);
            }
        }

        public void WriteFileFromFolder(string StrFilename, List<Question> questions)
        {
            string strPhysicalFolder = HttpContext.Current.Server.MapPath("..\\");
            string strFileFullPath = strPhysicalFolder + StrFilename;
            if (File.Exists(strFileFullPath))
            {
                File.WriteAllText(strFileFullPath, ToXML(questions));
            }
        }

        public void Add(Question quesion) {
            questions.Add(quesion);

            DeleteFileFromFolder(Q_PATH);
            WriteFileFromFolder(Q_PATH, questions);
        }

        public void Save(Question quesion) {
            questions.Remove(questions.FirstOrDefault(d => d.Key == quesion.Key));
            questions.Add(quesion);

            DeleteFileFromFolder(Q_PATH);
            WriteFileFromFolder(Q_PATH, questions);
        }

        private static string ToXML(List<Question> decisions) {
            using (StringWriter stringWriter = new StringWriter(new StringBuilder())) {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Question>));
                xmlSerializer.Serialize(stringWriter, decisions);
                return stringWriter.ToString();
            }
        }

        private static List<Question> FromXML(string xml) {
            using (StringReader stringReader = new StringReader(xml)) {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Question>));
                return (List<Question>)serializer.Deserialize(stringReader);
            }
        }

        private static List<Question> GetDefaultQuestions() {
            return new List<Question>() {
                new Question() {
                    Key = "Risks",
                    Text = "Wie hoch ist das Risiko?",
                    Effect = IntuitionEffect.Negative,
                    Weight = 1
                },
                new Question() {
                    Key = "Time",
                    Text = "Muss die Entscheidung schnell getroffen werden?",
                    Effect = IntuitionEffect.Positive,
                    Weight = 1
                },
                new Question() {
                    Key = "Emotional",
                    Text = "Ist die Entscheidung mit Emotionen behaftet?",
                    Effect = IntuitionEffect.Positive,
                    Weight = 1
                },
                new Question() {
                    Key = "PastDecisions",
                    Text = "War das Bauchgefühl bereits bei ähnlichen Entscheidungen erfolgreich?",
                    Effect = IntuitionEffect.Positive,
                    Weight = 1
                },
                new Question() {
                    Key = "AllInforrmation",
                    Text = "Sind alle benötigten Informationen zur Entscheidungsfindung verfügbar?",
                    Effect = IntuitionEffect.Negative,
                    Weight = 1
                },
                new Question() {
                    Key = "AllInforrmationProcessed",
                    Text = "Können auch alle Informationen berücksichtigt werden?",
                    Effect = IntuitionEffect.Negative,
                    Weight = 1
                },
                new Question() {
                    Key = "BiasedInfo",
                    Text = "Könnten die Informationen unausgewogen/tendenziös sein?",
                    Effect = IntuitionEffect.Positive,
                    Weight = 1
                },
                new Question() {
                    Key = "CostMoreInfo",
                    Text = "Wird erwartet, dass zusätzliche Informationen wenig Mehrwert liefern?",
                    Effect = IntuitionEffect.Positive,
                    Weight = 1
                },
                new Question() {
                    Key = "CostMoreInfo",
                    Text = "Gibt es rechtliche Einschränkungen bei der Entscheidungsfindung?",
                    Effect = IntuitionEffect.Negative,
                    Weight = 1
                },
                new Question() {
                    Key = "DefaultOption",
                    Text = "Ist eine Standardoption verfügbar?",
                    Effect = IntuitionEffect.Negative,
                    Weight = 1
                }
            };
        }
    }
}
