using System;
using VSW.Core.Models;

namespace VSW.Core.Interface
{
	public interface ISiteInterface
	{
		string Code { get; set; }
		int ID { get; set; }
		Custom Items { get; }
		int LangID { get; set; }
		int PageID { get; set; }
	}
}
