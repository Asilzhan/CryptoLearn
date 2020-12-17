using System.ComponentModel;

namespace CryptoLearn.Annotations.Interfaces
{
	public interface IStringKeyViewModel : INotifyPropertyChanged
	{
		public string PlainText { get; set; }
		public string Key { get; set; }
		public string CipherText { get; set; }
		
	}
}