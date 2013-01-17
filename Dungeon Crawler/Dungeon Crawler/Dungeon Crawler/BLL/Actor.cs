using System;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public virtual void Update()
        {
            //Add Our Acceleration
            pVelocity += pAcceleration;
        }

        public virtual String GetDebugInformation()
        {
            return "[Loc: " + pLocation.ToString() + "]";
        }

        public virtual void AddImpulse(Vector2 aImpulse)
        {
            pAcceleration += aImpulse;
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
        }

        public Vector2 Velocity
        {
            get { return pVelocity; }
        }

        public Vector2 Acceleration
        {
            get { return pAcceleration; }
        }

        public float Heading
        {
            get { return pHeading; }
        }
        #endregion
    }
}
