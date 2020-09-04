using MainBoard;
using System;
using MainBoard.BoardExceptions;
using Interface;
using MainMatch;

namespace MyChess {
    class Program {
        static void Main(string[] args) {

            try {

                // Inicializa uma partida
                Match match = new Match();

                while (match.MatchFinished != true) {

                    // Exibe o tabuleiro
                    Console.Clear();
                    Screen.DisplayBoard(match.Board);

                    // Solicita ao jogador a posicao
                    // de origem e destino para mover a peca
                    Console.Write("Origem: ");
                    Position2D from = Screen.ChessMove();
                    Console.Write("Destino: ");
                    Position2D to = Screen.ChessMove();

                    //realiza o movimento da peca
                    match.MovePiece(from, to);
                }             
            }catch (BoardException e) {
                Console.WriteLine(e.Message);
            }           
        }
    }
}
