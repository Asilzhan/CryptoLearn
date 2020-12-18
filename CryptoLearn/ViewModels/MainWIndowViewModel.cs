using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CryptoLearn.Annotations;
using CryptoLearn.Interfaces;

namespace CryptoLearn.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public CeaserViewModel CeaserViewModel { get; set; }
        public VigenereViewModel VigenereViewModel { get; set; }

        public RsaViewModel RsaViewModel { get; set; }
        
        public MainWindowViewModel()
        {
            CeaserViewModel = new CeaserViewModel();
            VigenereViewModel = new VigenereViewModel();
            RsaViewModel = new RsaViewModel();
        }

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