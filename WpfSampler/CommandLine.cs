using log4net;
using Mono.Options;
using System;
using System.Collections.Generic;
using System.IO;

namespace WpfSampler
{
    class CommandLine
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static bool DebugMode { get; private set; } = false;

        public static bool Parse(string[] args)
        {
            bool showHelp = false;

            var options = new OptionSet() {
                {
                    "d|debug",
                    "enable debug mode",
                    v => DebugMode = v != null
                },
                {
                    "h|help",
                    "show this message and exit",
                    v => showHelp = v != null
                }
            };

            List<string> extra;
            try
            {
                extra = options.Parse(args);
            }
            catch (OptionException e)
            {
                var writer = new StringWriter();
                writer.Write("WpfSampler: ");
                writer.WriteLine(e.Message);
                writer.WriteLine("Try `WpfSampler --help' for more information.");
                log.Info(writer.ToString());

                return false;
            }

            if (showHelp)
            {
                ShowHelp(options);
                return false;
            }

            return true;
        }

        static void ShowHelp(OptionSet p)
        {
            var writer = new StringWriter();
            writer.WriteLine("Usage: WpfSampler [OPTIONS]+");
            writer.WriteLine("A small GUI application showcasing a variety of Wpf techniques.");
            writer.WriteLine("This app doesn't accomplish anything. It is meant for educational purposes only.");
            writer.WriteLine();
            writer.WriteLine("Options:");
            p.WriteOptionDescriptions(writer);
            writer.WriteLine();

            log.Info(writer.ToString());
        }

    }
}
