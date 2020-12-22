using System.IO;

namespace CryptoLearn.Interfaces
{
	public interface IOService
	{
		string OpenFileDialog(string defaultPath);
		string SaveFileDialog(string defaultPath);
	}
}