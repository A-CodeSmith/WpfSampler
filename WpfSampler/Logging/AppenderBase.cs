using log4net.Appender;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfSampler.Logging
{
    abstract class AppenderBase : AppenderSkeleton, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetObservableProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
        {
            backingField = value;
            RaisePropertyChangedEvent(propertyName);
        }

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
