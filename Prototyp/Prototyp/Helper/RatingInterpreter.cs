namespace Helper {
    public class RatingInterpreter {
        public string GetResult(double rating) {
            string result;

            if (rating <= 3.25) {
                result = "Dem Bauchgefühl sollte nicht vertraut werden!";
            }
            else if (rating >= 3.75) {
                result = "Dem Bauchgefühl kann vertraut werden!";
            }
            else {
                result = "Keine Empfehlung möglich.";
            }

            return result;
        }
    }
}
