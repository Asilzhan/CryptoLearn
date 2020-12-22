using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CryptoLearn.Annotations;

namespace CryptoLearn.Models
{
	public class SymmetricCipherModel : INotifyPropertyChanged
	{
		#region Private members

		private string _inputFilePath;
		private string _outputFilePath;
		private string _key;

		#endregion

		#region Constructor

		public SymmetricCipherModel()
		{
		}

		#endregion
		
		#region Properties

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

		public string Key
		{
			get => _key;
			set
			{
				if (value == _key) return;
				_key = value;
				Algorithm.Key = StringToBytes(value);
				OnPropertyChanged();
			}
		}

		public SymmetricAlgorithm Algorithm { get; set; }
		public Encoding Encoding { get; set; }

		#endregion
		
		#region Methods

		public async Task<string> Encrypt(string data)
		{
			byte[] buff = new byte[Encoding.GetByteCount(data)];
			MemoryStream sin = new MemoryStream(Encoding.GetBytes(data));
			MemoryStream sout = new MemoryStream(buff);
			
			sout.SetLength(0);
			CryptoStream cryptoStream = new CryptoStream(sout, Algorithm.CreateEncryptor(), CryptoStreamMode.Write);
			
			byte[] buffer = new byte[0x1000];
			while (await sin.ReadAsync(buffer, 0, buffer.Length) != 0)
			{
				await cryptoStream.WriteAsync(buffer);
			}

			return Encoding.GetString(buff);
		}
		public async Task<string> Decrypt(string data)
		{
			byte[] buff = new byte[Encoding.GetByteCount(data)];
			MemoryStream sin = new MemoryStream(Encoding.GetBytes(data));
			MemoryStream sout = new MemoryStream(buff);
			
			sout.SetLength(0);
			CryptoStream cryptoStream = new CryptoStream(sout, Algorithm.CreateDecryptor(), CryptoStreamMode.Write);
			
			byte[] buffer = new byte[0x1000];
			while (await sin.ReadAsync(buffer, 0, buffer.Length) != 0)
			{
				await cryptoStream.WriteAsync(buffer);
			}

			return Encoding.GetString(buff);
		}
		public async void Encrypt()
		{
			FileStream fin = new FileStream(InputFilePath, FileMode.Open, FileAccess.Read);
			FileStream fout = new FileStream(OutputFilePath, FileMode.OpenOrCreate, FileAccess.Write);
			
			fout.SetLength(0);
			CryptoStream cryptoStream = new CryptoStream(fout, Algorithm.CreateEncryptor(), CryptoStreamMode.Write);
			
			byte[] buffer = new byte[0x1000];
			while (await fin.ReadAsync(buffer, 0, buffer.Length) != 0)
			{
				await cryptoStream.WriteAsync(buffer);
			}
		}

		public async void Decrypt()
		{
			FileStream fin = new FileStream(InputFilePath, FileMode.Open, FileAccess.Read);
			FileStream fout = new FileStream(OutputFilePath, FileMode.OpenOrCreate, FileAccess.Write);
			fout.SetLength(0);
			CryptoStream cryptoStream = new CryptoStream(fout, Algorithm.CreateDecryptor(), CryptoStreamMode.Write);
			
			byte[] buffer = new byte[0x1000];
			while (await fin.ReadAsync(buffer, 0, buffer.Length) != 0)
			{
				await cryptoStream.WriteAsync(buffer);
			}
		}

		public byte[] StringToBytes(string str)
		{
			var t = Encoding.GetBytes(str);
			var res = new byte[128];
			for (int i = 0; i < 128; i++)
			{
				res[i] = t[i % t.Length];
			}

			return res;
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