using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Businesses.Tools
{
	public class StringHelper
	{
		public static bool IsValidRegistration(string registration)
		{
			// Pattern to match the French registration 
			string pattern = @"^[A-Z]{2}-\d{3}-[A-Z]{2}$";

			// Registration verification
			return Regex.IsMatch(registration, pattern);
		}
	}
}
