using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using SongzaClasses;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Aac;
using Un4seen.Bass.Misc;

namespace SongzaDesktop
{
    public partial class _form : Form
    {
        private int _stream;
        private readonly Stack<List<SongzaMenuItem>> _menuStack;
        private Thread _monitorThread;
        private int _currentMilliseconds;

        public _form()
        {
            InitializeComponent();
            _menuStack = new Stack<List<SongzaMenuItem>>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);

            _mainList.Items.Add(new SongzaMenuItem("Concierge"));
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
            }
            Bass.BASS_ChannelPlay(_stream, false);

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
        }

        private void MonitorPlay()
        {
            while (_stream != 0)
            {
                TimeSpan duration = API.GetCurrentTrack().Song.DurationTimespan;
                double progress = _currentMilliseconds*100000.0/duration.TotalMilliseconds;
                int seconds = _currentMilliseconds/1000;
                var current = new TimeSpan(0,0,(int)seconds);

                _progress.Invoke(new Action(() =>
                {
                    _progress.Value = (int)progress;
                    _currentTime.Text = current.ToString(@"m\:ss");
                }));

                if (_currentMilliseconds >= duration.TotalMilliseconds)
                {
                    Invoke(new Action(NextTrack));
                    
                    return;
                }

                Thread.Sleep(500);
                _currentMilliseconds += 500;
            }
        }

        private void _mainList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_mainList.SelectedItem == null)
            {
                return;
            }

            _menuStack.Push(new List<SongzaMenuItem>(_mainList.Items.Cast<SongzaMenuItem>()));
            MenuClick((SongzaMenuItem)_mainList.SelectedItem);
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
                    }
                    break;
                case SongzaMenuItemType.Category:
                    break;
                case SongzaMenuItemType.Scenario:
                    var scenario = (Scenario) item.Tag;
                    if (scenario.Situations != null)
                    {
                        ListSituations(scenario.Situations);
                    }
                    else
                    {
                        ListStations(await API.ListStations(scenario.StationIds.ConvertAll<string>(s => s.ToString()).ToArray()));
                    }
                    break;
                case SongzaMenuItemType.Situation:
                    var situation = (Situation)item.Tag;
                    ListStations(await API.ListStations(situation.StationIds.ConvertAll<string>(s => s.ToString()).ToArray()));
                    break;
                case SongzaMenuItemType.Subcategory:
                    break;
                case SongzaMenuItemType.Station:
                    PlayStation((Station)item.Tag);
                    break;
                case SongzaMenuItemType.Artist:
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

        private void ListSituations(IEnumerable<Situation> situations)
        {
            _mainList.Items.Clear();

            foreach (var s in situations)
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
    }
}
