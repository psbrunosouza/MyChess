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
            Turn = 1;
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
         *  @ReleaseMove -> Realiza a jogada de uma peca
         *  
         */

        public void RealizeMove(Position2D from, Position2D to) {
            MovePiece(from, to);
            Turn++;
            ChangePlayer(CurrentPlayer);
        }

        /*
         * @ChangePlayer -> Altera o jogador atual quando passar o turno
         */

        private void ChangePlayer(Colors player) {
            if (player == Colors.White) {
                CurrentPlayer = Colors.Black;
            }
            else {
                CurrentPlayer = Colors.White;
            }
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
            InsertPiece(new King(Colors.White, Board), new ChessPosition('d', 1));
            InsertPiece(new Tower(Colors.White, Board), new ChessPosition('c', 1));
            InsertPiece(new Tower(Colors.White, Board), new ChessPosition('e', 1));
            InsertPiece(new Tower(Colors.White, Board), new ChessPosition('c', 2));
            InsertPiece(new Tower(Colors.White, Board), new ChessPosition('e', 2));
            InsertPiece(new Tower(Colors.White, Board), new ChessPosition('d', 2));


            InsertPiece(new King(Colors.Black, Board), new ChessPosition('d', 8));
            InsertPiece(new Tower(Colors.Black, Board), new ChessPosition('c', 8));
            InsertPiece(new Tower(Colors.Black, Board), new ChessPosition('e', 8));
            InsertPiece(new Tower(Colors.Black, Board), new ChessPosition('c', 7));
            InsertPiece(new Tower(Colors.Black, Board), new ChessPosition('e', 7));
            InsertPiece(new Tower(Colors.Black, Board), new ChessPosition('d', 7));
        }

        /*
         *  @ValidateStartPosition -> Valida a posicao da peca de acordo com o valor de origem 
         */

        public void ValidateStartPosition(Position2D pos) {
            if (Board.GetPiece(pos) == null) {
                throw new BoardException("Não existe peça na posição de origem selecionada!");
            }

            if (CurrentPlayer != Board.GetPiece(pos).Color) {
                throw new BoardException("A peça de origem escolhida não é sua!");
            }

            if (!Board.GetPiece(pos).ExistMoviments()) {
                throw new BoardException("Não existem moviemntos possíveis para a peça escolhida!");
            }
        }

        /*
         *  @ValidateEndPosition -> Valida a posicao da peca de acordo com a origem e destino
         */

        public void ValidateEndPosition(Position2D from, Position2D to) {
            if (!Board.GetPiece(from).CanMoveTo(to)) {
                throw new BoardException("A posição de destino selecionada não é valida!");
            }
        }
    }
}
