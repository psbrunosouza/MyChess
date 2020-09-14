using MainBoard;
using MainMatch;
using Pieces;
using Pieces.PieceColor;
using System;


namespace MyChess.Pieces {
    class Pawn : ChessPiece {

        private Match match;

        /*
         * Pawn -> Classe Peao
         */

        public Pawn(Colors color, Board board, Match match) : base(color, board){
            this.match = match;
        }

        /*
         * @PieceMoves -> Movimentos possiveis da peca 
         */
        public override bool[,] PieceMoves() {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position2D pos = new Position2D(0,0);

            if (Color == Colors.White) {
                pos.GetPosition(Position.Line - 1, Position.Column);
                if (Board.ValidatePosition(pos) && IsFree(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.GetPosition(Position.Line - 2, Position.Column);
                if (Board.ValidatePosition(pos) && IsFree(pos) && Moves == 0) {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.GetPosition(Position.Line - 1, Position.Column - 1);
                if (Board.ValidatePosition(pos) && HasEnemy(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.GetPosition(Position.Line - 1, Position.Column + 1);
                if (Board.ValidatePosition(pos) && HasEnemy(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }

                // #JOGADAS ESPECIAIS ->
                // En Passant

                if (Position.Line == 3) {
                    
                    Position2D left = new Position2D(Position.Line, Position.Column - 1);
                    if (Board.ValidatePosition(left) && HasEnemy(left) && Board.GetPiece(left) == match.IsEnPassant) {
                        mat[left.Line - 1, left.Column] = true;
                    }

                    Position2D right = new Position2D(Position.Line, Position.Column + 1);
                    if (Board.ValidatePosition(right) && HasEnemy(right) && Board.GetPiece(right) == match.IsEnPassant) {
                        mat[right.Line - 1, right.Column] = true;
                    }
                }
            }
            else {
                pos.GetPosition(Position.Line + 1, Position.Column);
                if (Board.ValidatePosition(pos) && IsFree(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.GetPosition(Position.Line + 2, Position.Column);
                if (Board.ValidatePosition(pos) && IsFree(pos) && Moves == 0) {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.GetPosition(Position.Line + 1, Position.Column - 1);
                if (Board.ValidatePosition(pos) && HasEnemy(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.GetPosition(Position.Line + 1, Position.Column + 1);
                if (Board.ValidatePosition(pos) && HasEnemy(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }

                // #JOGADAS ESPECIAIS ->
                // En Passant

                if (Position.Line == 4) {

                    Position2D left = new Position2D(Position.Line, Position.Column - 1);
                    if (Board.ValidatePosition(left) && HasEnemy(left) && Board.GetPiece(left) == match.IsEnPassant) {
                        mat[left.Line + 1, left.Column] = true;
                    }

                    Position2D right = new Position2D(Position.Line, Position.Column + 1);
                    if (Board.ValidatePosition(right) && HasEnemy(right) && Board.GetPiece(right) == match.IsEnPassant) {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }
            }
            

            return mat;
        }

        /*
         * @HasEnemy -> Verifica se existem inimigos na posicao diagonal para executar a captura
         */

        private bool HasEnemy(Position2D pos) {
            ChessPiece p = Board.GetPiece(pos);
            return p != null && p.Color != Color;
        }

        /*
         * @IsFree -> Verifica se a casa na qual realizara o movimento esta vazia
         */

        private bool IsFree(Position2D pos) {
            return Board.GetPiece(pos) == null;
        }

        /*
         * Retorna o tipo da peca 
         */

        public override string ToString() {
            return "P";
        }
    }
}
