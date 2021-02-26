using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using CryptoLearn.Annotations;
using CryptoLearn.Helper;
using CryptoLearn.Interfaces;
using CryptoLearn.Models;

namespace CryptoLearn.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public CeaserViewModel CeaserViewModel { get; set; }
        public VigenereViewModel VigenereViewModel { get; set; }
        public CipherViewModel DesViewModel { get; set; }
        public CipherViewModel AesViewModel { get; set; }
        public Rc5ViewModel Rc5ViewModel { get; set; }
        public RsaViewModel RsaViewModel { get; set; }
        
        public MainWindowViewModel()
        {
            CeaserViewModel = new CeaserViewModel();
            VigenereViewModel = new VigenereViewModel();
            RsaViewModel = new RsaViewModel();
            DesViewModel = new CipherViewModel(new SymmetricCipherModel(){Algorithm = new DESCryptoServiceProvider()}, new FileDialog());
            AesViewModel = new CipherViewModel(new SymmetricCipherModel(){Algorithm = new AesCryptoServiceProvider()}, new FileDialog());
            Rc5ViewModel = new Rc5ViewModel();
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