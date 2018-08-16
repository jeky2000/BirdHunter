//#define My_Debug
using BirdHunter.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BirdHunter
{
    public partial class BirdHunter : Form
    
    {
        
        int _gameFrame = 0;
        int _splatTime = 0;
#if My_Debug
        int _cursX=0;
        int _cursY=0;
#endif
        //const int FrameNumD = 5;
        const int FrameNum = 8;
        const int SplatNum = 3;
        bool splat = false;
        int _hits = 0;
        int _misses = 0;
        int _totalShots = 0;
        double _avrageHits = 0;

        CLovisa _lovisa;//drwa lovisa
        CBird _bird;//draw bird
        CDuck _duck; //draw duck
        CSign _sign;
        CScoreFrame _scoreframe;
        CSplat _splat;
        Random rnd = new Random();
        //CPlayStop _playstop;
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        public BirdHunter()
        {
            
            InitializeComponent();
            //create scope site
            player.SoundLocation = "electromania118.wav";
            BirdSound();
            
            Bitmap b = new Bitmap(Resources.site);
            this.Cursor = CustomCursor.CreateCursor(b, b.Height, b.Width);

            _bird = new CBird() { Left = 700, Top = 275 };//draw bird
            _lovisa = new CLovisa() { Left = 100, Top = 400 }; //drwa lovisa
            _duck = new CDuck() { Left = 200, Top = 10 };//draw duck
            _sign = new CSign() { Left = 10, Top = 400 };
            _scoreframe = new CScoreFrame() { Left = 0, Top = 0 };
            _splat = new CSplat();
            //_playstop = new CPlayStop() { Left = 800, Top = 0 };
        }

        private void timerGameLoop_Tick(object sender, EventArgs e)
        {
           
            //if(_gameFrame == FrameNumD)
            //{
            //    UpdateDuck();

            //}
            if (_gameFrame == 3)
            {

            }
            if(_gameFrame >= FrameNum)
            {
                UpdateBird();
                
                _gameFrame = 0;

            }
            if(splat)
            {
                if (_splatTime >= SplatNum)
                {
                    splat = false;
                    _splatTime = 0;
                    UpdateBird();
                    //UpdateDuck();
                }
                _splatTime++;
            }
            _gameFrame++;
            
            this.Refresh();//bird uppdate every 100
        }
        private void UpdateBird()
        {
            _bird.Update(rnd.Next(this.Width/3, this.Width - Resources.Bird.Width),
                rnd.Next( this.Height - Resources.Bird.Height ));
        }
        //private void UpdateDuck()
        //{
        //    _duck.Update(rnd.Next(Resources.Duck.Width, this.Width - Resources.Duck.Width),
        //    (this.Height/4 - Resources.Duck.Height));
        //}
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics dc = e.Graphics;
            if (splat == true)
            {
                _splat.DrawImage(dc);
            }
            else
            {
                _bird.DrawImage(dc);
            }
            
            
            _lovisa.DrawImage(dc);//drwa lovisa
            _duck.DrawImage(dc);//draw duck
            _sign.DrawImage(dc);
            _scoreframe.DrawImage(dc);
            //_playstop.DrawImage(dc);
#if My_Debug
            TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis;
            Font _font = new System.Drawing.Font("Stencil", 12, FontStyle.Regular);
            TextRenderer.DrawText(dc, "X =" + _cursX.ToString() + ":" + "Y=" + _cursY.ToString(), _font,
                new Rectangle(1, 1, 120, 20), SystemColors.ControlText, flags);
#endif
            _bird.DrawImage(dc);//draw bird

            //put score on the screen
            TextFormatFlags flags = TextFormatFlags.Left;
            Font _font = new System.Drawing.Font("Arial", 10, FontStyle.Bold); // how the text look like
            TextRenderer.DrawText(e.Graphics, "Shots : " + _totalShots.ToString(), _font, new Rectangle(30, 32, 120, 20), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Hits : " + _hits.ToString(), _font, new Rectangle(30, 52, 120, 20), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Miss : " + _misses.ToString(), _font, new Rectangle(30, 72, 120, 20), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Good : " + _avrageHits.ToString("F0") + "%", _font, new Rectangle(30, 92, 120, 20), SystemColors.ControlText, flags);
            base.OnPaint(e);
        }

        private void BirdHunter_MouseMove(object sender, MouseEventArgs e)
        {
#if My_Debug
            _cursX = e.X;
            _cursY = e.Y;
#endif
            //this.Refresh();
            
        }

        private void BirdHunter_MouseClick(object sender, MouseEventArgs e)
        {
           
            if( e.X > 140 && e.X < 216 && e.Y > 456  && e.Y < 481)
            {
                timerGameLoop.Start();
            }
            else if (e.X > 131 && e.X < 200 && e.Y > 495  && e.Y < 520)
            {
                this.Invalidate();
            }
            else if (e.X > 136 && e.X < 200 && e.Y > 530  && e.Y < 554)
            {
                timerGameLoop.Stop();
            }
            else if (e.X > 140 && e.X < 200 && e.Y > 570  && e.Y < 595)
            {
                this.Close();
            }
            ////playstop buttons
            //else if (e.X > 834 && e.X < 910 && e.Y > 40 && e.Y < 115)
            //{
            //    this.player.Play();
            //}
            //else if (e.X > 834 && e.X < 910 && e.Y > 40 && e.Y < 115)
            //{
            //    player.Play();
            //}
            ////endplaystop
            else
            {
                if (_bird.Hit(e.X, e.Y))
                {
                    splat = true;
                    _splat.Left = _bird.Left - Resources.Splat.Width/5 ;
                    _splat.Top = _bird.Top - Resources.Splat.Height /5;
                    _hits++;

                }
                else
                {
                    _misses++;
                }

                _totalShots = _hits + _misses;
                _avrageHits = (Double)_hits / (Double)_totalShots * 100.0;

            }

            
            FireGun();
           

        }
        private void FireGun()
        {
            SoundPlayer simpleSound = new SoundPlayer(Resources.Shotgun);
            simpleSound.Play();
        }
        private void BirdSong()
        {
            SoundPlayer simpleSound1 = new SoundPlayer(Resources.Birds);
            simpleSound1.Play();
        }
        public void BirdSound()
        {
            SoundPlayer simpleSound2 = new SoundPlayer(Resources.Birdsong);
            simpleSound2.PlayLooping();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            player.PlayLooping();
        }
    }
}
