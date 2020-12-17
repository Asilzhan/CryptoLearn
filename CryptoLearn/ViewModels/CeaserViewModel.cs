using CryptoLearn.Helper;
using CryptoLearn.Interfaces;

namespace CryptoLearn.ViewModels
{
	internal class CeaserViewModel
	{
		public ICeaserModel Ceaser { get; set; }

		public RelayCommand CipherCommand;

		public CeaserViewModel()
		{
			CipherCommand = new RelayCommand(o => Ceaser.Encrypt());
		}
	}
}