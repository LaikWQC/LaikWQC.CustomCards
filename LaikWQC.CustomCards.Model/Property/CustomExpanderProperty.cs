using System;
using System.Collections.Generic;
using System.Linq;

namespace LaikWQC.CustomCards.Model
{
    public class CustomExpanderProperty : CustomGroupProperty
    {
        public CustomExpanderProperty(string header, bool isExpanded, ICollection<ICustomProperty> properties)
            :base(header, properties)
        {
            IsExpanded = isExpanded;
        }

        public bool IsExpanded { get; }
    }
}
