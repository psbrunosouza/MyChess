using System;
using MainBoard;
using Pieces.PieceColor;

namespace Pieces {
    class Tower : ChessPiece {

        /*
         *  Classe TORRE
         */
        public Tower(Colors color, Board board) : base(color, board) { }

        /*
         *  @PieceMoves -> Movimentos possiveis da peca
         */
        public override bool[,] PieceMoves() {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position2D pos = new Position2D(0, 0);

            //TOP
            pos.GetPosition(Position.Line - 1, Position.Column);
            while (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) {
                    break;
                }

                pos.Line -= 1;
            }

            //DOWN
            pos.GetPosition(Position.Line + 1, Position.Column);
            while (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) {
                    break;
                }

                pos.Line += 1;
            }

            //RIGHT
            pos.GetPosition(Position.Line, Position.Column + 1);
            while (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) {
                    break;
                }

                pos.Column += 1;
            }

            //LEFT
            pos.GetPosition(Position.Line, Position.Column - 1);
            while (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) {
                    break;
                }

                pos.Column -= 1;
            }


            return mat;
        }

        /*
         *  Retorna o tipo da peca de xadrez
         */

        public override string ToString() {
            return "T";
        }
    }
}
