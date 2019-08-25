using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using WpfSampler.Logging;
using WpfSampler.Models;

namespace WpfSampler.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private bool _debugMode;
        private string _logExcerpt;
        private string _logFilename;
        private LogExcerptAppender _logExcerptAppender;
        private string _selectedFile;
        private Product _selectedProduct;

        public MainViewModel()
        {
            InitializeCommands();
            InitializeData();
        }

        public ICommand BrowseCommand { get; private set; }
        public ICommand PlaceOrderCommand { get; private set; }
        public ICommand ViewLogCommand { get; private set; }

        public bool DebugMode
        {
            get => _debugMode;
            set => SetObservableProperty(ref _debugMode, value);
        }
        public ObservableCollection<Feature> Features { get; private set; }
        public string LogExcerpt
        {
            get => _logExcerpt;
            set => SetObservableProperty(ref _logExcerpt, value);
        }
        public ObservableCollection<Product> Products { get; private set; }
        public string SelectedFile
        {
            get => _selectedFile;
            set => SetObservableProperty(ref _selectedFile, value);
        }
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set => SetObservableProperty(ref _selectedProduct, value);
        }

        private void PlaceOrder()
        {
            string selectedFeatures = string.Join(", ", Features.Where(f => f.Included == true).Select(f => f.Name));
            if (string.IsNullOrEmpty(selectedFeatures))
                selectedFeatures = "None";

            log.Debug("Placing order...");
            log.Debug($"Selected Product: {SelectedProduct.Name}, version {SelectedProduct.Version}");
            log.Debug($"Selected File: {SelectedFile}");
            log.Debug($"Selected Features: {selectedFeatures}");
            log.Debug("Order placed!");
        }

        private void InitializeCommands()
        {
            BrowseCommand = new DelegateCommand<TextBox>(ShowFileBrowser);
            PlaceOrderCommand = new DelegateCommand(PlaceOrder);
            ViewLogCommand = new DelegateCommand(ViewLog);
        }

        private void InitializeData()
        {
            var productCatalog = new ProductCatalog();

            log.Debug("Fetching products from product catalog...");
            Products = new ObservableCollection<Product>(productCatalog.GetProducts());
            foreach (var product in Products)
                log.Debug($"Found product: {product.Name}, version {product.Version}");

            log.Debug("Fetching features from product catalog...");
            Features = new ObservableCollection<Feature>(productCatalog.GetFeatures());
            foreach (var feature in Features)
                log.Debug($"Found feature: {feature.Name}");

            SelectedProduct = Products[0];
            DebugMode = CommandLine.DebugMode;

            var logHierarchy = LogManager.GetRepository() as Hierarchy;
            var fileAppender = logHierarchy.Root.Appenders.OfType<FileAppender>().FirstOrDefault();
            _logFilename = fileAppender.File;


            _logExcerptAppender = logHierarchy.Root.Appenders.OfType<LogExcerptAppender>().FirstOrDefault();
            _logExcerptAppender.PropertyChanged += UpdateLogExcerpt;
            UpdateLogExcerpt(null, null);
        }

        private void ShowFileBrowser(TextBox fileNameReciever)
        {
            OpenFileDialog fileBrowser = new OpenFileDialog();
            fileBrowser.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            fileBrowser.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (fileBrowser.ShowDialog() == true)
            {
                fileNameReciever.Text = fileBrowser.FileName;
                log.Debug($"File chosen from browser: {fileNameReciever.Text}");
            }
        }

        private void UpdateLogExcerpt(object sender, PropertyChangedEventArgs e)
        {
            LogExcerpt = _logExcerptAppender.Excerpt;
        }

        private void ViewLog()
        {
            log.Debug("Opening log file...");
            try
            {
                Process.Start(_logFilename);
            }
            catch (Exception e)
            {
                log.Error("Unable to open log file: " + e);
            }
        }
    }
}
