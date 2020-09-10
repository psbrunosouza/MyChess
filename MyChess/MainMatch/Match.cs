using System;
using MainBoard;
using Pieces.PieceColor;
using Pieces;
using MainBoard.BoardExceptions;
using MyChess.MainBoard;

namespace MainMatch {
    class Match {

        /*
         * Classe que controla todas acoes da partida, desde jogador atual, turnos, movimentos, etc.
         */
        public Colors CurrentPlayer { get; private set; }
        public int Turn { get; private set; }
        public Board Board { get; private set; }
        public bool MatchFinished { get; private set; }


        /*
         * Construtor que inica a partida com estrutura base de uma partida de xadrez,
         *  tabuleiro (8,8), turno 0 e jogador atual
         */

        public Match() {
            CurrentPlayer = Colors.White;
            Turn = 0;
            MatchFinished = false;
            Board = new Board(8, 8);
            InitializePieces();
        }

        /*
         *  @MovePiece -> Move uma peca de posicao de uma posicao de origem para 
         *  uma posicao destino no tabuleiro
         */

        public void MovePiece(Position2D from, Position2D to) {

            ChessPiece CurrentPiece = Board.RemovePiece(from);
            ChessPiece CapturedPiece = Board.RemovePiece(to);
            CurrentPiece.IncrementTotalMoves();
            Board.InsertPiece(CurrentPiece, to);
        }

        /*
         *  @InsertNewPiece -> Funcao de suporte para facilitar a insercao de novas pecas no tabuleiro
         *   a partir da partida atual
         */

        public void InsertPiece(ChessPiece piece, ChessPosition pos) {
            Board.InsertPiece(piece, pos.ToChessPosition());
        }

        /*
         *  @InsertPieces -> Insere as pecas no tabuleiro para o inicio da partida
         */

        public void InitializePieces() {
            InsertPiece(new King(Colors.White, Board), new ChessPosition('c', 1));
            InsertPiece(new King(Colors.Black, Board), new ChessPosition('e', 4));
        }
    }
}
