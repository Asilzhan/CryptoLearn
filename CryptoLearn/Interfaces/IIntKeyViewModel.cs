using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CryptoLearn.Annotations.Interfaces
{
	public interface IIntKeyViewModel : INotifyPropertyChanged
	{
		public string PlainText { get; set; }
		public int Key { get; set; }
		public string CipherText { get; set; }
		
	}
}