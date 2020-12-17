﻿namespace CryptoLearn.Interfaces
{
	public interface IVigenereModel
	{
		public string PlainText { get; set; }
		public string Alphabet { get; set; }
		public string CipherText { get; set; }
		
		public string Key { get; set; }
		
		public void Encrypt();
		public void Decrypt();
	}
}