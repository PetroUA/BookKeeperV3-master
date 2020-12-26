using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookKeeper_Entity
{
    public interface IWithId<TKey> where TKey : struct
    {
        TKey Id { get; set; }
    }
}
