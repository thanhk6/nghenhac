using System;
using System.Data.SqlClient;
namespace VSW.Core.Data
{
	public class SQLException : Exception
	{
		public string CommandText { get; private set; }
		public string Parameters { get; private set; }
		public SQLException(Exception exception, string command, SqlParameter[] Params) : this(exception.Message, command, Params)
		{
		}
		public SQLException(string message, string command, SqlParameter[] Params) : base(message)
		{
			this.CommandText = command;
			this.Parameters = string.Empty;
			if (Params != null)
			{
				foreach (SqlParameter sqlParameter in Params)
				{
					if (sqlParameter != null)
					{
						this.Parameters += string.Format("{0}={1}, ", sqlParameter.ParameterName, sqlParameter.Value);
					}
				}
			}
		}
	}
}
