using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Helper {
    public class Decision {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public int? ActualPerformance { get; set; }

        public List<QuestionAnswer> Answers { get; set; }

        public DateTime DecisionEntered { get; set; }
    }
}
