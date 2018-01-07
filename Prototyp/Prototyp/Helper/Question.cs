using System.ComponentModel.DataAnnotations;

namespace Helper {
    public class Question {
        [Required(ErrorMessage = "Bitte fügen Sie einen Key ein!")]
        [Key]
        public string Key { get; set; }

        [Required(ErrorMessage = "Bitte fügen Sie eine Beschreibung ein!")]
        public string Text { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Bitte fügen Sie eine positive Nummer ein!")]
        public double Weight { get; set; }

        [Required]
        public IntuitionEffect Effect { get; set; }

        public Question() { }
    }
}
