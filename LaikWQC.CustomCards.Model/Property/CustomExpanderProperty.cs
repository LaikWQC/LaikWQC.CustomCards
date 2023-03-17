using System;
using System.Collections.Generic;
using System.Linq;

namespace LaikWQC.CustomCards.Model
{
    public class CustomExpanderProperty : ICustomProperty
    {
        public CustomExpanderProperty(string header, bool isExpanded, ICollection<ICustomProperty> properties)
        {
            Header = header;
            IsExpanded = isExpanded;
            Properties = properties ?? new ICustomProperty[0];
        }

        public string Header { get; }
        public bool IsExpanded { get; }
        public ICollection<ICustomProperty> Properties { get; }

        public bool IsCorrected => Properties.All(x => x.IsCorrected);

        public event Action ValueChanged
        {
            add
            {
                foreach (var property in Properties)
                    property.ValueChanged += value;
            }
            remove
            {
                foreach (var property in Properties)
                    property.ValueChanged -= value;
            }
        }

        public void ConfirmChanges()
        {
            foreach (var property in Properties)
                property.ConfirmChanges();
        }

        public void Reset() 
        {
            foreach (var property in Properties)
                property.Reset();
        }
    }
}
