using MainBoard;
using Pieces;
using System;

namespace Interface {
    class Screen {

        /*
         *  Classe Screen, define toda a interacao do usuario com o sistema e a
         *  exibicao de informacoes ao usuario
         */


        /*
         *  @DisplayBoard -> Exibe ao jogador o tabuleiro anteriormente definido 
         *  em linhas e colunas (X, Y)
         */
        static public void DisplayBoard(Board board) {

            ChessPiece[,] pieces = board.Pieces;

            for (int i = 0; i < 8; i++) {              
                for (int j = 0; j < 8; j++) {
                    if (pieces[i, j] != null) {
                        Console.Write(" " + pieces[i, j]);
                        Console.Write(" ");
                    }
                    else {
                        Console.Write(" - ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
