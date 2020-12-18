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
			BigInteger prime = BigInteger.Parse(value?.ToString() ?? "0");
			
			if(prime!=2 && prime.IsEven)
				return new ValidationResult(false, $"2-ден басқа жай сан жұп бола алмайды");
			
			RobinMillerTest test = new RobinMillerTest(10);
			if(!test.TestAsync(prime).Result)
				return new ValidationResult(false, $"Енгізілген сан жай емес");
			
			return ValidationResult.ValidResult;
		}
	}
}