namespace CryptoLearn.Interfaces
{
	public interface ICeaserModel
	{
		public string Alphabet { get; set; }

		public string ShiftedAlphabet { get; }
		public int Key { get; set; }
		
		public string Encrypt(string ceaserPlainText);
		public string Decrypt(string plainText);
	}
}