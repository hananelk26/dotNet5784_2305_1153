using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal static class Config
{
    // Default XML file name for configuration
    static string s_data_config_xml = "data-config";

    // Property to get and increase the next Task ID
    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }

    // Property to get and increase the next Dependency ID
    internal static int NextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId"); }
}