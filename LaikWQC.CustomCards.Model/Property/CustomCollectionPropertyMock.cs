using System;
using System.Collections.Generic;
using System.Linq;

namespace LaikWQC.CustomCards.Model
{
    public class CustomCollectionPropertyMock : ICustomProperty
    {
        private ICollectionProperty _property;
        public CustomCollectionPropertyMock(ICollectionProperty property)
        {
            _property = property;
            Collection = _property.GetCollection();
            if (_property.SelectedIndex >= 0)
                SelectedItem = Collection.ElementAt(_property.SelectedIndex);
        }

        public string Header => _property.Header;
        public ICollection<object> Collection { get; }

        private object _selectedItem;

        public event Action ValueChanged;

        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem == value) return;
                _property.SetValue(value);
                _selectedItem = value;
                ValueChanged?.Invoke();
            }
        }

        public bool IsCorrected => _property.IsCorrected;

        public void ConfirmChanges()
        {
            _property.ConfirmChanges();
        }
    }

}
