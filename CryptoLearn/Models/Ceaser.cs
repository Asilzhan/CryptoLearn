using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using CryptoLearn.Annotations;
using CryptoLearn.Helper;
using CryptoLearn.Interfaces;

namespace CryptoLearn.Models
{
	public class Ceaser : ICeaserModel, INotifyPropertyChanged
	{
		
		#region Private members

		private string _plainText;
		private string _alphabet;
		private string _cipherText;
		private int _key;
		

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
				_alphabet = value.ToLower();
				OnPropertyChanged();
				OnPropertyChanged(nameof(ShiftedAlphabet));
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

		public int Key
		{
			get => _key;
			set
			{
				if (value == _key) return;
				_key = value;
				OnPropertyChanged();
			}
		}

		public string ShiftedAlphabet => Alphabet.Substring(Key) + Alphabet.Substring(0, _key);
		#endregion

		#region Methods

		public void Encrypt()
		{
			StringBuilder builder = new StringBuilder(PlainText);
			for (int i = 0; i < PlainText.Length; i++)
			{
				int pos = Alphabet.IndexOf(char.ToLower(PlainText[i]));
				pos = (pos + Key) % Alphabet.Length;
				builder[i] = Alphabet[pos].Capitalize(PlainText[i]);
			}

			CipherText = builder.ToString();
		}

		public void Decrypt()
		{
			StringBuilder builder = new StringBuilder(PlainText);
			for (int i = 0; i < PlainText.Length; i++)
			{
				int pos = Alphabet.IndexOf(char.ToLower(PlainText[i]));
				pos = (pos - Key + Alphabet.Length) % Alphabet.Length;
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