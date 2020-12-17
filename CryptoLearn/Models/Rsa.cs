using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using CryptoLearn.Annotations;
using CryptoLearn.Helper;
using CryptoLearn.Interfaces;
using Timer = System.Timers.Timer;

namespace CryptoLearn.Models
{
	public class Rsa : IRsaModel, INotifyPropertyChanged
	{
		private BigInteger _e;
		private BigInteger _d;
		private BigInteger _p;
		private BigInteger _q;
		private string _plainText;
		private string _cipherText;
		private BigInteger _totient;
		private BigInteger _n;

		#region Properties

		public string Alphabet { get; set; }
		public string PlainText
		{
			get => _plainText;
			set
			{
				if (value == _plainText) return;
				_plainText = value;
				OnPropertyChanged();
			}
		}
		public string CipherText
		{
			get => _cipherText;
			set
			{
				if (value == _cipherText) return;
				_cipherText = value;
				OnPropertyChanged();
			}
		}
		
		public BigInteger P
		{
			get => _p;
			set
			{
				if (value == _p) return;
				_p = value;
				Update();
				OnPropertyChanged();
			}
		}
		public BigInteger Q
		{
			get => _q;
			set
			{
				if (value == _q) return;
				_q = value;
				Update();
				OnPropertyChanged();
			}
		}
		public BigInteger E
		{
			get => _e;
			set
			{
				if (value == _e) return;
				_e = value;
				Update();
				OnPropertyChanged();
			}
		}
		public BigInteger D
		{
			get => _d;
			set
			{
				if (value == _d) return;
				_d = value;
				OnPropertyChanged();
			}
		}
		public BigInteger Totient
		{
			get => _totient;
			set
			{
				if (value == _totient) return;
				_totient = value;
				OnPropertyChanged();
			}
		}
		public BigInteger N
		{
			get => _n;
			set
			{
				if (value == _n) return;
				_n = value;
				OnPropertyChanged();
			}
		}

		#endregion

		#region Methods

		public void Encrypt()
		{
			throw new NotImplementedException();
		}

		public void Decrypt()
		{
			throw new NotImplementedException();
		}

		public void GeneratePrimes()
		{
			BigInteger p = GeneratePrime();
			BigInteger q = GeneratePrime();
			

		}

		public BigInteger GeneratePrime()
		{
			Random random = new Random(Environment.TickCount);
			byte[] t = new byte[8];
			BigInteger prime;
			do
			{
				random.NextBytes(t);
				prime = new BigInteger(t);
			} while (IsPrime(prime));
			return prime;
		}
		private bool IsPrime(BigInteger bigInteger)
		{
			throw new NotImplementedException();
		}


		public void CalculateDAndE()
		{
			throw new NotImplementedException();
		}
		private void Update()
		{
			N = P * Q;
			Totient = (P - 1) * (Q - 1);
		}
		
		#endregion

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}