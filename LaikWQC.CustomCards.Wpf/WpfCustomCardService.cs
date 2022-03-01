using LaikWQC.CustomCards.Model;
using System.Collections.Generic;
using System.Windows;

namespace LaikWQC.CustomCards.Wpf
{
    public static class WpfCustomCardService 
    {
        public static void ShowDialog(CustomCardModel cc, string title, Window owner, double width, double maxHeight = 800)
        {
            var wnd = CreateWindow(cc, title, width, maxHeight);
            wnd.Owner = owner;
            wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            wnd.ShowDialog();
        }

        public static void Show(CustomCardModel cc, string title, double width, double maxHeight = 800)
        {
            var wnd = CreateWindow(cc, title, width, maxHeight);
            wnd.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            wnd.Show();
        }
        public static void Show(CustomCardModel cc, string title, Window owner, double width, double maxHeight = 800)
        {
            var wnd = CreateWindow(cc, title, width, maxHeight);
            wnd.Owner = owner;
            wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            wnd.Show();
        }

        private static Window CreateWindow(CustomCardModel cc, string title, double width, double maxHeight = 800)
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
            wnd.Content = new CustomCardView();
            wnd.DataContext = cc;
            cc.OnClose += () => wnd.Close();
            return wnd;
        }
    }
}
