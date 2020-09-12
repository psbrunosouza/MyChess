using Pieces.PieceColor;
using MainBoard;
using Microsoft.VisualBasic;
using MainBoard.BoardExceptions;

namespace Pieces {
    abstract class ChessPiece {

        /*
         *  ChessPiece e a classe generica que define todas as pecas no tabuleiro
         */

        public Colors Color { get; set; }
        public int Moves { get; private set; }
        public Position2D Position { get; set; }
        public Board Board { get; set; }

        /*
         *  @ChessPiece -> Construtor da classe, constroi uma peca, com parametro cor
         */

        public ChessPiece(Colors color, Board board) {
            Color = color;
            Position = null;
            Moves = 0;
            Board = board;
        }

        /*
         *  @IncrementTotalMoves -> Incrementa a quantidade de movimentos que a peca ja executou
         */

        public void IncrementTotalMoves() {
            Moves++;
        }

        /*
         *  @DecrementTotalMoves -> Decrementa a quantidade total de movimentos que a peca realizou
         */

        public void DecrementTotalMoves() {
            Moves--;
        }

        /*
         *  @CanMove -> Verifica se a peca pode realizar o movimento
         */

        public bool CanMove(Position2D pos) {
            ChessPiece p = Board.GetPiece(pos);
            return p == null || p.Color != this.Color;
        }

        /*
         *  @ExistMovements -> Verifica se existe pelo menos um movimento possivel para uma peca
         */

        public bool ExistMoviments() {

            bool[,] mat = PieceMoves();

            for (int i = 0; i < Board.Lines; i++) {
                for (int j = 0; j < Board.Columns; j++) {
                    if (mat[i, j]) {
                        return true;
                    }
                }
            }

            return false;
        }

        /*
         *  @CanMoveTo -> Verifica se a peca pode realizar o movimento levando 
         *  em conta a posicao de destino dela
         */

        public bool PossibleMovements(Position2D pos) {
            return PieceMoves()[pos.Line, pos.Column];
        }

        /*
         *  @PieceMoves -> Movimentos possiveis da peca
         */

        abstract public bool[,] PieceMoves();

        
    }
}
