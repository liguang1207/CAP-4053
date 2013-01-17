using System;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Dungeon_Crawler.BLL
{
    class DCPlayer : Actor
    {
        private String pName;
        private Texture2D pAvatar;

        public DCPlayer(ref SpriteBatch aSpriteBatch)
            : base(ref aSpriteBatch)
        {
            Initialize();
        }

        public DCPlayer(Vector2 aLocation, Vector2 aVelocity, Vector2 aAcceleration, ref SpriteBatch aSpriteBatch)
            : base(aLocation, aVelocity, aAcceleration, ref aSpriteBatch)
        {
            Initialize();
        }

        public DCPlayer(Vector2 aLocation, Vector2 aVelocity, Vector2 aAcceleration, String Name, ref SpriteBatch aSpriteBatch)
            : base(aLocation, aVelocity, aAcceleration, ref aSpriteBatch)
        {
            pName = Name;
            Initialize();
        }

        public virtual void Initialize()
        {
            pAvatar.ToString();
            //pAvatar = Content.Load<Texture2D>("frame");
            
            //TODO: Load Player Avatar
        }
    }
}
