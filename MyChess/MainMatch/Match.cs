using System;
using MainBoard;
using Pieces.PieceColor;
using Pieces;
using MainBoard;
using MainBoard.BoardExceptions;

namespace MainMatch {
    class Match {

        /*
         * Classe que controla todas acoes da partida, desde jogador atual, turnos, movimentos, etc.
         */
        public Colors CurrentPlayer { get; private set; }
        public int Turn { get; private set; }
        public Board Board { get; private set; }


        /*
         * Construtor que inica a partida com estrutura base de uma partida de xadrez,
         *  tabuleiro (8,8), turno 0 e jogador atual
         */

        public Match() {
            CurrentPlayer = Colors.White;
            Turn = 0;
            Board = new Board(8, 8);
            InsertPiece();
        }

        /*
         *  @MovePiece -> Move uma peca de posicao de uma posicao de origem para 
         *  uma posicao destino no tabuleiro
         */

        public void MovePiece(Position2D from, Position2D to) {

            ChessPiece pieceRemoved = Board.RemovePiece(from);

            if (Board.ValidatePosition(to)) {
                Board.InsertPiece(pieceRemoved, to);
            }
            else {
                throw new BoardException("Ja existe uma peça na posição de destino informada!");
            }

        }

        /*
         *  @InsertPiece -> Insere uma nova peca no tabuleiro
         */

        public void InsertPiece() {
            Board.InsertPiece(new ChessPiece(Colors.White), new Position2D(0, 0));
        }
    }
}
