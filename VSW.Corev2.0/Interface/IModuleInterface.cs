using System;

namespace VSW.Core.Interface
{
	public interface IModuleInterface
	{
		string Code { get; set; }
		Type ModuleType { get; set; }
	}
}
