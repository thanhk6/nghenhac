using System;
using VSW.Core.Models;
namespace VSW.Core.Interface
{	
	public interface IPageInterface
	{
		string Custom { get; set; }
		int ID { get; set; }
		Custom Items { get; }
		int LangID { get; set; }
		int TemplateID { get; set; }
		int TemplateMobileID { get; set; }		
		int TemplateTabletID { get; set; }	
		string ModuleCode { get; set; }		
		string LinkTitle { get; set; }
		string Name { get; set; }		
		string PageTitle { get; set; }	
		string PageHeading { get; set; }
		string PageKeywords { get; set; }	
		string PageDescription { get; set; }		
		string PageURL { get; set; }
		string PageFile { get; set; }
	}
}
