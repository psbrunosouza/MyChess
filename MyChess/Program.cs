using MainBoard;
using Pieces;
using System;
using Pieces.PieceColor;
using MainBoard.BoardExceptions;
using Interface;

namespace MyChess {
    class Program {
        static void Main(string[] args) {

            try {
                
                Board board = new Board(8, 8);
                board.InsertPiece(new ChessPiece(Colors.Black), new Position2D(1, 7));
                board.InsertPiece(new ChessPiece(Colors.Black), new Position2D(1, 4));
                board.InsertPiece(new ChessPiece(Colors.Black), new Position2D(1, 3));
                board.InsertPiece(new ChessPiece(Colors.Black), new Position2D(3, 7));
                board.InsertPiece(new ChessPiece(Colors.Black), new Position2D(4, 4));
                board.InsertPiece(new ChessPiece(Colors.Black), new Position2D(5, 6));

                Screen.DisplayBoard(board);

                int line = int.Parse(Console.ReadLine());
                int column = int.Parse(Console.ReadLine());

                Position2D from = new Position2D(line, column);

                line = int.Parse(Console.ReadLine());
                column = int.Parse(Console.ReadLine());

                Position2D to = new Position2D(line, column);

                board.MovePiece(from, to);

                Screen.DisplayBoard(board);

              
                
            }
            catch (BoardException e) {
                Console.WriteLine(e.Message);
            }

            
        }
    }
}
