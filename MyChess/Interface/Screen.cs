using MainBoard;
using MainMatch;
using MyChess.MainBoard;
using Pieces;
using Pieces.PieceColor;
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
     
            for (int i = 0; i < board.Lines; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++) {
                
                    PieceToColor(board.Pieces[i, j]);

                }
                Console.WriteLine();
            }
            Console.WriteLine("   a  b  c  d  e  f  g  h");
        }

        /*
         *  @PossibleMovements -> Exibe as possibilidades de movimento de uma peca
         *  em linhas e colunas (X, Y)
         */

        static public void PossibleMovements(Board board, bool[,] possibleMovements) {       

            ConsoleColor lastBackground = Console.BackgroundColor;
            ConsoleColor newBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Lines; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++) {                 
                    
                    if (possibleMovements[i, j]) {
                        Console.BackgroundColor = newBackground;
                    }
                    else {
                        Console.BackgroundColor = lastBackground;
                    }

                    PieceToColor(board.Pieces[i, j]);

                }
                Console.WriteLine();
            }
            Console.WriteLine("   a  b  c  d  e  f  g  h");
        }

        /*
         *  @PieceToColor -> Impreme as pecas baseado na cor da peca
         */

        static public void PieceToColor(ChessPiece piece) {

            ConsoleColor lastColor = Console.ForegroundColor;
            ConsoleColor newColor = ConsoleColor.Red;

            if (piece == null) {
                Console.Write(" - ");
            }
            else {
                if (piece.Color == Colors.White) {
                    Console.ForegroundColor = lastColor;
                    Console.Write(" " + piece + " ");

                }
                else {
                    Console.ForegroundColor = newColor;
                    Console.Write(" " + piece + " ");
                }

                Console.ForegroundColor = lastColor;
            }           
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
