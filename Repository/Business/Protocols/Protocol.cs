using System;
using System.Collections.Generic;
using System.Linq;
namespace Repository.Business.Protocols{
    public class Protocol : List<Rule> {
        public string key { get; set; }
        private const string passMessage = "";
        public string failure { get; set; }
        internal Premise premise { get; set; }

        public bool passes(object target) {
            failure = passMessage;
            foreach (var rule in this) { 
                if(!rule.passes(target)){
                    failure = rule.failure;
                    return false;
                }
            }
            return true;
        }

        private Protocol() {
            failure = passMessage;
        }

        public static Protocol WithPremise(Premise premise, string key = "") {
            Protocol constructing = new Protocol { 
                premise = premise,
                key = key
            };
            constructing.ConstructRuleStack();
            return constructing;
        }

        private void ConstructRuleStack() { 
            if(!premise.nullable){
                Add(Rule.WithPredicateAndMessage(
                    x => x != null && !x.Equals(string.Empty),
                    string.Format("{0} cannot be null or empty", key)
                ));
            }
            if(premise.numeric){
                Add(Rule.WithPredicateAndMessage(
                    x => x.Numeric(),
                    string.Format("{0} must be a numeric value", key)
                ));
            }
            if(premise.hasMinLength){
                Add(Rule.WithPredicateAndMessage(
                    x => (premise.nullable && x == null)
                       ||(premise != null && x.ToString().Length >= premise.minLength),
                    string.Format("{0} cannot be less than {1} characters", key, premise.minLength)
                ));
            }
            if(premise.hasMaxLength){
                Add(Rule.WithPredicateAndMessage(
                    x => (premise.nullable && x == null)
                       ||(premise != null && x.ToString().Length <= premise.maxLength),
                    string.Format("{0} cannot exceed {1} characters", key, premise.maxLength)
                ));
            }
            if(premise.hasMinValue){
                Add(Rule.WithPredicateAndMessage(
                    x => x.Numeric() && Convert.ToInt32(x) <= premise.minValue,
                    string.Format("{0} cannot be less than {1}", key, premise.minValue)
                ));
            }
            if(premise.hasMaxValue){
                Add(Rule.WithPredicateAndMessage(
                    x => x.Numeric() && Convert.ToInt32(x) <= premise.maxValue,
                    string.Format("{0} cannot exceed {1}", key, premise.maxValue)
                ));
            }
            if (premise.hasStepValue) { 
                Add(Rule.WithPredicateAndMessage(
                    x => x.Numeric() && Convert.ToDouble(x) % premise.stepValue == 0,
                    string.Format("{0} must be divisible by {1}", key, premise.stepValue)
                ));
            }
        }
    }
}
