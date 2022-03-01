using LaikWQC.CustomCards.Test;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LaikWQC.CustomCards.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var wnd = new MainWindow();
            wnd.Show();
            TestCard.Test(wnd);
        }
    }
}
