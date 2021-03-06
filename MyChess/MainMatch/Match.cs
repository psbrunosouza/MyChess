﻿using System;
using MainBoard;
using Pieces.PieceColor;
using Pieces;
using MainBoard.BoardExceptions;
using MyChess.MainBoard;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using MyChess.Pieces;
using Microsoft.VisualBasic;

namespace MainMatch {
    class Match {

        /*
         * Classe que controla todas acoes da partida, desde jogador atual, turnos, movimentos, etc.
         */
        public Colors CurrentPlayer { get; private set; }
        public int Turn { get; private set; }
        public Board Board { get; private set; }
        public bool MatchFinished { get; private set; }
        public bool Xeque { get; private set; }
        public ChessPiece IsEnPassant { get; private set; }
        private HashSet<ChessPiece> Pieces;
        private HashSet<ChessPiece> CapturedPieces;
        
        
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
            IsEnPassant = null;
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

            // #JOGADASESPECIAIS ->
            // ROQUE PEQUENO
            if (currentPiece is King && to.Column == from.Column + 2) {
                Position2D startPosRook = new Position2D(from.Line, from.Column + 3);
                Position2D endPosRook = new Position2D(from.Line, from.Column + 1);

                ChessPiece rook = Board.RemovePiece(startPosRook);
                rook.IncrementTotalMoves();
                Board.InsertPiece(rook, endPosRook);

            }

            // ROQUE GRANDE
            if (currentPiece is King && to.Column == from.Column - 2) {
                Position2D startPosRook = new Position2D(from.Line, from.Column - 4);
                Position2D endPosRook = new Position2D(from.Line, from.Column - 1);

                ChessPiece rook = Board.RemovePiece(startPosRook);
                rook.IncrementTotalMoves();
                Board.InsertPiece(rook, endPosRook);

            }

            // EN PASSANT
            if (currentPiece is Pawn) {
                if (from.Column != to.Column && capturedPiece == null) {
                    Position2D p;
                    if (currentPiece.Color == Colors.White) {
                        p = new Position2D(to.Line + 1, to.Column);
                    }
                    else {
                        p = new Position2D(to.Line - 1, to.Column);
                    }

                    capturedPiece = Board.RemovePiece(p);
                    CapturedPieces.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        /*
         *  @ReleaseMove -> Realiza a jogada de uma peca
         *  
         */

        public void RealizeMove(Position2D from, Position2D to) {
            ChessPiece capturedPiece = MovePiece(from, to);

            // #JOGADAS ESPECIAIS
            ChessPiece p = Board.GetPiece(to);

            //PROMOTE
            if (p is Pawn) {
                if ((p.Color == Colors.White && to.Line == 0) || (p.Color == Colors.Black && to.Line == 7)){
                    p = Board.RemovePiece(to);
                    Pieces.Remove(p);
                    ChessPiece queen = new Queen(p.Color, Board);
                    Board.InsertPiece(queen, to);
                    Pieces.Add(queen);
                }
            }

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

            // #JOGADAS ESPECIAIS -> 
            // En Passant

           
            if (p is Pawn && (to.Line == from.Line - 2 || to.Line == from.Line + 2)) {
                IsEnPassant = p;
            }
            else {
                IsEnPassant = null;
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

            // #JOGADASESPECIAIS ->
            // ROQUE PEQUENO
            if (CapturedPieces is King && to.Column == from.Column + 2) {
                Position2D startPosRook = new Position2D(from.Line, from.Column + 3);
                Position2D endPosRook = new Position2D(from.Line, from.Column + 1);

                ChessPiece rook = Board.RemovePiece(endPosRook);
                rook.DecrementTotalMoves();
                Board.InsertPiece(rook, startPosRook);

            }

            // ROQUE GRANDE
            if (CapturedPieces is King && to.Column == from.Column - 2) {
                Position2D startPosRook = new Position2D(from.Line, from.Column - 4);
                Position2D endPosRook = new Position2D(from.Line, from.Column - 1);

                ChessPiece rook = Board.RemovePiece(endPosRook);
                rook.DecrementTotalMoves();
                Board.InsertPiece(rook, startPosRook);

            }

            // EN PASSANT
            if (p is Pawn) {
                if (from.Column != to.Column && capturedPiece == IsEnPassant) {
                    ChessPiece pawn = Board.RemovePiece(to);
                    Position2D pPos;
                    if (p.Color == Colors.White) {
                        pPos = new Position2D(3, to.Column);
                    }
                    else {
                        pPos = new Position2D(4, to.Column);
                    }

                    Board.InsertPiece(pawn, pPos);
                }
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

            // INICIALIZA TODAS AS PECAS BRANCAS
            InsertNewPiece('a', 1, new Rook(Colors.White, Board));
            InsertNewPiece('b', 1, new Knight(Colors.White, Board));
            InsertNewPiece('c', 1, new Bishop(Colors.White, Board));
            InsertNewPiece('d', 1, new Queen(Colors.White, Board));
            InsertNewPiece('e', 1, new King(Colors.White, Board, this));
            InsertNewPiece('f', 1, new Bishop(Colors.White, Board));
            InsertNewPiece('g', 1, new Knight(Colors.White, Board));
            InsertNewPiece('h', 1, new Rook(Colors.White, Board));
            InsertNewPiece('a', 2, new Pawn(Colors.White, Board, this));
            InsertNewPiece('b', 2, new Pawn(Colors.White, Board, this));
            InsertNewPiece('c', 2, new Pawn(Colors.White, Board, this));
            InsertNewPiece('d', 2, new Pawn(Colors.White, Board, this));
            InsertNewPiece('e', 2, new Pawn(Colors.White, Board, this));
            InsertNewPiece('f', 2, new Pawn(Colors.White, Board, this));
            InsertNewPiece('g', 2, new Pawn(Colors.White, Board, this));
            InsertNewPiece('h', 2, new Pawn(Colors.White, Board, this));

            // INICIALIZA TODAS AS PECAS PRETAS
            InsertNewPiece('a', 8, new Rook(Colors.Black, Board));
            InsertNewPiece('b', 8, new Knight(Colors.Black, Board));
            InsertNewPiece('c', 8, new Bishop(Colors.Black, Board));
            InsertNewPiece('d', 8, new Queen(Colors.Black, Board));
            InsertNewPiece('e', 8, new King(Colors.Black, Board, this));
            InsertNewPiece('f', 8, new Bishop(Colors.Black, Board));
            InsertNewPiece('g', 8, new Knight(Colors.Black, Board));
            InsertNewPiece('h', 8, new Rook(Colors.Black, Board));
            InsertNewPiece('a', 7, new Pawn(Colors.Black, Board, this));
            InsertNewPiece('b', 7, new Pawn(Colors.Black, Board, this));
            InsertNewPiece('c', 7, new Pawn(Colors.Black, Board, this));
            InsertNewPiece('d', 7, new Pawn(Colors.Black, Board, this));
            InsertNewPiece('e', 7, new Pawn(Colors.Black, Board, this));
            InsertNewPiece('f', 7, new Pawn(Colors.Black, Board, this));
            InsertNewPiece('g', 7, new Pawn(Colors.Black, Board, this));
            InsertNewPiece('h', 7, new Pawn(Colors.Black, Board, this));        
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
