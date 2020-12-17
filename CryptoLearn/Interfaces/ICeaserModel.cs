namespace CryptoLearn.Interfaces
{
	public interface ICeaserModel
	{
		public string PlainText { get; set; }
		public string Alphabet { get; set; }
		public string CipherText { get; set; }
		public ulong Key { get; set; }
		
		public string Encrypt();
		public string Decrypt();
	}
}