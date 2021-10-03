using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD49_butterfly
{
    class SpriteCollider
    {
        // position Vector for the sprite. (topleft.x, topleft.y, botright.x, botright.y)
        //public Vector4 position = new Vector4(0,0,0,0);
        public Rectangle box = new Rectangle(0, 0, 0, 0);

        // graficsfile/png/sprite
        public Texture2D face;


        public void initiateBox()
        {
            box.Width = face.Width;
            box.Height = face.Height;
        }


        public Vector2 getPos()
        {
            return new Vector2(box.X, box.Y);
        }

        public void setPos(Vector2 pos)
        {
            box.X = (int)pos.X;
            box.Y = (int)pos.Y;
        }

        public bool pointIsInside(Vector2 pos)
        {
            if (pos.X > box.X && pos.Y > box.Y && pos.X < (box.X + box.Width) && pos.Y < (box.Y + box.Height))
            {
                return true;
            }
            return false;
        }
    }
}
