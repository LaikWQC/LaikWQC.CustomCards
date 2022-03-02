using System;

namespace LaikWQC.CustomCards.Model
{
    public class CustomDateProperty : CustomProperty<DateTime?>, ICustomProperty
    {
        public CustomDateProperty(string header, DateTime? value, Action<DateTime?> setter, Func<DateTime?, bool> correctCondition = null)
            : base(header, value, setter, correctCondition) { }

        public CustomDateProperty(string header, DateTime? value, Action<DateTime?> setter, ConditionType conditionType)
            : base(header, value, setter, conditionType) { }
    }
}
