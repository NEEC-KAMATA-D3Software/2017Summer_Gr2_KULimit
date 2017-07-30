using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using KuLimit.Device;
using KuLimit.Actor;
using KuLimit.Utility;

namespace KuLimit.Scene
{
    class GamePlay : IScene
    {
        private GameDevice gameDevice;
        private bool isEnd;

        private Vector2 stagePosition;//背景画像の位置

        private Player player;
        private List<Enemy> enemys;
        private BigEnemy bigEnemy;
        private Vector2 velocity;

        //private Score score;
        //private Timer timer;
        //private TimerUI timerUI;
        private int pastTime;           //過ぎた時間(フレーム)
        private float pastSecond;       //過ぎた時間(秒)
        private Random rand = new Random();

        private Sound sound;

        public GamePlay(GameDevice gameDevice)
        {
            //this.input = input;
            this.gameDevice = gameDevice;
            isEnd = false;
            this.sound = gameDevice.GetSound();
        }

        public void Initialize()
        {
            isEnd = false;

            stagePosition = new Vector2(0.0f, 0.0f);
            pastTime = 0;
            pastSecond = 0;
            velocity = new Vector2(0, 1);

            player = new Player(gameDevice.GetInputState());
            player.Initialize();

            enemys = new List<Enemy>();
            CreateEnemy();
            
            bigEnemy = new BigEnemy();
            bigEnemy.Initialize(player);

            //timer = new Timer(100);
            //timer.Initialize();
            //timerUI = new TimerUI(timer);
        }

        //敵を重複に生成
        void CreatEnemyRepeat()
        {
            List<int> randomInt = new List<int>()
            {
                0,1,2,3,4,5,6,7,8,9
            };

            for (int i = 0; i < rand.Next(3, 10); i++)
            {
                int index = rand.Next(randomInt.Count);
                int x = randomInt[index];
                randomInt.RemoveAt(index);
                velocity = new Vector2(0, 1 + pastTime / 1800.0f);
                var pos = new Vector2(64.0f * x, -64f - 175.0f * 0) + velocity;
                Enemy e = new Enemy(pos, pastTime);
                e.Initialize(player);
                enemys.Add(e);
            }
        }
        //敵を生成(５列)
        void CreateEnemy()
        {
            for (int i = 0; i < 5; i++)
            {
                List<int> randomInt = new List<int>()
            {
                0,1,2,3,4,5,6,7,8,9
            };

                for (int j = 0; j < rand.Next(3, 9); j++)
                {
                    int index = rand.Next(randomInt.Count);
                    int x = randomInt[index];
                    randomInt.RemoveAt(index);
                    velocity = new Vector2(0, 1 + pastTime /1800.0f);
                    var pos = new Vector2(64.0f * x, 350.0f -64f - 175.0f * i) + velocity;
                    Enemy e = new Enemy(pos, pastTime);
                    e.Initialize(player);
                    enemys.Add(e);
                }
            }
        }


        public void Update(GameTime gameTime)
        {
            stagePosition += velocity;
            sound.PlayBGM("gameplaybgm");
            pastTime++;
            pastSecond = pastTime / 60;
            //timer.Update(); //ゲーム時間更新

            player.Update(gameTime,pastTime);

            enemys.ForEach((Enemy e) => e.Update(gameTime,pastTime));

            bigEnemy.Update(gameTime,pastTime);

            if (enemys[0].IsOutOfScreen() == true)
            {
                enemys.RemoveAll(e => e.IsOutOfScreen() == true);
                CreatEnemyRepeat();
            }
            if (bigEnemy.IsOutOfScreen() == true)
            {
                bigEnemy.ChangePosition();
            }

            //あたり判定
            if (player.IsCollition(bigEnemy))
            {
                if (player.GetTime() != 0)
                {
                    if (player.GetTime() >= bigEnemy.GetTime())
                    {
                        sound.PlaySE("gameplayse");
                        player.AddTime(bigEnemy.GetTime());
                        bigEnemy.Initialize(player);
                    }
                    else
                    {
                        sound.PlaySE("endingse");
                        isEnd = true;
                    }
                }
            }

            foreach (var b in enemys)
            {
                if (player.IsCollition(b))
                {
                    if (player.GetTime() != 0)
                    {
                        if (player.GetTime() > b.GetTime())
                        {
                            if (b.IsDead != true)
                            {
                                sound.PlaySE("gameplayse");
                                player.AddTime(b.GetTime() / 4);
                                enemys.ForEach(e => e.Initialize(player));
                                b.IsDead = true;
                            }
                        }
                        else
                        {
                            sound.PlaySE("endingse");
                            isEnd = true;
                        }

                    }
                }
                
                if (bigEnemy.IsCollition(b))
                {
                    b.Initialize(player);
                }

            }

            //foreach (var b in enemys)
            //{
            //    enemys.RemoveAll(c => c.IsDead);

            //    InitializeEnemyRepeat();
                
            //}
            if (player.GetTime() == 0)
            {
                sound.PlaySE("endingse");
                isEnd = true;
            }
            enemys.RemoveAll(e => e.IsDead == true);
            
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            for(int i = 0; i < 100; ++i)
            {
                renderer.DrawTexture("stage", stagePosition + new Vector2(-10, -699 * i));
            }
            //renderer.DrawTexture("stage", stagePosition + new Vector2 (-10,0));
            //renderer.DrawTexture("stage", stagePosition  + new Vector2 (-10, -699));
            player.Draw(renderer);
            foreach( var e in enemys)
            {
                if (e.IsDead != true )
                {
                    e.Draw(renderer);
                }
            }
            bigEnemy.Draw(renderer);
            //score.Draw(renderer);
            //timerUI.Draw(renderer);
            //renderer.DrawText3("Elapsed Time: " + pastSecond.ToString("000"), new Vector2(450, 30));
            player.Draw(renderer);
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
            return Scene.Ending;
        }

        float IScene.PastSecond()
        {
            return pastSecond;
        }
    }
}

