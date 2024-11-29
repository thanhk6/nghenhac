using System;

namespace VSW.Core.Interface
{
	public interface ITemplateServiceInterface
	{
		void VSW_Core_CPSave(ITemplateInterface item);
		ITemplateInterface VSW_Core_GetByID(int id);
	}
}
