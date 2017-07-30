using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KuLimit.Device;
using KuLimit.Def;
using KuLimit.Utility;


namespace KuLimit.Actor
{
    class Player : Character
    {
        private InputState inputState;
        private float playerTime;
        private float second;
        
        public Player(InputState input) : base("player", 32.0f)
        {
            inputState = input;
            
        }

        public override void Initialize()
        {
            position = new Vector2(64.0f * 5.0f, 600.0f);
            playerTime = 25.0f;
            second = playerTime * 60.0f;
        }


        public override void Update(GameTime gameTime,int pastTime)
        {
            second--;
            playerTime = second / 60.0f;
            
            var velocity = inputState.Velocity();
            position = position + velocity;

            //壁とのあたり判定   (Clampメソッド)
            //Vector2.Clamp(Vector2value1,Vector2min,Vector2max)
            float size = 64.0f;
            var min = Vector2.Zero;
            var max = new Vector2(Screen.Width - size, Screen.Height - size);
            position = Vector2.Clamp(position, min, max);

        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
            renderer.DrawText2(playerTime.ToString("000"), new Vector2(570, 660));
        }

        public float GetTime()
        {
            return playerTime;
        }

        public void AddTime(float time)
        {
            second += time * 60.0f;
        }

        public void ChangeTime(float playerTime)
        {
            this.playerTime = playerTime / 2;
        }
    }
}
