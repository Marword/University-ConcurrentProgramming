using Presentation.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Presentation.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, IChecker<T> checker, T def, [CallerMemberName] string propertyName = "")
        {
            if (checker.CheckNotCorrect(value)) value = def;
            return SetField(ref field, value, propertyName);
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

    }
}