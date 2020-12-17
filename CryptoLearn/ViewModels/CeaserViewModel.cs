using System.ComponentModel;
using System.Runtime.CompilerServices;
using CryptoLearn.Annotations;
using CryptoLearn.Helper;
using CryptoLearn.Interfaces;
using CryptoLearn.Models;

namespace CryptoLearn.ViewModels
{
	internal class CeaserViewModel : INotifyPropertyChanged
	{
		public ICeaserModel Ceaser { get; set; }
		public EncryptionType EncryptionType { get; set; }

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

		public RelayCommand CipherCommand;
		private Alphabet _alphabetPresenter;

		public CeaserViewModel(EncryptionType encryptionType)
		{
			EncryptionType = encryptionType;
			CipherCommand = new RelayCommand(o => Ceaser.Encrypt());
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}