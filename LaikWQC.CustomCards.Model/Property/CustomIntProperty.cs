using System;

namespace LaikWQC.CustomCards.Model
{
    public class CustomIntProperty : CustomProperty<int?>, ICustomProperty
    {
        public CustomIntProperty(string header, int? value, Action<int?> setter, Func<int?, bool> correctCondition = null) 
            : base(header, value, setter, correctCondition) { }

        public CustomIntProperty(string header, int? value, Action<int?> setter, ConditionType conditionType) 
            : base(header, value, setter, conditionType) { }
    }
}
