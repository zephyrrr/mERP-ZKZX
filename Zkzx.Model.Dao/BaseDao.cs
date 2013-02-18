using System;
using System.Collections.Generic;
using System.Text;
using Feng;

namespace Zkzx.Model
{
    public class BaseDao<T> : MultiOrgEntityDao<T>
        where T : class, IMultiOrgEntity, ILogEntity
    {
    }

}
