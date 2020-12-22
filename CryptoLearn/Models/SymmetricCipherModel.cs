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

		private string _key;

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

		public SymmetricAlgorithm Algorithm { get; set; }
		public Encoding Encoding { get; set; } = Encoding.Unicode;

		#endregion

		#region Methods

		// public string Encrypt(string data)
		// {
		// 	int n = Algorithm.BlockSize;
		// 	MemoryStream sin = new MemoryStream(Encoding.GetBytes(data));
		// 	MemoryStream sout = new MemoryStream();
		//
		// 	sout.SetLength(0);
		// 	CryptoStream cryptoStream = new CryptoStream(sout, Algorithm.CreateEncryptor(), CryptoStreamMode.Write);
		//
		// 	byte[] buffer = new byte[Encoding.GetByteCount(data)];
		// 	while (sin.Read(buffer, 0, buffer.Length) != 0)
		// 	{
		// 		cryptoStream.Write(buffer);
		// 	}
		//
		// 	return Encoding.GetString(new ReadOnlySpan<byte>(sout.GetBuffer()).Slice(0, Encoding.GetByteCount(data)));
		// }
		//
		// public string Decrypt(string data)
		// {
		// 	int n = Algorithm.BlockSize;
		// 	MemoryStream sin = new MemoryStream(Encoding.GetBytes(data));
		// 	MemoryStream sout = new MemoryStream(sin.Capacity);
		//
		// 	sout.SetLength(0);
		// 	CryptoStream cryptoStream = new CryptoStream(sout, Algorithm.CreateDecryptor(), CryptoStreamMode.Write);
		//
		// 	byte[] buffer = new byte[Encoding.GetByteCount(data)];
		// 	while (sin.Read(buffer, 0, buffer.Length) != 0)
		// 	{
		// 		cryptoStream.Write(buffer);
		// 	}
		//
		// 	return Encoding.GetString(new ReadOnlySpan<byte>(sout.GetBuffer()).Slice(0, Encoding.GetByteCount(data)));
		// }

		public void Encrypt(string inputPath, string outputPath)
		{
			FileStream fin = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
			FileStream fout = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);

			fout.SetLength(0);
			CryptoStream cryptoStream = new CryptoStream(fout, Algorithm.CreateEncryptor(), CryptoStreamMode.Write);

			byte[] buffer = new byte[0x1000];
			while (fin.Read(buffer, 0, buffer.Length) != 0)
			{
				cryptoStream.Write(buffer);
			}
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
			var res = new byte[Algorithm.LegalKeySizes[0].MinSize];
			for (int i = 0; i < Algorithm.LegalKeySizes[0].MinSize; i++)
			{
				res[i] = t[i % t.Length];
			}

			return res;
		}

		public string StreamToString(Stream stream)
		{
			stream.Position = 0;
			using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
			{
				return reader.ReadToEnd();
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