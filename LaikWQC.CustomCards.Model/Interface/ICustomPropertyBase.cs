using System;

namespace LaikWQC.CustomCards.Model
{
    public interface ICustomPropertyBase
    {
        string Header { get; }
        bool IsCorrected { get; }
        void ConfirmChanges();
        void Reset();
        event Action ValueChanged;
    }
}
