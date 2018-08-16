using BirdHunter.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdHunter
{
    class CDuck : CImageBase
    {
        private Rectangle _duckHotSpot = new Rectangle();
        public CDuck() : base(Resources.Duck)
        {
            _duckHotSpot.X = Left + 40;
            _duckHotSpot.Y = Top + 20;
            _duckHotSpot.Width = 40;
            _duckHotSpot.Height = 40;

        }
        public void Update(int X, int Y)
        {
            Left = X;
            Top = Y;
            _duckHotSpot.X = Left + 40;
            _duckHotSpot.Y = Top + 20;
        }
        public bool Hit(int X, int Y)
        {
            Rectangle c = new Rectangle(X, Y, 2, 2); //if the cursor is inside the bird return true
            if (_duckHotSpot.Contains(c))//if you are inside hotspot return true
            {
                return true;

            }
            return false;
        }
        //internal void Update(object p1, object p2)
        //{
        //    throw new NotImplementedException();
        //}
        //internal void Update(object p)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
