using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SongzaClasses;
using SongzaDesktop.Properties;
using Un4seen.Bass;

namespace SongzaDesktop
{
    public partial class SongzaForm : Form
    {
        private int _stream;
        private readonly Stack<List<SongzaMenuItem>> _menuStack;
        private Thread _monitorThread;
        private int _currentMilliseconds;
        private bool _trackPlaying;

        public SongzaForm()
        {
            InitializeComponent();
            _menuStack = new Stack<List<SongzaMenuItem>>();
            _trackPlaying = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Icon = System.Drawing.Icon.ExtractAssociatedIcon("songza.ico");

            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);

            _mainList.Items.Add(new SongzaMenuItem("Concierge"));
            _mainList.Items.Add(new SongzaMenuItem("Browse"));

            if (API.IsLoggedIn())
            {
                _mainList.Items.Add(new SongzaMenuItem("Recent"));
                _mainList.Items.Add(new SongzaMenuItem("Favorites"));
            }

            _mainList.Items.Add(new SongzaMenuItem("Search Artists"));
            _mainList.Items.Add(new SongzaMenuItem("Search Stations"));
        }

        private void PlaySong(Track t)
        {
            if (_monitorThread != null)
            {
                _monitorThread.Abort();
                _monitorThread = null;
            }
            Bass.BASS_StreamFree(_stream);
            _stream = Bass.BASS_StreamCreateURL(t.ListenUrl, 0, BASSFlag.BASS_DEFAULT, null, IntPtr.Zero);

            if (_stream == 0)
            {
                var err = Bass.BASS_ErrorGetCode();
                Trace.TraceError(err.ToString());
                MessageBox.Show(err.ToString());
                return;
            }
            Bass.BASS_ChannelPlay(_stream, false);
            _trackPlaying = true;

            UpdateUi(t);

            _currentMilliseconds = 0;
            _monitorThread = new Thread(MonitorPlay);
            _monitorThread.Start();
        }

        private void UpdateUi(Track t)
        {
            _trackTitle.Text = t.Song.Title;
            _artist.Text = t.Song.Artist.Name;
            _album.Text = t.Song.Album;
            _albumArt.LoadAsync(t.Song.CoverUrl);
            _station.Text = t.Station.Name;
            _duration.Text = t.Song.DurationTimespan.ToString(@"m\:ss");
            _playPause.Enabled = true;
            _playPause.Text = Resources._form_UpdateUi_Pause;
        }

        private void ClearUi()
        {
            _trackTitle.Text = "";
            _artist.Text = "";
            _album.Text = "";
            _albumArt.Image = null;
            _station.Text = "";
            _duration.Text = "";
            _playPause.Text = Resources.SongzaForm__playPause_Click_Play;
            _playPause.Enabled = false;
            _currentTime.Text = "";
            _duration.Text = "";
            _progress.Value = 0;
            _trackPlaying = false;
            _currentMilliseconds = 0;
        }

        private void MonitorPlay()
        {
            while (_stream != 0)
            {
                if (_trackPlaying)
                {
                    TimeSpan duration = API.GetCurrentTrack().Song.DurationTimespan;
                    double progress = _currentMilliseconds*100000.0/duration.TotalMilliseconds;
                    int seconds = _currentMilliseconds/1000;
                    var current = new TimeSpan(0, 0, seconds);

                    _progress.Invoke(new Action(() =>
                    {
                        _progress.Value = (int) progress;
                        _currentTime.Text = current.ToString(@"m\:ss");
                    }));

                    if (_currentMilliseconds >= duration.TotalMilliseconds)
                    {
                        Invoke(new Action(NextTrack));

                        return;
                    }
                }

                Thread.Sleep(500);

                if (_trackPlaying)
                {
                    _currentMilliseconds += 500;
                }
            }
        }

