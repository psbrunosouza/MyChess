using MainBoard;
using MainMatch;
using MyChess.MainBoard;
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

            for (int i = 0; i < board.Lines; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++) {
                    
                    if (pieces[i, j] != null) {
                        Console.Write(" " + pieces[i, j] + " ");
                    }
                    else {
                        Console.Write(" - ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("   a  b  c  d  e  f  g  h");
        }

        /*
         *  @ChessMove -> Funcao de suporte para a inserir as pecas de acordo com o padrao alfanumerico 
         *  do xadrez a/h e 1/8
         */

        static public Position2D ChessMove() {
            string pos = Console.ReadLine();
            return new ChessPosition(pos[0], int.Parse(pos[1] + "")).ToChessPosition();
        }
    }
}
