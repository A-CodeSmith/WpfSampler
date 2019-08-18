using log4net;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfSampler.Models;

namespace WpfSampler.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private bool _debugMode;
        private Product _selectedProduct;

        public MainViewModel()
        {
            InitializeCommands();
            InitializeData();
        }

        public ICommand BrowseCommand { get; private set; }
        public ICommand DoWorkCommand { get; private set; }
        public ICommand ViewLogCommand { get; private set; }

        public bool DebugMode
        {
            get => _debugMode;
            set => SetObservableProperty(ref _debugMode, value);
        }
        public ObservableCollection<Feature> Features { get; private set; }
        public ObservableCollection<Product> Products { get; private set; }
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set => SetObservableProperty(ref _selectedProduct, value);
        }

        private void DoWork()
        {
            log.Debug("Performing work...");
        }

        private void InitializeCommands()
        {
            BrowseCommand = new DelegateCommand<TextBox>(ShowFileBrowser);
            DoWorkCommand = new DelegateCommand(DoWork);
            ViewLogCommand = new DelegateCommand(ViewLog);
        }

        private void InitializeData()
        {
            var productCatalog = new ProductCatalog();
            Features = new ObservableCollection<Feature>(productCatalog.GetFeatures());
            Products = new ObservableCollection<Product>(productCatalog.GetProducts());
            SelectedProduct = Products[0];
            DebugMode = CommandLine.DebugMode;
        }

        private void ShowFileBrowser(TextBox fileNameReciever)
        {
            OpenFileDialog fileBrowser = new OpenFileDialog();
            fileBrowser.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            fileBrowser.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (fileBrowser.ShowDialog() == true)
                fileNameReciever.Text = fileBrowser.FileName;
        }

        private void ViewLog()
        {
            log.Debug("Opening log file...");
        }
    }
}
