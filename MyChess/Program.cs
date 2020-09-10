using MainBoard;
using System;
using MainBoard.BoardExceptions;
using Interface;
using MainMatch;

namespace MyChess {
    class Program {
        static void Main(string[] args) {


            // Inicializa uma partida
            Match match = new Match();

            while (match.MatchFinished != true) {

                // Exibe o tabuleiro
                Console.Clear();
                Screen.DisplayBoard(match.Board);

                try {
                    // Solicita ao jogador a posicao
                    // de origem e destino para mover a peca
                    Console.Write("Origem: ");
                    Position2D from = Screen.ChessMove();

                    Console.Clear();
                    Screen.PossibleMovements(match.Board, match.Board.GetPiece(from).PieceMoves());

                    Console.Write("Destino: ");
                    Position2D to = Screen.ChessMove();
               
                    //realiza o movimento da peca
                    match.MovePiece(from, to);
                }
                catch (BoardException e) {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
        }
    }
}
