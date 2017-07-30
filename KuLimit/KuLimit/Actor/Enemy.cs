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
    class Enemy : Character
    {
        private static Random rand = new Random();
        private float enemyTime;//敵が持っている時間
        private Vector2 velocity;
        private int pastTime;
        
        public Enemy(Vector2 position, int pastTime)         
            : base("smallenemy", 32.0f)
        {
            this.pastTime = pastTime;
            this.position = position;
            IsDead = false;
            isOutOfScreen = false;
            velocity = new Vector2(0, 1 + pastTime / 1800.0f);
        }

        //死んだのか？
        public bool IsDead
        {
            get;
            set;
        }

        public override void Initialize(Player player)
        {
            int x = rand.Next(1, 6);
            //敵のタイム
            if (player.GetTime() > 60 && player.GetTime() < 100)
            {
                if (x == 1 || x == 2 || x == 3)
                {
                    enemyTime = rand.Next((int)player.GetTime() - 30, (int)player.GetTime());
                    enemyTime += 10;
                    if (enemyTime < 4)
                    {
                        enemyTime = 4;
                    }
                }
                else
                {
                    enemyTime = rand.Next((int)player.GetTime(), (int)player.GetTime() + 60);
                    enemyTime += 10;
                    if (enemyTime < 4)
                    {
                        enemyTime = 4;
                    }
                }
            }
            else if (player.GetTime() > 100 && player.GetTime() < 300)
            {
                if (x == 1 || x == 2)
                {
                    enemyTime = rand.Next((int)player.GetTime() - 30, (int)player.GetTime());
                    enemyTime += 10;
                    if (enemyTime < 4)
                    {
                        enemyTime = 4;
                    }
                }
                else
                {
                    enemyTime = rand.Next((int)player.GetTime(), (int)player.GetTime() + 60);
                    enemyTime += 10;
                    if (enemyTime < 4)
                    {
                        enemyTime = 4;
                    }
                }
            }
            else if (player.GetTime() > 300)
            {
                if (x == 1)
                {
                    enemyTime = rand.Next((int)player.GetTime() - 30, (int)player.GetTime());
                    enemyTime += 10;
                    if (enemyTime < 4)
                    {
                        enemyTime = 4;
                    }
                }
                else
                {
                    enemyTime = rand.Next((int)player.GetTime(), (int)player.GetTime() + 60);
                    enemyTime += 10;
                    if (enemyTime < 4)
                    {
                        enemyTime = 4;
                    }
                }
            }
            
            else
            {
                if (x != 1)
                {
                    enemyTime = rand.Next((int)player.GetTime() - 30, (int)player.GetTime());
                    enemyTime += 10;
                    if (enemyTime < 4)
                    {
                        enemyTime = 4;
                    }
                }
                else
                {
                    enemyTime = rand.Next((int)player.GetTime(), (int)player.GetTime() + 60);
                    enemyTime += 10;
                    if (enemyTime < 4)
                    {
                        enemyTime = 4;
                    }
                }
            }
            
            
            //position = new Vector2(64.0f * rand.Next(10), -diameter * rand.Next(11));

        }

        public override void Update(GameTime gameTime, int pastTime)
        {
            velocity = new Vector2(0, 1 + pastTime / 1800.0f);

            position += velocity;
            //下壁とのあたり判定
            if (position.Y >= 875.0f - radius)
            {
                IsDead = true;
                //character.Initialize();
            }
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
            renderer.DrawText1(enemyTime.ToString("00"), position + Vector2.One * radius);
        }

        public float GetTime()
        {
            return enemyTime;
        }
        
        

    }  
}
