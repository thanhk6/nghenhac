using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
internal class Class630
{
	private string method_0(Match match_0, int int_1)
	{
		return " " + match_0.Groups[int_1].Value + " ";
	}
	public bool Boolean_0 { get; set; } = false;
	public char Char_0 { get; set; } = '\0';
	public void method_1(string string_0)
	{
		this.method_2(string_0, string.Empty);
	}
	public void method_2(string string_0, string string_1)
	{
		if (string_1 == string.Empty)
		{
			this.method_5(string_0, new Class630.Delegate10(this.method_0));
		}
		this.method_5(string_0, string_1);
	}
	public void method_3(string string_0, Class630.Delegate10 delegate10_0)
	{
		this.method_5(string_0, delegate10_0);
	}
	public string method_4(string string_0)
	{
		return this.regex_5.Replace(this.method_11(this.method_6().Replace(this.method_9(string_0), new MatchEvaluator(this.method_7))), string.Empty);
	}
	private void method_5(string string_0, object object_0)
	{
		Class630.Class631 @class = new Class630.Class631
		{
			string_0 = string_0,
			object_0 = object_0,
			int_0 = this.regex_0.Matches(this.method_13(string_0)).Count + 1
		};
		if (object_0 is string && this.regex_1.IsMatch((string)object_0))
		{
			string text = (string)object_0;
			if (this.regex_2.IsMatch(text))
			{
				@class.object_0 = int.Parse(text.Substring(1)) - 1;
			}
		}
		this.arrayList_0.Add(@class);
	}
	private Regex method_6()
	{
		StringBuilder stringBuilder = new StringBuilder(string.Empty);
		foreach (object obj in this.arrayList_0)
		{
			stringBuilder.Append(((Class630.Class631)obj).ToString() + "|");
		}
		stringBuilder.Remove(stringBuilder.Length - 1, 1);
		return new Regex(stringBuilder.ToString(), this.Boolean_0 ? RegexOptions.IgnoreCase : RegexOptions.None);
	}
	private string method_7(Match match_0)
	{
		int num = 1;
		int num2 = 0;
		Class630.Class631 @class;
		while ((@class = (Class630.Class631)this.arrayList_0[num2++]) != null)
		{
			if (match_0.Groups[num].Value != string.Empty)
			{
				object object_ = @class.object_0;
				string result;
				if (object_ is Class630.Delegate10)
				{
					result = ((Class630.Delegate10)object_)(match_0, num);
				}
				else if (object_ is int)
				{
					result = match_0.Groups[(int)object_ + num].Value;
				}
				else
				{
					result = this.method_8(match_0, num, (string)object_, @class.int_0);
				}
				return result;
			}
			num += @class.int_0;
		}
		return match_0.Value;
	}
	private string method_8(Match match_0, int int_1, string string_0, int int_2)
	{
		while (int_2 > 0)
		{
			string_0 = string_0.Replace("$" + int_2--.ToString(), match_0.Groups[int_1 + int_2].Value);
		}
		return string_0;
	}
	private string method_9(string string_0)
	{
		string result;
		if (this.Char_0 == '\0')
		{
			result = string_0;
		}
		else
		{
			Regex regex = new Regex("\\\\(.)");
			result = regex.Replace(string_0, new MatchEvaluator(this.method_10));
		}
		return result;
	}


	private string method_10(Match match_0)
	{
		this.stringCollection_0.Add(match_0.Groups[1].Value);
		return "\\";
	}
	private string method_11(string string_0)
	{
		string result;
		if (this.Char_0 == '\0')
		{
			result = string_0;
		}
		else
		{
			Regex regex = new Regex("\\" + this.Char_0.ToString());
			result = regex.Replace(string_0, new MatchEvaluator(this.method_12));
		}
		return result;
	}


	private string method_12(Match match_0)
	{
		string str = "\\";
		StringCollection stringCollection = this.stringCollection_0;
		int num = this.int_0;
		this.int_0 = num + 1;
		return str + stringCollection[num];
	}


	private string method_13(string string_0)
	{
		return this.regex_3.Replace(string_0, "");
	}


	private readonly Regex regex_0 = new Regex("\\(");


	private readonly Regex regex_1 = new Regex("\\$");

	
	private readonly Regex regex_2 = new Regex("^\\$\\d+$");


	private readonly Regex regex_3 = new Regex("\\\\.");

	
	private readonly Regex regex_4 = new Regex("'");

	
	private readonly Regex regex_5 = new Regex("\\x01[^\\x01]*\\x01");

	private bool bool_0;
	private char char_0;
	private readonly ArrayList arrayList_0 = new ArrayList();
	private readonly StringCollection stringCollection_0 = new StringCollection();
	private int int_0 = 0;
	public delegate string Delegate10(Match match, int offset);
	private class Class631
	{
		public override string ToString()
		{
			return "(" + this.string_0 + ")";
		}
		public string string_0;
		public object object_0;
		public int int_0;
	}
}
