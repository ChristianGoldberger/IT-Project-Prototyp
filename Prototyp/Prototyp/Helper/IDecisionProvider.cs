using System.Collections.Generic;

namespace Helper {
    interface IDecisionProvider {
        void Add(Decision decision);
        List<Decision> GetAll();
        void Save(Decision decision);
    }
}