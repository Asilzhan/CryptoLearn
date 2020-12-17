namespace CryptoLearn.Interfaces
{
	public interface ICipherModel
	{
		public string PlainText { get; set; }
		public string Key { get; set; }
		public string CipherText { get; set; }
		public string EncryptData();
		public string DecryptData();
	}
}