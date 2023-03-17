using System;

namespace LaikWQC.CustomCards.Model
{
    public abstract class EmptyProperty : ICustomProperty
    {
        public virtual string Header => "";
        public virtual bool IsCorrected => true;

        public event Action ValueChanged;
        public virtual void ConfirmChanges() { }
        public virtual void Reset() { }
    }
}
