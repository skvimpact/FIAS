using System.Collections.Generic;

namespace Fias.Repos
{
    public interface IRepo<in T>
    {       
        void AddRange(IEnumerable<T> items);
    }
}
