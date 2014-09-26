using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongzaClasses
{
    public class Equalizer
    {
        public List<Band> Bands { get; set; }

        public static Equalizer Default
        {
            get
            {
                return new Equalizer
                {
                    Bands = new List<Band>()
                    {
                        new Band
                        {
                            Frequency = 32f,
                            Gain = 0f
                        },
                        new Band
                        {
                            Frequency = 64f,
                            Gain = 0f
                        },
                        new Band
                        {
                            Frequency = 128f,
                            Gain = 0f
                        },
                        new Band
                        {
                            Frequency = 250f,
                            Gain = 0f
                        },
                        new Band
                        {
                            Frequency = 500f,
                            Gain = 0f
                        },
                        new Band
                        {
                            Frequency = 1000f,
                            Gain = 0f
                        },
                        new Band
                        {
                            Frequency = 2000f,
                            Gain = 0f
                        },
                        new Band
                        {
                            Frequency = 4000f,
                            Gain = 0f
                        },
                        new Band
                        {
                            Frequency = 8000f,
                            Gain = 0f
                        },
                        new Band
                        {
                            Frequency = 16000f,
                            Gain = 0f
                        }
                    }
                };
            }
        }
    }

    public class Band
    {
        public float Frequency { get; set; }
        public float Gain { get; set; }
    }
}
