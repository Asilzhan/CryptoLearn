using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CryptoLearn.Annotations;
using CryptoLearn.Helper;
using CryptoLearn.Interfaces;
using CryptoLearn.Models;
using System.Numerics;
using System.Text;
using Microsoft.Xaml.Behaviors.Core;


namespace CryptoLearn.ViewModels
{
    internal class RsaViewModel : INotifyPropertyChanged
    {
        #region Private members
        
        private EncryptionType _encryptionType;
        private string _plainText;
        private string _cipherText;
        private ulong[] _plainTextNumberRepresentation;
        private ulong[] _cipherTextNumberRepresentation;

        #endregion

        #region Commands

        public ICommand GeneratePrimesCommand { get; set; }
        public ICommand CalculateKeysCommand { get; set; }
        public ICommand EncryptCommand { get; set; }

        #endregion
        
        #region Properties
        
        public EncryptionType EncryptionType
        {
            get => _encryptionType;
            set
            {
                if (value == _encryptionType) return;
                _encryptionType = value;
                OnPropertyChanged();
            }
        }

        public string PlainText
        {
            get => _plainText;
            set
            {
                if (value == _plainText) return;
                _plainText = value;
                OnPropertyChanged();
            }
        }

        public string CipherText
        {
            get => _cipherText;
            set
            {
                if (value == _cipherText) return;
                _cipherText = value;
                OnPropertyChanged();
            }
        }

        public IRsaModel Rsa { get; set; }

        public ulong[] PlainTextNumberRepresentation
        {
            get => _plainTextNumberRepresentation;
            set
            {
                if (Equals(value, _plainTextNumberRepresentation)) return;
                _plainTextNumberRepresentation = value;
                OnPropertyChanged();
            }
        }

        public ulong[] CipherTextNumberRepresentation
        {
            get => _cipherTextNumberRepresentation;
            set
            {
                if (Equals(value, _cipherTextNumberRepresentation)) return;
                _cipherTextNumberRepresentation = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor

        public RsaViewModel()
        {
            Rsa = new Rsa();
            GeneratePrimesCommand = new RelayCommand(o => Rsa.GeneratePrimes());
            CalculateKeysCommand = new RelayCommand(o => Rsa.CalculateDAndE());
            EncryptCommand = new RelayCommand(o => Encrypt(), o => !string.IsNullOrEmpty(PlainText));
        }

        #endregion

        #region Methods

        private void Encrypt()
        {
            PlainTextNumberRepresentation = Rsa.StringToULongArray(PlainText, Encoding.Unicode);
            CipherTextNumberRepresentation = Rsa.Encrypt(PlainTextNumberRepresentation);
        }

        #endregion
        
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