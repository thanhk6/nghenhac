using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
internal class Class628
{
	public Class628.Enum82 Enum82_0 { get; set; } = Class628.Enum82.Normal;

	public bool Boolean_0 { get; set; } = true;
	public bool Boolean_1 { get; set; } = false;
	public bool Boolean_2 { get; set; } = true;
	public Class628()
	{
		this.Enum82_0 = Enum82.Normal;
		this.Boolean_0 = true;
		this.Boolean_1 = false;
	}

	public Class628(Enum82 encoding, bool fastDecode, bool specialChars)
	{
		this.Enum82_0 = encoding;
		this.Boolean_0 = fastDecode;
		this.Boolean_1 = specialChars;
	}

	public string method_0(string string_4)
	{
		if (this.Boolean_2)
		{
			string_4 += "\n";

			string_4 = this.method_1(string_4);

			if (this.Boolean_1)
			{
				string_4 = this.method_2(string_4);
			}
			if (this.Enum82_0 > Enum82.None)
			{
				string_4 = this.method_3(string_4);
			}
		}
		return string_4;
	}



	private string method_1(string string_4)
	{
		Class630 @class = new Class630
		{
			Char_0 = '\\'
		};
		@class.method_2("'[^'\\n\\r]*'", this.string_0);

		@class.method_2("\"[^\"\\n\\r]*\"", this.string_0);
		@class.method_1("\\/\\/[^\\n\\r]*[\\n\\r]");
		@class.method_1("\\/\\*[^*]*\\*+([^\\/][^*]*\\*+)*\\/");
		@class.method_2("\\s+(\\/[^\\/\\n\\r\\*][^\\/\\n\\r]*\\/g?i?)", "$2");
		@class.method_2("[^\\w\\$\\/'\"*)\\?:]\\/[^\\/\\n\\r\\*][^\\/\\n\\r]*\\/g?i?", this.string_0);
		if (this.Boolean_1)
		{
			@class.method_1(";;[^\\n\\r]+[\\n\\r]");
		}
		@class.method_2(";+\\s*([};])", "$2");
		@class.method_2("(\\b|\\$)\\s+(\\b|\\$)", "$2 $3");
		@class.method_2("([+\\-])\\s+([+\\-])", "$2 $3");
		@class.method_1("\\s+");

		return @class.method_4(string_4);
	}
	private string method_2(string string_4)
	{
		Class630 @class = new Class630();
		@class.method_3("((\\$+)([a-zA-Z\\$_]+))(\\d*)", new Class630.Delegate10(this.method_13));
		Regex regex_ = new Regex("\\b_[A-Za-z\\d]\\w*");
		this.struct98_0 = this.method_16(string_4, regex_, new Class628.Delegate9(this.method_15));
		@class.method_3("\\b_[A-Za-z\\d]\\w*", new Class630.Delegate10(this.method_14));
		string_4 = @class.method_4(string_4);
		return string_4;
	}



	private string method_3(string string_4)
	{
		if (this.Enum82_0 == Class628.Enum82.HighAscii)
		{
			string_4 = this.method_11(string_4);
		}
		Class630 @class = new Class630();
		Class628.Delegate9 delegate9_ = this.method_6(this.Enum82_0);
		Regex regex_ = new Regex((this.Enum82_0 == Class628.Enum82.HighAscii) ? "\\w\\w+" : "\\w+");
		this.struct98_0 = this.method_16(string_4, regex_, delegate9_);
		@class.method_3((this.Enum82_0 == Class628.Enum82.HighAscii) ? "\\w\\w+" : "\\w+", new Class630.Delegate10(this.method_14));
		return (string_4 == string.Empty) ? "" : this.method_4(@class.method_4(string_4), this.struct98_0);
	}



