using System.Drawing;

namespace Chess
{
    public abstract class Chess : IDrawable
    {
        public ChessDeskCell Cell { get; set; }
        public Sprite Sprite { get; set; }
        public GameColor Color { get; private set; }
        public ChessType Type { get; private set; }
        public ChessFactory Factory { get; private set; }
        public bool UsingFreeMove { get; set; }

        protected Chess( ChessFactory factory, GameColor color, ChessType type )
        {
            Factory = factory;
            Color = color;
            Type = type;
            Sprite = Factory.Skin[ color, type ].Clone();
        }

        public void UpdateSprite()
        {
            Sprite.Cut = Factory.Skin[ Color, Type ].Cut;
        }

        public void Kill()
        {
            Cell.Chess = null;
            Cell = null;
            Factory.Kill( this );
        }

        public void Dispose()
        {
            Cell.Chess = null;
            Cell = null;
            Factory.Remove( this );
        }

        #region Checkers

        protected abstract bool CanMoveInternal( Point index );

        public bool CanMove( Point index )
        {
            if ( Cell == null                           /* Killed chess can't move */
              || index == Cell.Index                    /* Same position */
              || ChessDesk.CheckOutOfRange( index ) )   /* In range desk */
                return false;

            if ( UsingFreeMove )
                return true;

            return CanMoveInternal( index ); // Our algorithm
        }

        protected bool CheckMove( Point index )
        {
            Chess chess = Cell.Desk[ index ].Chess;
            if ( chess == null )
                return true; // Empty cell is true
            return chess.Color != Color; // Chess opponent is true
        }
        protected bool CheckMoveKillOpponent( Point index )
        {
            Chess chess = Cell.Desk[ index ].Chess;
            if ( chess == null )
                return false; // Empty cell is not kill
            return chess.Color != Color; // Chess opponent kill
        }
        protected bool CheckMoveOnEmptyCell( Point index )
        {
            return Cell.Desk[ index ].Chess == null;
        }

        protected bool CheckRookLine( Point index )
        {
            if ( index.X != Cell.Index.X && index.Y != Cell.Index.Y )
                return false;

            Point i = Cell.Index;
            if ( index.X > Cell.Index.X ) // right
            {
                ++i.X;
                for ( ; i.X < 8; ++i.X )
                {
                    if ( Cell.Desk[ i ].Chess != null )
                        return ( Cell.Desk[ i ].Chess.Color != Color && i == index ) ? true : false;
                    else if ( i == index )
                        return true;
                }
            }
            else if ( index.X < Cell.Index.X ) // left
            {
                --i.X;
                for ( ; i.X >= 0; --i.X )
                {
                    if ( Cell.Desk[ i ].Chess != null )
                        return ( Cell.Desk[ i ].Chess.Color != Color && i == index ) ? true : false;
                    else if ( i == index )
                        return true;
                }
            }
            else if ( index.Y < Cell.Index.Y ) // bottom
            {
                --i.Y;
                for ( ; i.Y >= 0; --i.Y )
                {
                    if ( Cell.Desk[ i ].Chess != null )
                        return ( Cell.Desk[ i ].Chess.Color != Color && i == index ) ? true : false;
                    else if ( i == index )
                        return true;
                }
            }
            else if ( index.Y > Cell.Index.Y ) // up
            {
                ++i.Y;
                for ( ; i.Y < 8; ++i.Y )
                {
                    if ( Cell.Desk[ i ].Chess != null )
                        return ( Cell.Desk[ i ].Chess.Color != Color && i == index ) ? true : false;
                    else if ( i == index )
                        return true;
                }
            }
            return false;
        }
        protected bool CheckBishopLine( Point index )
        {
            Point i = Cell.Index;
            if ( index.X < i.X ) // left
            {
                if ( index.Y < i.Y ) // top
                {
                    while ( true )
                    {
                        --i.X;
                        --i.Y;
                        if ( i.X >= 0 && i.Y >= 0 )
                        {
                            if ( Cell.Desk[ i ].Chess != null )
                                return ( Cell.Desk[ i ].Chess.Color != Color && i == index ) ? true : false;
                            else if ( i == index )
                                return true;
                        }
                        else
                            break;
                    }
                }
                else if ( index.Y > i.Y ) // bottom
                {
                    while ( true )
                    {
                        --i.X;
                        ++i.Y;
                        if ( i.X >= 0 && i.Y < 8 )
                        {
                            if ( Cell.Desk[ i ].Chess != null )
                                return ( Cell.Desk[ i ].Chess.Color != Color && i == index ) ? true : false;
                            else if ( i == index )
                                return true;
                        }
                        else
                            break;
                    }
                }
            }
            else if ( index.X > i.X ) // right
            {
                if ( index.Y < i.Y ) // top
                {
                    while ( true )
                    {
                        ++i.X;
                        --i.Y;
                        if ( i.X < 8 && i.Y >= 0 )
                        {
                            if ( Cell.Desk[ i ].Chess != null )
                                return ( Cell.Desk[ i ].Chess.Color != Color && i == index ) ? true : false;
                            else if ( i == index )
                                return true;
                        }
                        else
                            break;
                    }
                }
                else if ( index.Y > i.Y ) // Bottom
                {
                    while ( true )
                    {
                        ++i.X;
                        ++i.Y;
                        if ( i.X < 8 && i.Y < 8 )
                        {
                            if ( Cell.Desk[ i ].Chess != null )
                                return ( Cell.Desk[ i ].Chess.Color != Color && i == index ) ? true : false;
                            else if ( i == index )
                                return true;
                        }
                        else
                            break;
                    }
                }
            }
            return false;
        }

        public bool CheckMayDie( Point index )
        {
            // TODO
            return true;
        }
        public bool CheckMayDieThis()
        {
            if ( Cell != null )
                return CheckMayDie( Cell.Index );
            return false;
        }

        #endregion

        #region Actions

        public abstract void Accept( IChessVisitor visitor );

        public void UpdatePosition()
        {
            if ( Cell != null )
                Sprite.Position = Cell.Position;
        }

        public void Draw( Graphics g )
        {
            g.Draw( Sprite );
        }

        protected bool firstMove;
        public void Move( Point index )
        {
            if ( Cell == null || !CanMove( index ) )
            {
                UpdatePosition();
                return;
            }

            Cell.Chess = null;
            Cell = Cell.Desk[ index ];
            if ( Cell.Desk[ index ].Chess != null )
                Cell.Desk[ index ].Chess.Kill();
            Cell.Desk[ index ].Chess = this;

            Sprite.Position = Cell.Position;
            firstMove = true;
        }

        #endregion
    }
}
