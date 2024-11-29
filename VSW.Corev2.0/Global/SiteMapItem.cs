using System;

namespace VSW.Core.Global
{
	public class SiteMapItem
	{
		public string Loc { get; set; }

		public DateTime LastMod { get; set; }

		public string Priority { get; set; }

		
		public ChangeFrequency ChangeFreq { get; set; }
	}
}
