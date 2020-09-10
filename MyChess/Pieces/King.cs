using System;
using System.Collections.Generic;
using System.Text;
using Pieces.PieceColor;
using MainBoard;

namespace Pieces {
    class King : ChessPiece{

        /*
         *  Classe rei
         */

        public King(Colors color, Board board) : base(color, board) {

        }

        /*
        *  @PieceMoves -> Movimentos possiveis da peca
        */

        public override bool[,] PieceMoves() {

            bool[,] mat = new bool[Board.Lines, Board.Columns]; 
            Position2D pos = new Position2D(0, 0);

            //cima
            pos.GetPosition(Position.Line - 1, Position.Column);
            if (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //ne
            pos.GetPosition(Position.Line - 1, Position.Column + 1);
            if (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //direita
            pos.GetPosition(Position.Line, Position.Column + 1);
            if (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //se
            pos.GetPosition(Position.Line + 1, Position.Column + 1);
            if (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //abaixo
            pos.GetPosition(Position.Line + 1, Position.Column);
            if (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //so
            pos.GetPosition(Position.Line + 1, Position.Column - 1);
            if (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //esquerda
            pos.GetPosition(Position.Line, Position.Column - 1);
            if (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            //no
            pos.GetPosition(Position.Line - 1, Position.Column - 1);
            if (Board.ValidatePosition(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            return mat;
        }

        /*
        *  Retorna o tipo da peca de xadrez
        */

        public override string ToString() {
            return "K";
        }
    }
}
