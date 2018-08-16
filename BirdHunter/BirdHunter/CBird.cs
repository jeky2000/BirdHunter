using BirdHunter.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdHunter 
{
    class CBird : CImageBase
    {
        private Rectangle _birdHotSpot = new Rectangle();

        public CBird(): base(Resources.Bird)
        {
            _birdHotSpot.X = Left + 40;
            _birdHotSpot.Y = Top +40;
            _birdHotSpot.Width = 40;
            _birdHotSpot.Height = 40; //pixal
            
        }
        public void Update(int X, int Y)
        {
            Left = X;
            Top = Y;
            _birdHotSpot.X = Left + 40; // bird shot point ,/ bilden är fyrkant
            _birdHotSpot.Y = Top +40;
        }
        public bool Hit(int X, int Y)
        {
            Rectangle c = new Rectangle(X, Y, 2, 2);
            if(_birdHotSpot.Contains(c))//if you are inside hotspot return true
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
