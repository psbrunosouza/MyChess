using Pieces;
using System;
using MainBoard.BoardExceptions;

namespace MainBoard {
    class Board {
        
        /*
         *  Classe Board, contem a estrutura do tabuleiro em linhas e colunas e o conjunto de pecas
         *  e suas respectivas posicoes no tabuleiro
         */

        public int Lines { get; set; }
        public int Columns { get; set; }
        public ChessPiece[,] Pieces { get; set; }

        public Board() {
        }
        public Board(int lines, int columns) {
            Lines = lines;
            Columns = columns;
            Pieces = new ChessPiece[lines, columns];
        }

        /*
        *  @InsertPiece -> Inserir uma peca na posicao especificada
        */

        public void InsertPiece(ChessPiece piece, Position2D pos) {
            
            Pieces[pos.Line, pos.Column] = piece;
            piece.Position = pos;
        }

        /*
         *  @RemovePiece -> Remover uma peca indicando uma posicao especifica
         */

        public ChessPiece RemovePiece(Position2D pos) {
            ChessPiece PieceToRemove = FindPiece(pos);
            Pieces[pos.Line, pos.Column] = null;
            return PieceToRemove;
        }

        /*
         *  @FindPiece -> Encontrar uma peca na posicao especificada
         */

        private ChessPiece FindPiece(Position2D pos) {

            if (Pieces[pos.Line, pos.Column] == null) {
                throw new BoardException("Não foi localizado peça na posição informada");
            }

            ChessPiece PieceToFind = Pieces[pos.Line, pos.Column];
            return PieceToFind;
        }


        /*
         *  Retorna a estrutura do tabuleiro em linhas e colunas (x, y)
         */
        public override string ToString() {
            return "(" + Lines + ", " + Columns + ")";
        }

    }
}
