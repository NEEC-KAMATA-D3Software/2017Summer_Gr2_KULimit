using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
//namespace[Oikake.Device]に属するものをこのファイルで利用可能に
using KuLimit.Device;
//namespace[Oikake.Actor]に属するものをこのファイルで利用可能に
using KuLimit.Actor;
using KuLimit.Def;
using KuLimit.Scene;
using KuLimit.Utility;

namespace KuLimit
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphicsDeviceManager;     //グラフィック管理者          
        private GameDevice gameDevice;                           //ゲームデバイス            
        private Renderer renderer;                               //描画オブジェクトを宣言
        private SceneManager sceneManager;                         //シーン管理者 
        private Sound sound;                                        // sound

        //コンストラクタ
        public Game1()
        {
            //グラフィック機器管理者の実体生成
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            graphicsDeviceManager.PreferredBackBufferWidth = Screen.Width;    //画面横幅
            graphicsDeviceManager.PreferredBackBufferHeight = Screen.Height;　//画面縦幅
            //コンテンツの基本ディレクトリをContentに設定
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // TODO: ここに初期化ロジックを追加します。  

            //ゲームデバイスの実体を生成
            gameDevice = new GameDevice(Content, GraphicsDevice);
            //レンダラーの取得
            renderer = gameDevice.GetRenderer();
            //シーン管理生成
            sceneManager = new SceneManager();
            //シーンの登録
            sceneManager.Add(Scene.Scene.Title, new Title(gameDevice));
            //ゲームデバイスからsoundオブジェクトを取得
            sound = gameDevice.GetSound();

            IScene gamePlay = new GamePlay(gameDevice);
            sceneManager.Add(Scene.Scene.GamePlay, gamePlay);
            sceneManager.Add(Scene.Scene.Ending, new Ending(gameDevice,gamePlay));
            //最初のシーンへ
            sceneManager.Change(Scene.Scene.Title);

            base.Window.Title = "喰リミット";

            base.Initialize();                              //絶対に消すな
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //画像の読み込み
            renderer.LoadTexture("stage");
            renderer.LoadTexture("player");
            renderer.LoadTexture("smallenemy");
            renderer.LoadTexture("ending");
            renderer.LoadTexture("number");
            renderer.LoadTexture("title");
            renderer.LoadTexture("bigenemy");
            renderer.LoadTexture("keika");
            renderer.LoadTexture("record");
            
            //１ピクセル画像の生成
            Texture2D fade = new Texture2D(GraphicsDevice, 1, 1);
            Color[] data = new Color[1 * 1];
            data[0] = new Color(0, 0, 0);
            fade.SetData(data);
            renderer.LoadTexture("fade", fade);
            /////応用
            //Texture2D texture = new Texture2D(GraphicsDevice, 800, 600);
            //Color[] color = new Color[texture.Width * texture.Height];
            //for(int index = 0, h = 0; h < texture.Height; h++)
            //{
            //    for(int w = 0; w < texture.Width; w ++)
            //    {
            //        byte red = (byte)(0xFF * ((float)w / texture.Width));
            //        color[index] = new Color(red, 0, 0);
            //        index++;
            //    }
            //}
            //texture.SetData(color);
            //renderer.LoadTexture("red", texture);

            //soundの BGM、SEの読み込み
            #region BGM読み込み
            sound.LoadBGM("titlebgm");
            sound.LoadBGM("endingbgm");
            sound.LoadBGM("gameplaybgm");
            #endregion
            sound.LoadBGM("titlebgm");
            sound.LoadBGM("gameplaybgm");
            sound.LoadBGM("endingbgm");

            sound.LoadSE("titlese");
            sound.LoadSE("endingse");
            sound.LoadSE("gameplayse");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            renderer.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            //ゲームの終了条件をチェックします。
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
                (Keyboard.GetState().IsKeyDown(Keys.Escape)))
            //Escapeで終了
            {
                this.Exit();
            }

            // TODO: Add your update logic here
            // TODO:ここにゲームのアップデート　ロジックを追加します。

            //ゲームデバイスの更新(プロジェクト内この1回しか呼んじゃダメ)
            gameDevice.Update(gameTime);
            sceneManager.Update(gameTime);
            
            base.Update(gameTime);                  //絶対に消すな
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //描画クリア時の色を設定
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // TODO:　ここに描画コ-ドを追加します

            sceneManager.Draw(renderer);


            base.Draw(gameTime);                        //絶対に消すな
        }
    }
}
