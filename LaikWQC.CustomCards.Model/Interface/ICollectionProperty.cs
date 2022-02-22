using System;
using System.Collections.Generic;

namespace LaikWQC.CustomCards.Model
{
    public interface ICollectionProperty : ICustomPropertyBase
    {
        ICollection<object> GetCollection();
        void SetValue(object value);
        int SelectedIndex { get; }
    }

}
