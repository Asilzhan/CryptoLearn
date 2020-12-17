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
		
		public int Key
		{
			get => _key;
			set
			{
				if (value == _key) return;
				_key = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(ShiftedAlphabet));
			}
		}

		public string ShiftedAlphabet
		{
			get => Alphabet.Substring(Key % Alphabet.Length) + Alphabet.Substring(0, Key % Alphabet.Length);
			set{}
		}

		#endregion

		#region Methods

		public string Encrypt(string plainText)
		{
			StringBuilder builder = new StringBuilder(plainText);
			for (int i = 0; i < plainText.Length; i++)
			{
				int pos = Alphabet.IndexOf(char.ToLower(plainText[i]));
				if (pos == -1) continue;
				pos = (pos + Key) % Alphabet.Length;
				builder[i] = Alphabet[pos].Capitalize(plainText[i]);
			}

			return builder.ToString();
		}

		public string Decrypt(string plainText)
		{
			StringBuilder builder = new StringBuilder(plainText);
			for (int i = 0; i < plainText.Length; i++)
			{
				int pos = Alphabet.IndexOf(char.ToLower(plainText[i]));
				if (pos == -1) continue;
				pos = (pos - Key + Alphabet.Length) % Alphabet.Length;
				builder[i] = Alphabet[pos].Capitalize(plainText[i]);
			}

			return builder.ToString();
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