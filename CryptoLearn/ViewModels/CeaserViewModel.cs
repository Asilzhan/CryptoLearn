using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CryptoLearn.Annotations;
using CryptoLearn.Helper;
using CryptoLearn.Interfaces;
using CryptoLearn.Models;

namespace CryptoLearn.ViewModels
{
	internal class CeaserViewModel : INotifyPropertyChanged
	{
		private Alphabet _alphabetPresenter;
		private EncryptionType _encryptionType;
		private string _plainText;
		private string _cipherText;
		public ICeaserModel Ceaser { get; set; }

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
				Ceaser.Alphabet = value.Value;
				OnPropertyChanged();
			}
		}

		public ICommand EncryptCommand { get; set; }
		public ICommand SwapTextCommand { get; set; }

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

		public CeaserViewModel()
		{
			Ceaser = new Ceaser();
			EncryptCommand = new RelayCommand(o =>
			{
				if(EncryptionType==EncryptionType.Encrypt)
					CipherText = Ceaser.Encrypt(PlainText);
				else CipherText = Ceaser.Decrypt(PlainText);
			});
			SwapTextCommand = new RelayCommand(o =>
			{
				PlainText = CipherText;
				CipherText = "";
			});
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}