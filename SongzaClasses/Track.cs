using System;

namespace SongzaClasses
{
    public class Track
    {
        public string ListenUrl { get; set; }
        public TrackDetails Song { get; set; }
        public Station Station { get; set; }
        public string ThumbUpState { get; set; }

        public Track()
        {
            ThumbUpState = "0";
        }

        public class TrackDetails
        {
            public string Album { get; set; }
            public Artist Artist { get; set; }
            public string Title { get; set; }
            public string CoverUrl { get; set; }
            public string Genre { get; set; }
            public string Duration { get; set; }
            public string Id { get; set; }

            public TimeSpan DurationTimespan
            {
                get
                {
                    int seconds = int.Parse(Duration);
                    return new TimeSpan(0,0,seconds);
                }
            }
        }

        public class Artist
        {
            public string Id { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}
