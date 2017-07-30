using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using KuLimit.Device;
using KuLimit.Utility;


namespace KuLimit.Scene
{
    class Title : IScene
    {
        private InputState input;
        private bool isEnd;
        private Sound sound;
        //private Motion motion;

        public Title(GameDevice gameDevice)
        {
            this.input = gameDevice.GetInputState();
            isEnd = false;
            this.sound = gameDevice.GetSound();
        }

        public void Initialize()
        {
            isEnd = false;

            //motion = new Motion();
            //motion.Initialize(new Range(0, 0), new Timer(0.01f));

        }
        public void Update(GameTime gameTime)
        {
            sound.PlayBGM("titlebgm");
            //motion.Update(gameTime);
            //スペースキーが押されたら次のシーンへ
            if (input.GetKeyTrigger(Keys.Space))
            {
                sound.PlaySE("titlese");
                isEnd = true;
            }
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("title", Vector2.Zero);
            renderer.End();
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void ShutDown()
        {
            sound.StopBGM();
        }

        /// <summary>
        /// シーンが終了したか？
        /// </summary>
        /// <returns></returns>
        bool IScene.IsEnd()
        {
            return isEnd;
        }

        /// <summary>
        /// 次のシーン名を返す
        /// </summary>
        /// <returns></returns>
        public Scene Next()
        {
            //次のシーンはゲームプレイ
            return Scene.GamePlay;
        }

        public float PastSecond()
        {
            return 0.0f;
        }
    }
}

