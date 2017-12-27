using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Helper {
    class DecisionProvider : IDecisionProvider {
        static string D_PATH = @"~\Content\Decisions.xml";

        static DecisionProvider provider = null;

        List<Decision> decisions;

        private DecisionProvider() {
            decisions = new List<Decision>();

            if (File.Exists(D_PATH)) {
                try {
                    decisions = FromXML(File.ReadAllText(D_PATH));
                }
                catch {

                }
            }
        }

        public static DecisionProvider GetDecisionProvider() {
            if(provider == null) {
                provider = new DecisionProvider();
            }

            return provider;
        }

        public List<Decision> GetAll() {
            return decisions;
        }

        public void Add(Decision decision) {
            decision.Id =  decisions.Count == 0 ? 0 : decisions.Max(d => d.Id) + 1;
            decisions.Add(decision);

            File.Delete(D_PATH);
            File.WriteAllText(D_PATH, ToXML(decisions));
        }

        public void Save(Decision decision) {
            decisions.Remove(decisions.FirstOrDefault(d => d.Id == decision.Id));
            decisions.Add(decision);

            File.Delete(D_PATH);
            File.WriteAllText(D_PATH, ToXML(decisions));
        }

        private static string ToXML(List<Decision> decisions) {
            using (StringWriter stringWriter = new StringWriter(new StringBuilder())) {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Decision>));
                xmlSerializer.Serialize(stringWriter, decisions);
                return stringWriter.ToString();
            }
        }

        private static List<Decision> FromXML(string xml) {
            using (StringReader stringReader = new StringReader(xml)) {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Decision>));
                return (List<Decision>)serializer.Deserialize(stringReader);
            }
        }
    }
}
