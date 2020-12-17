namespace CryptoLearn.Interfaces
{
	public interface IRsaModel
	{
		public ulong E { get; set; }
		public ulong D { get; set; }
		public ulong P { get; set; }
		public ulong Q { get; set; }
		public string PlainText { get; set; }
		public string CipherText { get; set; }
		public string EncryptData(string text, ulong key1, ulong key2);
		public string DecryptData(string text, ulong key1, ulong key2);
	}
}