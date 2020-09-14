using System;
using System.Collections.Generic;
using System.Text;
using Pieces.PieceColor;
using MainBoard;
using MainMatch;

namespace Pieces {
    class King : ChessPiece{

        /*
         *  Classe rei
         */

        private Match match;

        public King(Colors color, Board board, Match match) : base(color, board) {
            this.match = match;
        }

        /*
        *  @PieceMoves -> Movimentos possiveis da peca
        */

        private bool IsCastling(Position2D pos) {
            ChessPiece p = Board.GetPiece(pos);
            return p != null && p is Rook && p.Color == Color && Moves == 0;
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

            // #JOGADASESPECIAIS ->

            if (Moves == 0 && !match.Xeque) {

                //ROQUE PEQUENO
                Position2D posRook1 = new Position2D(Position.Line, Position.Column + 3);
                if (IsCastling(posRook1)) {

                    Position2D pos1 = new Position2D(Position.Line, Position.Column + 1);
                    Position2D pos2 = new Position2D(Position.Line, Position.Column + 2);

                    if (Board.GetPiece(pos1) == null && Board.GetPiece(pos2) == null) {
                        mat[Position.Line, Position.Column + 2] = true;
                    }
                }

                //ROQUE GRANDE
                Position2D posRook2 = new Position2D(Position.Line, Position.Column - 4);
                if (IsCastling(posRook2)) {

                    Position2D pos1 = new Position2D(Position.Line, Position.Column - 1);
                    Position2D pos2 = new Position2D(Position.Line, Position.Column - 2);
                    Position2D pos3 = new Position2D(Position.Line, Position.Column - 3);

                    if (Board.GetPiece(pos1) == null 
                        && Board.GetPiece(pos2) == null 
                        && Board.GetPiece(pos3) == null) {
                        mat[Position.Line, Position.Column - 2] = true;
                    }
                }
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
