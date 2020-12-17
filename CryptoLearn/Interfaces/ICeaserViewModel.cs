namespace CryptoLearn.Interfaces
{
	public interface ICeaserViewModel
	{
		public string PlainText { get; set; }
		public string Alphabet { get; set; }
		public string CipherText { get; set; }
		public ulong Key { get; set; }
		
		public string EncryptData(string text, ulong key);
		public string DecryptData(string text, ulong key);
	}
}