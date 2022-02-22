using LaikWQC.CustomCards.Model;
using System.Collections.Generic;
using System.Windows;

namespace LaikWQC.CustomCards.Wpf
{
    public static class WpfCustomCardService 
    {
        public static void ShowDialog(CustomCardModel cc, string title, double width, double maxHeight = 800)
        {
            var wnd = CreateWindow(cc, title, width, maxHeight);
            wnd.ShowDialog();
        }
        public static void Show(CustomCardModel cc, string title, double width, double maxHeight = 800)
        {
            var wnd = CreateWindow(cc, title, width, maxHeight);
            wnd.Show();
        }
        private static Window CreateWindow(CustomCardModel cc, string title, double width, double maxHeight = 800)
        {
            var wnd = new Window()
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
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