        private void _mainList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_mainList.SelectedItem == null)
            {
                return;
            }

            _menuStack.Push(new List<SongzaMenuItem>(_mainList.Items.Cast<SongzaMenuItem>()));
            MenuClick((SongzaMenuItem) _mainList.SelectedItem);
        }

        private async void MenuClick(SongzaMenuItem item)
        {
            switch (item.Type)
            {
                case SongzaMenuItemType.Menu:
                    switch (item.Name)
                    {
                        case "Concierge":
                            int day, period;
                            API.DayAndPeriod(out day, out period);
                            var scenarios = await API.ConciergeCategories(day, period);
                            ListScenarios(scenarios);
                            break;
                        case "Browse":
                            var categories = await API.Categories();
                            ListCategories(categories);
                            break;
                        case "Recent":
                            var stations = await API.Recent();
                            ListStations(stations);
                            break;
                        case "Favorites":
                            var favorites = await API.Favorites();
                            var favorite = favorites.Single(f => f.Title == "Favorites");
                            ListStations(await API.ListStations(favorite.StationIds.ConvertAll(s => s.ToString()).ToArray()));
                            break;
                        case "Search Artists":
                            var sf = new SearchForm {Type = SearchType.Artist};
                            sf.ShowDialog();
                            if (sf.DialogResult == DialogResult.OK)
                            {
                                ListArtists(sf.Artists);
                            }
                            break;
                        case "Search Stations":
                            sf = new SearchForm {Type = SearchType.Station};
                            sf.ShowDialog();
                            if (sf.DialogResult == DialogResult.OK)
                            {
                                ListStations(sf.Stations);
                            }
                            break;
                    }
                    break;
                case SongzaMenuItemType.Category:
                    var category = (Category) item.Tag;
                    var subcategories = await API.SubCategories(category.Id);
                    ListSubcategories(subcategories);
                    break;
                case SongzaMenuItemType.Scenario:
                    var scenario = (Scenario) item.Tag;
                    if (scenario.Situations != null)
                    {
                        ListSituations(scenario.Situations);
                    }
                    else
                    {
                        ListStations(await API.ListStations(scenario.StationIds.ConvertAll(s => s.ToString()).ToArray()));
                    }
                    break;
                case SongzaMenuItemType.Situation:
                    var situation = (Situation) item.Tag;
                    ListStations(await API.ListStations(situation.StationIds.ConvertAll(s => s.ToString()).ToArray()));
                    break;
                case SongzaMenuItemType.Subcategory:
                    var subcategory = (SubCategory) item.Tag;
                    ListStations(await API.ListStations(subcategory.StationIds.ConvertAll(s => s.ToString()).ToArray()));
                    break;
                case SongzaMenuItemType.Station:
                    PlayStation((Station) item.Tag);
                    break;
                case SongzaMenuItemType.Artist:
                    var artist = (Track.Artist) item.Tag;
                    ListStations(await API.StationsForArtist(artist.Id));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ListScenarios(IEnumerable<Scenario> scenarios)
        {
            _mainList.Items.Clear();

            foreach (var s in scenarios)
            {
                _mainList.Items.Add(new SongzaMenuItem(s));
            }
        }

        private void ListCategories(IEnumerable<Category> categories)
        {
            _mainList.Items.Clear();

            foreach (var s in categories)
            {
                _mainList.Items.Add(new SongzaMenuItem(s));
            }
        }

        private void ListSituations(IEnumerable<Situation> situations)
        {
            _mainList.Items.Clear();

            foreach (var s in situations)
            {
                _mainList.Items.Add(new SongzaMenuItem(s));
            }
        }

        private void ListSubcategories(IEnumerable<SubCategory> subcategories)
        {
            _mainList.Items.Clear();

            foreach (var s in subcategories)
            {
                _mainList.Items.Add(new SongzaMenuItem(s));
            }
        }

        private void ListArtists(IEnumerable<Track.Artist> artists)
        {
            _mainList.Items.Clear();

            foreach (var s in artists)
            {
                _mainList.Items.Add(new SongzaMenuItem(s));
            }
        }

        private void ListStations(IEnumerable<Station> stations)
        {
            _mainList.Items.Clear();

            foreach (var s in stations)
            {
                _mainList.Items.Add(new SongzaMenuItem(s));
            }
        }

        private void _back_Click(object sender, EventArgs e)
        {
            if (_menuStack.Count == 0)
            {
                return;
            }

            var list = _menuStack.Pop();
            _mainList.Items.Clear();
            foreach (var songzaMenuItem in list)
            {
                _mainList.Items.Add(songzaMenuItem);
            }
        }

        private async void PlayStation(Station station)
        {
            Track t = await API.NextTrack(station);
            PlaySong(t);
        }

        private async void NextTrack()
        {
            Track t = await API.NextTrack(API.GetCurrentStation());
            PlaySong(t);
        }

        private void _next_Click(object sender, EventArgs e)
        {
            NextTrack();
        }

        private void _form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_monitorThread != null)
            {
                _monitorThread.Abort();
                _monitorThread = null;
            }
            Bass.BASS_StreamFree(_stream);
        }

        private void PlayPause()
        {
            if (_playPause.Text == Resources._form_UpdateUi_Pause)
            {
                _playPause.Text = Resources.SongzaForm__playPause_Click_Play;
                Bass.BASS_Pause();
                _trackPlaying = false;
            }
            else
            {
                _playPause.Text = Resources._form_UpdateUi_Pause;
                Bass.BASS_Start();
                _trackPlaying = true;
            }
        }

        private void _playPause_Click(object sender, EventArgs e)
        {
            PlayPause();
        }

        private void Stop()
        {
            Bass.BASS_Stop();
            ClearUi();
        }

        private void _stop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void _login_Click(object sender, EventArgs e)
        {
            var lf = new LoginForm();
            lf.ShowDialog();
        }
    }
}