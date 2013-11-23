using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Repository.Helpers;
namespace Repository.Business.Protocols{
    public static class Helpers{
        private static string InputType(Type owner, string specialType = "") {
            if (!specialType.Equals(string.Empty)) {
                return specialType;
            }
            else if (owner == typeof(int) || owner == typeof(double) 
                  || owner == typeof(long)){
                return "number";
            }
            else if (owner == typeof(Date)){
                return "Date";
            }
            else if (owner == typeof(Time)){
                return "Time";
            }
            else if (owner == typeof(DateTime)){
                return "Datetime";
            }
            return "text";
        }

        public static HtmlString HtmlInput(
            this Protocol protocol, object target, string specialType = "") {
            Premise premise = protocol.premise;
            StringBuilder builder = new StringBuilder();

            if (specialType.Equals("") && premise.hidden) {
                specialType = "hidden";
            }

            builder.Append("<input");
            string type = InputType(target.GetType(), specialType);
            builder.Append(" type=\"" + type + "\"");
            builder.Append(" name=\"" + protocol.key + "\"");
            builder.Append(" value=\"" + target.ToString() + "\"");
            if(!protocol.failure.Equals(string.Empty)){
                builder.Append(" class=\"invalid\"");
            }

            //Goes through the list of premises in the protocol and adds tags to help abide.
            if (!protocol.premise.nullable) {
                builder.Append(" required=\"required\"");
            }
            if (protocol.premise.hasMinLength) {
                builder.Append(" minlength=\"" + protocol.premise.minLength +"\"");
            }
            if (protocol.premise.hasMaxLength) {
                builder.Append(" maxlength=\"" + protocol.premise.maxLength +"\"");
            }
            if(protocol.premise.hidden){
                builder.Append(" hidden=\"hidden\"");
            }
            if(protocol.premise.hasMinValue && type=="number"){
                builder.AppendFormat(" min=\"{0}\"", protocol.premise.minValue);
            }
            if(protocol.premise.hasMaxValue && type=="number"){
                builder.AppendFormat(" max=\"{0}\"", protocol.premise.maxValue);
            }
            if(protocol.premise.hasStepValue && type=="number"){
                builder.AppendFormat(" step=\"{0}\"", protocol.premise.stepValue);
            }
            if (!protocol.premise.hidden){
                builder.AppendFormat(" placeholder=\"{0}\"", protocol.key.Replace('_', ' '));
            }
            builder.AppendLine(" />");

            builder.Append("<span class=\"ValidationError\">");
            builder.Append(protocol.failure);
            builder.Append("</span>");

            return new HtmlString(builder.ToString());
        }

