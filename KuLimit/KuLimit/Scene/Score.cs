using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;              //vector用
using KuLimit.Device;

namespace KuLimit.Scene
{
    class Score
    {
        //フィールド
        private int score;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Score()
        {
            Initialize();
        }

        public void Initialize()
        {
            score = 0;
        }

        public void Add()
        {
            //score += 1;
            score++;
        }

        public void Add(int num)
        {
            score += num;
        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("score", new Vector2(50, 10));
            renderer.DrawNumber("number", new Vector2(200, 13), score);

        }
    }
}
