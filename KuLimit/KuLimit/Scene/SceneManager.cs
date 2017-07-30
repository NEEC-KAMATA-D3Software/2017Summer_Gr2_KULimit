using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using KuLimit.Device;

namespace KuLimit.Scene
{
    class SceneManager
    {
        //シーン管理用ディクショナリ
        private Dictionary<Scene, IScene> scenes = new Dictionary<Scene, IScene>();
        //現在のシーン
        private IScene currentScene = null;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SceneManager()
        { }

        /// <summary>
        /// シーンの追加
        /// </summary>
        /// <param name="name">シーン名</param>
        /// <param name="scene">具体的なシーン</param>
        public void Add(Scene name, IScene scene)
        {
            //すでにシーン名がディクショナリに登録されていたら
            if (scenes.ContainsKey(name))
            {
                return;
            }
            //シーンを追加
            scenes.Add(name, scene);
        }

        /// <summary>
        /// シーン切り替え
        /// </summary>
        /// <param name="name">次のシーン名</param>
        public void Change(Scene name)
        {
            //何らかのシーンが登録されていたら
            if (currentScene != null)
            {
                //現在のシーンの終了処理を行う
                currentScene.ShutDown();
            }
            //ディクショナリから次のシーンを取り出し、
            //現在のシーンに設定
            currentScene = scenes[name];
            //シーンの初期化
            currentScene.Initialize();
        }

        /// <summary>
        /// シーンの更新
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //まだシーンが登録されていなければ
            if (currentScene == null)
            {
                return;
            }
            //現在のシーンを更新
            currentScene.Update(gameTime);
            //現在のシーンが終了していたら？
            if (currentScene.IsEnd())
            {
                //次のシーンを取得して、シーン切り替え
                Change(currentScene.Next());
            }
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer">描画オブジェクト</param>
        public void Draw(Renderer renderer)
        {
            //現在のシーンがまだ登録されていなければ
            if (currentScene == null)
            {
                return;
            }
            //現在のシーンを描画
            currentScene.Draw(renderer);
        }
    }
}