using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CryptoLearn.Annotations;
using CryptoLearn.Helper;
using CryptoLearn.Interfaces;
using CryptoLearn.Models;
using System.Numerics;
using System.Text;
using System.Xml.Schema;
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
        private ICommand _swapTextCommand;

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
        public ICommand SwapTextCommand
        {
            get => _swapTextCommand;
            set
            {
                if (Equals(value, _swapTextCommand)) return;
                _swapTextCommand = value;
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
            EncryptCommand = new RelayCommand(o =>
            {
                if(EncryptionType==EncryptionType.Encrypt)
                    Encrypt();
                else if(EncryptionType==EncryptionType.Decrypt)
                    Decrypt();
            }, o => !string.IsNullOrEmpty(PlainText));
            SwapTextCommand = new RelayCommand(o =>
            {
                if (!string.IsNullOrEmpty(CipherText))
                {
                    PlainText = CipherText;
                } else
                {
                    PlainText = string.Join('#', CipherTextNumberRepresentation);
                }
                PlainTextNumberRepresentation = null;

                CipherText = "";
                CipherTextNumberRepresentation = null;
            });

            Test1();
            Test2();
        }

        #endregion

        #region Methods

        private void Encrypt()
        {
            PlainTextNumberRepresentation = Rsa.StringToArray(PlainText, Encoding.Unicode);
            CipherTextNumberRepresentation = Rsa.Encrypt(PlainTextNumberRepresentation);
        }

        private void Decrypt()
        {
            try
            {
                PlainTextNumberRepresentation = PlainText.Split('#', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s=> Convert.ToUInt64(s)).ToArray();
                CipherTextNumberRepresentation = Rsa.Decrypt(PlainTextNumberRepresentation);
                CipherText = Rsa.ArrayToString(CipherTextNumberRepresentation, Encoding.Unicode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // throw;
            }
            
        }

        private void Test2()
        {
            Rsa.GeneratePrimes();
            Rsa.CalculateDAndE();
            var encoding = Encoding.Unicode;
            ulong[] s = {123456789};
            var t1 = Rsa.Encrypt(s);
            var t2 = Rsa.Decrypt(t1);
            bool b = true;
            for (int i = 0; i < s.Length; i++)
            {
                b &= s[i] == t2[i];
            }
            Debug.Assert(b);
        }

        private void Test1()
        {
            string s = "Asilasdfl:<mvgdsaw1n9цір к2йі00,йәАЧСМЗОШ ЛИЛДЖАМСЧzhan01";
            var t1 = Rsa.StringToArray(s, Encoding.Unicode);
            var t2 = Rsa.ArrayToString(t1, Encoding.Unicode);
            Debug.Assert(s.Equals(t2));
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