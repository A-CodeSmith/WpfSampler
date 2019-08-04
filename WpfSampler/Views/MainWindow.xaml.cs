﻿using System.Windows;
using WpfSampler.ViewModels;
using WpfSampler.ViewModels.MarkupExtensions;

namespace WpfSampler.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
