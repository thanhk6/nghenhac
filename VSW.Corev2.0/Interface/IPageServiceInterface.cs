using System;
using VSW.Core.MVC;

namespace VSW.Core.Interface
{
	
	public interface IPageServiceInterface
	{
		
		void VSW_Core_CPSave(IPageInterface item);

		
		IPageInterface VSW_Core_CurrentPage(ViewPage viewPage);

		
		IPageInterface VSW_Core_GetByID(int id);
	}
}
