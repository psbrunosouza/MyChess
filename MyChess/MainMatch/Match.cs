using System;
using MainBoard;
using Pieces.PieceColor;
using Pieces;
using MainBoard.BoardExceptions;
using MyChess.MainBoard;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace MainMatch {
    class Match {

        /*
         * Classe que controla todas acoes da partida, desde jogador atual, turnos, movimentos, etc.
         */
        public Colors CurrentPlayer { get; private set; }
        public int Turn { get; private set; }
        public Board Board { get; private set; }
        public bool MatchFinished { get; private set; }
        private HashSet<ChessPiece> Pieces;
        private HashSet<ChessPiece> CapturedPieces;
        public bool Xeque { get; private set; }


        /*
         * Construtor que inica a partida com estrutura base de uma partida de xadrez,
         *  tabuleiro (8,8), turno 0 e jogador atual
         */

        public Match() {
            CurrentPlayer = Colors.White;
            Turn = 1;
            MatchFinished = false;
            Board = new Board(8, 8);
            Pieces = new HashSet<ChessPiece>();
            CapturedPieces = new HashSet<ChessPiece>();
            Xeque = false;
            InitializePieces();
        }

        /*
         *  @MovePiece -> Move uma peca de posicao de uma posicao de origem para 
         *  uma posicao destino no tabuleiro
         */

        public ChessPiece MovePiece(Position2D from, Position2D to) {

            ChessPiece currentPiece = Board.RemovePiece(from);
            currentPiece.IncrementTotalMoves();
            ChessPiece capturedPiece = Board.RemovePiece(to);    
            Board.InsertPiece(currentPiece, to);

            if(capturedPiece != null) {
                CapturedPieces.Add(capturedPiece);
            }

            return capturedPiece;
        }

        /*
         *  @ReleaseMove -> Realiza a jogada de uma peca
         *  
         */

        public void RealizeMove(Position2D from, Position2D to) {
            ChessPiece capturedPiece = MovePiece(from, to);
            
            if (IsXeque(CurrentPlayer)) {
                UndoMove(from, to, capturedPiece);
                throw new BoardException("Você não pode se colocar em XEQUE!");
            }

            if (IsXeque(Enemy(CurrentPlayer))){
                Xeque = true;
            }else {
                Xeque = false;
            }

            if (IsXequeMate(Enemy(CurrentPlayer))) {
                MatchFinished = true;
            }else {
                Turn++;
                ChangePlayer(CurrentPlayer);
            }        
        }

        /*
        *  @UndoMove -> Desfaz a jogada realizada por uma peca
        *  
        */

        public void UndoMove(Position2D from, Position2D to, ChessPiece capturedPiece) {
            ChessPiece p = Board.RemovePiece(to);
            p.DecrementTotalMoves();
            if (capturedPiece != null) {
                Board.InsertPiece(capturedPiece, to);
                CapturedPieces.Remove(capturedPiece);
            }

            Board.InsertPiece(p, from);
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
         * @GetCapturedPieces -> Classe que retorna todas as pecas capturadas atraves de uma cor
         */

        public HashSet<ChessPiece> GetCapturedPieces(Colors color) {
            HashSet<ChessPiece> aux = new HashSet<ChessPiece>();
            
            foreach (ChessPiece x in CapturedPieces) {
                if (x.Color == color) {
                    aux.Add(x);
                }
            }
            return aux;
        }

        /*
         * @GetAllPieces -> Classe que retorna todas as pecas em jogo, exceto as pecas capturadas
         */

        public HashSet<ChessPiece> GetAllPieces(Colors color) {
            HashSet<ChessPiece> aux = new HashSet<ChessPiece>();

            foreach (ChessPiece x in Pieces) {
                if (x.Color == color) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(GetCapturedPieces(color));
            return aux;        
        }


        /*
         *  @InsertNewPiece -> Funcao de suporte para facilitar a insercao de novas pecas no tabuleiro
         *   a partir da partida atual
         */

        public void InsertNewPiece(char column, int line, ChessPiece piece) {
            Board.InsertPiece(piece, new ChessPosition(column, line).ToChessPosition());
            Pieces.Add(piece);
        }


        /*
         *  @InsertPieces -> Insere as pecas no tabuleiro para o inicio da partida
         */

        public void InitializePieces() {
            InsertNewPiece('h', 7, new Tower(Colors.White, Board));
            InsertNewPiece('d', 1, new King(Colors.White, Board));
            InsertNewPiece('c', 1, new Tower(Colors.White, Board));
            InsertNewPiece('b', 8, new Tower(Colors.Black, Board));
            InsertNewPiece('a', 8, new King(Colors.Black, Board));
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
            if (!Board.GetPiece(from).PossibleMovements(to)) {
                throw new BoardException("A posição de destino selecionada não é valida!");
            }
        }


        /*
         *  @Enemy -> Funcao para encontrar o adversario de uma dada peca
         */

        private Colors Enemy(Colors color) {
            if(color == Colors.White) {
                return Colors.Black;
            }
            else{
                return Colors.White;
            }
        }

        /*
         *  @King -> Funcao usada para identificar o rei de uma dada cor
         */

        private ChessPiece King(Colors color) {
            foreach (ChessPiece x in GetAllPieces(color)) {
                if (x is King) {
                    return x;
                }
            }

            return null;
        }

        /*
         *  @IsXeque -> Funcao usada para testar se um dado rei de uma cor especifica
         *  esta em xeque
         */

        private bool IsXeque(Colors color) {
            ChessPiece king = King(color);
            if (king == null) {
                throw new BoardException("Nao existe a peca KING no tabuleiro!");
            }

            foreach (ChessPiece x in GetAllPieces(Enemy(color))) {
                bool[,] mat = x.PieceMoves();
                if (mat[king.Position.Line, king.Position.Column]) {
                    return true;
                }
            }

            return false;
        }

        /*
         *  @IsXequeMate -> Funcao usada para verificar se um rei sofreu xequemate
         */

        public bool IsXequeMate(Colors color) {
            if (!IsXeque(color)) {
                return false;
            }

            foreach (ChessPiece x in GetAllPieces(color)) {
                bool[,] mat = x.PieceMoves();
                for (int i = 0; i < Board.Lines; i++) {
                    for (int j = 0; j < Board.Columns; j++) {
                        if (mat[i,j]) {
                            Position2D from = x.Position;
                            Position2D to = new Position2D(i, j);
                            ChessPiece capturedPiece = MovePiece(from, to);
                            bool isXeque = IsXeque(color);
                            UndoMove(from, to, capturedPiece);
                            if (!isXeque) {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
    }
}
