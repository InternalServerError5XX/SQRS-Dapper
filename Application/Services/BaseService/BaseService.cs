using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Application.Services.BaseService
{
    public class BaseService<Entity> : IBaseService<Entity> where Entity : class
    {

    }
}
