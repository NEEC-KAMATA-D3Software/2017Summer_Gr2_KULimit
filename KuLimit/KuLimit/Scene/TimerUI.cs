using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using KuLimit.Utility;
using KuLimit.Device;

namespace KuLimit.Scene
{
    class TimerUI
    {
        private Timer timer;

        public TimerUI(Timer timer)
        {
            this.timer = timer;
        }

        public void Draw(Renderer renderer)
        {

            renderer.DrawTexture("timer", new Vector2(400, 10));
            //時間の計算と文字列化
            string gameTime = (timer.Now() / 60.0f).ToString();
            //描画したい数字が5桁以上は小数点含め5桁で描画
            int digit = 5;
            if (gameTime.Length > digit)
            {
                renderer.DrawNumber("number", new Vector2(600, 13),
                    gameTime, digit);
            }
            else
            {
                renderer.DrawNumber("number", new Vector2(600, 13),
                    gameTime, gameTime.Length);
            }

        }

    }

}
