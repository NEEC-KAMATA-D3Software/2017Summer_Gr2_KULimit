using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using KuLimit.Device;
using KuLimit.Utility;

namespace KuLimit.Device
{
    class Motion
    {
        private Range range;                         //範囲
        private Timer timer;                         //モーション時間
        private int motionNumber;                    //モーション番号

        //表示位置を番号で管理
        //Dictionaryを使えば登録順番を気にしなくてもよい
        private Dictionary<int, Rectangle> rectangles = new Dictionary<int, Rectangle>();

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public Motion()
        {
            //何もしない
            Initialize(new Range(0, 0), new Timer());
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="range">範囲</param>
        /// <param name="timer">モーション切り替え時間</param>
        public Motion(Range range, Timer timer)
        {
            Initialize(range, timer);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="range">範囲</param>
        /// <param name="timer">モーション切り替え時間</param>
        public void Initialize(Range range, Timer timer)
        {
            this.range = range;
            this.timer = timer;
            //モーション番号の最初は範囲の最初に設定
            motionNumber = range.First();
        }

        /// <summary>
        /// モーション矩形情報の追加
        /// </summary>
        /// <param name="index"></param>
        /// <param name="rect"></param>
        public void Add(int index, Rectangle rect)
        {
            if (rectangles.ContainsKey(index))
            {
                return;
            }
            rectangles.Add(index, rect);
        }

        /// <summary>
        /// モーションの更新
        /// </summary>
        private void MotionUpdate()
        {
            motionNumber += 1;      //モーション番号をインクリメント
            //範囲外なら最初に戻す
            if (range.IsOutOfRange(motionNumber))
            {
                motionNumber = range.First();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //ガード節
            if (range.IsOutOfRange())
            {
                return;         //変化なし
            }

            timer.Update();
            if (timer.IsTime())
            {
                timer.Initialize();
                MotionUpdate();
            }
        }

        /// <summary>
        /// 描画範囲の取得
        /// </summary>
        /// <returns></returns>
        public Rectangle DrawingRange()
        {
            return rectangles[motionNumber];
        }
    }
}
