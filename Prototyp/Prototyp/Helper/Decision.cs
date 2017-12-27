using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper {
    public class Decision {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? ActualPerformance { get; set; }

        public List<QuestionAnswer> Answers { get; set; }

        public DateTime DecisionEntered { get; set; }
    }
}
