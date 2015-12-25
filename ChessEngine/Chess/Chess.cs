using System.Drawing;

namespace Chess
{
    public abstract class Chess : IDrawable
    {
        public DeskCell Cell { get; set; }
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
            if ( Cell == null || index == Cell.Index || Desk.IsOutOfRange( index ) )
                return false;

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
        
        public bool CheckMayDie( int x, int y )
        {
            return CheckMayDie( new Point( x, y ) );
        }

        public bool CheckMayDie( Point index )
        {
            foreach ( Chess chess in Factory.ActiveChess )
            {
                if ( chess != this && chess.Color != Color )
                {
                    if ( !Desk.IsOutOfRange( index ) )
                    {
                        Chess chessOnCell = Cell.Desk[ index ].Chess;
                        if ( chess.CanMove( index ) )
                            return true;
                    }
                }
            }
            return false;
        }

        #endregion

        #region Actions

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
        public bool Move( Point index )
        {
            if ( Cell == null || !CanMove( index ) )
            {
                UpdatePosition();
                return false;
            }

            Cell.Chess = null;
            Cell = Cell.Desk[ index ];
            if ( Cell.Desk[ index ].Chess != null )
                Cell.Desk[ index ].Chess.Kill();
            Cell.Desk[ index ].Chess = this;

            Sprite.Position = Cell.Position;
            firstMove = true;

            return true;
        }

        #endregion
    }
}
