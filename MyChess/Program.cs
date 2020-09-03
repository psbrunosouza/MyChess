using MainBoard;
using Pieces;
using System;
using Pieces.PieceColor;
using MainBoard.BoardExceptions;

namespace MyChess {
    class Program {
        static void Main(string[] args) {

            try {
                Board board = new Board(8, 8);

                board.InsertPiece(new ChessPiece(Colors.Black), new Position2D(1, 7));
                board.RemovePiece(new Position2D(1, 7));



            }
            catch (BoardException e) {
                Console.WriteLine(e.Message);
            }

            
        }
    }
}
