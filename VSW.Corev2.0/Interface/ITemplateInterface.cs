using System;
using VSW.Core.Models;

namespace VSW.Core.Interface
{
	public interface ITemplateInterface
	{
		string Custom { get; set; }
		string File { get; set; }
		int ID { get; set; }
		Custom Items { get; }
		int LangID { get; set; }
	}
}
