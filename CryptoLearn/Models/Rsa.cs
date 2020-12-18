using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using CryptoLearn.Annotations;
using CryptoLearn.Interfaces;
using PrimeHelper.Primality;
using PrimeHelper.Primality.Heuristic;

namespace CryptoLearn.Models
{
	public class Rsa : IRsaModel, INotifyPropertyChanged
	{
		#region Private members

		private BigInteger _e;
		private BigInteger _d;
		private BigInteger _p;
		private BigInteger _q;

		#endregion

		public Rsa()
		{
			P = 13;
			Q = 97;
			
			string text = "AsilzhanАлмат1234567890Қ";
			var t1 = StringToULongArray(text);
			var t2 = Encrypt(t1);
			var t3 = Decrypt(t2);
			var t4 = ULongArrayToString(t1);
			Debug.Assert(text.Equals(t4));
		}
		
		#region Properties

		public string Alphabet =>
			"abcdefghijklmnopqrstuvwxyz,.?!;:\"()'+-*/~@#$%^&-=_0123456789 №<>аәбвгғдеёжзийкқлмнңоөпрстуұүфхһцчшщъыіьэюя[\\]{|}─│┌┐└┘├┤┬┴┼═║╒‰₸";

		public BigInteger P
		{
			get => _p;
			set
			{
				if (value == _p) return;
				_p = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(Totient));
				OnPropertyChanged(nameof(N));
			}
		}
		public BigInteger Q
		{
			get => _q;
			set
			{
				if (value == _q) return;
				_q = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(Totient));
				OnPropertyChanged(nameof(N));
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

		public ulong[] Encrypt(ulong[] arr)
		{
			ulong[] res = new ulong[arr.Length];
			for (int i = 0; i < arr.Length; i++)
			{
				res[i] = (ulong) BigInteger.ModPow(arr[i], D, N);
			}

			return res;
		}

		public ulong[] Decrypt(ulong[] arr)
		{
			ulong[] res = new ulong[arr.Length];
			for (int i = 0; i < arr.Length; i++)
			{
				res[i] = (ulong) BigInteger.ModPow(arr[i], E, N);
			}

			return res;
		}

		public ulong[] StringToULongArray(string s, Encoding encoding = null)
		{
			Span<byte> t = (encoding ?? Encoding.Unicode).GetBytes(s);
			ulong[] res = new ulong[t.Length / 8];
			for (int i = 0; i < res.Length; i++)
			{
				res[i] = BitConverter.ToUInt64(t.Slice(8 * i, 8));
			}

			return res;
		}
		
		public string ULongArrayToString(ulong[] b, Encoding encoding = null)
		{
			byte[] t = new byte[b.Length * 8];
			for (int i = 0; i < b.Length; i++)
			{
				var bytes = BitConverter.GetBytes(b[i]);
				for (int j = 0; j < 8; j++)
				{
					t[8 * i + j] = bytes[j];
				}
			}

			return (encoding ?? Encoding.Unicode).GetString(t);
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

		private BigInteger GeneratePrime()
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

			BigInteger.DivRem(E * D, Totient, out var t);
			BigInteger.DivRem(t + Totient, Totient, out var rem);
			Debug.Assert(rem == 1);
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