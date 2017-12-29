using System.ComponentModel.DataAnnotations;

namespace Helper {
    public class Question {
        [Required]
        [Key]
        public string Key { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a positive number")]
        public double Weight { get; set; }

        [Required]
        public IntuitionEffect Effect { get; set; }

        public Question() { }
    }
}
