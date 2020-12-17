namespace CryptoLearn.Interfaces
{
	public interface ICipherViewModel
	{
		public string PlainText { get; set; }
		public string Key { get; set; }
		public string CipherText { get; set; }
		public string EncryptData(string text, string key);
		public string DecryptData(string text, string key);
	}
}