using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CryptoLearn.Annotations;
using CryptoLearn.Helper;
using CryptoLearn.Interfaces;
using CryptoLearn.Models;

namespace CryptoLearn.ViewModels
{
	public class CipherViewModel : INotifyPropertyChanged
	{
		#region Private members

		private string _inputFilePath;
		private string _outputFilePath;
		private EncryptionType _encryptionType;

		#endregion

		#region Constructor

		public CipherViewModel(SymmetricCipherModel symmetricCipher, IOService service)
		{
			FileService = service;
			SymmetricCipher = symmetricCipher;
			EncryptCommand = new RelayCommand(o =>
			{
				Encrypt();
			}, o => !string.IsNullOrEmpty(InputFilePath) &&
			         !string.IsNullOrEmpty(OutputFilePath));
			SwapTextCommand = new RelayCommand(o =>
			{
				InputFilePath = OutputFilePath;
				OutputFilePath = "";
			});
			OpenFileCommand = new RelayCommand(o=>OpenFile());
			SaveFileCommand = new RelayCommand(o=>SaveFile());
			GenerateKeysCommand = new RelayCommand(o=>
			{
				GenerateValues();
			});
		}

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

		private IOService FileService { get; }
		
		#endregion

		#region Commands

		public ICommand EncryptCommand { get; set; }

		public ICommand SaveFileCommand { get; set; }
		public ICommand OpenFileCommand { get; set; }

		public ICommand SwapTextCommand { get; set; }

		public ICommand GenerateKeysCommand { get; set; }
		
		#endregion

		#region Methods

		private async void Encrypt()
		{
			if (EncryptionType == EncryptionType.Encrypt)
			{
				await Task.Run(() => SymmetricCipher.Encrypt(_inputFilePath, _outputFilePath));
			}
			else if (EncryptionType == EncryptionType.Decrypt)
			{
				await Task.Run(() => SymmetricCipher.Decrypt(_inputFilePath, _outputFilePath));
			}
		}
		private void OpenFile()
		{
			InputFilePath = FileService.OpenFileDialog(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)) ?? string.Empty;
		}
		private void SaveFile()
		{
			OutputFilePath = FileService.SaveFileDialog(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)) ?? string.Empty;
		}

		private void GenerateValues()
		{
			SymmetricCipher.Algorithm.GenerateKey();
			SymmetricCipher.Algorithm.GenerateIV();
			SymmetricCipher.UpdateKeys();
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