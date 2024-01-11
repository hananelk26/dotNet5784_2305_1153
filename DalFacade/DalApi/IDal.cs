using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public interface IDal
{
    IDependency dependency { get; }
    IEngineer engineer { get; }
    ITask task { get; }
}
