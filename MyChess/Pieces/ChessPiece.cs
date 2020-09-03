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

        /*
         *  @ChessPiece -> Construtor da classe, constroi uma peca, com parametro cor
         */

        public ChessPiece(Colors color) {
            Color = color;
            Position = null;
            Moves = 0;
        }

        /*
         *  Retorna a sigla de peca generica "G", utilizado para fins de teste
         */

        public override string ToString() {
            return "G";
        }
    }
}
