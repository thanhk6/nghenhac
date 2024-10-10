using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Web;
using VSW.Core.Data;
namespace VSW.Core.Global
{
	public static class Bug
	{
		public static void Write(params string[] error)
		{
			if (error != null)
			{
				HttpResponse response = HttpContext.Current.Response;
				response.Clear();
				response.Buffer = true;
				response.ContentType = "text/html; charset=utf-8";
				foreach (string str in error)
				{
					response.Write(str + Environment.NewLine);
				}
				response.End();
			}
		}
		public static void Write(Exception exception)
		{
			HttpResponse response = HttpContext.Current.Response;
			response.Clear();
			response.Buffer = true;
			response.ContentType = "text/html; charset=utf-8";
			string str = "Exception: ";
			Exception innerException = exception.InnerException;
			string str2 = ((str + ((innerException != null) ? innerException.ToString() : null) != null) ? exception.InnerException.Message : exception.Message) + Environment.NewLine;
			SQLException ex = exception as SQLException;
			if (ex != null)
			{
				str2 = str2 + "SQLText : " + ex.CommandText + Environment.NewLine;
				str2 = str2 + "SQLParameters : " + ex.Parameters + Environment.NewLine;
			}
			response.End();
		}
		public static void Write(SQLException ex)
		{
			HttpResponse response = HttpContext.Current.Response;
			response.Clear();
			response.Buffer = true;
			response.ContentType = "text/html; charset=utf-8";
			response.Write(ex.CommandText + Environment.NewLine);
			response.Write(ex.Parameters + Environment.NewLine);
			response.End();
		}
		public static void Write(SqlException ex)
		{
			Bug.Class624 @class = new Bug.Class624();
			@class.sqlException_0 = ex;
			HttpResponse response = HttpContext.Current.Response;
			response.Clear();
			response.Buffer = true;
			response.ContentType = "text/html; charset=utf-8";
			List<string> list = new List<string>();
			Bug.Class625 class2 = new Bug.Class625();
			class2.class624_0 = @class;
			class2.int_0 = 0;
			while (class2.int_0 < class2.class624_0.sqlException_0.Errors.Count)
			{
				if (list.Find(new Predicate<string>(class2.method_0)) == null)
				{
					list.Add(class2.class624_0.sqlException_0.Errors[class2.int_0].Message);
				}
				int int_ = class2.int_0;
				class2.int_0 = int_ + 1;
			}
			foreach (string str in list)
			{
				response.Write(str + Environment.NewLine);
			}
			response.End();
		}
		public static void Write(SqlException ex, string spName, SqlParameter[] sqlParameters)
		{
			Bug.Class626 @class = new Bug.Class626();
			@class.sqlException_0 = ex;
			HttpResponse response = HttpContext.Current.Response;
			response.Clear();
			response.Buffer = true;
			response.ContentType = "text/html; charset=utf-8";
			List<string> list = new List<string>();
			Bug.Class627 class2 = new Bug.Class627();
			class2.class626_0 = @class;
			class2.int_0 = 0;
			while (class2.int_0 < class2.class626_0.sqlException_0.Errors.Count)
			{
				if (list.Find(new Predicate<string>(class2.method_0)) == null)
				{
					list.Add(class2.class626_0.sqlException_0.Errors[class2.int_0].Message);
				}
				int int_ = class2.int_0;
				class2.int_0 = int_ + 1;
			}
			foreach (string str in list)
			{
				response.Write(str + Environment.NewLine);
			}
			string str2 = string.Empty;
			if (sqlParameters != null)
			{
				foreach (SqlParameter sqlParameter in sqlParameters)
				{
					if (sqlParameter != null)
					{
						str2 += string.Format("{0}={1}, ", sqlParameter.ParameterName, sqlParameter.Value);
					}
				}
				response.Write(spName + Environment.NewLine);
				response.Write(str2 + Environment.NewLine);
				response.End();
			}
		}
		private sealed class Class624
		{
			public SqlException sqlException_0;
		}
		private sealed class Class625
		{
			internal bool method_0(string string_0)
			{
				return string_0.Equals(this.class624_0.sqlException_0.Errors[this.int_0].Message);
			}
			public int int_0;
			public Bug.Class624 class624_0;
		}
		private sealed class Class626
		{
			public SqlException sqlException_0;
		}

		private sealed class Class627
		{
			internal bool method_0(string string_0)
			{
				return string_0.Equals(this.class626_0.sqlException_0.Errors[this.int_0].Message);
			}
			public int int_0;
			public Bug.Class626 class626_0;
		}
	}
}
