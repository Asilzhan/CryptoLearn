using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
using CryptoLearn.Annotations;
using CryptoLearn.Helper;
using CryptoLearn.Interfaces;
using CryptoLearn.Models;
using CryptoLearn.Validations;

namespace CryptoLearn.ViewModels
{
    internal class VigenereViewModel : INotifyPropertyChanged
    {
        #region Private members

        private Alphabet _alphabetPresenter;
        private EncryptionType _encryptionType;
        private string _plainText = "";
        private string _cipherText;
        private bool _isValid = true;
        private string _key;
        private bool _isValidPlainText = true;

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

        public Alphabet AlphabetPresenter
        {
            get => _alphabetPresenter;
            set
            {
                if (value.Equals(_alphabetPresenter)) return;
                _alphabetPresenter = value;
                Vigenere.Alphabet = value.Value;
                IsValid = IsKeyValidate(Key);
                IsValidPlainText = IsPlainTextValidate(PlainText);
                OnPropertyChanged();
            }
        }

        public string PlainText
        {
            get => _plainText;
            set
            {
                if (value == _plainText) return;
                IsValidPlainText = IsPlainTextValidate(value);
                _plainText = value;
                PlainText = value;
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

        public string Key
        {
            get => _key;
            set
            {
                if (value == _key) return;
                IsValid = IsKeyValidate(value);
                _key = value;
                Vigenere.Key = value;
                OnPropertyChanged();
            }
        }

        public bool IsValid
        {
            get => _isValid;
            set
            {
                if (value == _isValid) return;
                _isValid = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(BorderBrush));
            }
        }

        
        public bool IsValidPlainText
        {
            get => _isValidPlainText;
            set
            {
                if (value == _isValidPlainText) return;
                _isValidPlainText = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(BorderBrushPlainText));
            }
        }
        
        public Brush BorderBrush
        {
            get
            {
                if (IsValid) return Brushes.DarkGray;
                return Brushes.Red;
            }
        }
        
        public Brush BorderBrushPlainText
        {
            get
            {
                if (IsValidPlainText) return Brushes.DarkGray;
                return Brushes.Red;
            }
        }
        #endregion

        #region Commands

        public IVigenereModel Vigenere { get; set; }
        public ICommand EncryptCommand { get; set; }
        public ICommand SwapTextCommand { get; set; }

        #endregion

        #region Constuctor

        public VigenereViewModel()
        {
            Vigenere = new Vigenere();
            EncryptCommand = new RelayCommand(o =>
            {
                if (EncryptionType == EncryptionType.Encrypt)
                    CipherText = Vigenere.Encrypt(PlainText);
                else CipherText = Vigenere.Decrypt(PlainText);
            }, o => IsValid && IsValidPlainText);
            SwapTextCommand = new RelayCommand(o =>
            {
                PlainText = CipherText;
                CipherText = "";
            });

            IsValid = false;
            IsValidPlainText = false;
        }

        #endregion

        #region Methods

        private bool IsKeyValidate(string key)
        {
            if (string.IsNullOrEmpty(key))
                return false;
            foreach (var t in key)
            {
                if (AlphabetPresenter.Value.IndexOf(t, StringComparison.OrdinalIgnoreCase) == -1)
                    return false;
            }
            return true;
        }

        private bool IsPlainTextValidate(string plainText)
        {
            if (plainText == null)
                return false;
            foreach (var t in plainText)
            {
                if (AlphabetPresenter.Value.IndexOf(t, StringComparison.OrdinalIgnoreCase) == -1 && t != ' ')
                    return false;
            }

            return true;
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