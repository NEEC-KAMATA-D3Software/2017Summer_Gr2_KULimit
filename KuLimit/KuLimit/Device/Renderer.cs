using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;                       //Vector2用
using Microsoft.Xna.Framework.Graphics;              //spriteBatch   
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;                            //Assert用


namespace KuLimit.Device
{
    class Renderer
    {
        private ContentManager contentManager;      //コンテンツ管理者
        private GraphicsDevice graphicsDevice;      //グラフィック機器
        private SpriteBatch spriteBatch;            //スプライト一括
        private SpriteFont spriteFont1;             //フォント表示

        //Dictionaryで複数の画像を管理
        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        ///<summary>
        ///コンストラクタ
        ///</summary>
        ///<param name = "content"> Game1 のコンテンツ管理者</param>
        ///<param name = "graphics"> Game1 のグラフィック機器</param>

        public Renderer(ContentManager content, GraphicsDevice graphics)
        {
            contentManager = content;
            graphicsDevice = graphics;
            spriteBatch = new SpriteBatch(graphicsDevice);
            //フォント表示
            spriteFont1 = content.Load<SpriteFont>("./SpriteFont1");
        }

        ///<summary>
        ///コンストラクタ
        ///</summary>
        ///<param name = "name">アセット名</param>
        ///<param name = "filepath">ファイルまでのバス</param>
        public void LoadTexture(string name, string filepath = "./")
        {
            //ガード節
            //Dictionaryへの2重登録を回避
            if (textures.ContainsKey(name))
            {
#if DEBUG  //DEBUGモードの時のみ有効
                System.Console.WriteLine("この" + name +
                    "はKeyで、すでに登録してます");
#endif
                //処理終了
                return;
            }
            //がぞうの読み込みとDictionaryにアセット名と画像を追加
            textures.Add(name, contentManager.Load<Texture2D>(filepath + name));
        }

        ///<summary>
        ///アンロード
        ///</summary>
        public void Unload()
        {
            //Dictionary登録情報をクリア
            textures.Clear();
        }

        ///<summary>
        ///描画開始
        ///</summary>
        public void Begin()
        {
            spriteBatch.Begin();
        }

        ///<summary>
        ///描画終了
        ///</summary>
        public void End()
        {
            spriteBatch.End();
        }

        ///<summary>
        ///画像の描画
        ///</summary>
        ///<param name = "name">アセット名</param>
        ///<param name = "position">位置</param>
        ///<param name = "alpha">透明値(0.0:透明、1.0:不透明)</param>
        public void DrawTexture(string name, Vector2 position, float alpha = 1.0f)
        {
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n " +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？" +
                "プログラムを確認してください");
            spriteBatch.Draw(textures[name], position, Color.White * alpha);

        }

