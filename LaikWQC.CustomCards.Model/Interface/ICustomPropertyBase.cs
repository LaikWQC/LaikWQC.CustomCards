using System;

namespace LaikWQC.CustomCards.Model
{
    public interface ICustomPropertyBase
    {
        string Header { get; }
        bool IsCorrected { get; }
        void ConfirmChanges();
        event Action ValueChanged;
    }
}
