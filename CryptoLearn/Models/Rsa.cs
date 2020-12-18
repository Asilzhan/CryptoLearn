using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using CryptoLearn.Annotations;
using CryptoLearn.Helper;
using CryptoLearn.Interfaces;
using PrimeHelper.Primality;
using PrimeHelper.Primality.Deterministic;
using PrimeHelper.Primality.Heuristic;
using PrimeHelper.Randomness;
using Timer = System.Timers.Timer;

namespace CryptoLearn.Models
{
	public class Rsa : IRsaModel, INotifyPropertyChanged
	{
		#region Private members

		private BigInteger _e;
		private BigInteger _d;
		private BigInteger _p;
		private BigInteger _q;
		private string _plainText;
		private string _cipherText;

		#endregion

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
				OnPropertyChanged(nameof(Totient));
				OnPropertyChanged(nameof(N));
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
				OnPropertyChanged(nameof(Totient));
				OnPropertyChanged(nameof(N));
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
		public BigInteger Totient => (P - 1) * (Q - 1);

		public BigInteger N => P * Q;

		#endregion

		#region Methods

		public byte[] Encrypt(byte[] arr)
		{
			throw new NotImplementedException();
		}

		public byte[] Decrypt(byte[] arr)
		{
			throw new NotImplementedException();
		}

		public byte[] StringToByteArray(string s)
		{
			throw new NotImplementedException();
		}

		public string ByteArrayToString(byte[] b)
		{
			throw new NotImplementedException();
		}
		public void GeneratePrimes()
		{
			P = GeneratePrime();
			Q = GeneratePrime();
			
			#region Tests

			Debug.Assert(P != Q);
			
			IPrimalityTest test1 = new RobinMillerTest(10);
			Debug.Assert(test1.TestAsync(P).Result);
			Debug.Assert(test1.TestAsync(Q).Result);

			#endregion
			
		}

		public BigInteger GeneratePrime()
		{
			return PrimeHelper.Tools.PrimaryBigInteger
				.GenerateProbablyPrime(32).Result;
		}
		public void CalculateDAndE()
		{
			BigInteger x = 0, y = 0, e = Totient - 2;
			while (BigInteger.GreatestCommonDivisor(e, Totient)!=1)
			{
				e--;
			}

			PrimeHelper.Helpers.BigIntegerHelpers.GcdEx(e, Totient, ref x, ref y);
			x = (x % Totient + Totient) % Totient;

			E = e;
			D = x;

			Debug.Assert(E * D + y * Totient == 1);
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