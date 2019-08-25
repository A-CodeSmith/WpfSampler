using System.Windows;
using System.Windows.Controls;

namespace WpfSampler.Views.Controls
{
    public partial class TextViewerControl : CustomControl
    {
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(object), typeof(TextViewerControl), new PropertyMetadata(null));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(object), typeof(TextViewerControl), new PropertyMetadata(null));

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public TextViewerControl()
        {
            InitializeComponent();

            TextViewerTextBox.TextChanged += TextViewerTextBox_TextChanged;
        }

        private void TextViewerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextViewerTextBox.ScrollToEnd();
        }
    }
}
