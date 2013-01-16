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

using XNAKeys = Microsoft.Xna.Framework.Input.Keys;
using XNAMouse = Microsoft.Xna.Framework.Input.Mouse;
using XNAKeyboard = Microsoft.Xna.Framework.Input.Keyboard;
using System.Windows.Input;

namespace APGameEngine
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameEngine : Microsoft.Xna.Framework.Game
    {
        #region Properties

        //Declaration of graphic manager and SpriteBatch
        GraphicsDeviceManager m_graphicsMngr = null;
        SpriteBatch m_spriteBatch = null;

        //Creation of the 3D render matrices
        Matrix m_view;
        Matrix m_projection;
        Matrix m_world;
        int m_fps = 60;

        //Set up of the camera
        Vector3 m_cameraPosition = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 m_cameraTarget = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 m_cameraUp = Vector3.Up;

        //Set up of visual effects (shaders)
        BasicEffect m_basicEffect = null;

        //Set up of 3D models
        Dictionary<string, Object> m_modelList = new Dictionary<string, Object>();
        Pipe m_pipe = null;

        //Set up of 3D Menues Background
        Model m_mazda = null;
        float m_angleRotation = 0;

        //Set up of the external modules for analysis and generation
        GameGenerator m_gameGenerator = null;
        XMLAnalyzer m_xmlAnalyzer = null;

        //Set up of the game phasis system
        string m_gamePhase = null;
        List<string> m_previousGamePhase = null;

        //Set up of the menues
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

        //Setting up the Inputs
        MouseState m_oldMouseState;
        MouseState m_actualMouseState;
        KeyboardState m_keyboardState;
        bool m_WindowsStateIsActive = false;

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
            this.IsMouseVisible = true;

            m_previousGamePhase = new List<string>();
            m_gamePhase = "Launch";

            m_view = Matrix.CreateLookAt(m_cameraPosition, m_cameraTarget, m_cameraUp);
            m_projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, this.GraphicsDevice.Viewport.AspectRatio, 0.01f, 10000.0f);
            m_world = Matrix.Identity;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            if (m_gamePhase == "Launch")
            {
                m_spriteBatch = new SpriteBatch(GraphicsDevice);

                //Shader attribution
                m_basicEffect = new BasicEffect(GraphicsDevice);
                m_mazda = Content.Load<Model>("Mazda");

                XNAMouse.SetPosition(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            }
            //Loadings of menues
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
                    m_gameMenuHost.Location = new System.Drawing.Point(0, 0);
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
            else if (m_gamePhase == "Loading Difficulty Choice Menu")
            {
                if (m_difficultyChoiceMenu == null || m_difficultyChoiceMenuHost == null)
                {
                    m_difficultyChoiceMenu = new Difficulty_Choice_Menu();
                    m_difficultyChoiceMenuHost = new ElementHost();
                    m_difficultyChoiceMenuHost.Location = new System.Drawing.Point(0, 100);
                    m_difficultyChoiceMenuHost.Size = new Size(800, 600);
                    m_difficultyChoiceMenuHost.Child = m_difficultyChoiceMenu;
                    m_difficultyChoiceMenuHost.BackColorTransparent = true;
                }

                m_previousGamePhase.Add(m_gamePhase);
                m_gamePhase = "Difficulty Choice Menu";
            }
            else if (m_gamePhase == "Loading Game")
            {
                UnloadContent();

                int divisions = 20, lenght = 2000, step = 2;
                float radius = 150.0f;

                //3D objects
                m_pipe = new Pipe(GraphicsDevice , lenght, radius, divisions, step, XNAColor.Red, XNAColor.Black);
                m_modelList.Add("pipe", m_pipe);

                //Camera
                m_cameraTarget = new Vector3(0, -((8*radius-1)/10), lenght*step);
                m_cameraPosition = new Vector3(0, -((8*radius - 1) / 10), 0);
                m_cameraUp = Vector3.Up;

                m_basicEffect.FogStart = m_cameraPosition.Z + 50;
                m_basicEffect.FogEnd = m_cameraPosition.Z + 300;

                m_view = Matrix.CreateLookAt(m_cameraPosition, m_cameraTarget, m_cameraUp);

                m_previousGamePhase.Add(m_gamePhase);
                m_gamePhase = "Playing Game";
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            //Removing menues from visuals
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
            else if (m_gamePhase == "Loading Game")
            {
                Control.FromHandle(Window.Handle).Controls.Remove(m_gameMenuHost);
                Content.Unload();
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            if (this.IsActive)
            {
                if (!m_WindowsStateIsActive)
                    m_actualMouseState = m_oldMouseState = XNAMouse.GetState();
                else
                    m_actualMouseState = XNAMouse.GetState();
                m_keyboardState = XNAKeyboard.GetState();
            }
            m_WindowsStateIsActive = this.IsActive;

            if (m_gamePhase != "Playing Game")
            {
                m_cameraTarget = m_mazda.Root.Transform.Translation - 2*(m_mazda.Root.Transform.Translation/3);
                m_angleRotation += 0.75f;

                if (m_angleRotation >= 360)
                    m_angleRotation = 0;

                double RadianAngle = MathHelper.ToRadians(m_angleRotation);
                float x = ((float)Math.Sin(RadianAngle)*500);
                float z = ((float)Math.Cos(RadianAngle)*500);

                m_cameraPosition = new Vector3(x * 2.5f + m_cameraTarget.X, 500,z * 2.5f + m_cameraTarget.Z);
                m_cameraUp = new Vector3(0, 1, 0);
                m_view = Matrix.CreateLookAt(m_cameraPosition, m_cameraTarget, m_cameraUp);
            }
            else if (m_gamePhase == "Playing Game")
            {
                if (m_oldMouseState != m_actualMouseState)
                {
                    float xRotation = MathHelper.Pi;
                    const float rotationSpeed = 0.005f;

                    float xDiff = m_actualMouseState.X - m_oldMouseState.X;
                    xRotation -= rotationSpeed * xDiff;

                    Matrix cameraRotation = Matrix.CreateRotationZ(xRotation);

                    m_cameraUp = Vector3.Transform(m_cameraUp, cameraRotation);
                    m_cameraPosition = Vector3.Transform(m_cameraPosition, cameraRotation);
                    m_cameraTarget = Vector3.Transform(m_cameraTarget, cameraRotation);
                }

                if(m_keyboardState.IsKeyDown(XNAKeys.S))
                    m_cameraPosition.Z -= 2000*15/ (30.0f * 60.0f);
                else if(m_keyboardState.IsKeyDown(XNAKeys.Z))
                    m_cameraPosition.Z += 2000*15 / (30.0f * 60.0f);

                m_view = Matrix.CreateLookAt(m_cameraPosition, m_cameraTarget, m_cameraUp);
            }

            m_oldMouseState = m_actualMouseState;

            //Control of the interface
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
                else if (m_gameMenu.selectDifficultyButton.IsPressed)
                {
                    m_previousGamePhase.Add(m_gamePhase);
                    m_gamePhase = "Loading Difficulty Choice Menu";
                    LoadContent();
                }
                else if (m_gameMenu.playButton.IsPressed)
                {
                    m_previousGamePhase.Add(m_gamePhase);
                    m_gamePhase = "Loading Game";
                    LoadContent();
                }
            }
            else if(m_gamePhase == "Music Choice Menu")
            {
                m_gameMenu.backButton.IsEnabled = false;
                m_gameMenu.playButton.IsEnabled = false;

                if (m_musicChoiceMenu.SaveMusicSelectionButton.IsPressed)
                {
                    m_gamePhase = "Unloading Music Choice Menu";
                    UnloadContent();
                    m_gamePhase = "Game Menu";
                    m_gameMenu.backButton.IsEnabled = true;
                    m_gameMenu.playButton.IsEnabled = true;
                }
                else if (m_musicChoiceMenu.DiscardMusicSelectionMusic.IsPressed)
                {
                    m_gamePhase = "Unloading Music Choice Menu";
                    UnloadContent();
                    m_gamePhase = "Game Menu";
                    m_gameMenu.backButton.IsEnabled = true;
                    m_gameMenu.playButton.IsEnabled = true;
                }
            }
            else if(m_gamePhase == "Difficulty Choice Menu")
            {
                m_gameMenu.backButton.IsEnabled = false;
                m_gameMenu.playButton.IsEnabled = false;

                if (m_difficultyChoiceMenu.easyButton.IsPressed)
                {
                    m_gamePhase = "Unloading Difficulty Choice Menu";
                    UnloadContent();
                    m_gamePhase = "Game Menu";
                    m_gameMenu.backButton.IsEnabled = true;
                    m_gameMenu.playButton.IsEnabled = true;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, (int)1000/m_fps);

            #region Launch

            //Start game
            if (m_gamePhase == "Launch")
            {
                m_gamePhase = "Loading Main Menu";
                m_previousGamePhase.Add(m_gamePhase);
                LoadContent();
            }

            #endregion

            #region 3D Drawing

            //Drawing 3D
            if (m_gamePhase != "Playing Game")
            {
                GraphicsDevice.Clear(XNAColor.CornflowerBlue);

                foreach (ModelMesh mesh in m_mazda.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.World = Matrix.Identity;
                        effect.View = m_view;
                        effect.Projection = m_projection;
                        mesh.Draw();
                    }
                }
            }
            else if (m_gamePhase == "Playing Game")
            {
                GraphicsDevice.Clear(XNAColor.White);

                m_basicEffect.View = m_view;
                m_basicEffect.Projection = m_projection;
                m_basicEffect.World = m_world;
                m_basicEffect.VertexColorEnabled = true;

                m_basicEffect.FogEnabled = true;
                m_basicEffect.VertexColorEnabled = true;

                if(m_basicEffect != m_pipe.getBasicEffect())
                    m_pipe.setBasicEffect(m_basicEffect);

                m_pipe.Draw();
            }

            #endregion

            #region Menus Drawing

            //Showing menues
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

            #endregion

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

        public XMLAnalyzer getSoundAnalyzer()
        {
            return m_xmlAnalyzer;
        }

        public bool setSoundAnalyzer(XMLAnalyzer xmlAnalyzer)
        {
            m_xmlAnalyzer = xmlAnalyzer;
            if (m_xmlAnalyzer == xmlAnalyzer)
                return true;
            else
                return false;
        }  

        #endregion
    }

    public class Pipe
    {
        #region Properties

        VertexPositionColor[] m_pipeVertices = null;
        int[] m_pipeIndices = null;
        VertexPositionColor[] m_linesVertices = null;
        int[] m_linesIndices = null;
        GraphicsDevice m_graphicsDevice = null;
        BasicEffect m_basicEffect = null;

        VertexBuffer m_pipeVerticeBuffer = null;
        IndexBuffer m_pipeIndiceBuffer = null;
        VertexBuffer m_linesVerticesBuffer = null;
        IndexBuffer m_linesIndicesBuffer = null;

        int m_lenght = 0;
        float m_radius = 0.0f;
        int m_divisions = 0;
        int m_step = 0;
        XNAColor m_pipeColor;
        XNAColor m_linesColor;

        #endregion

        #region Constructors

        public Pipe(GraphicsDevice graphicsDevice, int lenght, float radius, int divisions, int step, XNAColor pipecolor, XNAColor linesColor)
        {
            m_graphicsDevice = graphicsDevice;
            m_lenght = lenght;
            m_radius = radius;
            m_divisions = divisions;
            m_step = step;
            m_pipeColor = pipecolor;
            m_linesColor = linesColor;

            m_basicEffect = new BasicEffect(m_graphicsDevice);

            m_pipeVertices = createPipeVertices(lenght, radius, divisions, step, pipecolor);
            m_pipeIndices = createPipeIndicesBuffer(m_pipeVertices, divisions);

            m_pipeVerticeBuffer = createVertexBuffer(m_pipeVertices);
            m_pipeIndiceBuffer = createIndexBuffer(m_pipeIndices);

            m_linesVertices = createPipeVertices(lenght, radius - (radius/20), divisions, step, linesColor);
            m_linesIndices = createLinesIndicesBuffer(m_linesVertices, divisions, lenght);

            m_linesVerticesBuffer = createVertexBuffer(m_linesVertices);
            m_linesIndicesBuffer = createIndexBuffer(m_linesIndices);
        }

        #endregion

        #region Private methods

        private VertexPositionColor[] createPipeVertices(int lenght, float radius, int divisions, int step, XNAColor color)
        {
            VertexPositionColor[] pipeVerticesArray = new VertexPositionColor[(divisions * lenght) + divisions];

            int pipeDataArrayAccessIndex = 0;
            double angle = MathHelper.ToRadians(360.0f / divisions);

            for (int i = 0; i <= lenght; i++)
            {
                for (int j = 0; j < divisions; j++)
                {
                    createPoint(pipeVerticesArray, pipeDataArrayAccessIndex, radius, angle, j, i * step, color);
                    pipeDataArrayAccessIndex++;
                }
            }

            return pipeVerticesArray;
        }

        private void createPoint(VertexPositionColor[] pipe, int pipeDataArrayAccessIndex, float radius, double angle, int angleMultiplier, int lenght, XNAColor color)
        {
            pipe[pipeDataArrayAccessIndex] = new VertexPositionColor(new Vector3(radius * (float)Math.Cos(angleMultiplier * angle), radius * (float)Math.Sin(angleMultiplier * angle), lenght), color);
        }

        private VertexBuffer createVertexBuffer(VertexPositionColor[] VerticesArray)
        {
            VertexBuffer verticeBuffer = new VertexBuffer(m_graphicsDevice, typeof(VertexPositionColor), VerticesArray.Length, BufferUsage.WriteOnly);
            verticeBuffer.SetData(VerticesArray);
            m_graphicsDevice.SetVertexBuffer(verticeBuffer);

            return verticeBuffer;
        }

        private int[] createPipeIndicesBuffer(VertexPositionColor[] pipe, int divisions)
        {
            int[] indicesBuffer = new int[pipe.Length * 6 - (6 * divisions)];

            int index = 0;

            for (int i = 0; i < pipe.Length - divisions; i++)
            {
                if (i % divisions != divisions - 1)
                {
                    indicesBuffer[index] = i + divisions;
                    indicesBuffer[++index] = i;
                    indicesBuffer[++index] = i + 1;
                    indicesBuffer[++index] = i + divisions;
                    indicesBuffer[++index] = i + 1;
                    indicesBuffer[++index] = i + divisions + 1;
                    index++;
                }
                else
                {
                    indicesBuffer[index] = i + divisions;
                    indicesBuffer[++index] = i;
                    indicesBuffer[++index] = i - divisions + 1;
                    indicesBuffer[++index] = i + divisions;
                    indicesBuffer[++index] = i - divisions + 1;
                    indicesBuffer[++index] = i + 1;
                    index++;
                }
            }

            return indicesBuffer;
        }

        private IndexBuffer createIndexBuffer(int[] indices)
        {
            IndexBuffer indiceBuffer = new IndexBuffer(m_graphicsDevice, IndexElementSize.ThirtyTwoBits, indices.Length, BufferUsage.WriteOnly);
            indiceBuffer.SetData(indices);
            m_graphicsDevice.Indices = indiceBuffer;

            return indiceBuffer;
        }

        private int[] createLinesIndicesBuffer(VertexPositionColor[] lines, int divisions, int lenght)
        {
            int[] indicesBuffer = new int[lines.Length * 2];

            int indexAccess = 0;

            for (int i = 0; i < divisions; i++)
            {
                for (int j = 0; j < lenght; j++)
                {
                    indicesBuffer[indexAccess] = j * divisions + i;
                    indexAccess++;
                    indicesBuffer[indexAccess] = (j + 1) * divisions + i;
                    indexAccess++;
                }
            }

            return indicesBuffer;
        }

        #endregion

        #region Public methods

        public void Draw()
        {
            m_basicEffect.FogEnabled = true;
            m_basicEffect.VertexColorEnabled = true;

            foreach (EffectPass pass in m_basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                m_graphicsDevice.SetVertexBuffer(m_pipeVerticeBuffer);
                m_graphicsDevice.Indices = m_pipeIndiceBuffer;
                m_graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, m_pipeVertices.Length, 0, m_pipeIndices.Length / 3);
                m_graphicsDevice.SetVertexBuffer(m_linesVerticesBuffer);
                m_graphicsDevice.Indices = m_linesIndicesBuffer;
                m_graphicsDevice.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0, m_linesVertices.Length, 0, m_linesIndices.Length / 2);
            }
        }

        #endregion

        #region Setters & Getters

        public void setGraphicDevice(GraphicsDevice graphicDevice)
        {
            m_graphicsDevice = graphicDevice;
        }

        public GraphicsDevice getGraphicDevice()
        {
            return m_graphicsDevice;
        }

        public int getLenght()
        {
            return m_lenght;
        }

        public float getRadius()
        {
            return m_radius;
        }

        public int getDivisions()
        {
            return m_divisions;
        }

        public int getStep()
        {
            return m_step;
        }

        public XNAColor getPipeColor()
        {
            return m_pipeColor;
        }

        public XNAColor getLineColor()
        {
            return m_linesColor;
        }

        public VertexPositionColor[] getPipeVerticesArray()
        {
            return m_pipeVertices;
        }

        public VertexPositionColor[] getLineVerticesArray()
        {
            return m_linesVertices;
        }

        public int[] getPipeIndexArray()
        {
            return m_pipeIndices;
        }

        public int[] getLineIndexArray()
        {
            return m_linesIndices;
        }

        public Effect getEffect()
        {
            return m_basicEffect;
        }

        public VertexBuffer getPipeVertexBuffer()
        {
            return m_pipeVerticeBuffer;
        }

        public IndexBuffer getPipeIndexBuffer()
        {
            return m_pipeIndiceBuffer;
        }

        public VertexBuffer getLinesVertexBuffer()
        {
            return m_linesVerticesBuffer;
        }

        public IndexBuffer getLinesIndexBuffer()
        {
            return m_linesIndicesBuffer;
        }

        public GraphicsDevice getGraphicsDevice()
        {
            return m_graphicsDevice;
        }

        public void setBasicEffect(BasicEffect effect)
        {
            m_basicEffect = effect;
        }

        public BasicEffect getBasicEffect()
        {
            return m_basicEffect;
        }

        #endregion
    }

    public class Bloc
    {
        #region Properties

        VertexPositionColor[] m_blocVertices = null;
        int[] m_blocIndices = null;
        VertexPositionColor[] m_blocOutLineVertices = null;
        int[] m_blocOutLineIndices = null;
        GraphicsDevice m_graphicsDevice = null;
        BasicEffect m_basicEffect = null;

        VertexBuffer m_blocVertexBuffer = null;
        IndexBuffer m_blocIndexBuffer = null;
        VertexBuffer m_blocOutLineVertexBuffer = null;
        IndexBuffer m_blocOutLineIndexBuffer = null;

        Matrix m_PositionMatrix;
        XNAColor m_blocColor;
        XNAColor m_blocOutLineColor;

        #endregion

        #region Constructors

        public Bloc(GraphicsDevice graphicsDevice, Matrix positionMatrix, XNAColor blocColor, XNAColor outLineBlocColor)
        {
            m_graphicsDevice = graphicsDevice;
            m_PositionMatrix = positionMatrix;
            m_blocColor = blocColor;
            m_blocOutLineColor = outLineBlocColor;
            m_basicEffect = new BasicEffect(m_graphicsDevice);

            m_blocVertices = createBlocVertices();
            m_blocIndices = createBlocIndices();

            m_blocVertexBuffer = createBlocVerticesBuffer();
            m_blocIndexBuffer = createBlocIndexBuffer();

            m_blocOutLineVertices = createBlocVertices();
            m_blocOutLineIndices = createBlocOutLineIndices();

            m_blocOutLineVertexBuffer = createBlocVerticesBuffer();
            m_blocOutLineIndexBuffer = createBlocIndexBuffer();
        }

        #endregion

        #region Private methods

        private VertexPositionColor[] createBlocVertices(VertexPositionColor[] vertexArray, Matrix position, XNAColor verticesColor)
        {
            VertexPositionColor[] blocVertices = new VertexPositionColor[8];

            for (int i = 0; i < 8; i++)
            {

            }
        }

        private void createPoint(VertexPositionColor vertexArray, int accesIndex, Matrix position, XNAColor vertexColor)
        {
            VertexPositionColor[accesIndex] = new VertexPositionColor(new Vector3( , , ), vertexColor);
        }

        private int[] createBlocIndices()
        {

        }

        private VertexBuffer createBlocVerticesBuffer()
        {
        }

        private IndexBuffer createBlocIndexBuffer()
        {
        }

        private int[] createBlocOutlineIndices()
        {
        }

        #endregion

        #region Public methods



        #endregion

        #region Setters & Getters



        #endregion
    }
}
