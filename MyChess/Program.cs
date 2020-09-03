using MainBoard;
using Pieces;
using System;
using Pieces.PieceColor;
using MainBoard.BoardExceptions;
using Interface;
using MainMatch;

namespace MyChess {
    class Program {
        static void Main(string[] args) {

            try {

                Match match = new Match();

                Screen.DisplayBoard(match.Board);

                int line = int.Parse(Console.ReadLine());
                int column = int.Parse(Console.ReadLine());

                Position2D from = new Position2D(line, column);

                line = int.Parse(Console.ReadLine());
                column = int.Parse(Console.ReadLine());

                Position2D to = new Position2D(line, column);

                match.MovePiece(from, to);

                Screen.DisplayBoard(match.Board);

              
                
            }
            catch (BoardException e) {
                Console.WriteLine(e.Message);
            }

            
        }
    }
}
