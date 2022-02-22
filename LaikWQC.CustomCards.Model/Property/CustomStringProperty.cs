using System;

namespace LaikWQC.CustomCards.Model
{
    public class CustomStringProperty : CustomProperty<string>, ICustomProperty
    {
        public CustomStringProperty(string header, string value, Action<string> setter, Func<string, bool> correctCondition = null) : base(header, setter)
        {
            Value = value ?? "";
            _correctCondition = correctCondition;
        }

        public CustomStringProperty(string header, string value, Action<string> setter, ConditionType conditionType) : base(header, setter)
        {
            Value = value ?? "";
            switch (conditionType)
            {
                case ConditionType.NoCondition:
                    _correctCondition = null;
                    break;
                case ConditionType.NoEmpty:
                    _correctCondition = x => !string.IsNullOrEmpty(x);
                    break;
            }
        }
    }

}
