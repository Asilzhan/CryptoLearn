﻿using System.Collections.ObjectModel;
using Microsoft.Xaml.Behaviors.Media;

namespace CryptoLearn.Interfaces
{
	public interface IVigenereModel
	{

		public string Alphabet { get; set; }		
		public string Key { get; set; }
		public ObservableCollection<string> ShiftedAlphabet { get; }
		
		public string Encrypt(string plainText);
		public string Decrypt(string plainText);
	}
}