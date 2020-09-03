using Pieces.PieceColor;
using MainBoard;

namespace Pieces {
    class ChessPiece {

        /*
         *  ChessPiece e a classe generica que define todas as pecas no tabuleiro
         */

        public Colors Color { get; set; }
        public int Moves { get; set; }
        public Position2D Position { get; set; }

        public ChessPiece(Colors color) {
            Color = color;
            Position = null;
            Moves = 0;
        }
    }
}
