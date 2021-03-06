﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KuLimit.Device
{
    class GameDevice
    {
        private Renderer renderer;              //描画
        private InputState input;               //入力
        private Sound sound;                    //サウンド
        private Random rand;                    //乱数

        public GameDevice(ContentManager contentManager, GraphicsDevice graphics)
        {
            renderer = new Renderer(contentManager, graphics);
            input = new InputState();
            sound = new Sound(contentManager);
            rand = new Random();
        }

        public void Initialize()
        {

        }

        public void Update(GameTime gameTime)
        {
            //デバイスで絶対に更新が必要なもの
            input.Update();
        }

        /// <summary>
        /// 描画オブジェクトの取得
        /// </summary>
        /// <returns></returns>
        public Renderer GetRenderer()
        {
            return renderer;
        }

        /// <summary>
        /// 入力オブジェクトの取得
        /// </summary>
        /// <returns></returns>
        public InputState GetInputState()
        {
            return input;
        }

        /// <summary>
        /// サウンドオブジェクトの取得
        /// </summary>
        /// <returns></returns>
        public Sound GetSound()
        {
            return sound;
        }

        /// <summary>
        /// 乱数オブジェクトの取得
        /// </summary>
        /// <returns></returns>
        public Random GetRandom()
        {
            return rand;
        }
    }
}
