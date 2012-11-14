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
using XNAColor = Microsoft.Xna.Framework.Color;
using DNColor = System.Drawing.Color;

using System.Windows.Forms;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

using Rectangle = Microsoft.Xna.Framework.Rectangle;

using Keys = Microsoft.Xna.Framework.Input.Keys;
using System.Windows.Input;

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
        GameGenerator m_gameGenerator = null;
        SoundAnalyzer m_soundAnalyzer = null;

        string m_gamePhase = null;
        List<string> m_previousGamePhase = null;

        Main_Menu m_mainMenu = null;
        ElementHost m_mainMenuHost = null;

        Settings m_settingsMenu = null;
        ElementHost m_settingsHost = null;

        Game_Menu m_gameMenu = null;
        ElementHost m_gameMenuHost = null;

        Music_Choice_Menu m_musicChoiceMenu = null;
        ElementHost m_musicChoiceMenuHost = null;

        Difficulty_Choice_Menu m_difficultyChoiceMenu = null;
        ElementHost m_difficultyChoiceMenuHost = null;

        #endregion

        #region Constructors

        public GameEngine()
        {
            m_graphicsMngr = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            m_graphicsMngr.PreferredBackBufferWidth = 800;
            m_graphicsMngr.PreferredBackBufferHeight = 600;
            m_gamePhase = "Launch";
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
            this.IsMouseVisible = true;

            m_previousGamePhase = new List<string>();
            m_gamePhase = "Launch";

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            if(m_gamePhase == "Launch")
                m_spriteBatch = new SpriteBatch(GraphicsDevice);

            else if (m_gamePhase == "Loading Main Menu")
            {
                if (m_mainMenuHost == null || m_mainMenu == null)
                {
                    m_mainMenuHost = new ElementHost();
                    m_mainMenu = new Main_Menu();
                    m_mainMenuHost.Location = new System.Drawing.Point(200, 175);
                    m_mainMenuHost.Size = new Size(400, 250);
                    m_mainMenuHost.Child = m_mainMenu;
                }

                m_previousGamePhase.Add(m_gamePhase);
                m_gamePhase = "Main Menu";
            }

            else if (m_gamePhase == "Loading Music Settings Menu")
            {
                m_previousGamePhase.Add(m_gamePhase);
                m_gamePhase = "Music Settings Menu";
            }

            else if (m_gamePhase == "Loading Settings Menu")
            {
                if (m_settingsMenu == null || m_settingsHost == null)
                {
                    m_settingsMenu = new Settings();
                    m_settingsHost = new ElementHost();
                    m_settingsHost.Location = new System.Drawing.Point(250, 150);
                    m_settingsHost.Size = new Size(300, 300);
                    m_settingsHost.Child = m_settingsMenu;
                }
               
                m_previousGamePhase.Add(m_gamePhase);
                m_gamePhase = "Settings Menu";
            }
            else if (m_gamePhase == "Loading Game Menu")
            {
                if (m_gameMenu == null || m_gameMenuHost == null)
                {
                    m_gameMenu = new Game_Menu();
                    m_gameMenuHost = new ElementHost();
                    m_gameMenuHost.Location = new System.Drawing.Point(0,0);
                    m_gameMenuHost.Size = new Size(800, 100);
                    m_gameMenuHost.Child = m_gameMenu;
                }

                m_previousGamePhase.Add(m_gamePhase);
                m_gamePhase = "Game Menu";
            }
            else if (m_gamePhase == "Loading Music Choice Menu")
            {
                if (m_musicChoiceMenu == null || m_musicChoiceMenuHost == null)
                {
                    m_musicChoiceMenu = new Music_Choice_Menu();
                    m_musicChoiceMenuHost = new ElementHost();
                    m_musicChoiceMenuHost.Location = new System.Drawing.Point(0, 100);
                    m_musicChoiceMenuHost.Size = new Size(800, 600);
                    m_musicChoiceMenuHost.Child = m_musicChoiceMenu;
                    m_musicChoiceMenuHost.BackColorTransparent = true;
                }

                m_previousGamePhase.Add(m_gamePhase);
                m_gamePhase = "Music Choice Menu";
            }

            // TODO: use this.Content to load your game content here

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            if (m_gamePhase == "Main Menu")
            {
                Control.FromHandle(Window.Handle).Controls.Remove(m_mainMenuHost);
            }
            else if (m_gamePhase == "Settings Menu")
            {
                Control.FromHandle(Window.Handle).Controls.Remove(m_settingsHost);
            }
            else if (m_gamePhase == "Game Menu")
            {
                Control.FromHandle(Window.Handle).Controls.Remove(m_gameMenuHost);                   
            }
            else if (m_gamePhase == "Unloading Music Choice Menu")
            {
                Control.FromHandle(Window.Handle).Controls.Remove(m_musicChoiceMenuHost);
            }
            else if (m_gamePhase == "Unloading Difficulty Choice Menu")
            {
                Control.FromHandle(Window.Handle).Controls.Remove(m_difficultyChoiceMenuHost);
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (m_gamePhase == "Main Menu")
            {
                if (m_mainMenu.playButton.IsPressed)
                {
                    m_previousGamePhase.Add(m_gamePhase);
                    UnloadContent();
                    m_gamePhase = "Loading Game Menu";
                    LoadContent();
                }
                else if (m_mainMenu.settingsButton.IsPressed)
                {
                    m_previousGamePhase.Add(m_gamePhase);
                    UnloadContent();
                    m_gamePhase = "Loading Settings Menu";
                    LoadContent();
                }
                else if (m_mainMenu.quitButton.IsPressed)
                {
                    UnloadContent();
                    Exit();
                }
            }
            else if(m_gamePhase == "Settings Menu")
            {
                if(m_settingsMenu.saveParamsButton.IsPressed)
                {
                    m_previousGamePhase.Add(m_gamePhase);
                    UnloadContent();
                    m_gamePhase = "Loading Main Menu";
                    LoadContent();
                }
                else if(m_settingsMenu.discardParamsButton.IsPressed)
                {
                    m_previousGamePhase.Add(m_gamePhase);
                    UnloadContent();
                    m_gamePhase = "Loading Main Menu";
                    LoadContent();
                }
            }
            else if (m_gamePhase == "Game Menu")
            {
                if (m_gameMenu.backButton.IsPressed)
                {
                    m_previousGamePhase.Add(m_gamePhase);
                    UnloadContent();
                    m_gamePhase = "Loading Main Menu";
                    LoadContent();
                }
                else if (m_gameMenu.selectMusicButton.IsPressed)
                {
                    m_previousGamePhase.Add(m_gamePhase);
                    m_gamePhase = "Loading Music Choice Menu";
                    LoadContent();
                }
            }
            else if(m_gamePhase == "Music Choice Menu")
            {
                m_gameMenu.backButton.IsEnabled = false;

                if (m_musicChoiceMenu.SaveMusicSelectionButton.IsPressed)
                {
                    m_gamePhase = "Unloading Music Choice Menu";
                    UnloadContent();
                    m_gamePhase = "Game Menu";
                    m_gameMenu.backButton.IsEnabled = true;
                }
                else if (m_musicChoiceMenu.DiscardMusicSelectionMusic.IsPressed)
                {
                    m_gamePhase = "Unloading Music Choice Menu";
                    UnloadContent();
                    m_gamePhase = "Game Menu";
                    m_gameMenu.backButton.IsEnabled = true;
                }
            }
            else if(m_gamePhase == "Difficulty Choice Menu")
            {
                m_gameMenu.backButton.IsEnabled = false;


            }


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (m_gamePhase == "Launch")
            {
                m_gamePhase = "Loading Main Menu";
                m_previousGamePhase.Add(m_gamePhase);
                LoadContent();
            }

            if (m_gamePhase == "Main Menu")
            {
                Control.FromHandle(Window.Handle).Controls.Add(m_mainMenuHost);
            }
            else if (m_gamePhase == "Settings Menu")
            {
                Control.FromHandle(Window.Handle).Controls.Add(m_settingsHost);
            }
            else if (m_gamePhase == "Game Menu")
            {
                Control.FromHandle(Window.Handle).Controls.Add(m_gameMenuHost);
            }
            else if (m_gamePhase == "Music Choice Menu")
            {
                Control.FromHandle(Window.Handle).Controls.Add(m_musicChoiceMenuHost);
                m_musicChoiceMenuHost.BringToFront();
            }

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
