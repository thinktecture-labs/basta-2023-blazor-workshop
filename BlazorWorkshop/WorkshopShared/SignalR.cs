using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopShared;
public static class SignalRNames
{
    public static string HubName = "/conferences";

    public static string ConferenceAdded = "ConferenceAdded";
    public static string ConferenceUpdated = "ConferenceUpdated";
}
