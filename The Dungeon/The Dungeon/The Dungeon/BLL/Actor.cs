using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_Dungeon.BLL
{
    class Actor
    {
        public enum CollisionType
        {
            Hollow = 0,
            Solid = 1
        };

        //Physical Properties
        private float pHealth;
        private float pMaxHealth = 100;
        private CollisionType pCollision = CollisionType.Hollow;

        private Vector2 pPosition;
        private Vector2 pLastPosition = new Vector2(0, 0);
        private Vector2 pVelocity;

        private float pRotation;
        private float pRotationalVelocity = 4f;

        //Graphic Properties
        private Texture2D pSprite;
        private Color[] pSpriteData = null;
        private Rectangle? pSourceRectangle;
        private Color pSourceColor;

        //Debug Properties
        private Boolean bDebugMode = false;

        public Actor() { }

        public Actor(Texture2D aSprite, Rectangle? aSourceRectangle, Color aSourceColor)
        {
            pSprite = aSprite;
            pSpriteData = new Color[aSprite.Width * aSprite.Height];
            pSprite.GetData(pSpriteData);

            pSourceRectangle = aSourceRectangle;
            pSourceColor = aSourceColor;
        }

        public virtual void Reinitialize(Texture2D aSprite,  Rectangle? aSourceRectangle, Color aSourceColor)
        {
            pSprite = aSprite;
            pSpriteData = new Color[aSprite.Width * aSprite.Height];
            pSprite.GetData(pSpriteData);

            pSourceRectangle = aSourceRectangle;
            pSourceColor = aSourceColor;

            
        }

        public virtual void SetCollision(CollisionType aCollision)
        {
            pCollision = aCollision;
        }

        public virtual Boolean CheckCollision(Actor B)
        {
            if (CollisionRectangle.Intersects(B.CollisionRectangle))
            {
                pPosition = pLastPosition;
                return true;
            }

            return false;
        }


        public virtual void Update(GameTime gameTime) {}


        public virtual void Draw(ref SpriteBatch SB)
        {
            if (bDebugMode)
            {
                //TODO: Get Debug Output
                pSourceColor = Color.Yellow;
            }

            SB.Draw(pSprite, pPosition, pSourceRectangle, pSourceColor, pRotation, new Vector2(pSprite.Width / 2, pSprite.Height / 2), 1f, SpriteEffects.None, 0);
        }

        public virtual void ToggleDebug(Boolean IsDebugMode)
        {
            bDebugMode = IsDebugMode;
        }


        public virtual void TakeDamage(float Damage)
        {
            pHealth += Damage;

            if (pHealth <= 0)
            {
                pHealth = 0;
                Die();
            }
            else if (pHealth > pMaxHealth)
            {
                pHealth = pMaxHealth;
            }
        }

        public virtual void Die()
        {
            
        }



        public CollisionType Collision
        {
            get { return pCollision; }
        }

        public Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle(
                    (int)Math.Floor(pPosition.X - (pSprite.Width / 2)),
                    (int)Math.Floor(pPosition.Y - (pSprite.Height / 2)),
                    pSprite.Width,
                    pSprite.Height);
            }
        }

        public Vector2 Position
        {
            get { return pPosition; }
            set 
            { 
                pLastPosition = pPosition; 
                pPosition = value;

                if (pPosition.X < pSprite.Width + 4) pPosition.X = pSprite.Width + 4;
                if (pPosition.Y < pSprite.Height + 4) pPosition.Y = pSprite.Height + 4;
            }
        }

        public Vector2 Velocity
        {
            get { return pVelocity; }
            set { pVelocity = value; }
        }
        
        public float Rotation
        {
            get { return pRotation; }
            set { pRotation = value; }
        }

        public float RotationalVelocity
        {
            get { return pRotationalVelocity; }
            set { pRotationalVelocity = value; }
        }
    }
}
