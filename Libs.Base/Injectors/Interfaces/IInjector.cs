using System.Collections.Generic;

namespace Libs.Base.Injectors.Interfaces
{
    public interface IInjector<T>
    {
        T Get();
        IEnumerable<T> GetAll();
    }
}