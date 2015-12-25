using System.Drawing;

namespace Chess
{
    public class Desk : IDrawable
    {
        public static bool IsOutOfRange( int x, int y )
        {
            return x < 0 || y < 0 || x >= 8 || y >= 8;
        }
        public static bool IsOutOfRange( Point p )
        {
            return IsOutOfRange( p.X, p.Y );
        }

        public RectangleF Rectangle { get; set; }

        private DeskCell[ , ] cells = new DeskCell[ 8, 8 ];
        public DeskCell this[ Point p ]
        {
            get { return this[ p.X, p.Y ]; }
            set { cells[ p.X, p.Y ] = value; }
        }

        public DeskCell this[ int x, int y ]
        {
            get
            {
                if ( !IsOutOfRange( x, y ) )
                    return cells[ x, y ];
                return null;
            }
            set { cells[ x, y ] = value; }
        }

        public DeskCell GetCellByMouse( Point mouseCoord )
        {
            if ( mouseCoord.X != 0 )
                mouseCoord.X /= ( int ) CellsSize.Width;
            if ( mouseCoord.Y != 0 )
                mouseCoord.Y /= ( int ) CellsSize.Height;
            return this[ mouseCoord ];
        }

        public SizeF CellsSize { get { return cells[ 0, 0 ].Size; } }

        public Desk()
        {
            LoadCells();
        }

        public void Clean()
        {
            for ( int i = 0; i < 8; ++i )
                for ( int j = 0; j < 8; ++j )
                {
                    if ( cells[ i, j ].Chess != null )
                    {
                        cells[ i, j ].Chess.Dispose();
                        cells[ i, j ].Chess = null;
                    }
                }
        }
        
        private void LoadCells()
        {
            GameColor currentType;

            for ( int j = 0; j < 8; ++j )
            {
                currentType = ( j % 2 == 0 )
                    ?   GameColor.Black
                    :   GameColor.White;

                for ( int i = 0; i < 8; ++i )
                {
                    cells[ i, j ] = new DeskCell( this, currentType, new Point( i, j ) );

                    currentType = ( currentType == GameColor.Black )
                        ?   GameColor.White
                        :   GameColor.Black;
                }
            }
        }

        public void Draw( Graphics g )
        {
            foreach ( DeskCell cell in cells )
                cell.Draw( g );
        }
    }
}
