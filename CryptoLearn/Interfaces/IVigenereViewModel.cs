namespace CryptoLearn.Interfaces
{
	public interface IVigenereViewModel
	{
		public string PlainText { get; set; }
		public string Alphabet { get; set; }
		public string CipherText { get; set; }
		
		public string Key { get; set; }
		
		public string EncryptData(string text, string key);
		public string DecryptData(string text, string key);
	}
}