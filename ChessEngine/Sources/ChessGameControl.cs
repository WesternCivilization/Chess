﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Chess
{
    public class ChessGameControl : Control
    {
        public ChessGame Game { get; set; }

        public ChessGameControl(ChessGame game)
        {
            Game = game;
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
                    new RectangleShape(Color.FromArgb(30, Color.White))
                ,   new RectangleShape(Color.FromArgb(30, Color.Black))
            );
        }
        

        public void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Draw(Game.Desk);
            select.DrawHover(e.Graphics);
            e.Graphics.Draw(ChessFactory.Instance);
            select.DrawSelect(e.Graphics);
            if (holdChess != null)
                e.Graphics.Draw(holdChess);
        }

        public void OnResize(object sender, EventArgs e)
        {
            Game.Desk.Rectangle = ClientRectangle;
            ChessFactory.Instance.ChessSize = Game.Desk.CellsSize;
            ChessFactory.Instance.UpdatePositions();
        }

        private Chess holdChess;
        private PointF holdDiff;

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            ChessDeskCell cell = Game.Desk.GetCellByMouse(e.Location);
            if (cell != null)
            {
                Chess chess = cell.Chess;
                if (chess != null)
                {
                    holdChess = chess;
                    holdDiff = new PointF(e.X - chess.Sprite.Position.X, e.Y - chess.Sprite.Position.Y);
                }
                this.Repaint();
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            ChessDeskCell cell = Game.Desk.GetCellByMouse(e.Location);
            if(cell != null)
            {
                select.HoveredCell = cell;
                select.DrawingHovered.Rectangle = cell.Rectangle;

                if (holdChess != null)
                    holdChess.Sprite.Position = new PointF(e.X - holdDiff.X, e.Y - holdDiff.Y);
                this.Repaint();
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (holdChess != null)
            {
                ChessDeskCell cell = Game.Desk.GetCellByMouse(e.Location);
                if (cell != null)
                {
                    Game.Move(holdChess, cell.Index);
                    holdChess = null;
                    this.Repaint();
                }
            }
        }
    }
}