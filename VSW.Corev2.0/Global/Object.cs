using System;
using System.Runtime.CompilerServices;

namespace VSW.Core.Global
{
	public class Object
	{
		public object Current
		{
			[CompilerGenerated]
			get
			{
				return this.current;
			}
		}
		public Object()
		{
		}
		public Object(object o)
		{
			this.current = o;
		}
		public bool ToBool()
		{
			return this.ToBool(false);
		}
		public bool ToBool(bool source)
		{
			return Convert.ToBool(this.Current, source);
		}
		public bool[] ToBools()
		{
			return Array.ToBools(this.ToStrings());
		}
		public DateTime ToDateTime()
		{
			return this.ToDateTime(DateTime.MinValue);
		}
		public DateTime ToDateTime(DateTime source)
		{
			return Convert.ToDateTime(this.Current, source);
		}
		public DateTime[] ToDateTimes()
		{
			return Array.ToDateTimes(this.ToStrings());
		}
		public decimal ToDecimal()
		{
			return this.ToDecimal(-1m);
		}
		public decimal ToDecimal(decimal source)
		{
			return Convert.ToDecimal(this.Current, source);
		}
		public decimal[] ToDecimals()
		{
			return Array.ToDecimals(this.ToStrings());
		}
		public double ToDouble()
		{
			return this.ToDouble(-1.0);
		}
		public double ToDouble(double source)
		{
			return Convert.ToDouble(this.Current, source);
		}
		public double[] ToDoubles()
		{
			return Array.ToDoubles(this.ToStrings());
		}
		public int ToInt()
		{
			return this.ToInt(-1);
		}
		public int ToInt(int source)
		{
			return Convert.ToInt(this.Current, source);
		}
		public int[] ToInts()
		{
			return Array.ToInts(this.ToStrings());
		}
		public long ToLong()
		{
			return this.ToLong(-1L);
		}
		public long ToLong(long source)
		{
			return Convert.ToLong(this.Current, source);
		}
		public long[] ToLongs()
		{
			return Array.ToLongs(this.ToStrings());
		}
		public override string ToString()
		{
			return this.ToString(string.Empty);
		}
		public string ToString(string source)
		{
			return Convert.ToString(this.Current, source);
		}
		public string[] ToStrings()
		{
			string text = this.ToString().Trim();
			string[] result;
			if (string.IsNullOrEmpty(text))
			{
				result = null;
			}
			else
			{
				result = text.Split(new char[]
				{
					','
				});
			}
			return result;
		}
		private object current;
	}
}
