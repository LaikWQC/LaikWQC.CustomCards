using System;

namespace LaikWQC.CustomCards.Model
{
    public class CustomSeparatorProperty : ICustomProperty
    {
        public CustomSeparatorProperty() { }
        public string Header => "";
        public bool IsCorrected => true;

        public event Action ValueChanged;
        public void ConfirmChanges() { }
    }
}
