using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;//Keyboardクラス利用のため

namespace KuLimit.Device
{
    class InputState
    {
        //フィールド
        private Vector2 velocity = Vector2.Zero;            //移動量
        private KeyboardState currentKey;                   //現在のキー
        private KeyboardState previousKey;                  //１フレーム前のキー
        private int num1 = 1, num2 = 1;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InputState()
        { }

        /// <summary>
        /// 移動量の取得
        /// </summary>
        /// <returns>移動量</returns>
        public Vector2 Velocity()
        {
            return velocity;
        }

        /// <summary>
        /// 移動量の更新(このクラス内だけのメソッド、private宣言)
        /// </summary>
        /// <param name="keyState">キーボードの状態</param>
        private void UpdateVelocity(KeyboardState keyState)
        {
            velocity = Vector2.Zero;

            //左右操作処理
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                //ずっと動かないように
                if (num1 == num2)
                {
                    velocity.X += (64.0f);
                    num1++;
                    //num1 = 2, num2 = 1, num1 != num2 実行しない
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (num1 == num2)
                {
                    velocity.X -= (64.0f);
                    num1--;
                }
            }
            //どちらも押さえてなければ　num1 = num2 
            if (!Keyboard.GetState().IsKeyDown(Keys.Left) &&
                !Keyboard.GetState().IsKeyDown(Keys.Right))
            { num1 = num2; }

        }

        /// <summary>
        /// キー情報の更新
        /// </summary>
        /// <param name="keyState"></param>
        private void UpdateKey(KeyboardState keyState)
        {
            //現在登録されているキーを1フレームまえのキーに
            previousKey = currentKey;
            //現在のキーを最新のキーに
            currentKey = keyState;
        }

        /// <summary>
        /// キーが押されているか？
        /// </summary>
        /// <param name="key">調べたいキー</param>
        /// <returns>キーが押されていて、1フレーム前に押されていなければtrue</returns>
        public bool IskeyDown(Keys key)
        {
            //現在チェックしたいキーが押されたか
            bool current = currentKey.IsKeyDown(key);
            //1フレーム前に押されていたか
            bool previous = previousKey.IsKeyDown(key);
            return current && !previous;
        }

        /// <summary>
        /// キー入力のトリガー判定
        /// </summary>
        /// <param name="key"></param>
        /// <returns>1フレーム前に押されていたらfalse</returns>
        public bool GetKeyTrigger(Keys key)
        {
            return IskeyDown(key);
        }

        /// <summary>
        /// キー入力の状態判定
        /// </summary>
        /// <param name="key"></param>
        /// <returns>押されていたらture</returns>
        public bool GetKeyState(Keys key)
        {
            return currentKey.IsKeyDown(key);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            //現在のキーボードの状態を取得
            var keyState = Keyboard.GetState();
            //移動量の更新
            UpdateVelocity(keyState);
            //キーの更新
            UpdateKey(keyState);
        }
        
    }
}
