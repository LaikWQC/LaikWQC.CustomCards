using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace LaikWQC.CustomCards.Model
{
    public class CustomCollectionPropertyMock : ICustomProperty, INotifyPropertyChanged
    {
        private ICollectionProperty _property;
        public CustomCollectionPropertyMock(ICollectionProperty property)
        {
            _property = property;
            Collection = _property.GetCollection();
            SelectItemFromSource();
        }
        private void SelectItemFromSource()
        {
            if (_property.SelectedIndex >= 0)
                SelectedItem = Collection.ElementAt(_property.SelectedIndex);
        }

        public string Header => _property.Header;
        public ICollection<object> Collection { get; }

        private object _selectedItem;

        public event Action ValueChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem == value) return;
                _property.SetValue(value);
                _selectedItem = value;
                ValueChanged?.Invoke();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
            }
        }

        public bool IsCorrected => _property.IsCorrected;

        public void ConfirmChanges()
        {
            _property.ConfirmChanges();
        }

        public void Reset()
        {
            //TODO в настоящий момент CustomCollectionProperty (единсвенная реализация ICollectionProperty) не меняет свой SelectedIndex
            //так что снова выбрать айтем по начальному идексу будет являтся резетом
            //но на всякий случай вызовем _property.Reset() для других реализаций
            _property.Reset();
            SelectItemFromSource();
        }
    }
}
