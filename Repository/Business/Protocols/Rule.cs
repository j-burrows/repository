using System;
namespace Repository.Business.Protocols{
    public class Rule {
        private Predicate<object> rule;
        public string failure { get; private set; }

        public bool passes(object checking) {
            return rule(checking);
        }

        public static Rule WithPredicateAndMessage(Predicate<object> rule, string failure) {
            return new Rule { 
                rule = rule,
                failure = failure
            };
        }

        public static Rule EmailStandard() {
            return new Rule{
                rule = new Predicate<object>(x => System.Text.RegularExpressions.Regex.IsMatch(x.ToString(),
                    "^[a-zA-Z0-9]+@[a-zA-Z0-9]+(.[a-zA-Z0-9]+)+$")),
                failure = "Email must contain some characters, an '@', and a '.'"
            };
        }
    }
}
