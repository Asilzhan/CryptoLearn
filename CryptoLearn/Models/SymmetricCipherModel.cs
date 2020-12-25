using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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

		private string _key;
		private string _iv;
		private long _totalLengthBytes;
		private long _read;
		private long _elapsed;

		#endregion

		#region Constructor

		public SymmetricCipherModel()
		{
		}

		#endregion

		#region Properties

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

		public string Iv
		{
			get => _iv;
			set
			{
				if (value == _iv) return;
				_iv = value;
				Algorithm.IV = StringToBytes(value);
				OnPropertyChanged();
			}
		}

		public SymmetricAlgorithm Algorithm { get; set; }
		public Encoding Encoding { get; set; } = Encoding.Unicode;

		public long TotalLengthBytes
		{
			get => _totalLengthBytes;
			set
			{
				if (value == _totalLengthBytes) return;
				_totalLengthBytes = value;
				OnPropertyChanged();
			}
		}

		public long Read
		{
			get => _read;
			set
			{
				if (value == _read) return;
				_read = value;
				OnPropertyChanged();
			}
		}

		public long Elapsed
		{
			get => _elapsed;
			set
			{
				if (value == _elapsed) return;
				_elapsed = value;
				OnPropertyChanged();
			}
		}

		#endregion

		#region Methods

		public void Encrypt(string inputPath, string outputPath)
		{
			FileStream fin = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
			FileStream fout = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);

			TotalLengthBytes = fin.Length;
			
			fout.SetLength(0);
			CryptoStream cryptoStream = new CryptoStream(fout, Algorithm.CreateEncryptor(), CryptoStreamMode.Write);
			byte[] buffer = new byte[0x1000];
			Stopwatch stopwatch = Stopwatch.StartNew();
			while (fin.Read(buffer, 0, buffer.Length) != 0)
			{
				cryptoStream.Write(buffer);
				Read += buffer.Length;
			}
			stopwatch.Stop();
			Elapsed = stopwatch.ElapsedMilliseconds;
			fin.Close();
			fout.Close();
		}

		public void Decrypt(string inputPath, string outputPath)
		{
			FileStream fin = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
			FileStream fout = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);
			fout.SetLength(0);
			CryptoStream cryptoStream = new CryptoStream(fout, Algorithm.CreateDecryptor(), CryptoStreamMode.Write);

			byte[] buffer = new byte[0x1000];
			while (fin.Read(buffer, 0, buffer.Length) != 0)
			{
				cryptoStream.Write(buffer);
			}
			fin.Close();
			fout.Close();
		}

		public byte[] StringToBytes(string str)
		{
			var t = Encoding.GetBytes(str);
			int n = Algorithm.LegalKeySizes[0].MinSize / 8;
			var res = new byte[n];
			for (int i = 0; i < n; i++)
			{
				res[i] = t[i % t.Length];
			}

			return res;
		}

		public void UpdateKeys()
		{
			Key = Encoding.GetString(Algorithm.Key);
			Iv = Encoding.GetString(Algorithm.IV);
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