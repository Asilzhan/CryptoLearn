using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CryptoLearn.Annotations;
using CryptoLearn.Helper;
using CryptoLearn.Models;
using Microsoft.Xaml.Behaviors.Core;

namespace CryptoLearn.ViewModels
{
	public class CipherViewModel : INotifyPropertyChanged
	{
		#region Private members

		private DataSourceType _dataSourceType;
		private string _inputFilePath;
		private string _outputFilePath;
		private string _plainText;
		private string _cipherText;
		private EncryptionType _encryptionType;

		#endregion

		#region Constructor

		public CipherViewModel(SymmetricCipherModel symmetricCipher)
		{
			SymmetricCipher = symmetricCipher;
			EncryptCommand = new RelayCommand(o =>
			{
				Encrypt();
			}, o => (DataSourceType == DataSourceType.FromFile && !string.IsNullOrEmpty(InputFilePath) &&
			         !string.IsNullOrEmpty(OutputFilePath))
			        || DataSourceType == DataSourceType.FromString && string.IsNullOrEmpty(PlainText));
		}

		#endregion

		#region Properties

		public DataSourceType DataSourceType
		{
			get => _dataSourceType;
			set
			{
				if (value == _dataSourceType) return;
				_dataSourceType = value;
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

		public SymmetricCipherModel SymmetricCipher { get; set; }

		public string InputFilePath
		{
			get => _inputFilePath;
			set
			{
				if (value == _inputFilePath) return;
				_inputFilePath = value;
				OnPropertyChanged();
			}
		}

		public string OutputFilePath
		{
			get => _outputFilePath;
			set
			{
				if (value == _outputFilePath) return;
				_outputFilePath = value;
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

		#endregion

		#region Commands

		public ICommand EncryptCommand { get; set; }

		#endregion

		#region Methods

		private void Encrypt()
		{
			if (DataSourceType == DataSourceType.FromFile && EncryptionType == EncryptionType.Encrypt)
			{
				SymmetricCipher.Encrypt(_inputFilePath, _outputFilePath);
			}
			else if (DataSourceType == DataSourceType.FromFile && EncryptionType == EncryptionType.Decrypt)
			{
				SymmetricCipher.Decrypt(_inputFilePath, _outputFilePath);
			}
			else if (DataSourceType == DataSourceType.FromString && EncryptionType == EncryptionType.Encrypt)
			{
				CipherText = SymmetricCipher.Encrypt(PlainText).Result;
			}
			else if (DataSourceType == DataSourceType.FromString && EncryptionType == EncryptionType.Decrypt)
			{
				CipherText = SymmetricCipher.Decrypt(PlainText).Result;
			}		
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