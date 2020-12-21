using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

		private ulong _e;
		private ulong _d;
		private ulong _p;
		private ulong _q;

		#endregion

		public Rsa()
		{
			P = 13;
			Q = 97;
			GeneratePrimes();
			CalculateDAndE();
			// string text = "AsilzhanАлмат1234567890Қ";
			// var t1 = StringToArray(text);
			// var t2 = Encrypt(t1);
			// var t3 = Decrypt(t2);
			// var t4 = ArrayToString(t3);
			// Debug.Assert(text.Equals(t4));
			//
			// Debug.WriteLine(Encoding.Unicode.GetByteCount(text));
		}
		
		#region Properties

		public ulong P
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
		public ulong Q
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
		public ulong E
		{
			get => _e;
			set
			{
				if (value == _e) return;
				_e = value;
				OnPropertyChanged();
			}
		}
		public ulong D
		{
			get => _d;
			set
			{
				if (value == _d) return;
				_d = value;
				OnPropertyChanged();
			}
		}
		public ulong Totient => (P - 1) * (Q - 1);

		public ulong N => P * Q;

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

		public ulong[] StringToArray(string s, Encoding encoding = null)
		{
			Span<byte> bytes = (encoding ?? Encoding.Unicode).GetBytes(s.PadRight((s.Length / 4 + 1) * 4));
			
			Span<ulong> ulongs = MemoryMarshal.Cast<byte, ulong>(bytes);
			return ulongs.ToArray();
		}
		
		public string ArrayToString(ulong[] b, Encoding encoding = null)
		{
			Span<byte> bytes = MemoryMarshal.Cast<ulong, byte>(b);
			return (encoding ?? Encoding.Unicode).GetString(bytes).TrimEnd();
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

		private ulong GeneratePrime()
		{
			var bigInteger = PrimeHelper.Tools.PrimaryBigInteger
				.GenerateProbablyPrime(32).Result;
			var ul = (ulong) bigInteger;
			Debug.Assert(ul == bigInteger);
			return ul;
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

			E = (ulong) e;
			D = (ulong) x;

			BigInteger.DivRem(((BigInteger)E) * ((BigInteger)D), Totient, out var t);
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