using System;

namespace LaikWQC.CustomCards.Model
{
    public class CustomDoubleProperty : CustomProperty<double?>, ICustomProperty
    {
        public CustomDoubleProperty(string header, double? value, Action<double?> setter, Func<double?, bool> correctCondition = null)
            : base(header, value, setter, correctCondition) { }

        public CustomDoubleProperty(string header, double? value, Action<double?> setter, ConditionType conditionType)
            : base(header, value, setter, conditionType) { }
    }
}
