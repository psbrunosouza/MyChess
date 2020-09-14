using MainBoard;
using MainMatch;
using MyChess.MainBoard;
using Pieces;
using Pieces.PieceColor;
using System;
using System.Collections.Generic;

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
         
                Console.Write(8 - i + "  ");    

                for (int j = 0; j < board.Columns; j++) {
                
                    PieceToColor(board.Pieces[i, j]);

                }
                Console.WriteLine();
            }       
            Console.WriteLine();
            Console.WriteLine("    a  b  c  d  e  f  g  h");        
        }

        /*
         *  @PossibleMovements -> Exibe as possibilidades de movimento de uma peca
         *  em linhas e colunas (X, Y)
         */

        static public void PossibleMovements(Board board, bool[,] possibleMovements) {       

            ConsoleColor lastBackground = Console.BackgroundColor;
            ConsoleColor newBackground = ConsoleColor.Magenta;

            for (int i = 0; i < board.Lines; i++) {
                       
                Console.Write(8 - i + "  ");   
                
                for (int j = 0; j < board.Columns; j++) {                 
                    
                    if (possibleMovements[i, j]) {
                        Console.BackgroundColor = newBackground;
                    }
                    else {
                        Console.BackgroundColor = lastBackground;
                    }

                    PieceToColor(board.Pieces[i, j]);
                    Console.BackgroundColor = lastBackground;
                }
                Console.WriteLine();
            }
          
            Console.WriteLine();
            Console.WriteLine("    a  b  c  d  e  f  g  h");
        }

        /*
         *  @PieceToColor -> Impreme as pecas baseado na cor da peca
         */

        static public void PieceToColor(ChessPiece piece) {

            ConsoleColor lastColor = Console.ForegroundColor;           

            if (piece == null) {
                Console.Write(" X ");
            }
            else {
                if (piece.Color == Colors.White) {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(" " + piece + " ");

                }else if (piece.Color == Colors.Black) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" " + piece + " ");
                }else{
                    Console.ForegroundColor = ConsoleColor.Blue;
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

        /*
         *  @DisplayMatch -> Imprime as informações da partida atual
         */

        static public void DisplayMatch(Match match) {          
            DisplayBoard(match.Board);
            Console.WriteLine();
            DisplayCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("    Turno(s) -> " + match.Turn);
            
            if (!match.MatchFinished) {
                Console.WriteLine("    Jogador -> " + match.CurrentPlayer);
                if (match.Xeque == true) {
                    Console.WriteLine("    XEQUE!");
                }
            }
            else {
                Console.WriteLine("    XEQUE-MATE!");
                Console.WriteLine("    VENCEDOR! -> " + match.CurrentPlayer);
            }
                    
        }

        /*
        *  @DisplayCapturedPieces -> Exibe as peças capturadas pretas e brancas
        */
        static public void DisplayCapturedPieces(Match match) {
            Console.WriteLine("   - Peças capturadas -");
            Console.WriteLine();
            Console.Write("    Brancas -> ");
            DisplayCapturedSet(match.GetCapturedPieces(Colors.White));
            Console.WriteLine();
            Console.Write("    Pretas  -> ");
            DisplayCapturedSet(match.GetCapturedPieces(Colors.Black));
            Console.WriteLine("");
            Console.WriteLine("   ---------------------");

        }

        /*
        *  @DisplayCapturedSet -> Retorna o conjunto de peças capturadas
        */

        static public void DisplayCapturedSet(HashSet<ChessPiece> setOfPieces) {
            //Console.Write("[");
            foreach (ChessPiece x in setOfPieces) {
                Console.Write(x + " ");
            }
            //Console.Write("]");
        }
    }
}