        /// <summary>
        /// 画像の描画
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="rect">画像の切り出し範囲</param>
        /// <param name="alpha"><透明値/param>
        public void DrawTexture(string name, Vector2 position, Rectangle rect, float alpha = 1.0f)
        {
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n " +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？" +
                "プログラムを確認してください");
            spriteBatch.Draw(
                textures[name],                         //画像
                position,                               //位置
                rect,                                   //矩形の指定範囲(左上の座標　Ｘ、Ｙ、幅，高さ)
                Color.White * alpha);
        }



        /// <summary>
        /// 数字の描画(整数のみ版、簡易)
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="number">表示したい数字(整数)</param>
        /// <param name="alpha">透明値</param>
        public void DrawNumber(string name, Vector2 position, int number, float alpha = 1.0f)
        {
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n " +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？" +
                "プログラムを確認してください");

            //マイナスの数は（）
            //if (number < 0)
            //{
            //    number = 0;
            //}
            number = Math.Max(number, 0);

            foreach (var n in number.ToString())
            {
                spriteBatch.Draw(
                    textures[name],
                    position,
                    new Rectangle((n - '0') * 32, 0, 32, 64),
                    Color.White * alpha
                    );
                position.X += 32;           //1桁分右ずらす
            }
        }

        /// <summary>
        /// 数字の描画(詳細版)
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="number">描画したい数(文字列でもらう)</param>
        /// <param name="digit">桁数</param>
        /// <param name="alpha">透明度</param>
        public void DrawNumber(string name, Vector2 position, string number, int digit, float alpha = 1.0f)
        {
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n " +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？" +
                "プログラムを確認してください");

            //桁数ループして、1の位を表示
            for (int i = 0; i < digit; i++)
            {
                //小数点の点か？
                if (number[i] == '.')
                {
                    //幅をかけて座標を求め、1文字を絵から切り出す
                    spriteBatch.Draw(
                        textures[name],
                        position,
                        new Rectangle(10 * 32, 0, 32, 64),
                        Color.White * alpha
                        );
                }
                else
                {
                    //1文字分の数値を数値文字で取得
                    char n = number[i];

                    //幅をかけて座標を求め、1文字を絵から切り出す
                    spriteBatch.Draw(
                        textures[name],
                        position,
                        new Rectangle((n - '0') * 32, 0, 32, 64),
                        Color.White * alpha
                        );
                }
                //表示座標のｘ座標を右へ移動
                position.X += 32;
            }
        }

        /// <summary>
        /// 画像の登録
        /// </summary>
        /// <param name="name"></param>
        /// <param name="texture"></param>
        public void LoadTexture(string name, Texture2D texture)
        {
            if (textures.ContainsKey(name))
            {
#if DEBUG   //debugモードの時のみ有効
                System.Console.WriteLine(
                    "この" + name + "はKeyで、すでに登録されています");
#endif
                //処理終了
                return;
            }
            textures.Add(name, texture);
        }

        /// <summary>
        /// (拡大縮小対応版)画像の描画
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="positon">位置</param>
        /// <param name="scale">拡大縮小値</param>
        /// <param name="alpha">透明値</param>
        public void DrawTexture(string name, Vector2 position, Vector2 scale,
            float alpha = 1.0f)
        {
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違えていませんか？\n" +
                "LoadTextureメソッドで読み込んでいますか？\n" +
                "プログラムを確認してください\n"
                );

            spriteBatch.Draw(
                textures[name],             //画像
                position,                   //位置
                null,                       //切り取り範囲
                Color.White * alpha,        //透過
                0.0f,                       //回転
                Vector2.Zero,               //回転軸の位置
                scale,                      //拡大縮小
                SpriteEffects.None,         //表示反転効果
                0.0f);                      //スプライト表示深度
        }

        //フォント表示
        public void DrawText1(string text, Vector2 position)                                                           //大きさ
        {
            spriteBatch.DrawString(spriteFont1, text, position, Color.White, 0.0f, GetFontScale(text, spriteFont1) / 2.0f, 1, 0, 0.0f);
        }
        //フォント表示
        public void DrawText2(string text, Vector2 position)
        {
            spriteBatch.DrawString(spriteFont1, text, position, Color.White, 0.0f, GetFontScale(text, spriteFont1) / 2.0f, 2, 0, 0.0f);
        }
        public void DrawText3(string text, Vector2 position)                                                           //大きさ
        {
            spriteBatch.DrawString(spriteFont1, text, position, Color.Red, 0.0f, GetFontScale(text, spriteFont1) / 2.0f, 1, 0, 0.0f);
        }
        public void DrawText4(string text, Vector2 position)                                                           //大きさ
        {
            spriteBatch.DrawString(spriteFont1, text, position, Color.Red, 0.0f, GetFontScale(text, spriteFont1) / 2.0f, 2, 0, 0.0f);
        }
        //フォントの中心
        public Vector2 GetFontScale(string text, SpriteFont font)
        {
            return spriteFont1.MeasureString(text);
        }
    }
}
