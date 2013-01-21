using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_Dungeon.BLL
{
    class ControllableActor : Actor
    {
        Controller pController;
        List<object> Sensors = new List<object>();

        public ControllableActor(Texture2D aSprite, Rectangle? aSourceRectangle, Color aSourceColor)
            : base(aSprite, aSourceRectangle, aSourceColor)
        {
            pController = new Controller(this);

            SetCollision(Actor.CollisionType.Solid);
        }

        public virtual void SetController(Controller C)
        {
            pController = C;
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();
        }


        public virtual void HandleInput()
        {
            if (pController != null)
            {
                pController.HandleInput();
            }

            Position += Velocity;
        }
    }
}
