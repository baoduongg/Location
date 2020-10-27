using System;
using Prism.Ioc;

namespace GetLocation.Extensions
{
    public interface IIocRegistry
    {
        void Register(IContainerRegistry containerRegistry);
    }
}
