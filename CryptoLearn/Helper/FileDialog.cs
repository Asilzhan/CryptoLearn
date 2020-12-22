using CryptoLearn.Interfaces;
using Microsoft.Win32;

namespace CryptoLearn.Helper
{
	public class FileDialog :IOService
	{
		public string OpenFileDialog(string defaultPath)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() == true)
				return openFileDialog.FileName;
			return "";
		}

		public string SaveFileDialog(string defaultPath)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			if (saveFileDialog.ShowDialog() == true)
				return saveFileDialog.FileName;
			return "";
		}
	}
}