	private string method_4(string string_4, Class628.Struct98 struct98_1)
	{
		string_4 = "'" + this.method_5(string_4) + "'";
		int num = Math.Min(struct98_1.stringCollection_0.Count, (int)this.Enum82_0);
		if (num == 0)
		{
			num = 1;
		}
		int count = struct98_1.stringCollection_0.Count;
		foreach (object obj in struct98_1.hybridDictionary_1.Keys)
		{
			struct98_1.stringCollection_0[(int)obj] = "";
		}
		StringBuilder stringBuilder = new StringBuilder("'");
		foreach (string str in struct98_1.stringCollection_0)
		{
			stringBuilder.Append(str + "|");
		}
		stringBuilder.Remove(stringBuilder.Length - 1, 1);
		string text = stringBuilder.ToString() + "'.split('|')";
		string text2 = "c";
		Class628.Enum82 @enum = this.Enum82_0;
		Class628.Enum82 enum2 = @enum;
		string str2;
		if (enum2 != Class628.Enum82.Mid)
		{
			if (enum2 != Class628.Enum82.Normal)
			{
				if (enum2 != Class628.Enum82.HighAscii)
				{
					str2 = "function(c){return c}";
				}
				else
				{
					str2 = "function(c){return(c<a?\"\":e(c/a))+String.fromCharCode(c%a+161)}";
					text2 += ".toString(a)";
				}
			}
			else
			{
				str2 = "function(c){return(c<a?\"\":e(parseInt(c/a)))+((c=c%a)>35?String.fromCharCode(c+29):c.toString(36))}";
				text2 += ".toString(a)";
			}
		}
		else
		{
			str2 = "function(c){return c.toString(36)}";
			text2 += ".toString(a)";
		}
		string text3 = "";
		if (this.Boolean_0)
		{
			text3 = "if(!''.replace(/^/,String)){while(c--)d[e(c)]=k[c]||e(c);k=[function(e){return d[e]}];e=function(){return'\\\\w+'};c=1;}";
			if (this.Enum82_0 == Class628.Enum82.HighAscii)
			{
				text3 = text3.Replace("\\\\w", "[\\xa1-\\xff]");
			}
			else if (this.Enum82_0 == Class628.Enum82.Numeric)
			{
				text3 = text3.Replace("e(c)", text2);
			}
			if (count == 0)
			{
				text3 = text3.Replace("c=1", "c=0");
			}
		}
		string text4 = "function(p,a,c,k,e,d){while(c--)if(k[c])p=p.replace(new RegExp('\\\\b'+e(c)+'\\\\b','g'),k[c]);return p;}";
		if (this.Boolean_0)
		{
			Regex regex = new Regex("\\{");
			text4 = regex.Replace(text4, "{" + text3 + ";", 1);
		}
		if (this.Enum82_0 == Class628.Enum82.HighAscii)
		{
			Regex regex = new Regex("'\\\\\\\\b'\\s*\\+|\\+\\s*'\\\\\\\\b'");
			text4 = regex.Replace(text4, "");
		}
		if (this.Enum82_0 == Class628.Enum82.HighAscii || num > 62 || this.Boolean_0)
		{
			Regex regex = new Regex("\\{");
			text4 = regex.Replace(text4, "{e=" + str2 + ";", 1);
		}
		else
		{
			Regex regex = new Regex("e\\(c\\)");
			text4 = regex.Replace(text4, text2);
		}
		string text5 = string.Concat(new string[]
		{
			string_4,
			",",
			num.ToString(),
			",",
			count.ToString(),
			",",
			text
		});
		if (this.Boolean_0)
		{
			text5 += ",0,{}";
		}
		return string.Concat(new string[]
		{
			"eval(",
			text4,
			"(",
			text5,
			"))\n"
		});
	}
	private string method_5(string string_4)
	{
		Regex regex = new Regex("([\\\\'])");
		return regex.Replace(string_4, "\\$1");
	}
	private Class628.Delegate9 method_6(Class628.Enum82 enum82_1)
	{
		Class628.Delegate9 result;
		if (enum82_1 != Class628.Enum82.Mid)
		{
			if (enum82_1 != Class628.Enum82.Normal)
			{
				if (enum82_1 != Class628.Enum82.HighAscii)
				{
					result = new Class628.Delegate9(this.method_7);
				}
				else
				{
					result = new Class628.Delegate9(this.method_10);
				}
			}
			else
			{
				result = new Class628.Delegate9(this.method_9);
			}
		}
		else
		{
			result = new Class628.Delegate9(this.method_8);
		}
		return result;
	}
	private string method_7(int int_0)
	{
		return int_0.ToString();
	}
	private string method_8(int int_0)
	{
		string text = "";
		int num = 0;
		do
		{
			int num2 = int_0 / (int)Math.Pow(36.0, (double)num) % 36;
			text = Class628.string_1[num2].ToString() + text;
			int_0 -= num2 * (int)Math.Pow(36.0, (double)num++);
		}
		while (int_0 > 0);
		return text;
	}
	private string method_9(int int_0)
	{
		string text = "";
		int num = 0;
		do
		{
			int num2 = int_0 / (int)Math.Pow(62.0, (double)num) % 62;
			text = Class628.string_2[num2].ToString() + text;
			int_0 -= num2 * (int)Math.Pow(62.0, (double)num++);
		}
		while (int_0 > 0);
		return text;
	}
	private string method_10(int int_0)
	{
		string text = "";
		int num = 0;
		do
		{
			int num2 = int_0 / (int)Math.Pow(95.0, (double)num) % 95;

			text = string_3[num2].ToString() + text;
			int_0 -= num2 * (int)Math.Pow(95.0, (double)num++);
		}
		while (int_0 > 0);
		return text;
	}
	private string method_11(string string_4)
	{
		Regex regex = new Regex("[¡-ÿ]");
		return regex.Replace(string_4, new MatchEvaluator(this.method_12));
	}
	private string method_12(Match match_0)
	{
		return "\\x" + ((int)match_0.Value[0]).ToString("x");
	}
	private string method_13(Match match_0, int int_0)
	{
		int length = match_0.Groups[int_0 + 2].Length;
		int startIndex = length - Math.Max(length - match_0.Groups[int_0 + 3].Length, 0);
		return match_0.Groups[int_0 + 1].Value.Substring(startIndex, length) + match_0.Groups[int_0 + 4].Value;
	}
	private string method_14(Match match_0, int int_0)
	{
		return (string)this.struct98_0.hybridDictionary_0[match_0.Groups[int_0].Value];
	}
	private string method_15(int int_0)
	{
		return "_" + int_0.ToString();
	}
	private Class628.Struct98 method_16(string string_4, Regex regex_0, Class628.Delegate9 delegate9_0)
	{
		MatchCollection matchCollection = regex_0.Matches(string_4);
		Class628.Struct98 @struct;
		@struct.stringCollection_0 = new StringCollection();
		@struct.hybridDictionary_1 = new HybridDictionary();
		@struct.hybridDictionary_0 = new HybridDictionary();
		if (matchCollection.Count > 0)
		{
			StringCollection stringCollection = new StringCollection();
			HybridDictionary hybridDictionary = new HybridDictionary();
			HybridDictionary hybridDictionary2 = new HybridDictionary();
			HybridDictionary hybridDictionary3 = new HybridDictionary();
			int num = matchCollection.Count;
			int num2 = 0;
			do
			{
				string text = "$" + matchCollection[--num].Value;
				if (hybridDictionary3[text] == null)
				{
					hybridDictionary3[text] = 0;
					stringCollection.Add(text);
					HybridDictionary hybridDictionary4 = hybridDictionary;
					string str = "$";
					object obj = hybridDictionary2[num2] = delegate9_0(num2);
					hybridDictionary4[str + ((obj != null) ? obj.ToString() : null)] = num2++;
				}
				hybridDictionary3[text] = (int)hybridDictionary3[text] + 1;
			}
			while (num > 0);
			num = stringCollection.Count;
			string[] array = new string[stringCollection.Count];
			do
			{
				string text = stringCollection[--num];
				if (hybridDictionary[text] != null)
				{
					array[(int)hybridDictionary[text]] = text.Substring(1);
					@struct.hybridDictionary_1[(int)hybridDictionary[text]] = true;
					hybridDictionary3[text] = 0;
				}
			}
			while (num > 0);
			string[] array2 = new string[stringCollection.Count];
			stringCollection.CopyTo(array2, 0);
			Array.Sort(array2, new Class628.Class629(hybridDictionary3));
			num2 = 0;
			do
			{
				if (array[num] == null)
				{
					array[num] = array2[num2++].Substring(1);
				}
				@struct.hybridDictionary_0[array[num]] = hybridDictionary2[num];
			}
			while (++num < array2.Length);
			@struct.stringCollection_0.AddRange(array);
		}
		return @struct;
	}
	private string string_0 = "$1";

	private Class628.Enum82 enum82_0;

	
	private bool bool_0;

	private bool bool_1;

	private bool bool_2;
	private Class628.Struct98 struct98_0;

	private static string string_1 = "0123456789abcdefghijklmnopqrstuvwxyz";
	private static string string_2 = string_1 + "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	private static string string_3 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	public enum Enum82
	{
		None,
		Numeric = 10,
		Mid = 36,
		Normal = 62,
		HighAscii = 95
	}
	private delegate string Delegate9(int code);
	private struct Struct98
	{
		public StringCollection stringCollection_0;
		public HybridDictionary hybridDictionary_0;
		public HybridDictionary hybridDictionary_1;
	}
	private class Class629 : IComparer
	{
		public Class629(HybridDictionary count)
		{
			this.hybridDictionary_0 = count;
		}
		public int Compare(object x, object y)
		{
			return (int)this.hybridDictionary_0[y] - (int)this.hybridDictionary_0[x];
		}
		private HybridDictionary hybridDictionary_0;
	}
}
