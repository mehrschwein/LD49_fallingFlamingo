using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace LD49_butterfly
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont font;

        Texture2D cursorTexture;
        Vector2 cursorPos;

        //flamingo variables
        //Texture2D flamingo;
        Texture2D[] flamingo = new Texture2D[12];

        int flamingoState = 0;
        int lastFlamingoState = 0;
        float legChange = 1000;

        Rectangle flamingoPos;
        float flamingoTilt = 0;
        float addedFlamingoTiltShit = 0;
        Vector2 flamingoTurningPoint;
        //Vector2 flamingoOrigin;
        float tiltVelocity = 0f;

        Vector2 flamingoFeetOrigin;

        //start/endscreen
        Texture2D startscreen;
        Texture2D endscreen;
        int starttime = 0;
        int endtime = 0;
        int record = 0;
        string deathmessage;

        bool newRecord = false;
        //gamestate: 0 = startscreen, 1 = playing, 2 = endscreen;
        int gameState = 0;

        //background 
        Texture2D backgroundRight;
        Rectangle backgroundRightPos;
        Texture2D slideBar;
        Rectangle slideBarPos;

        Rectangle screen;

        Texture2D backdropLawn;
        Texture2D backdropLake;
        Texture2D backdropHill;


        //Leg pressure gauge;
        Texture2D legPressureGauge;
        Texture2D legPressureGaugeAlert;
        Rectangle legPressureGaugePos;

        Texture2D legPressureLOn;
        Texture2D legPressureROn;
        Texture2D legPressureLOff;
        Texture2D legPressureROff;
        Rectangle legPressureLPos;
        Rectangle legPressureRPos;
        SpriteCollider legPressureLCollider = new SpriteCollider();
        SpriteCollider legPressureRCollider = new SpriteCollider();
        bool leftLegOn = true;

        float leftArrowTilt = -0.5f;
        float rightArrowTilt = -0.5f;
        Texture2D leftArrow;
        Texture2D rightArrow;
        Rectangle leftArrowPos;
        Rectangle rightArrowPos;



        //slider
        SpriteCollider flamingoSlider = new SpriteCollider();
        Texture2D sliderButtonFree;
        Texture2D sliderButtonPressed;
        int sliderBarMin = 0;
        int sliderBarMax = 0;
        int sliderBarMiddle = 0;

        bool ButtonDown = false;
        bool ButtonDownChange = false;

        bool flamingoButtonDown = false;
        bool flamingoButtonDownChange = false;


        //foodbar
        Texture2D foodBar;
        Texture2D foodBarAlert;
        Texture2D food;
        Rectangle foodBarPos;
        Rectangle foodPos;
        float foodMeter = 200;

        //fish
        Texture2D fish;
        SpriteCollider[] fishes = new SpriteCollider[10];
        bool[] fishesalive = new bool[10];
        float[] fishspeed = new float[10];
        float[] fishheight = new float[10];
        int fishcounter = 0;


        //shitbar
        SpriteCollider shitButtonCollider = new SpriteCollider();
        Rectangle shitBarPos;
        Rectangle shitButtonPos;
        Rectangle shitPos;
        Texture2D shitBar;
        Texture2D shitBarBlink;
        Texture2D shitButton;
        Texture2D shitButtonDown;
        Texture2D shit;

        //poop
        Texture2D poopTex;
        int poopcounter = 0;
        Vector2 poopOrigin;
        Vector2 changingPoopOrigin;
        Vector2[] poopDirection = new Vector2[10];
        SpriteCollider[] poop = new SpriteCollider[10];

        float shitMeter = 100;

        System.Random rng = new System.Random();

        Vector2 lastMousePos;


        //clouds
        Texture2D cloudTex;
        int cloudCounter = 0;
        float[] cloudspeed = new float[10];
        Vector2[] cloudPos = new Vector2[10];

        //flamingo fun facts
        int funfactNumber = 0;
        string[] funfacts = new string[6] {"Even a dead flamingo stands better than you.",
            "There are 6 flamingo species.",
            "Flamingo nests are made of mud.",
            "Flamingos get their pink color from their food.",
            "Flamingo legs actually dont bend backwards.",
            "A group of flamingos is called a flamboyance."};



        //Sound Effects
        SoundEffect sfxShitMeter;
        bool sfxShitMeterPlayed = false;
        SoundEffect sfxFoodMeter;
        bool sfxFoodMeterPlayed = false;
        SoundEffect sfxGauge;
        bool sfxGaugePlayed = false;
        SoundEffect sfxTilt;
        bool sfxTiltPlayed = false;
        SoundEffect sfxClick;
        bool sfxClickPlayed = false;
        SoundEffect sfxDeath;
        bool sfxDeathPlayed = false;
        SoundEffect sfxLegChange;
        bool sfxLegChangePlayed = false;


        public static double ConvertDegreesToRadians(double degrees)
        {
            double radians = (System.Math.PI / 180) * degrees;
            return (radians);
        }

        public float getYPos()
        {
            //return (float)((306 * System.Math.Sin(ConvertDegreesToRadians(90 - 19 + MathHelper.ToDegrees(flamingoTilt)))) / System.Math.Sin(ConvertDegreesToRadians(90)));
            return 240 * (float)System.Math.Sin(ConvertDegreesToRadians(-10 + 90 + MathHelper.ToDegrees(flamingoTilt)));
        }

        public float getXPos()
        {
            //return (float)(306 * System.Math.Cos(ConvertDegreesToRadians(19 + MathHelper.ToDegrees(flamingoTilt))) + changingPoopOrigin.Y * System.Math.Cos(ConvertDegreesToRadians(90)));
            /*if (flamingoTilt > 0)
            {
                return (float)System.Math.Sqrt((getYPos() * getYPos()) - (2 * getYPos()) * 306 * System.Math.Cos(ConvertDegreesToRadians(MathHelper.ToDegrees(-19 + flamingoTilt))) + (306 * 306));

            }
            else {*/
            //return (float)System.Math.Sqrt((getYPos() * getYPos()) + (306 * 306) - 2 * getYPos() * 306 * System.Math.Cos(ConvertDegreesToRadians(-19 + MathHelper.ToDegrees(flamingoTilt))));
            //}
            return 240 * (float)System.Math.Cos(ConvertDegreesToRadians(-10 + 90 + MathHelper.ToDegrees(flamingoTilt)));
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";


            if (GraphicsDevice == null)
            {
                _graphics.ApplyChanges();
            }

            //_graphics.IsFullScreen = true;

            _graphics.ApplyChanges();

            IsMouseVisible = true;
            Window.Title = "falling flamingo LD49";

            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 576;
            _graphics.ApplyChanges();
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here



            screen = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            flamingoPos = new Rectangle(0, 4 * (_graphics.PreferredBackBufferHeight / 5), _graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            backgroundRightPos = new Rectangle(3 * (_graphics.PreferredBackBufferWidth / 4), 0, _graphics.PreferredBackBufferWidth / 4, _graphics.PreferredBackBufferHeight);
            slideBarPos = new Rectangle(_graphics.PreferredBackBufferWidth / 8, 7*(_graphics.PreferredBackBufferHeight/8), _graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight/16);
            

            flamingoSlider.box = new Rectangle(slideBarPos.X + 12, (slideBarPos.Y - (_graphics.PreferredBackBufferHeight / 48)), (_graphics.PreferredBackBufferHeight / 8), _graphics.PreferredBackBufferHeight / 8);
            sliderBarMin = slideBarPos.X + 10;
            sliderBarMax = slideBarPos.X + slideBarPos.Width - 30;
            sliderBarMiddle = flamingoSlider.box.X + ((sliderBarMax - sliderBarMin)/2);
            flamingoSlider.box.X = sliderBarMiddle;
            flamingoPos.X = sliderBarMiddle + 10;

            shitBarPos = new Rectangle(805, 270, 80, 200);
            shitButtonPos = new Rectangle(805, 480, 80, 80);
            shitPos = new Rectangle(810, 275, 70, 190);
            shitButtonCollider.box = shitButtonPos;

            foodBarPos = new Rectangle(905, 270, 80, 290);
            foodPos = new Rectangle(910, 275, 70, 280);

            legPressureGaugePos = new Rectangle(805, 30, 180, 180);
            leftArrowPos = new Rectangle(895, 120, 180, 180);
            rightArrowPos = new Rectangle(895, 120, 180, 180);
            legPressureLPos = new Rectangle(795, 200, 60, 60);
            legPressureRPos = new Rectangle(935, 200, 60, 60);
            legPressureLCollider.box = legPressureLPos;
            legPressureRCollider.box = legPressureRPos;

            for (int i = 0; i < 10; i++)
            {
                cloudspeed[i] = rng.Next(10, 30);
                cloudPos[i] = new Vector2(1000,0);
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            font = Content.Load<SpriteFont>("SlangOutfit");

            //start/endscreen
            startscreen = Content.Load<Texture2D>("startscreen");
            endscreen = Content.Load<Texture2D>("startscreen");

            //flamingo loader
            flamingo[0] = Content.Load<Texture2D>("flamingoLeftLeg");
            flamingo[1] = Content.Load<Texture2D>("flamingoLeftLegExitedR");
            flamingo[2] = Content.Load<Texture2D>("flamingoLeftLegExitedL");
            flamingo[9] = Content.Load<Texture2D>("flamingoLeftMoving");
            flamingo[10] = Content.Load<Texture2D>("flamingoLeftMovingExitedR");
            flamingo[11] = Content.Load<Texture2D>("flamingoLeftMovingExitedL");

            flamingo[6] = Content.Load<Texture2D>("flamingoRightLeg");
            flamingo[7] = Content.Load<Texture2D>("flamingoRightLegExitedR");
            flamingo[8] = Content.Load<Texture2D>("flamingoRightLegExitedL");
            flamingo[3] = Content.Load<Texture2D>("flamingoRightMoving");
            flamingo[4] = Content.Load<Texture2D>("flamingoRightMovingExitedR");
            flamingo[5] = Content.Load<Texture2D>("flamingoRightMovingExitedL");

            flamingoTurningPoint.X = flamingo[0].Width/2;
            flamingoTurningPoint.Y = flamingo[0].Height;

            //background loading
            backgroundRight = Content.Load<Texture2D>("RightBackground");
            slideBar = Content.Load<Texture2D>("sliderBar");
            backdropHill = Content.Load<Texture2D>("backdrop_hills");
            backdropLake = Content.Load<Texture2D>("backdrop_lake");
            backdropLawn = Content.Load<Texture2D>("backdrop_lawn");

            //flamingo slider textures
            sliderButtonFree = Content.Load<Texture2D>("SliderButtonFree");
            sliderButtonPressed = Content.Load<Texture2D>("SliderButtonPressed");
            flamingoSlider.face = sliderButtonFree;
            flamingoSlider.initiateBox();

            //shitButton textures
            shitBar = Content.Load<Texture2D>("shitBar");
            shitBarBlink = Content.Load<Texture2D>("shitBarBlinking");
            shitButton = Content.Load<Texture2D>("shitButtonFree");
            shitButtonDown = Content.Load<Texture2D>("shitButtonDown");
            shitButtonCollider.face = shitButton;
            shit = Content.Load<Texture2D>("shit");
            poopTex = Content.Load<Texture2D>("poop");
            for(int i = 0; i < 10; i++){
                poop[i] = new SpriteCollider();
                poopDirection[i] = new Vector2(rng.Next(0,10), rng.Next(0,10));
                poop[i].box = new Rectangle(200, 200, 32, 32);
                poop[i].face = poopTex;
            }

            //clouds
            cloudTex = Content.Load<Texture2D>("cloud");

            //fishes
            fish = Content.Load<Texture2D>("fish");
            rng = new System.Random();
            for (int i = 0; i < 10; i++)
            {
                fishes[i] = new SpriteCollider();
                fishspeed[i] = rng.Next(5,35);
                fishes[i].box = new Rectangle(-100, 300, 32, 32);
                fishes[i].face = fish;
                fishesalive[i] = false;
            }

            //food
            foodBar = Content.Load<Texture2D>("foodBar");
            foodBarAlert = Content.Load<Texture2D>("foodBarAlert");
            food = Content.Load<Texture2D>("food");

            //leg pressure gauge
            legPressureGauge = Content.Load<Texture2D>("pressureGauge");
            legPressureGaugeAlert = Content.Load<Texture2D>("pressureGaugeAlert");
            rightArrow = Content.Load<Texture2D>("rightArrow");
            leftArrow = Content.Load<Texture2D>("leftArrow");

            legPressureLOn = Content.Load<Texture2D>("LegButtonLOn");
            legPressureLOff = Content.Load<Texture2D>("LegButtonLOff");
            legPressureROn = Content.Load<Texture2D>("LegButtonROn");
            legPressureROff = Content.Load<Texture2D>("LegButtonROff");


            //load Sounds
            sfxShitMeter = Content.Load<SoundEffect>("shitBarAlertSound");
            sfxFoodMeter = Content.Load<SoundEffect>("foodBarAlertSound");
            sfxGauge = Content.Load<SoundEffect>("GaugeAlertSound");
            sfxTilt = Content.Load<SoundEffect>("tiltAlertSound");
            sfxClick = Content.Load<SoundEffect>("clickSound");
            sfxDeath = Content.Load<SoundEffect>("deathSound");
            sfxLegChange = Content.Load<SoundEffect>("legChangeSound");


            cursorTexture = Content.Load<Texture2D>("cursor");
            
            Mouse.SetCursor(MouseCursor.FromTexture2D(cursorTexture,8,8));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            var kstate = Keyboard.GetState();
            var mstate = Mouse.GetState();

            cursorPos.X = mstate.X;
            cursorPos.Y = mstate.Y;


            if (gameState != 1)
            {
                if (endtime - starttime > record && newRecord == false)
                {
                    record = endtime-starttime;
                    newRecord = true;
                }

                //game reset setters
                foodMeter = 200;
                shitMeter = 0;

                flamingoTilt = 0;
                tiltVelocity = 0f;
                flamingoSlider.box.X = sliderBarMiddle;
                leftArrowTilt = -0.5f;
                rightArrowTilt = -0.5f;
            }

            //deathchecks
            if ((flamingoTilt > 1.4 || flamingoTilt < -1.4) && gameState == 1)
            {
                die("Vou've fallen.");
            }

            if (shitMeter > 250)
            {
                die("You forgot to eject your waste.");
            }

            if (foodMeter <= 0)
            {
                die("You starved.");
            }

            if (leftArrowTilt > 3.9f)
            {
                die("Your leg left you.");
            }
            if (rightArrowTilt > 3.9f)
            {
                die("Your right leg failed.");
            }

            void die(string deathstring)
            {
                endtime = (int)gameTime.TotalGameTime.TotalSeconds;
                gameState = 2;
                funfactNumber = rng.Next(0, 6);
                deathmessage = deathstring;
                sfxDeath.Play();
            }
            //startscreen reset game
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && gameState == 0)
            {
                starttime = (int)gameTime.TotalGameTime.TotalSeconds;
                gameState = 1;
            }

            //endcreen reset to game
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && gameState == 2)
            {
                starttime = (int)gameTime.TotalGameTime.TotalSeconds;
                gameState = 1;
                newRecord = false;
            }

            if (legPressureLCollider.pointIsInside(cursorPos))
            {
                if (mstate.LeftButton == ButtonState.Pressed)
                {
                    leftLegOn = true;
                    legChange = 0;
                }
            }

            if (legPressureRCollider.pointIsInside(cursorPos))
            {
                if (mstate.LeftButton == ButtonState.Pressed)
                {
                    leftLegOn = false;
                    legChange = 0;
                }
            }

            legChange += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 10;

            for(int i = 0; i < 10; i++)
            {
                if (fishes[i].pointIsInside(cursorPos) && mstate.LeftButton == ButtonState.Pressed && fishesalive[i] == true)
                {
                    foodMeter += 50;
                    shitMeter += 10;

                    sfxClick.Play();

                    fishesalive[i] = false;
                }
            }

            if (flamingoSlider.pointIsInside(cursorPos) || flamingoSlider.pointIsInside(lastMousePos))
            {
                if (mstate.LeftButton == ButtonState.Pressed)
                {
                    flamingoButtonDown = true;
                    if (flamingoButtonDown != flamingoButtonDownChange)
                    {
                        flamingoSlider.face = sliderButtonPressed;
                    }

                    if ((flamingoSlider.box.X + (cursorPos.X - lastMousePos.X)) > sliderBarMin && ((flamingoSlider.box.X + flamingoSlider.face.Width) + (cursorPos.X - lastMousePos.X)) < sliderBarMax)
                    {
                        flamingoSlider.setPos(new Vector2(flamingoSlider.box.X + (cursorPos.X - lastMousePos.X), flamingoSlider.box.Y));
                    }
                }
                else
                {
                    flamingoButtonDown = false;
                    flamingoButtonNotDown();
                }
            }
            else
            {
                flamingoButtonDown = false;
                flamingoButtonNotDown();
            }
            void flamingoButtonNotDown()
            {
                if (flamingoButtonDown != flamingoButtonDownChange)
                {
                    flamingoSlider.face = sliderButtonFree;
                }

            }

            tiltVelocity += ((float)(flamingoSlider.box.X - sliderBarMiddle)) / 30000;


            if (shitButtonCollider.pointIsInside(cursorPos))
            {
                if (mstate.LeftButton == ButtonState.Pressed)
                {
                    ButtonDown = true;
                    if (ButtonDown != ButtonDownChange)
                    {
                        shitButtonCollider.face = shitButtonDown;
                    }
                    shitMeter -= 60 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (shitMeter < 0) 
                    {
                        shitMeter = 0;
                    }
                    else
                    {
                        poopcounter = (int)gameTime.TotalGameTime.TotalMilliseconds % 9;
                        poop[poopcounter].setPos(changingPoopOrigin);
                        tiltVelocity += 0.005f;
                        //addedFlamingoTiltShit = 1;// += 10 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
                else
                {
                    ButtonDown = false;
                    notshitting();
                }
            }
            else
            {
                ButtonDown = false;
                notshitting();
            }

            void notshitting()
            {
                if (ButtonDown != ButtonDownChange)
                {
                    shitButtonCollider.face = shitButton;
                }

                if (addedFlamingoTiltShit > 0)
                {
                    addedFlamingoTiltShit = 0;// 20 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            shitMeter += 8 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            foodMeter -= 6 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (shitMeter < 190)
            {
                shitPos.Height = (int)shitMeter;
                shitPos.Y = 275 + 190 - shitPos.Height;
            }

            if (foodMeter < 280)
            {
                foodPos.Height = (int)foodMeter;
                foodPos.Y = 275 + 280 - foodPos.Height;
            }
            else
                foodMeter = 280;

            for (int i = 0; i < 9; i++)
            {
                poop[i].box.X -= (int)((gameTime.ElapsedGameTime.TotalMilliseconds) * (poopDirection[i].X/15));
                poop[i].box.Y += (int)((gameTime.ElapsedGameTime.TotalMilliseconds) * (0.9 + (poopDirection[i].Y/100)));
            }


            //flamingo rotation changer
            flamingoTilt += MathHelper.ToRadians(tiltVelocity + addedFlamingoTiltShit);


            //legPressureGauge Arrow movement and sprite logic
            if (leftLegOn)
            {
                if (leftArrowTilt < 4)
                    leftArrowTilt += (float)((gameTime.ElapsedGameTime.TotalMilliseconds) / 3200);
                if (rightArrowTilt > -0.8f)
                    rightArrowTilt -= (float)((gameTime.ElapsedGameTime.TotalMilliseconds) / 2500);

                if (legChange > 20)
                {
                    flamingoState = 0;

                    if (flamingoTilt < -0.9f)
                    {
                        flamingoState = 2;
                    }
                    else if (flamingoTilt > 0.9f)
                    {
                        flamingoState = 1;
                    }
                }
                else
                {
                    flamingoState = 3;

                    if (flamingoTilt < -0.9f)
                    {
                        flamingoState = 5;
                    }
                    else if (flamingoTilt > 0.9f)
                    {
                        flamingoState = 4;
                    }
                }
            }
            else
            {
                if (rightArrowTilt < 4)
                    rightArrowTilt += (float)((gameTime.ElapsedGameTime.TotalMilliseconds) / 3200);
                if (leftArrowTilt > -0.8f)
                    leftArrowTilt -= (float)((gameTime.ElapsedGameTime.TotalMilliseconds) / 2500);

                if (legChange > 20)
                {
                    flamingoState = 6;

                    if (flamingoTilt < -0.9f)
                        flamingoState = 8;
                    else if (flamingoTilt > 0.9f)
                        flamingoState = 7;
                }
                else
                {
                    flamingoState = 9;

                    if (flamingoTilt < -0.9f)
                        flamingoState = 11;
                    else if (flamingoTilt > 0.9f)
                        flamingoState = 10;
                }
            }
            
            if(flamingoState != lastFlamingoState && (flamingoState == 0 || flamingoState == 6))
            {
                sfxLegChange.Play();
                lastFlamingoState = flamingoState;
            }
            
            if ((flamingoTilt < -0.9f || flamingoTilt > 0.9f) && ((int)(gameTime.TotalGameTime.TotalMilliseconds / 300) % 2) == 1 && sfxTiltPlayed == false)
                {
                    sfxTilt.Play();
                    sfxTiltPlayed = true;
                }
            else if (((int)(gameTime.TotalGameTime.TotalMilliseconds / 300) % 2) != 1)
                {
                    sfxTiltPlayed = false;
                }
            


            //Math for finding the flamingo butt
            flamingoFeetOrigin = new Vector2(flamingoPos.X + flamingoTurningPoint.X, flamingoPos.Y + flamingoTurningPoint.Y);
            poopOrigin = new Vector2(flamingoFeetOrigin.X - 100, flamingoFeetOrigin.Y - 290);

            //changingPoopOrigin.X = poopOrigin.X;
            //changingPoopOrigin.Y = poopOrigin.Y;

            //changingPoopOrigin.Y = flamingoFeetOrigin.Y - getYPos();
            //changingPoopOrigin.X = flamingoFeetOrigin.X - getXPos();

            changingPoopOrigin.Y = flamingoPos.Y - getYPos();
            changingPoopOrigin.X = flamingoPos.X - getXPos();


            //finalFlamingoTilt = flamingoTilt;


            //fish logic
            for (int i = 0; i < 9; i++)
            {
                fishes[i].box.X += (int)((gameTime.ElapsedGameTime.TotalMilliseconds) / fishspeed[i]);
            }

            for (int i = 0; i < 19; i=i+2)
            {
                if (i == (((int)gameTime.TotalGameTime.TotalSeconds) % 20))
                {
                    fishesalive[i/2] = true;
                    fishes[i/2].box.X = -100;
                    fishes[i / 2].box.Y = rng.Next(300,500);
                    fishcounter++;
                    if (fishcounter >= 10)
                        fishcounter = 0;
                }
            }


            //sound controls
            if (shitMeter > 180 && ((int)(gameTime.TotalGameTime.TotalMilliseconds / 500) % 2) == 1 && sfxShitMeterPlayed == false)
            {
                sfxShitMeter.Play();
                sfxShitMeterPlayed = true;
            }
            else if (((int)(gameTime.TotalGameTime.TotalMilliseconds / 500) % 2) != 1)
            {
                sfxShitMeterPlayed = false;
            }

            if (foodMeter < 50 && ((int)(gameTime.TotalGameTime.TotalMilliseconds / 700) % 2) == 1 && sfxFoodMeterPlayed == false)
            {
                sfxFoodMeter.Play();
                sfxFoodMeterPlayed = true;
            }
            else if (((int)(gameTime.TotalGameTime.TotalMilliseconds / 700) % 2) != 1)
            {
                sfxFoodMeterPlayed = false;
            }

            if ((leftArrowTilt > 2.7f || rightArrowTilt > 2.7f) && ((int)(gameTime.TotalGameTime.TotalMilliseconds / 200) % 2) == 1 && sfxGaugePlayed == false)
            {
                sfxGauge.Play();
                sfxGaugePlayed = true;
            }
            else if (((int)(gameTime.TotalGameTime.TotalMilliseconds / 200) % 2) != 1)
            {
                sfxGaugePlayed = false;
            }


            //cloud logic
            for (int i = 0; i < 10; i++)
            {
                cloudPos[i].X += (float)((gameTime.ElapsedGameTime.TotalMilliseconds) / cloudspeed[i]);
            }

            for (int i = 0; i < 10; i++)
            {
                if (((int)(gameTime.TotalGameTime.TotalSeconds / 5) % 10) == i)
                {
                    cloudPos[i].X = -100;
                    cloudPos[i].Y = rng.Next(10,190);
                }
            }


            flamingoButtonDownChange = flamingoButtonDown;
            ButtonDownChange = ButtonDown;
            lastMousePos = cursorPos;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AliceBlue);


            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);


            _spriteBatch.Draw(backdropLake, screen, Color.White);

            //fish drawing
            for (int i = 0; i < 10; i++)
            {
                if (fishesalive[i])
                    _spriteBatch.Draw(fishes[i].face, fishes[i].box, Color.White);
            }

            _spriteBatch.Draw(backdropHill, screen, Color.White);

            //poop drawing
            for (int i = 0; i < 9; i++)
            {
                _spriteBatch.Draw(poop[i].face, poop[i].box, Color.White);
            }

            //cloud drawing
            for (int i = 0; i < 10; i++)
            {
                _spriteBatch.Draw(cloudTex, cloudPos[i], Color.White);
            }


            _spriteBatch.Draw(flamingo[flamingoState], new Vector2(flamingoPos.X, flamingoPos.Y), null, Color.White, flamingoTilt, flamingoTurningPoint, 5.5f,SpriteEffects.None, 1);

            _spriteBatch.Draw(backdropLawn, screen, Color.White);

            _spriteBatch.Draw(backgroundRight, backgroundRightPos, Color.White);
            _spriteBatch.Draw(slideBar, slideBarPos, Color.White);

            _spriteBatch.Draw(flamingoSlider.face, flamingoSlider.getPos(), Color.White);


            //Leg pressure Gauge
            if ((leftArrowTilt > 2.7f || rightArrowTilt > 2.7f) && ((int)(gameTime.TotalGameTime.TotalMilliseconds / 200) % 2) == 1)
                _spriteBatch.Draw(legPressureGaugeAlert, legPressureGaugePos, Color.White);
            else
                _spriteBatch.Draw(legPressureGauge, legPressureGaugePos, Color.White);

            _spriteBatch.Draw(leftArrow, new Vector2(leftArrowPos.X, leftArrowPos.Y), null, Color.White, leftArrowTilt, new Vector2((leftArrow.Width / 2), (leftArrow.Height / 2)), 2.1f, SpriteEffects.None, 1);
            _spriteBatch.Draw(rightArrow, new Vector2(rightArrowPos.X, rightArrowPos.Y), null, Color.White, rightArrowTilt, new Vector2((rightArrow.Width / 2), (rightArrow.Height / 2)), 2.1f, SpriteEffects.None, 1);

            if (leftLegOn)
            {
                _spriteBatch.Draw(legPressureLOn, legPressureLPos, Color.White);
                _spriteBatch.Draw(legPressureROff, legPressureRPos, Color.White);
            }
            else
            {
                _spriteBatch.Draw(legPressureROn, legPressureRPos, Color.White);
                _spriteBatch.Draw(legPressureLOff, legPressureLPos, Color.White);
            }


            //foodbar
            _spriteBatch.Draw(food, foodPos, Color.White);
            if(foodMeter < 50 && ((int)(gameTime.TotalGameTime.TotalMilliseconds / 700) % 2) == 1)
                _spriteBatch.Draw(foodBarAlert, foodBarPos, Color.White);
            else
                _spriteBatch.Draw(foodBar, foodBarPos, Color.White);


            //shitbar
            _spriteBatch.Draw(shit, shitPos, Color.White);
            if (shitMeter > 180 && ((int)(gameTime.TotalGameTime.TotalMilliseconds / 500) % 2) == 1)
            {
                _spriteBatch.Draw(shitBarBlink, shitBarPos, Color.White);
            }
            else
                _spriteBatch.Draw(shitBar, shitBarPos, Color.White);
            _spriteBatch.Draw(shitButtonCollider.face, shitButtonCollider.box, Color.White);


            //Debug stuff

            //_spriteBatch.Draw(shitBar, new Rectangle((int)changingPoopOrigin.X, (int)changingPoopOrigin.Y,20,20), Color.White);

            //_spriteBatch.DrawString(font, "X: " + flamingoPos.X, new Vector2(10, 10), Color.Black);
            //_spriteBatch.DrawString(font, "Y: " + flamingoPos.Y, new Vector2(10, 40), Color.Black);
            //_spriteBatch.DrawString(font, "LastButtonDown: " + ButtonDownChange, new Vector2(10, 10), Color.Black);
            //_spriteBatch.DrawString(font, "ButtonDown: " + ButtonDown, new Vector2(10, 40), Color.Black);
            //_spriteBatch.DrawString(font, "rotation as radiant: " + flamingoTilt, new Vector2(10, 40), Color.Black);
            //_spriteBatch.DrawString(font, "b: " + ((306 * System.Math.Sin(ConvertDegreesToRadians(90 - 19))) / System.Math.Sin(ConvertDegreesToRadians(90))), new Vector2(10, 10), Color.Black);
            //_spriteBatch.DrawString(font, "x: " + getXPos(), new Vector2(10, 0), Color.Black);
            //_spriteBatch.DrawString(font, "y: " + getYPos(), new Vector2(10, 40), Color.Black);


            //startscreen drawing
            if (gameState == 0)
            {
                _spriteBatch.Draw(startscreen, screen, Color.White);
                _spriteBatch.DrawString(font, "Beeing a flamingo is hard sometimes. You need to:", new Vector2(140, 230), Color.White);
                _spriteBatch.DrawString(font, "-eat fish", new Vector2(140, 280), Color.White);
                _spriteBatch.DrawString(font, "-digest fish", new Vector2(140, 330), Color.White);
                _spriteBatch.DrawString(font, "-change legs", new Vector2(140, 380), Color.White);
                _spriteBatch.DrawString(font, "-and keep. your. balance.", new Vector2(140, 430), Color.White);
                _spriteBatch.DrawString(font, "Press enter to play!", new Vector2(140, 480), Color.White);
                _spriteBatch.DrawString(font, "Press esc to exit", new Vector2(140, 520), Color.Gray);

            }

            //endscreen drawing
            if (gameState == 2)
            {
                _spriteBatch.Draw(endscreen, screen, Color.White);
                _spriteBatch.DrawString(font, funfacts[funfactNumber], new Vector2(140, 230), Color.White);
                if(newRecord)
                    _spriteBatch.DrawString(font, "new Record: " + record + "!", new Vector2(140, 330), Color.Red);
                else
                    _spriteBatch.DrawString(font, "Record: " + record, new Vector2(140, 330), Color.White);
                _spriteBatch.DrawString(font, "You survived for " + (endtime - starttime) + " Seconds.", new Vector2(140, 430), Color.White);
               // _spriteBatch.DrawString(font, "-change legs", new Vector2(140, 430), Color.White);
                _spriteBatch.DrawString(font, "" + deathmessage, new Vector2(140, 380), Color.White);
                _spriteBatch.DrawString(font, "Press enter to play again!", new Vector2(140, 480), Color.White);
                _spriteBatch.DrawString(font, "Press esc to exit", new Vector2(140, 520), Color.Gray);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
       
    }

}
