using System.Collections.Generic;

namespace Helper {
    interface IQuestionProvider {
        //void Add(Question quesion); obsolet
        List<Question> GetQuestions();
        void Save(Question quesion);
    }
}