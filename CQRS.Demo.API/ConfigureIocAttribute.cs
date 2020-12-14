using System;

namespace CQRS.Demo.API
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigureIocAttribute : Attribute
    {
        public ScopeType ScopeType { get; private set; }

        public ConfigureIocAttribute(ScopeType scopeType = ScopeType.Scoped)
        {
            ScopeType = scopeType;
        }
    }

    public enum ScopeType
    {
        Scoped = 0,
        Transient = 1,
        Singleton = 2
    }
}
