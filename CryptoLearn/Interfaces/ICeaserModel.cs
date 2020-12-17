namespace CryptoLearn.Interfaces
{
	public interface ICeaserModel
	{
		public string PlainText { get; set; }
		public string Alphabet { get; set; }
		public string CipherText { get; set; }
		public int Key { get; set; }
		
		public void Encrypt();
		public void Decrypt();
	}
}