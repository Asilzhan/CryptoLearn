using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CryptoLearn.Annotations;
using CryptoLearn.Helper;
using CryptoLearn.Models;

namespace CryptoLearn.ViewModels
{
    public class Rc5ViewModel : INotifyPropertyChanged
    {
        private Rc5 _rc5;
        private byte _r;
        private string _key;
        private string _plainText = "";
        private string _cipherText;
        private EncryptionType _encryptionType;

        public byte R
        {
            get => _r;
            set
            {
                if (value == _r) return;
                _r = value;
                Update();
                OnPropertyChanged();
            }
        }

        public string Key
        {
            get => _key;
            set
            {
                if (value == _key) return;
                _key = value;
                Update();
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
        public Rc5ViewModel()
        {
            UpdateCommand = new RelayCommand(o => Update());
            SwapTextCommand = new RelayCommand(o =>
            {
                PlainText = CipherText;
                CipherText = "";
            });
            EncryptCommand = new RelayCommand(o => Encrypt());
            // Update();
        }

        public ICommand UpdateCommand { get; set; }
        public ICommand SwapTextCommand { get; set; } 
        public ICommand EncryptCommand { get; set; }
        
        private async void Encrypt()
        {
            var m = StringToBytes(PlainText);
            var c = new byte[m.Length];
            if (EncryptionType == EncryptionType.Encrypt)
            {
                await Task.Run(() => _rc5.Encrypt(m, c));
            }
            else if (EncryptionType == EncryptionType.Decrypt)
            {
                await Task.Run(() => _rc5.Decrypt(m, c));
            }

            CipherText = BytesToString(c);
        }
        private void Update()
        {
            _rc5 = new Rc5(StringToBytes(Key)) {R = R};
        }
        public byte[] StringToBytes(string str)
        {
            var t = Encoding.Unicode.GetBytes(str.PadRight((str.Length / 4 + 1) * 4));

            return t;
        }

        public string BytesToString(byte[] bytes)
        {
            return Encoding.Unicode.GetString(bytes).TrimEnd();
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