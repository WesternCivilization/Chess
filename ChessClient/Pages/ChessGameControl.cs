using System;
using System.Windows.Forms;
using System.Drawing;
using Tools;

namespace Chess
{
    public class ChessGameControl : UserControl
    {
        public Game Game { get; private set; }

        public ChessGameControl( Skin skin )
        {
            Game = new Game( skin );
            DoubleBuffered = true;
            LoadSelect();

            Paint += OnPaint;
            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
            MouseUp += OnMouseUp;
            Resize += OnResize;
        }

        private ChessSelect select;
        private void LoadSelect()
        {
            select = new ChessSelect();
            select.DrawingHovered = new ChessSelect.CellDrawSelect(
                    new RectangleShape( Color.FromArgb( 50, Color.White ) )
                ,   new RectangleShape( Color.FromArgb( 50, Color.Black ) )
            );
        }
        
        public void OnPaint( object sender, PaintEventArgs e )
        {
            e.Graphics.Draw( Game.Desk );
            e.Graphics.Draw( Game.Factory.AllChess );
            e.Graphics.Draw( select );
            if ( holdChess != null )
                e.Graphics.Draw( holdChess );
        }

        public void OnResize( object sender, EventArgs e )
        {
            Game.Desk.Rectangle = ClientRectangle;
            Game.Factory.ChessSize = Game.Desk.CellsSize;
            Game.Factory.AllChess.UpdatePositions();
            this.Repaint();
        }

        private Chess holdChess;
        private PointF holdDiff;

        private void OnMouseDown( object sender, MouseEventArgs e )
        {
            if ( e.Button != MouseButtons.Left )
                return;
            DeskCell cell = Game.Desk.GetCellByMouse( e.Location );
            if ( cell != null )
            {
                Chess chess = cell.Chess;
                if ( chess != null )
                {
                    holdChess = chess;
                    holdDiff = new PointF( e.X - chess.Sprite.Position.X, e.Y - chess.Sprite.Position.Y );
                }
                this.Repaint();
            }
        }

        private void OnMouseMove( object sender, MouseEventArgs e )
        {
            DeskCell cell = Game.Desk.GetCellByMouse( e.Location );
            if ( cell != null )
            {
                select.HoveredCell = cell;
                select.DrawingHovered.Rectangle = cell.Rectangle;

                if ( holdChess != null )
                    holdChess.Sprite.Position = new PointF( e.X - holdDiff.X, e.Y - holdDiff.Y );
                this.Repaint();
            }
        }

        private void OnMouseUp( object sender, MouseEventArgs e )
        {
            if ( holdChess != null )
            {
                DeskCell cell = Game.Desk.GetCellByMouse( e.Location );
                if ( cell != null )
                {
                    Game.Move( holdChess, cell.Index );
                    holdChess = null;
                    this.Repaint();
                }
            }
        }
    }
}
