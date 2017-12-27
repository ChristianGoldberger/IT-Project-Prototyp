using System.Collections.Generic;

namespace Helper {
    interface IQuestionProvider {
        void Add(Question quesion);
        List<Question> GetQuestions();
        void Save(Question quesion);
    }
}