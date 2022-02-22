using System;

namespace LaikWQC.CustomCards.Model
{
    public interface ICustomPropertyBase
    {
        string Header { get; }
        bool IsCorrected { get; }
        void ConfirmChanges();
        public event Action ValueChanged;
    }
}
