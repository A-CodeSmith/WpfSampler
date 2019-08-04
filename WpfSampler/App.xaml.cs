using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfSampler.Views;

namespace WpfSampler
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            //CommandLine.Parse(e.Args);
            //if (CommandLine.Debug)
            //    MessageBox.Show("Debug", "Attach!");

            var window = new MainWindow();
            window.Show();
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //logger.Fatal("A fatal error has occurred, and this application must close.");
            //var exception = e.ExceptionObject as Exception;
            //if (exception != null)
            //{
            //    logger.Fatal(exception);
            //}
        }
    }
}
