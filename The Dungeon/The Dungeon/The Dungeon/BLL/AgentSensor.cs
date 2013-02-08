using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Drawing;

namespace The_Dungeon.BLL
{
    class AgentSensor: Sensor
    {
        private List<Vector2> EndPoints = new List<Vector2>();
        private const float MAX_RANGE = 64;

        public AgentSensor(ref List<Actor> aWorldActors, Actor aHost, SpriteFont aDebugFont)
            : base(ref aWorldActors, aHost, aDebugFont)
        {

        }

        public override void Sense()
        {
            base.Sense();

            EndPoints = new List<Vector2>();
            foreach (Actor A in pWorldActors)
            {
                if (A is Pawn && A != pHost)
                {
                    //Get Distance
                    float Distance = Vector2.Distance(A.Position, pHost.Position);
                    if (Distance <= MAX_RANGE + A.CollisionRectangle.Width/2)
                    {
                        EndPoints.Add(A.Position);
                        DebugInformation += "Enemy(" + (EndPoints.Count).ToString() + ") - " + A.Position + " , Heading: " + A.Rotation +"\r\n";
                    }
                }
            }
        }

        public override void Draw(ref SpriteBatch SB)
        {
            base.Draw(ref SB);

            DrawingHelper.DrawCircle(pHost.Position, MAX_RANGE, Color.Blue, false);

            DrawingHelper.Begin(PrimitiveType.LineList);
            for (int i = 0; i < EndPoints.Count; i++)
            {
                DrawingHelper.DrawLine(pHost.Position, EndPoints[i], Color.Yellow);
            }
            DrawingHelper.End();
        }
    }
}

