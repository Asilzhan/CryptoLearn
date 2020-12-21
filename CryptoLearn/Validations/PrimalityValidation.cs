using System;
using System.Globalization;
using System.Numerics;
using System.Windows.Controls;
using PrimeHelper.Primality.Heuristic;

namespace CryptoLearn.Validations
{
	public class PrimalityValidation : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			if (value != null)
			{
				ulong prime = Convert.ToUInt64(value.ToString());

				if (prime != 2 && prime % 2 == 0)
					return new ValidationResult(false, $"2-ден басқа жай сан жұп бола алмайды");
			
				RobinMillerTest test = new RobinMillerTest(10);
				if(!test.TestAsync(prime).Result)
					return new ValidationResult(false, $"Енгізілген сан жай емес");
			}

			return new ValidationResult(true, "sd");
		}
	}
}