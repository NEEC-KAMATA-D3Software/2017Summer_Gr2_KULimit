using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using KuLimit.Device;


namespace KuLimit.Actor
{
    abstract class Character
    {
        protected string name;              //アセット名
        protected Vector2 position;         //位置
        protected float radius;             //半径
        protected float diameter;           //直径
        protected bool isOutOfScreen;//沈んだか？

        /// <summary>
        ///コンストラクタ
        /// </summary>
        /// <param name = "name"> アセット名 </param>
        /// <param name = "radius"> 半径 </param>
        public Character(string name, float radius)
        {
            this.name = name;
            position = Vector2.Zero;
            this.radius = radius;
            diameter = radius * 2.0f;
        }

        /// <summary>
        ///抽象初期化メソッド
        /// </summary>
        public virtual void Initialize (){}
        public virtual void Initialize(Player player) { }

        /// <summary>
        ///抽象更新メソッド
        /// </summary>
        ///<param name = "gameTime"></param>
        public abstract void Update(GameTime gameTime, int pastTime);
        
        /// <summary>
        ///描画
        /// </summary>
        ///<param name = "renderer"></param>
        public virtual void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }

        /// <summary>
        ///衝突判定
        /// </summary>
        ///<param name = "other"> 相手のキャラ </param>
        ///<returns> 衝突していればtrue </returns>
        //yesかnoを調べるメソッドを作る時は、メソッド名の最初に "Is" をつけ、bool値をreturnする
        public bool IsCollition(Character other)
        {
            //中心座標の計算　　　　左上の座標に半径を足して中心座標を求める
            Vector2 myCenter = position + new Vector2(radius , radius);
            Vector2 otherCenter = other.position + new Vector2(other.radius, other.radius);

            //相手キャラとのＸ、Ｙのそれぞれの長さ
            float xLength = myCenter.X - otherCenter.X ;
            float yLength = myCenter.Y - otherCenter.Y ;

            //2点間の距離の2乗値
            float squareLength = xLength * xLength + yLength * yLength;

            //半径の和の2乗値
            float squareRadius = (radius - 5 + other.radius - 5) * (radius - 5 + other.radius - 5);

            //半径の和と距離を比べて、等しいかまたは小さいか？
            if (squareLength < squareRadius)
            {
                return true;
            }
            //距離の方が長ければ衝突していない
            return false;
        }

        ///<summary>
        ///位置の受け渡し
        ///引数で渡された、変数に自分の位置を渡す
        /// </summary>
        ///<param name = "other"> 位置を送りたい相手 </param>
        public void SetPosition(ref Vector2 other)
        {
            other = position;
        }
        public bool IsOutOfScreen()
        {
            if(position.Y >= 875.0f - radius)
            {
                isOutOfScreen = true;
            }
            return isOutOfScreen;
        }
        
    }
}
