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
using APGameGenerator;
using APSoundAnalyzer;

using System.Windows.Forms.Integration;

using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;

using System.Windows.Forms;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

using Rectangle = Microsoft.Xna.Framework.Rectangle;

using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace APGameEngine
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameEngine : Microsoft.Xna.Framework.Game
    {
        #region Properties

        GraphicsDeviceManager m_graphicsMngr = null;
        SpriteBatch m_spriteBatch = null;
        ElementHost m_mainMenuHost = null;
        UserControls m_userInterface = null;
        GameGenerator m_gameGenerator = null;
        SoundAnalyzer m_soundAnalyzer = null;

        string m_gamePhase = null;
        List<string> m_previousGamePhase = null;

        Rectangle m_mainMenuInterfaceHandler;

        #endregion

        #region Constructors

        public GameEngine()
        {
            m_graphicsMngr = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            m_graphicsMngr.PreferredBackBufferWidth = 800;
            m_graphicsMngr.PreferredBackBufferHeight = 600;
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            m_previousGamePhase = new List<string>();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            if (m_gamePhase == "launch")
            {
                // Create a new SpriteBatch, which can be used to draw textures.
                m_spriteBatch = new SpriteBatch(GraphicsDevice);

                m_mainMenuHost = new ElementHost();
                m_userInterface = new UserControls();

                m_gamePhase = "main menu";
            }

            if (m_gamePhase == "main menu")
            {
                //Adding interface
                m_mainMenuHost.Location = new System.Drawing.Point(250, 150);
                m_mainMenuHost.Size = new Size(300, 300);
                m_mainMenuHost.Child = m_userInterface;
            }

            

            // TODO: use this.Content to load your game content here

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);

            

            base.Draw(gameTime);
        }

        #endregion

        #region Public methods

        public GameGenerator getGameGenerator()
        {
            return m_gameGenerator;
        }

        public bool setGameGenerator(GameGenerator generator)
        {
            m_gameGenerator = generator;
            if (m_gameGenerator == generator)
                return true;
            else 
                return false;
        }

        public SoundAnalyzer getSoundAnalyzer()
        {
            return m_soundAnalyzer;
        }

        public bool setSoundAnalyzer(SoundAnalyzer soundAnalyzer)
        {
            m_soundAnalyzer = soundAnalyzer;
            if (m_soundAnalyzer == soundAnalyzer)
                return true;
            else
                return false;
        }

        #endregion
    }
}
