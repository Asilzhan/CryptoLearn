using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CryptoLearn.Annotations;
using CryptoLearn.Helper;

namespace CryptoLearn.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}