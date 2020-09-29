using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Modify_Gravity_andor_Jump
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player1;
        Player player2;

        Texture2D PinkHat;
        
        Vector2 GravityDir;
        float friction;
        float gravityAccel;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            PinkHat = Content.Load<Texture2D>("PinkHat");

            player1 = new Player(25, -200, 250, new Vector2(0, 0), new Vector2(20, 0), PinkHat,false);
            player2 = new Player(50, -300, 200, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), new Vector2(30, 0), PinkHat,false);

            GravityDir = new Vector2(0, 1);
            gravityAccel = 200.0f;
            friction = 10.0f;
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            player1.playerLoc = player1.playerLoc + ((player1.playerDir * (time / 1000)));
            player2.playerLoc = player2.playerLoc + ((player2.playerDir * (time / 1000)));

            player1.playerDir = player1.playerDir + (GravityDir * gravityAccel) * (time / 1000);
            player2.playerDir = player2.playerDir + (GravityDir * gravityAccel) * (time / 1000);

            player1.playerMovement(Keys.W,Keys.A,Keys.D,friction);
            player2.playerMovement(Keys.Up, Keys.Left, Keys.Right, friction);
            KeepPlayerOnScreen();

            base.Update(gameTime);
        }

        public void KeepPlayerOnScreen()
        {
            if ((player1.playerLoc.X > GraphicsDevice.Viewport.Width - player1.playerImg.Width) || (player1.playerLoc.X < 0))
            {
                player1.playerDir = player1.playerDir * new Vector2(-1, 1);
            }

            if ((player2.playerLoc.X > GraphicsDevice.Viewport.Width - player2.playerImg.Width) || (player2.playerLoc.X < 0))
            {
                player2.playerDir = player2.playerDir * new Vector2(-1, 1);
            }

            if (player1.playerLoc.Y > 400)
            {
                player1.playerLoc.Y = 400;
                player1.playerLoc.Y = 00;
                player1.playerOnGround = true;
            }

            if (player2.playerLoc.Y > 400)
            {
                player2.playerLoc.Y = 400;
                player2.playerLoc.Y = 00;
                player2.playerOnGround = true;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(player1.playerImg,player1.playerLoc,Color.White);
            spriteBatch.Draw(player2.playerImg, player2.playerLoc, Color.Green);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
