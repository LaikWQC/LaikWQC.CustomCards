using System;

namespace LaikWQC.CustomCards.Model
{
    public class CustomIntProperty : CustomProperty<int?>, ICustomProperty
    {
        public CustomIntProperty(string header, int? value, Action<int?> setter, Func<int?, bool> correctCondition = null) : base(header, setter)
        {
            Value = value;
            _correctCondition = correctCondition;
        }

        public CustomIntProperty(string header, int? value, Action<int?> setter, ConditionType conditionType) : base(header, setter)
        {
            Value = value;
            switch (conditionType)
            {
                case ConditionType.NoCondition:
                    _correctCondition = null;
                    break;
                case ConditionType.NoEmpty:
                    _correctCondition = x => x != null;
                    break;
            }
        }
    }

}
