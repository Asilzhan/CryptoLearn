using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CryptoLearn.Annotations;
using CryptoLearn.Helper;
using CryptoLearn.Interfaces;

namespace CryptoLearn.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public ICeaserViewModel CeaserViewModel { get; set; }
        public IRsaViewModel RsaViewModel { get; set; }
        public IVigenereViewModel VigenereViewModel { get; set; }
        public ICipherViewModel DesViewModel { get; set; }
        public ICipherViewModel AesViewModel { get; set; }
        public ICipherViewModel GostViewModel { get; set; }
        
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