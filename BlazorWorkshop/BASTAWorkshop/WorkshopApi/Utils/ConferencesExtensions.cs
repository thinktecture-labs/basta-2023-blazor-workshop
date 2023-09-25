using WorkshopApi.Database;
using WorkshopShared;

namespace WorkshopApi.Utils
{
    public static class ConferencesExtensions
    {
        public static ConferenceDetails ToDetails(this Conference conf)
        => new ConferenceDetails() { Id = conf.Id, Title = conf.Title, City = conf.City, DateFrom = conf.DateFrom, DateTo = conf.DateTo, Country = conf.Country, Url = conf.Url, };

        public static ConferenceOverview ToOverview(this Conference conf)
        => new ConferenceOverview() { Id = conf.Id, Title = conf.Title, };

        public static Conference ToConference(this ConferenceDetails details)
        => new Conference() { Id = details.Id, Title = details.Title, City = details.City, DateFrom = details.DateFrom, DateTo = details.DateTo, Country = details.Country, Url = details.Url, };
    }



}

