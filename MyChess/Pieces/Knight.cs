using MainBoard;
using Pieces.PieceColor;
using System;

namespace Pieces {
    class Knight : ChessPiece{

        public Knight(Colors color, Board board) : base(color, board) {}

        public override bool[,] PieceMoves() {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position2D pos = new Position2D(0, 0);

            //CIMA ESQUERDA
            pos.GetPosition(Position.Line - 2, Position.Column - 1);
            if (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //CIMA DIREITA
            pos.GetPosition(Position.Line - 2, Position.Column + 1);
            if (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //ESQUERDA CIMA
            pos.GetPosition(Position.Line - 1, Position.Column - 2);
            if (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //DIREITA CIMA
            pos.GetPosition(Position.Line + 1, Position.Column - 2);
            if (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //BAIXO ESQUERDA
            pos.GetPosition(Position.Line + 2, Position.Column - 1);
            if (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //BAIXO DIREITA
            pos.GetPosition(Position.Line + 2, Position.Column + 1);
            if (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //DIREITA CIMA
            pos.GetPosition(Position.Line - 1, Position.Column + 2);
            if (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //DIREITA BAIXO
            pos.GetPosition(Position.Line + 1, Position.Column + 2);
            if (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            return mat;
        }

        public override string ToString() {
            return "N";
        }
    }
}
