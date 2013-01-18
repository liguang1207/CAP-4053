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
    class DCEnemy : DCPlayer
    {
        public DCEnemy(Vector2 aLocation, Vector2 aVelocity, Vector2 aAcceleration, ref SpriteBatch aSpriteBatch)
            : base(aLocation, aVelocity, aAcceleration, ref aSpriteBatch)
        {
            Initialize();
        }

        public DCEnemy(Vector2 aLocation, Vector2 aVelocity, Vector2 aAcceleration, String Name, ref SpriteBatch aSpriteBatch, Texture2D aAvatar)
            : base(aLocation, aVelocity, aAcceleration, Name, ref aSpriteBatch, aAvatar)
        {
            Initialize();
        }

        public override void Initialize()
        {
            HumanControlled = false;
        }

    }
}
