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
                Screen.DisplayMatch(match);            

                try {
                    // Solicita ao jogador a posicao
                    // de origem e destino para mover a peca
                    Console.WriteLine();
                    Console.Write("    Origem -> ");
                    Position2D from = Screen.ChessMove();
                    // Realiza a validacao da posicao de origem
                    match.ValidateStartPosition(from);

                    // Exibe os movimentos possiveis de uma peca ao 
                    // selecionar a peca a ser movida
                    Console.Clear();
                    Screen.PossibleMovements(match.Board, match.Board.GetPiece(from).PieceMoves());

                    Console.WriteLine();
                    Console.Write("    Destino -> ");
                    Position2D to = Screen.ChessMove();
                    // Realiza a validacao da posicao de destino
                    match.ValidateEndPosition(from, to);

                    // realiza o movimento da peca
                    match.RealizeMove(from, to);
                }
                catch (BoardException e) {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }

            }

            //Ultima exibicao informando XEQUE-MATE e jogaor vencedor
            Console.Clear();
            Screen.DisplayMatch(match);
        }
    }
}
