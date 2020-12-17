using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using CryptoLearn.Annotations;
using CryptoLearn.Helper;
using CryptoLearn.Interfaces;

namespace CryptoLearn.Models
{
	public class Vigenere : IVigenereModel, INotifyPropertyChanged
	{
		
		#region Private members

		private string _alphabet;
		private string _key;

		#endregion

		#region Properties

		public string Alphabet
		{
			get => _alphabet;
			set
			{
				if (value == _alphabet) return;
				_alphabet = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(ShiftedAlphabet));
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

		public ObservableCollection<string> ShiftedAlphabet
		{
			get
			{
				ObservableCollection<string> res = new ObservableCollection<string>();
				for (int i = 0; i < Alphabet.Length; i++)
				{
					res.Add(Alphabet.Substring(i) + Alphabet.Substring(0, i));
				}

				return res;
			} 
			set{}
		}

		#endregion

		#region Methods

		public string Encrypt(string plainText)
		{
			string key = new string(Enumerable.Repeat(_key.ToCharArray(), plainText.Length / Key.Length + 1).SelectMany(c => c).ToArray());
			key = key.Substring(0, plainText.Length);
			StringBuilder builder = new StringBuilder(plainText);
			for (int i = 0; i < plainText.Length; i++)
			{
				int pos1 = Alphabet.IndexOf(char.ToLower(plainText[i]));
				int pos2 = Alphabet.IndexOf(char.ToLower(key[i]));
				int pos = (pos1 + pos2) % Alphabet.Length;
				builder[i] = Alphabet[pos].Capitalize(plainText[i]);
			}
			return builder.ToString();
		}

		public string Decrypt(string plainText)
		{
			string key = new string(Enumerable.Repeat(_key.ToCharArray(), plainText.Length / Key.Length + 1).SelectMany(c => c).ToArray());
			key = key.Substring(0, plainText.Length);
			StringBuilder builder = new StringBuilder(plainText);
			for (int i = 0; i < plainText.Length; i++)
			{
				int pos1 = Alphabet.IndexOf(char.ToLower(plainText[i]));
				int localKey = Alphabet.IndexOf(char.ToLower(key[i]));
				int pos = (pos1 - localKey + Alphabet.Length) % Alphabet.Length;
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