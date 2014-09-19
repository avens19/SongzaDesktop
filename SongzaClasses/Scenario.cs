using System.Collections.Generic;

namespace SongzaClasses
{
    public class Scenario
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Id { get; set; }
        public string SelectedMessage { get; set; }
        public List<Situation> Situations { get; set; }
        public List<int> StationIds { get; set; }
    }
}
