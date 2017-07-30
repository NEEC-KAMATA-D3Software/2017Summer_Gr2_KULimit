using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KuLimit.Def;
using KuLimit.Device;
using KuLimit.Utility;

namespace KuLimit.Actor
{
    class BigEnemy : Character
    {
        private static Random rand = new Random();
        private float enemyTime;//敵が持っている時間
        private Vector2 velocity;
        private float pastTime;
        private Player player;

        public BigEnemy()             
            : base("bigenemy", 128.0f)
        {
            isOutOfScreen = false;
        }

        public override void Initialize(Player player)
        {
            this.player = player;
            pastTime = 0.0f;
            //velocity = new Vector2 (0, 2 + pastTime / 1800.0f);
            position = new Vector2(64.0f * rand.Next(7), -diameter * rand.Next(1,6));
            enemyTime = rand.Next((int)player.GetTime() + 50,(int)player.GetTime() + 200);
        }

        public override void Update(GameTime gameTime, int pastTime)
        {
            velocity = new Vector2(0, 2 + pastTime / 1800.0f);
            position += velocity;
            if(position.Y > 875.0f - radius)
            {
                Initialize(player);
            }
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
            renderer.DrawText2(enemyTime.ToString("00"), position + Vector2.One * radius);
        }

        public float GetTime()
        {
            return enemyTime; 
        }
        
        public void ChangePosition()
        {
            position = new Vector2(64.0f * rand.Next(7), -diameter * rand.Next(1,3));
        }
    }
}
