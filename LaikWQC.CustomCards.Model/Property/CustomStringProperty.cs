using System;

namespace LaikWQC.CustomCards.Model
{
    public class CustomStringProperty : CustomProperty<string>, ICustomProperty
    {
        public CustomStringProperty(string header, string value, Action<string> setter, Func<string, bool> correctCondition = null) : base(header, value ?? "", setter, correctCondition) { }

        public CustomStringProperty(string header, string value, Action<string> setter, ConditionType conditionType) : base(header, value ?? "", setter) 
        {
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
