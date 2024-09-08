using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fotbal
{
    public class CustomButton : Button
    {
        public bool StangaSus { get; set; }
        public bool Sus { get; set; }
        public bool DreaptaSus { get; set; }

        public bool Stanga { get; set; }
        public bool Dreapta { get; set; }

        public bool StangaJos { get; set; }
        public bool Jos { get; set; }
        public bool DreaptaJos { get; set; }
        public CustomButton()
        {
            StangaSus = false;
            Sus = false;
            DreaptaSus = false;

            Stanga = false;
            Dreapta = false;

            StangaJos = false;
            Jos = false;
            DreaptaJos = false;
        }
        

    }
}
