using System.Collections.Generic;
using System.Linq;

namespace Helper {
    public class RatingCalculator {
        public double GetRating(List<QuestionAnswer> answers) {
            double rating = 0;
            var questions = QuestionProvider.GetQuestionProvider().GetQuestions();

            foreach (var answer in answers) {
                var question = questions.FirstOrDefault(q => q.Key == answer.QuestionKey);
                if (question != null) {
                    if (question.Effect == IntuitionEffect.Positiv) {
                        rating += answer.Rating * question.Weight;
                    }
                    else if (question.Effect == IntuitionEffect.Negativ) {
                        rating += ((7 - (answer.Rating)) * question.Weight);
                    }
                }
            }

            double baseValue = 0;
            foreach (var question in questions.Where(q => q.Effect != IntuitionEffect.Neutral && answers.FirstOrDefault(a => a.QuestionKey == q.Key) != null)) {
                baseValue += (1 * question.Weight);
            }

            rating = rating / baseValue;

            return rating;
        }
    }
}
