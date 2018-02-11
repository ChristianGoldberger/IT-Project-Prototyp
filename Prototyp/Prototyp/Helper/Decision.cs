using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Helper {
    public class Decision {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Bitte fügen Sie eine Titel ein!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Bitte fügen Sie eine Beschreibung ein!")]
        public string Description { get; set; }

        public int ActualPerformance { get; set; }

        public List<QuestionAnswer> Answers { get; set; }

        public DateTime DecisionEntered { get; set; }
    }
}
