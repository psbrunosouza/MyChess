using MainBoard;
using Pieces.PieceColor;
using System;

namespace Pieces {
    class Bishop : ChessPiece {
        
        public Bishop(Colors color, Board board) : base(color, board){

        }

        public override bool[,] PieceMoves() {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position2D pos = new Position2D(0, 0);

            //NE
            pos.GetPosition(Position.Line - 1, Position.Column - 1);
            while (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) {
                    break;
                }

                pos.Line -= 1;
                pos.Column -= 1;
            }

            //NO
            pos.GetPosition(Position.Line - 1, Position.Column + 1);
            while (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) {
                    break;
                }

                pos.Line -= 1;
                pos.Column += 1;
            }

            //SE
            pos.GetPosition(Position.Line + 1, Position.Column - 1);
            while (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) {
                    break;
                }

                pos.Line += 1;
                pos.Column -= 1;
            }

            //SO
            pos.GetPosition(Position.Line + 1, Position.Column + 1);
            while (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) {
                    break;
                }

                pos.Line += 1;
                pos.Column += 1;
            }

            return mat;
        }

        public override string ToString() {
            return "B";
        }
    }
}
