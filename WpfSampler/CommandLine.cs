using Mono.Options;
using System;
using System.Collections.Generic;

namespace WpfSampler
{
    class CommandLine
    {
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
                Console.Write("WpfSampler: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `WpfSampler --help' for more information.");
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
            Console.WriteLine("Usage: WpfSampler [OPTIONS]+");
            Console.WriteLine("A small GUI application showcasing a variety of Wpf techniques.");
            Console.WriteLine("This app doesn't accomplish anything. It is meant for educational purposes only.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
            Console.WriteLine();
        }

    }
}
