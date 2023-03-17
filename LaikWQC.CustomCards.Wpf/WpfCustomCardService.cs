using LaikWQC.CustomCards.Model;
using System.Collections.Generic;
using System.Windows;

namespace LaikWQC.CustomCards.Wpf
{
    public static class WpfCustomCardService 
    {
        public static void ShowDialog(CustomCardModel cc, string title, Window owner = null, double width = 400, double maxHeight = double.PositiveInfinity)
        {
            var wnd = CreateWindow(cc, owner, title, width, maxHeight);            
            wnd.ShowDialog();
        }

        public static void Show(CustomCardModel cc, string title, Window owner = null, double width = 400, double maxHeight = double.PositiveInfinity)
        {
            var wnd = CreateWindow(cc, owner, title, width, maxHeight);
            wnd.Show();
        }

        private static Window CreateWindow(CustomCardModel cc, Window owner, string title, double width, double maxHeight)
        {
            var wnd = new Window()
            {
                Title = title,
                Width = width,
                MaxHeight = maxHeight,
                WindowStyle = WindowStyle.ToolWindow,
                ResizeMode = ResizeMode.NoResize,
                SizeToContent = SizeToContent.Height
            };
            wnd.Owner = owner;
            wnd.WindowStartupLocation = owner == null ? WindowStartupLocation.CenterScreen : WindowStartupLocation.CenterOwner;
            wnd.Content = new CustomCardView();
            wnd.DataContext = cc;
            cc.SetBothCallback(wnd.Close);
            return wnd;
        }
    }
}
