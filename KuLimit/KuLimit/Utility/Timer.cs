using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace KuLimit.Utility
{
    class Timer
    {
        private float limitTimer;               //制限時間タイマー
        private float currentTimer;             //現在の時間
        private float pastTimer;                //過ぎた時間

        /// <summary>
        /// コンストラクタ
        /// 一秒に設定
        /// </summary>
        public Timer ()
        {
            limitTimer = 60.0f;
            pastTimer = 0.0f;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="second">制限時間秒</param>
        public Timer(float second)
        {
            pastTimer = 0.0f;
            limitTimer = 60.0f * second;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="limitTimer"></param>
        public void Change(float limitTimer)
        {
            this.limitTimer = limitTimer;
            Initialize();
        }

        public float GetLimitTimer()
        {
            return limitTimer;
        }

        public void Initialize()
        {
            currentTimer = limitTimer;
            pastTimer = 0.0f;
        }


        public void Update()
        {
            pastTimer++;
            currentTimer -= 1.0f;
            //マイナス値になったら０で設定する
            if (currentTimer < 0.0f)
            {
                currentTimer = 0.0f;
            }
        }

        /// <summary>
        /// 現在の時間(フレーム)を取得
        /// </summary>
        /// <returns></returns>
        public float Now()
        {
            return currentTimer;
        }

        /// <summary>
        /// 過ぎた時間　＝＝　(スコア)
        /// </summary>
        /// <returns></returns>
        public float PastTime()
        {
            return pastTimer;
        }


        /// <summary>
        /// 時間になったか？
        /// </summary>
        /// <returns></returns>
        public bool IsTime()
        {
            return currentTimer <= 0.0f;
        }

        /// <summary>
        /// 割合
        /// </summary>
        /// <returns></returns>
        public float Rate()
        {
            return currentTimer / limitTimer;
        }
    }
}
