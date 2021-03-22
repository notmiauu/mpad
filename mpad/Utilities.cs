using System.Linq;
using static mpad.Settings;

namespace mpad
{
    public class mUtils
    {
        internal static void updateExtensions(bool cleanExts)
        {
            switch (cleanExts)
            {
                case false:
                    Data.exts = getSupportedExtensions().Split(',').ToList();
                    break;
                case true:
                    Data.exts.Clear();
                    break;
            }
        }

        internal static bool fileStringMatch(int dataChecking, string newData = "")
        {
            switch (dataChecking)
            {
                case 1:
                    return impConfig.version >= latestVer;
                case 2:
                    return false;
                case 3:
                    return false;
                default:
                    return false;
            }
        }
    }
}

/*
 * 1. Config Check
 * 2. 
*/