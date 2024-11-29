using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace VSW.Core.Global
{
	public class Array
	{
		public static bool[] ToBools(string source)
		{
			return Array.ToBools(source.Split(new char[]
			{
				','
			}));
		}
		public static bool[] ToBools(string[] source)
		{
			bool[] result;
			if (source == null || source.Length == 0)
			{
				result = null;
			}
			else
			{
				bool[] array = new bool[source.Length];
				for (int i = 0; i < source.Length; i++)
				{
					array[i] = Convert.ToBool(source[i]);
				}
				result = array;
			}
			return result;
		}


		public static bool[] ToBools(string source, char split)
		{
			return Array.ToBools(source.Split(new char[]
			{
				split
			}));
		}
		public static bool[] ToBools(string source, string split)
		{
			return Array.ToBools(Regex.Split(source, split));
		}
		public static DateTime[] ToDateTimes(string source)
		{
			return Array.ToDateTimes(source.Split(new char[]
			{
				','
			}));
		}
		public static DateTime[] ToDateTimes(string[] source)
		{
			DateTime[] result;
			if (source == null || source.Length == 0)
			{
				result = null;
			}
			else
			{
				DateTime[] array = new DateTime[source.Length];
				for (int i = 0; i < source.Length; i++)
				{
					array[i] = Convert.ToDateTime(source[i]);
				}
				result = array;
			}
			return result;
		}
		public static DateTime[] ToDateTimes(string source, char split)
		{
			return Array.ToDateTimes(source.Split(new char[]
			{
				split
			}));
		}
		public static DateTime[] ToDateTimes(string source, string split)
		{
			return Array.ToDateTimes(Regex.Split(source, split));
		}
		public static decimal[] ToDecimals(string source)
		{
			return Array.ToDecimals(source.Split(new char[]
			{
				','
			}));
		}
		public static decimal[] ToDecimals(string[] source)
		{
			decimal[] result;
			if (source == null || source.Length == 0)
			{
				result = null;
			}
			else
			{
				decimal[] array = new decimal[source.Length];
				for (int i = 0; i < source.Length; i++)
				{
					array[i] = Convert.ToDecimal(source[i]);
				}
				result = array;
			}
			return result;
		}

		public static List<object> ToList(string[] source)
		{
			return new List<object>(source);
		}

		public static decimal[] ToDecimals(string source, char split)
		{
			return Array.ToDecimals(source.Split(new char[]
			{
				split
			}));
		}
		public static decimal[] ToDecimals(string source, string split)
		{
			return Array.ToDecimals(Regex.Split(source, split));
		}
		public static double[] ToDoubles(string source)
		{
			return Array.ToDoubles(source.Split(new char[]
			{
				','
			}));
		}
		public static double[] ToDoubles(string[] source)
		{
			double[] result;
			if (source == null || source.Length == 0)
			{
				result = null;
			}
			else
			{
				double[] array = new double[source.Length];
				for (int i = 0; i < source.Length; i++)
				{
					array[i] = Convert.ToDouble(source[i]);
				}
				result = array;
			}
			return result;
		}
		public static double[] ToDoubles(string source, char split)
		{
			return Array.ToDoubles(source.Split(new char[]
			{
				split
			}));
		}
		public static double[] ToDoubles(string source, string split)
		{
			return Array.ToDoubles(Regex.Split(source, split));
		}
		public static int[] ToInts(string source)
		{
			return Array.ToInts(source.Split(new char[]
			{
				','
			}));
		}
		public static int[] ToInts(string[] source)
		{
			int[] result;
			if (source == null || source.Length == 0)
			{
				result = null;
			}
			else
			{
				int[] array = new int[source.Length];
				for (int i = 0; i < source.Length; i++)
				{
					array[i] = Convert.ToInt(source[i]);
				}
				result = array;
			}
			return result;
		}
		public static int[] ToInts(string source, char split)
		{
			return Array.ToInts(source.Split(new char[]
			{
				split
			}));
		}
		public static int[] ToInts(string source, string split)
		{
			return Array.ToInts(Regex.Split(source, split));
		}
		public static long[] ToLongs(string source)
		{
			return Array.ToLongs(source.Split(new char[]
			{
				','
			}));
		}
		public static long[] ToLongs(string[] source)
		{
			long[] result;
			if (source == null || source.Length == 0)
			{
				result = null;
			}
			else
			{
				long[] array = new long[source.Length];
				for (int i = 0; i < source.Length; i++)
				{
					array[i] = Convert.ToLong(source[i]);
				}
				result = array;
			}
			return result;
		}
		public static long[] ToLongs(string source, char split)
		{
			return Array.ToLongs(source.Split(new char[]
			{
				split
			}));
		}

		public static long[] ToLongs(string source, string split)
		{
			return Array.ToLongs(Regex.Split(source, split));
		}
		public static string[] ToString(string source)
		{
			string[] result;
			if (string.IsNullOrEmpty(source))
			{
				result = null;
			}
			else
			{
				if (source.EndsWith(","))
				{
					source = source.Substring(1, source.Length - 2);
				}
				result = source.Split(new char[]
				{
					','
				});
			}
			return result;
		}
		public static string[] ToString(string source, char split)
		{
			string[] result;
			if (string.IsNullOrEmpty(source))
			{
				result = null;
			}
			else
			{
				if (source.EndsWith(split.ToString()))
				{
					source = source.Substring(1, source.Length - 2);
				}
				result = source.Split(new char[]
				{
					split
				});
			}
			return result;
		}
		public static string[] ToString(string source, string split)
		{
			string[] result;
			if (string.IsNullOrEmpty(source))
			{
				result = null;
			}
			else
			{
				if (source.EndsWith(split))
				{
					source = source.Substring(1, source.Length - split.Length - 1);
				}
				result = Regex.Split(source, split);
			}
			return result;
		}
		public static string ToString(string[] source)
		{
			string result;
			if (source == null || source.Length == 0)
			{
				result = string.Empty;
			}
			else
			{
				string text = string.Empty;
				for (int i = 0; i < source.Length; i++)
				{
					string text2 = source[i].Trim();
					if (text2 != string.Empty)
					{
						if (text == string.Empty)
						{
							text = text2;
						}
						else
						{
							text = text + "," + text2;
						}
					}
				}
				result = text;
			}
			return result;
		}
		public static string ToString(int[] source)
		{
			string result;
			if (source == null || source.Length == 0)
			{
				result = string.Empty;
			}
			else
			{
				string text = string.Empty;
				for (int i = 0; i < source.Length; i++)
				{
					if (i == 0)
					{
						text = source[i].ToString();
					}
					else
					{
						text = text + "," + source[i].ToString();
					}
				}
				result = text;
			}
			return result;
		}
		public static string ToString(int[] source, char split)
		{
			string result;
			if (source == null || source.Length == 0)
			{
				result = string.Empty;
			}
			else
			{
				string text = string.Empty;
				for (int i = 0; i < source.Length; i++)
				{
					if (i == 0)
					{
						text = source[i].ToString();
					}
					else
					{
						text = text + split.ToString() + source[i].ToString();
					}
				}
				result = text;
			}
			return result;
		}
		public static string ToString(string[] source, char split)
		{
			string result;
			if (source == null || source.Length == 0)
			{
				result = string.Empty;
			}
			else
			{
				string text = string.Empty;
				for (int i = 0; i < source.Length; i++)
				{
					string text2 = source[i].Trim();
					if (!(text2 == string.Empty))
					{
						if (text == string.Empty)
						{
							text = text2;
						}
						else
						{
							text = text + split.ToString() + text2;
						}
					}
				}
				result = text;
			}
			return result;
		}
		public static List<object> Tolist(string[] source)
		{
			return new List<object>(source);
		}
	}
}
