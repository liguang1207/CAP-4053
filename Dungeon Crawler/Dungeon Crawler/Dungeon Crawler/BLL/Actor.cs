using System;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Dungeon_Crawler.BLL
{
    class Actor
    {
        #region Private Members
        private int pHealth;
        private int pMaxHealth;

        private Vector2 pLocation;
        private Vector2 pVelocity;
        private Vector2 pAcceleration;

        private float pHeading;

        private SpriteBatch pSpriteBatch;

        private Boolean bHumanControlled = false;
        #endregion

        #region Constructors
        public Actor(ref SpriteBatch aSpriteBatch)
        {
            pLocation = new Vector2(0, 0);
            pVelocity = new Vector2(0, 0);
            pAcceleration = new Vector2(0, 0);

            pSpriteBatch = aSpriteBatch;
        }

        public Actor(Vector2 aLocation, Vector2 aVelocity, Vector2 aAcceleration, ref SpriteBatch aSpriteBatch)
        {
            pLocation = aLocation;
            pVelocity = aVelocity;
            pAcceleration = aAcceleration;

            pSpriteBatch = aSpriteBatch;
        }
        #endregion

        public virtual void Update(float ViewPort_Width, float ViewPort_Height)
        {
            //Newtons I Law
            if (pAcceleration.X > 0) pAcceleration.X -= 0.01f;
            else if (pAcceleration.X < 0) pAcceleration.X += 0.01f;

            if (pAcceleration.Y > 0) pAcceleration.Y -= 0.01f;
            else if (pAcceleration.Y < 0) pAcceleration.Y += 0.01f;

            //Add Our Acceleration
            pVelocity += pAcceleration;

            if(pVelocity.X > 0)
                pVelocity.X = Math.Min(pVelocity.X, 2);
            else if (pVelocity.X < 0)
                pVelocity.X = Math.Max(pVelocity.X, -2);

            if (pVelocity.Y > 0)
                pVelocity.Y = Math.Min(pVelocity.Y, 2);
            else if (pVelocity.Y < 0)
                pVelocity.Y = Math.Max(pVelocity.Y, -2);

            //Update Position
            pLocation += pVelocity;

            pLocation.X = MathHelper.Clamp(pLocation.X, 0, ViewPort_Width - 64);
            pLocation.Y = MathHelper.Clamp(pLocation.Y, 0, ViewPort_Height - 64);
        }

        public virtual void Tick(GameTime GameTime)
        {
            Velocity = new Vector2(0, 0);
            Acceleration = new Vector2(0, 0);

            if (bHumanControlled)
            {
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.W))
                {
                    AddImpulse(new Vector2(0, -2f));
                    Heading = 0;
                }

                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.A))
                {
                    AddImpulse(new Vector2(-2f, 0));
                    Heading = 4.7f;
                }

                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S))
                {
                    AddImpulse(new Vector2(0, 2f));
                    Heading = 3.15f;
                }

                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.D))
                {
                    AddImpulse(new Vector2(2f, 0));
                    Heading = 1.55f;
                }
            }
        }



        public virtual String GetDebugInformation()
        {
            return "[Loc: " + pLocation.ToString() + "]\r\n[Vel: " + pVelocity.ToString() + "]\r\n[Acc: " + pAcceleration.ToString() + "]\r\n[Hea: " + pHeading.ToString() + "]\r\n" ;
        }

        public virtual void AddImpulse(Vector2 aImpulse)
        {
            pAcceleration += aImpulse;

            pAcceleration.X = Math.Min(pAcceleration.X, 1);
            pAcceleration.Y = Math.Min(pAcceleration.Y, 1);
        }

        public virtual void TakeDamage(int aDamage)
        {
            pHealth -= aDamage;

            pHealth = Math.Max(pHealth, 0);

            if (pHealth == 0)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            throw new NotImplementedException();
        }

        public virtual void TakeHealth(int aHealth)
        {
            pHealth += aHealth;

            pHealth = Math.Min(pHealth, pMaxHealth);
        }

        #region Public Access Variables
        public int Health
        {
            get { return pHealth; }
        }

        public int MaxHealth
        {
            get { return pMaxHealth; }
        }

        public Vector2 Location
        {
            get { return pLocation; }
            set { pLocation = value; }
        }

        public Vector2 Velocity
        {
            get { return pVelocity; }
            set { pVelocity = value; }
        }

        public Vector2 Acceleration
        {
            get { return pAcceleration; }
            set { pAcceleration = value; }
        }

        public float Heading
        {
            get { return pHeading; }
            set { pHeading = value; }
        }

        public Boolean HumanControlled
        {
            get { return bHumanControlled; }
            set { bHumanControlled = value; }
        }
        #endregion
    }
}
