using System.ComponentModel.DataAnnotations;

namespace WorkshopShared
{
    public class ConferenceDetails
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        public string Url { get; set; }
    }
}
