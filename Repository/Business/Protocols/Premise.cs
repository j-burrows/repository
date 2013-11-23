using System;
namespace Repository.Business.Protocols{
    public class Premise : BasePremise{
        public bool nullable { get; set; }
        public bool numeric { get; set; }
        public bool hidden { get; set; }

        public override int maxLength { get {
            return base.maxLength;
        } set{
            base.maxLength = value;
            hasMaxLength = true;
        } }
        public override int minLength { get {
            return base.minLength;
        } set{
            base.minLength = value;
            hasMinLength = true;
        } }
        public bool hasMaxLength { get; private set; }
        public bool hasMinLength { get; private set; }

        public override int maxValue { get {
            return base.maxValue;
        } set{
            base.maxValue = value;
            hasMaxValue = true;
        } }
        public override int minValue { get {
            return base.minValue;
        } set{
            base.minValue = value;
            hasMinValue = true;
        }}
        public bool hasMaxValue { get; private set; }
        public bool hasMinValue { get; private set; }

        public override double stepValue { get {
            return base.stepValue;
        } set{
            base.stepValue = value;
            hasStepValue = true;
        } }
        public bool hasStepValue { get; private set; }

        public Premise() {
            nullable = true;
            hidden = false;
            numeric = false;
        }

        public Premise Clone() {
            Premise constructing = CloneNonflaggables();
            if(hasStepValue){
                constructing.stepValue = stepValue;
            }
            if(hasMinLength){
                constructing.minLength = minLength;
            }
            if(hasMaxLength){
                constructing.maxLength = maxLength;
            }
            if(hasMinValue){
                constructing.minValue = minValue;
            }
            if(hasMaxValue){
                constructing.maxValue = maxValue;
            }
            return constructing;       
        }

        public Premise CloneNonflaggables() {
            return new Premise { 
                hidden = hidden,
                nullable = nullable,
                numeric = numeric
            };
        }
    }

    //Used to allow custom get sets without verbose "__" prefixes
    public class BasePremise {
        public virtual int maxLength { get; set; }
        public virtual int minLength { get; set; }

        public virtual int maxValue { get; set; }
        public virtual int minValue { get; set; }

        public virtual double stepValue { get; set; }
    }
}
