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
//namespace[Oikake.Device]�ɑ�������̂����̃t�@�C���ŗ��p�\��
using KuLimit.Device;
//namespace[Oikake.Actor]�ɑ�������̂����̃t�@�C���ŗ��p�\��
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
        private GraphicsDeviceManager graphicsDeviceManager;     //�O���t�B�b�N�Ǘ���          
        private GameDevice gameDevice;                           //�Q�[���f�o�C�X            
        private Renderer renderer;                               //�`��I�u�W�F�N�g��錾
        private SceneManager sceneManager;                         //�V�[���Ǘ��� 
        private Sound sound;                                        // sound

        //�R���X�g���N�^
        public Game1()
        {
            //�O���t�B�b�N�@��Ǘ��҂̎��̐���
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            graphicsDeviceManager.PreferredBackBufferWidth = Screen.Width;    //��ʉ���
            graphicsDeviceManager.PreferredBackBufferHeight = Screen.Height;�@//��ʏc��
            //�R���e���c�̊�{�f�B���N�g����Content�ɐݒ�
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
            // TODO: �����ɏ��������W�b�N��ǉ����܂��B  

            //�Q�[���f�o�C�X�̎��̂𐶐�
            gameDevice = new GameDevice(Content, GraphicsDevice);
            //�����_���[�̎擾
            renderer = gameDevice.GetRenderer();
            //�V�[���Ǘ�����
            sceneManager = new SceneManager();
            //�V�[���̓o�^
            sceneManager.Add(Scene.Scene.Title, new Title(gameDevice));
            //�Q�[���f�o�C�X����sound�I�u�W�F�N�g���擾
            sound = gameDevice.GetSound();

            IScene gamePlay = new GamePlay(gameDevice);
            sceneManager.Add(Scene.Scene.GamePlay, gamePlay);
            sceneManager.Add(Scene.Scene.Ending, new Ending(gameDevice,gamePlay));
            //�ŏ��̃V�[����
            sceneManager.Change(Scene.Scene.Title);

            base.Window.Title = "�򃊃~�b�g";

            base.Initialize();                              //��΂ɏ�����
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //�摜�̓ǂݍ���
            renderer.LoadTexture("stage");
            renderer.LoadTexture("player");
            renderer.LoadTexture("smallenemy");
            renderer.LoadTexture("ending");
            renderer.LoadTexture("number");
            renderer.LoadTexture("title");
            renderer.LoadTexture("bigenemy");
            renderer.LoadTexture("keika");
            renderer.LoadTexture("record");
            
            //�P�s�N�Z���摜�̐���
            Texture2D fade = new Texture2D(GraphicsDevice, 1, 1);
            Color[] data = new Color[1 * 1];
            data[0] = new Color(0, 0, 0);
            fade.SetData(data);
            renderer.LoadTexture("fade", fade);
            /////���p
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

            //sound�� BGM�ASE�̓ǂݍ���
            #region BGM�ǂݍ���
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
            //�Q�[���̏I���������`�F�b�N���܂��B
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
                (Keyboard.GetState().IsKeyDown(Keys.Escape)))
            //Escape�ŏI��
            {
                this.Exit();
            }

            // TODO: Add your update logic here
            // TODO:�����ɃQ�[���̃A�b�v�f�[�g�@���W�b�N��ǉ����܂��B

            //�Q�[���f�o�C�X�̍X�V(�v���W�F�N�g������1�񂵂��Ă񂶂�_��)
            gameDevice.Update(gameTime);
            sceneManager.Update(gameTime);
            
            base.Update(gameTime);                  //��΂ɏ�����
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //�`��N���A���̐F��ݒ�
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // TODO:�@�����ɕ`��R-�h��ǉ����܂�

            sceneManager.Draw(renderer);


            base.Draw(gameTime);                        //��΂ɏ�����
        }
    }
}
