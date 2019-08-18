using log4net;
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
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            if (!CommandLine.Parse(e.Args))
            {
                log.Fatal("Command line parsing failed. Shutting down.");
                Shutdown(1);
                return;
            }

            if (CommandLine.DebugMode)
            {
                log.Debug("DebugMode enabled.");
                MessageBox.Show("Debug", "Attach!");
            }

            var window = new MainWindow();
            window.Show();
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            log.Fatal("A fatal error has occurred, and this application must close.");
            var exception = e.ExceptionObject as Exception;
            if (exception != null)
            {
                log.Fatal(exception);
            }
        }
    }
}
