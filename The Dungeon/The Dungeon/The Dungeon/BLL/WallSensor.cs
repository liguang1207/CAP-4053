using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Drawing;

namespace The_Dungeon.BLL
{
    class WallSensor : Sensor
    {
        private List<Vector2> EndPoints = new List<Vector2>();
        private const float MAX_RANGE = 256f;

        public WallSensor(ref List<Actor> aWorldActors, Actor aHost)
            : base(ref aWorldActors, aHost)
        {

        }

        public override void Sense()
        {
            base.Sense();

            EndPoints = new List<Vector2>();
            foreach (Actor A in pWorldActors)
            {
                if (A is BlockingActor)
                {
                    //Get Distance
                    float Distance = Vector2.Distance(A.Position, pHost.Position);
                    if (Distance <= MAX_RANGE)
                    {
                        EndPoints.Add(A.Position);
                    }
                }
            }
        }

        public override void Draw(ref SpriteBatch SB)
        {
            base.Draw(ref SB);

            DrawingHelper.Begin(PrimitiveType.LineList);

            for (int i = 0; i < EndPoints.Count; i++)
            {
                DrawingHelper.DrawLine(pHost.Position, EndPoints[i], Color.Yellow);
            }
            DrawingHelper.End();
        }
    }
}
