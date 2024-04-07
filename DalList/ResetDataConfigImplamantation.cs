using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class ResetDataConfigImplamantation : IResetDataConfig
{
    public void resetDataConfig()
    {
        DataSource.Config.ResetID();
    }
}
