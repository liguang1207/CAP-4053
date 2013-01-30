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
<<<<<<< HEAD
        private List<Vector2> RecEdge = new List<Vector2>();
        private List<float> intersectDistance = new List<float>();
        private const float MAX_RANGE = 100f;
        private float CurDistance = 0;
        private float ShortestDistance = 100f;
        private float rTop, sTop, rBot, sBot;
=======
        private const float MAX_RANGE = 128f;
>>>>>>> Version 1.0.0.4

       
        public WallSensor(ref List<Actor> aWorldActors, Actor aHost)
            : base(ref aWorldActors, aHost)
        {

        }

        public override void Sense()
        {
            base.Sense();

            EndPoints = new List<Vector2>();
<<<<<<< HEAD
            EndPoints.Add(new Vector2((float)Math.Cos(pWorldActors[0].Rotation) * 100 + pWorldActors[0].Position.X, (float)Math.Sin(pWorldActors[0].Rotation) * 100 + pWorldActors[0].Position.Y));
            EndPoints.Add(new Vector2((float)Math.Cos(pWorldActors[0].Rotation + .8) * 100 + pWorldActors[0].Position.X, (float)Math.Sin(pWorldActors[0].Rotation + .8) * 100 + pWorldActors[0].Position.Y));
            EndPoints.Add(new Vector2((float)Math.Cos(pWorldActors[0].Rotation - .8) * 100 + pWorldActors[0].Position.X, (float)Math.Sin(pWorldActors[0].Rotation - .8) * 100 + pWorldActors[0].Position.Y));
            foreach (Actor A in pWorldActors)
=======

            for (int i = 0; i < 3; i++) //3 Feelers
>>>>>>> Version 1.0.0.4
            {
                float RotationOffset = 0f;
                if (i == 0) { RotationOffset = -0.4f; }
                else if (i == 2) { RotationOffset = 0.4f; }

                //Get Next Position
                Vector2 Velocity = pHost.Velocity;
                Velocity.X = (float)Math.Cos(pHost.Rotation + RotationOffset) * (MAX_RANGE + pHost.RotationalVelocity);
                Velocity.Y = (float)Math.Sin(pHost.Rotation + RotationOffset) * (MAX_RANGE + pHost.RotationalVelocity);
                Vector2 NextPosition = pHost.Position + Velocity;

                //Make Ray
                Ray2D R = new Ray2D(pHost.Position, NextPosition);
                Boolean HitWall = false;

                foreach (Actor A in pWorldActors)
                {
                    if (A is BlockingActor) //If its a wall
                    {
<<<<<<< HEAD
                        RecEdge = A.Edges;

                        int i,j;
                        for (i = 0; i < 3; i++)
                        {
                            for (j = 0; j < 4; j++)
                            {
                                rTop = (pHost.Position.Y - RecEdge[j].Y) * (RecEdge[(j + 1) % 4].X - RecEdge[j].X) - (pHost.Position.X - RecEdge[j].X) * (RecEdge[(j + 1) % 4].Y - RecEdge[j].Y);
                                rBot = (EndPoints[i].X - pHost.Position.X) * (RecEdge[(j + 1) % 4].Y - RecEdge[j].Y) - (EndPoints[i].Y - pHost.Position.Y) * (RecEdge[(j + 1) % 4].X - RecEdge[j].X);

                                sTop = (pHost.Position.Y - RecEdge[j].Y) * (EndPoints[i].X - pHost.Position.X) - (pHost.Position.X - RecEdge[j].X) * (EndPoints[i].Y - pHost.Position.Y);
                                sBot = (EndPoints[i].X - pHost.Position.X) * (RecEdge[(j + 1) % 4].Y - RecEdge[j].Y) - (EndPoints[i].Y - pHost.Position.Y) * (RecEdge[(j + 1) % 4].X - RecEdge[j].X);

                                if ((rBot == 0) || (sBot == 0))
                                {
                                    //they are parallel
                                    intersectDistance[i] = 100f; 
                                }

                                float r = rTop / rBot;
                                float s = sTop / sBot;

                                if ((r > 0) && (r < 1) && (s > 0) && (s < 1))
                                {
                                    intersectDistance[i] = Vector2.Distance(pHost.Position, EndPoints[i]) * r;

                                }
                                else
                                {
                                    intersectDistance[i] = 0;
                                }
                            
                            
                            
                            }

=======
                        //Get Distance
                        float Distance = Vector2.Distance(A.Position, pHost.Position);
                        if (Distance <= MAX_RANGE + pHost.RotationalVelocity) //In Range
                        {
                            Vector2 HitAt = R.Intersects(A.CollisionRectangle); //Check if it intersects
                            if (HitAt != Vector2.Zero)
                            {
                                EndPoints.Add(HitAt);
                                HitWall = true;
                            }
>>>>>>> Version 1.0.0.4
                        }
                    }
                    
                   
                }

                if (!HitWall)
                {
                    EndPoints.Add(NextPosition);
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
