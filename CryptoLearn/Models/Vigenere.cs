using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using CryptoLearn.Annotations;
using CryptoLearn.Helper;
using CryptoLearn.Interfaces;

namespace CryptoLearn.Models
{
	public class Vigenere :IVigenereModel, INotifyPropertyChanged
	{
		
		#region Private members

		private string _plainText;
		private string _alphabet;
		private string _cipherText;
		private string _key;

		#endregion

		#region Properties

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

		public string Alphabet
		{
			get => _alphabet;
			set
			{
				if (value == _alphabet) return;
				_alphabet = value;
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

		public string Key
		{
			get => _key;
			set
			{
				if (value == _key) return;
				_key = value;
				OnPropertyChanged();
			}
		}

		#endregion

		#region Methods

		public void Encrypt()
		{
			string key = new string(Enumerable.Repeat(_key.ToCharArray(), PlainText.Length / Key.Length + 1).SelectMany(c => c).ToArray());
			key = key.Substring(0, PlainText.Length);
			StringBuilder builder = new StringBuilder(PlainText);
			for (int i = 0; i < PlainText.Length; i++)
			{
				int pos1 = Alphabet.IndexOf(char.ToLower(PlainText[i]));
				int pos2 = Alphabet.IndexOf(char.ToLower(key[i]));
				int pos = (pos1 + pos2) % Alphabet.Length;
				builder[i] = Alphabet[pos].Capitalize(PlainText[i]);
			}
			CipherText = builder.ToString();
		}

		public void Decrypt()
		{
			string key = new string(Enumerable.Repeat(_key.ToCharArray(), PlainText.Length / Key.Length + 1).SelectMany(c => c).ToArray());
			key = key.Substring(0, PlainText.Length);
			StringBuilder builder = new StringBuilder(PlainText);
			for (int i = 0; i < PlainText.Length; i++)
			{
				int pos1 = Alphabet.IndexOf(char.ToLower(PlainText[i]));
				int localKey = Alphabet.IndexOf(char.ToLower(key[i]));
				int pos = (pos1 - localKey + Alphabet.Length) % Alphabet.Length;
				builder[i] = Alphabet[pos].Capitalize(PlainText[i]);
			}
			CipherText = builder.ToString();
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