using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfSampler.ViewModels
{
    public abstract class ObservableObject : INotifyPropertyChanged
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