        public static HtmlString JsValidation(
            this List<Protocol> protocols, string identifier, 
            string updateTarget="#mainContent", bool confirmation=false, string additionConditions="") {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat(@"
            $('.{0}').submit(function(event){{
                var isValid = false;",
            identifier);

            foreach (var protocol in protocols) {
                builder.Append(protocol.JsValidation());
            }

            builder.Append(additionConditions);

            builder.AppendFormat(@"
                $.ajax({{
                    type: 'POST',
                    dataType: 'html',
                    url: $(this).attr('action'),
                    data: $(this).serialize(),
                    success: function (data) {{
                        var ViewResult = $('<div>' + data + '</div>');
                        $('{0}').html(ViewResult.find('{0}').html());
                    }}
                }});
            ", updateTarget);

            builder.Append(@"
                event.preventDefault();
                return false;
            });
            ");

            return new HtmlString(builder.ToString());
        }

        public static HtmlString JsValidation(this Protocol protocol) {
            StringBuilder builder = new StringBuilder();
            string readfriendlyKey = protocol.key.Replace('_', ' ');
            bool switchStarted = false;

            builder.AppendFormat(" var {0}Input = $(this).children('input[name={0}]'); ", protocol.key);
            if(!protocol.premise.nullable){
                builder.AppendFormat(@"
                    if({0}Input.val().length == 0){{
                        addError({0}Input, '{1} cannot be empty or null.');
                        isValid = false;
                    }}", protocol.key, readfriendlyKey
                );
                switchStarted = true;
            }
            if(protocol.premise.hasMinLength){
                if(switchStarted){
                    builder.Append(" else");
                }
                builder.AppendFormat(@"
                    if({0}Input.val().length < {1}){{
                        addError({0}Input, '{2} must be at least {1} characters.');
                        isValid = false;
                    }}", protocol.key, protocol.premise.minLength, readfriendlyKey);
                switchStarted = true;
            }
            if(protocol.premise.hasMaxLength){
                if(switchStarted){
                    builder.Append(" else");
                }
                builder.AppendFormat(@"
                    if({0}Input.val().length > {1}){{
                        addError({0}Input, '{2} cannot exceed {1} characters.');
                        isValid = false;
                    }}
                ", protocol.key, protocol.premise.maxLength, readfriendlyKey);
                switchStarted = true;
            }
            if(protocol.premise.numeric){
                if (switchStarted) {
                    builder.Append(" else");
                }
                builder.AppendFormat(@"
                    if(!$.isNumeric({0}Input.val())){{
                        addError({0}Input, '{1} must be a numeric value.');
                        isValid = false;
                    }}
                ", protocol.key, readfriendlyKey);
                switchStarted = true;
            }
            if(protocol.premise.hasStepValue){
                if (switchStarted) {
                    builder.Append(" else");
                }
                builder.AppendFormat(@"
                    if(!$.isNumeric({0}Input.val()) || {0}Input.val() % {1} != 0){{
                        addError({0}Input, '{2} must be a numeric value.');
                        isValid = false;
                    }}
                ", protocol.key, protocol.premise.stepValue, readfriendlyKey);
                switchStarted = true;
            }
            if(switchStarted){
                builder.AppendFormat(@"
                    else{{
                        removeError({0}Input);
                    }}", protocol.key
                );
            }
            else{
                builder.AppendFormat("removeError({0}Input); ", protocol.key);
            }

            return new HtmlString(builder.ToString());
        }

        public static HtmlString JsOnChangeValidation(
            this Protocol protocol, string parentId) {
            StringBuilder builder = new StringBuilder();
            string readfriendlyKey = protocol.key.Replace('_', ' ');
            bool elseNeeded = false;
            Premise premise = protocol.premise; //shorthand.

            builder.AppendFormat(@"
                $('.{0} > input[name={1}]').on('input', function(){{
                    if($(this).hasClass('invalid')){{
            ", parentId, protocol.key);

            if(premise.hasMinLength){
                builder.Append(
                    MinLengthJsCase(readfriendlyKey, premise.minLength, elseNeeded));
                elseNeeded = true;
            }
            if(premise.hasMaxLength){
                builder.Append(
                    MaxLengthJsCase(readfriendlyKey, premise.maxLength, elseNeeded));
                elseNeeded = true;
            }
            if(premise.nullable){
                builder.Append(NullableJsCase(readfriendlyKey,elseNeeded));
                elseNeeded = true;
            }
            if(premise.numeric){
                builder.Append(
                    NumericJsCase(readfriendlyKey, elseNeeded));
                elseNeeded = true;
            }
            if(premise.hasStepValue){
                builder.Append(
                    StepJsCase(readfriendlyKey, premise.stepValue, elseNeeded));
                elseNeeded = true;
            }
            if(premise.hasMinValue){
                builder.Append(
                    MinValueJsCase(readfriendlyKey, premise.minValue, elseNeeded));
                elseNeeded = true;
            }
            if(premise.hasMaxValue){
                builder.Append(
                    MaxValueJsCase(readfriendlyKey, premise.maxValue, elseNeeded));
                elseNeeded = true;
            }

            if(elseNeeded){
                builder.Append(" else{ removeError($(this)); } ");
            }
            else{
                builder.Append(" removeError($(this));");
            }

            builder.Append(@"
                    }
                });
            ");

            return new HtmlString(builder.ToString());
        }

        private static string MinLengthJsCase(string key, int minLength, bool switchOccured){
            StringBuilder builder = new StringBuilder();

            if(switchOccured){
                builder.Append(" else");
            }
            builder.AppendFormat(@"
                if($(this).val().length < {0}){{
                    addError($(this), '{1} cannot be less than {0} characters.');
                }}
            ", minLength, key);

            return builder.ToString();
        }

        private static string MaxLengthJsCase(string key, int maxLength, bool switchOccured){
            StringBuilder builder = new StringBuilder();

            if(switchOccured){
                builder.Append(" else");
            }
            builder.AppendFormat(@"
                if($(this).val().length > {0}){{
                    addError($(this), '{1} cannot exceed {0} characters.');
                }}
            ", maxLength, key);

            return builder.ToString();
        }

        private static string NullableJsCase(string key, bool switchOccured){
            StringBuilder builder = new StringBuilder();

            if(switchOccured){
                builder.Append(" else");
            }
            builder.AppendFormat(@"
                if($(this).val().length == 0){{
                    addError($(this), '{0} cannot be null or empty.');
                }}
            ", key);

            return builder.ToString();
        }

        private static string NumericJsCase(string key, bool switchOccured){
            StringBuilder builder = new StringBuilder();

            if(switchOccured){
                builder.Append(" else");
            }
            builder.AppendFormat(@"
                if(!$.IsNumeric((this).val())){{
                    addError($(this), '{0} must be a numeric value.');
                }}
            ", key);

            return builder.ToString();
        }

        private static string StepJsCase(string key, double step, bool switchOccured){
            StringBuilder builder = new StringBuilder();

            if(switchOccured){
                builder.Append(" else");
            }
            builder.AppendFormat(@"
                if($.IsNumeric((this).val()) && $(this).val() % {0} != 0){{
                    addError($(this), '{1} must be divisible by {0}.');
                }}
            ", step, key);

            return builder.ToString();
        }

        private static string MinValueJsCase(string key, int minValue, bool switchOccured){
            StringBuilder builder = new StringBuilder();

            if(switchOccured){
                builder.Append(" else");
            }
            builder.AppendFormat(@"
                if($.IsNumeric((this).val()) && $(this).val() < {0}){{
                    addError($(this), '{1} be at least {0}.');
                }}
            ", minValue, key);

            return builder.ToString();
        }

        private static string MaxValueJsCase(string key, int maxValue, bool switchOccured){
            StringBuilder builder = new StringBuilder();

            if(switchOccured){
                builder.Append(" else");
            }
            builder.AppendFormat(@"
                if($.IsNumeric((this).val()) && $(this).val() > {0}){{
                    addError($(this), '{1} be at least {0}.');
                }}
            ", maxValue, key);

            return builder.ToString();
        }

        public static bool Numeric(this object value) {
            long i = 0;
            double j = 0;
            return Int64.TryParse(value.ToString(), out i)
                || Double.TryParse(value.ToString(), out j);
        }
    }
}
