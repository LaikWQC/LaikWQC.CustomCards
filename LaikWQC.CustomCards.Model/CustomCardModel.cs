using LaikWQC.Utils.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace LaikWQC.CustomCards.Model
{
    public class CustomCardModel
    {
        public CustomCardModel(IEnumerable<ICustomProperty> properties)
        {
            _cmdConfirm = new MyCommand(Confirm, CanConfirm);
            CmdClose = new MyCommand(CmdCloseExecute);

            Properties = new List<ICustomProperty>(properties.Where(x => x != null));

            foreach (var prop in Properties)
            {
                prop.ValueChanged += () => _cmdConfirm?.RaiseCanExecuteChanged();
            }
        }

        public List<ICustomProperty> Properties { get; }
        public void Reset() => Properties.ForEach(x => x.Reset());

        private readonly MyCommand _cmdConfirm;
        public ICommand CmdConfirm => _cmdConfirm;

        public bool? IsConfirmed { get; private set; } = false;
        private void Confirm()
        {
            IsConfirmed = true;
            foreach (var property in Properties)
                property.ConfirmChanges();
            ConfirmCallback?.Invoke();
        }

        private bool CanConfirm()
        {
            foreach (var property in Properties)
                if (!property.IsCorrected)
                    return false;
            return true;
        }

        public ICommand CmdClose { get; }
        private void CmdCloseExecute()
        {
            IsConfirmed = false;
            CancelCallback?.Invoke();
        }

        public string ConfirmButtonText { get; set; } = "Ok";
        public string CancelButtonText { get; set; } = "Cancel";
        public event Action ConfirmCallback;
        public event Action CancelCallback;
    }

    public static class CustomCardModelFluent
    {
        public static CustomCardModel SetConfirmButtonText(this CustomCardModel target, string text)
        {
            target.ConfirmButtonText = text;
            return target;
        }
        public static CustomCardModel SetCancelButtonText(this CustomCardModel target, string text)
        {
            target.CancelButtonText = text;
            return target;
        }
        public static CustomCardModel SetBothCallback(this CustomCardModel target, Action callback)
        {
            target.SetConfirmCallback(callback);
            target.SetCancelCallback(callback);
            return target;
        }
        public static CustomCardModel SetConfirmCallback(this CustomCardModel target, Action callback)
        {
            target.ConfirmCallback += callback;
            return target;
        }
        public static CustomCardModel SetCancelCallback(this CustomCardModel target, Action callback)
        {
            target.CancelCallback += callback;
            return target;
        }
    }
}
