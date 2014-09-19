using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SongzaClasses;

namespace SongzaDesktop
{
    public partial class SearchForm : Form
    {
        public SearchType Type { get; set; }
        public IEnumerable<Track.Artist> Artists { get; set; }
        public IEnumerable<Station> Stations { get; set; }

        public SearchForm()
        {
            InitializeComponent();
        }

        private async void _search_Click(object sender, EventArgs e)
        {
            if (Type == SearchType.Artist)
            {
                try
                {
                    Artists = await API.QueryArtists(_query.Text);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception)
                {
                    DialogResult = DialogResult.Abort;
                    Close();
                }
            }
            else
            {
                try
                {
                    Stations = await API.QueryStations(_query.Text);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception)
                {
                    DialogResult = DialogResult.Abort;
                    Close();
                }
            }
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {
            Text = string.Format("{0} Search", Type);
        }
    }
}