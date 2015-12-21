using System;
using System.Collections.Generic;
using System.Drawing;

namespace Chess
{
    public class ChessCollection : List<Chess>, IDrawable
    {
        public void Draw( Graphics g )
        {
            foreach ( Chess chess in this )
                g.Draw( chess );
        }

        private ChessCollection GetChessByColor( GameColor color )
        {
            ChessCollection collection = new ChessCollection();
            foreach ( Chess chess in this )
            {
                if ( chess.Color == color )
                    collection.Add( chess );
            }
            return collection;
        }

        public ChessCollection Black
        {
            get { return GetChessByColor( GameColor.Black ); }
        }

        public ChessCollection White
        {
            get { return GetChessByColor( GameColor.White ); }
        }

        public void UpdatePositions()
        {
            foreach ( Chess chess in this )
                chess.UpdatePosition();
        }

        public void UpdateSprite()
        {
            foreach ( Chess chess in this )
                chess.UpdateSprite();
        }

        public SizeF ChessSize
        {
            get
            {
                if ( Count == 0 )
                    return new SizeF();
                return this[ 0 ].Sprite.Size;
            }
            set
            {
                foreach ( Chess chess in this )
                    chess.Sprite.Size = value;
            }
        }
    }
}
