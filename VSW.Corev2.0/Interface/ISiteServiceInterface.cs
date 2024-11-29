﻿using System;

namespace VSW.Core.Interface
{

	public interface ISiteServiceInterface
	{
		
		ISiteInterface VSW_Core_GetByCode(string code);
		ISiteInterface VSW_Core_GetByID(int id);
		ISiteInterface VSW_Core_GetDefault();
	}
}
