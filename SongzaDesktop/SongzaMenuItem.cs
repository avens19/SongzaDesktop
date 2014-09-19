using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SongzaClasses;

namespace SongzaDesktop
{
    public enum SongzaMenuItemType
    {
        Menu,
        Category,
        Scenario,
        Situation,
        Subcategory,
        Station,
        Artist
    }

    public class SongzaMenuItem
    {
        public string Name { get; set; }
        public SongzaMenuItemType Type { get; set; }
        public object Tag { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public SongzaMenuItem(string name)
        {
            Name = name;
            Type = SongzaMenuItemType.Menu;
        }

        public SongzaMenuItem(Category c)
        {
            Name = c.Name;
            Type = SongzaMenuItemType.Category;
            Tag = c;
        }

        public SongzaMenuItem(Scenario s)
        {
            Name = s.Title;
            Type = SongzaMenuItemType.Scenario;
            Tag = s;
        }

        public SongzaMenuItem(Situation s)
        {
            Name = s.Title;
            Type = SongzaMenuItemType.Situation;
            Tag = s;
        }

        public SongzaMenuItem(Station s)
        {
            Name = s.Name;
            Type = SongzaMenuItemType.Station;
            Tag = s;
        }

        public SongzaMenuItem(SubCategory s)
        {
            Name = s.Name;
            Type = SongzaMenuItemType.Subcategory;
            Tag = s;
        }

        public SongzaMenuItem(Track.Artist a)
        {
            Name = a.Name;
            Type = SongzaMenuItemType.Artist;
            Tag = a;
        }
    }
}
