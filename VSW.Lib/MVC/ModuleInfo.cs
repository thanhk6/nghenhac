using System;
using VSW.Core.Interface;

namespace VSW.Lib.MVC
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleInfo : Attribute, IModuleInterface
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsControl { get; set; }
        public int Order { get; set; }
        public bool Activity { get; set; } = true;

        public Type ModuleType { get; set; }
        public bool Crawl { get; set; }
    }
}