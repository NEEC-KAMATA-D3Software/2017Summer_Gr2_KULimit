using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using KuLimit.Device;

namespace KuLimit.Scene
{
    class Ending : IScene
    {
        private InputState input;
        private bool isEnd;
        private Sound sound;
        private IScene gamePlay;
        private int pastMinute;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="gameDevice">ゲームデバイス</param>
        /// <param name="gamePaly">ゲームプレイシーン</param>
        public Ending(GameDevice gameDevice, IScene gamePlay)
        {
            this.input = gameDevice.GetInputState();
            this.gamePlay = gamePlay;
            this.sound = gameDevice.GetSound();
            isEnd = false;
        }

        public void Initialize()
        {
            isEnd = false;
            pastMinute = (int)(gamePlay.PastSecond() / 60);
        }

        public void Update(GameTime gameTime)
        {

            pastMinute = (int)(gamePlay.PastSecond() / 60);
            sound.PlayBGM("endingbgm");
            if (input.IskeyDown(Keys.Space))
            {
                isEnd = true;
                sound.PlaySE("titlese");
            }
        }
        public void Draw(Renderer renderer)
        {
            //エンディングシーンでは、ゲームプレイ画像を背景に
            gamePlay.Draw(renderer);
            //エンディング画像の描画
            renderer.Begin();
            renderer.DrawTexture("ending", new Vector2 (0, -200));
            renderer.DrawTexture("record", new Vector2(-5, 160));
            renderer.DrawText4((gamePlay.PastSecond() - pastMinute * 60).ToString("00"), new Vector2 (530, 300));
            renderer.DrawText4(pastMinute.ToString("00"), new Vector2(340, 300));
            renderer.DrawText4("Press Spacebar", new Vector2 (320, 480));
            renderer.End();
        }
        
        public void ShutDown()
        {
            sound.StopBGM();
        }

        public bool IsEnd()
        {
            return isEnd;
        }

        public Scene Next()
        {
            return Scene.Title;
        }

        public float PastSecond()
        {
            return 0.0f;
        }
    }
}
