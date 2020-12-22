using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CryptoLearn.Annotations;
using CryptoLearn.Helper;
using CryptoLearn.Interfaces;
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
		private string _key;

		#endregion

		#region Constructor

		public CipherViewModel(SymmetricCipherModel symmetricCipher, IOService service)
		{
			FileService = service;
			SymmetricCipher = symmetricCipher;
			EncryptCommand = new RelayCommand(o =>
			{
				Encrypt();
			}, o => (DataSourceType == DataSourceType.FromFile && !string.IsNullOrEmpty(InputFilePath) &&
			         !string.IsNullOrEmpty(OutputFilePath))
			        || DataSourceType == DataSourceType.FromString && !string.IsNullOrEmpty(PlainText));
			SwapTextCommand = new RelayCommand(o =>
			{
				InputFilePath = OutputFilePath;
				OutputFilePath = "";
			});
			OpenFileCommand = new RelayCommand(o=>OpenFile());
			SaveFileCommand = new RelayCommand(o=>SaveFile());
			GenerateKeyCommand = new RelayCommand(o=>SymmetricCipher.Algorithm.GenerateKey());
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

		public IOService FileService { get; set; }
		
		#endregion

		#region Commands

		public ICommand EncryptCommand { get; set; }

		public ICommand SaveFileCommand { get; set; }
		public ICommand OpenFileCommand { get; set; }

		public ICommand SwapTextCommand { get; set; }

		public ICommand GenerateKeyCommand { get; set; }


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
			// else if (DataSourceType == DataSourceType.FromString && EncryptionType == EncryptionType.Encrypt)
			// {
			// 	CipherText = SymmetricCipher.Encrypt(PlainText);
			// }
			// else if (DataSourceType == DataSourceType.FromString && EncryptionType == EncryptionType.Decrypt)
			// {
			// 	CipherText = SymmetricCipher.Decrypt(PlainText);
			// }		
		}
		private void OpenFile()
		{
			InputFilePath = FileService.OpenFileDialog(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)) ?? string.Empty;
		}
		private void SaveFile()
		{
			OutputFilePath = FileService.SaveFileDialog(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)) ?? string.Empty;
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