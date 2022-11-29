using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grains.DataAccess
{
    internal interface IDocumentStore<TType> where TType : class
    {
        TType Get(Guid id);
        TType[] Get(Guid[] ids);
    }
}
