using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AiLaTrieuPhu_DEMO
{
    public class SoundHelper
    {
        private static SoundPlayer clickSound;

        static SoundHelper()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Music", "clickar.wav");
            if (File.Exists(path))
            {
                clickSound = new SoundPlayer(path);
                clickSound.Load();
            }
        }

        public static void PlayClick()
        {
            clickSound?.PlaySync(); // hoặc .Play() nếu bạn muốn không block
        }
    }
}
