using System;

namespace MovieAPI.Models
{
    public class MovieLogModel
    {
        public string SearchToken { get; set; }
        public string ImdbID { get; set; }
        public long ProcessingTimeInMs { get; set; }
        public DateTime Timetamp { get; set; }
        public string IpAddress { get; set; }
    }
}
