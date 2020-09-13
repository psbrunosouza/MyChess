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

            ConsoleColor aux;

            for (int i = 0; i < board.Lines; i++) {
                
                aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(8 - i + " ");
                Console.ForegroundColor = aux;

                for (int j = 0; j < board.Columns; j++) {
                
                    PieceToColor(board.Pieces[i, j]);

                }
                Console.WriteLine();
            }
            aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("   a  b  c  d  e  f  g  h");
            Console.ForegroundColor = aux;
        }

        /*
         *  @PossibleMovements -> Exibe as possibilidades de movimento de uma peca
         *  em linhas e colunas (X, Y)
         */

        static public void PossibleMovements(Board board, bool[,] possibleMovements) {       

            ConsoleColor lastBackground = Console.BackgroundColor;
            ConsoleColor newBackground = ConsoleColor.DarkGray;
            ConsoleColor aux;

            for (int i = 0; i < board.Lines; i++) {
                
                aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(8 - i + " ");
                Console.ForegroundColor = aux;

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
            aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("   a  b  c  d  e  f  g  h");
            Console.ForegroundColor = aux;
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

            char column = pos[0];
            int line = int.Parse(pos[1] + "");        

            if(pos.Length > 2) {
                throw new Exception("Voce digitou mais do que dois digitos validos!");
            }

            /*if (column.GetType() != typeof(char) || line.GetType() != typeof(int)) {
                throw new Exception("Digite uma posição válida!");
            }*/
            
            return new ChessPosition(column, line).ToChessPosition();

        }

        /*
         *  @DisplayMatch -> Imprime as informações da partida atual
         */

        static public void DisplayMatch(Match match) {
            DisplayBoard(match.Board);
            Console.WriteLine();
            DisplayCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Turno(s): " + match.Turn);
            
            if (!match.MatchFinished) {
                Console.WriteLine("Aguardando jogada: " + match.CurrentPlayer);
                if (match.Xeque == true) {
                    Console.WriteLine("XEQUE!");
                }
            }
            else {
                Console.WriteLine("XEQUE-MATE!");
                Console.WriteLine("Vencedor: " + match.CurrentPlayer);
            }
                    
        }

        /*
        *  @DisplayCapturedPieces -> Exibe as peças capturadas pretas e brancas
        */
        static public void DisplayCapturedPieces(Match match) {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            DisplayCapturedSet(match.GetCapturedPieces(Colors.White));
            Console.Write("Pretas: ");
            DisplayCapturedSet(match.GetCapturedPieces(Colors.Black));

        }

        /*
        *  @DisplayCapturedSet -> Retorna o conjunto de peças capturadas
        */

        static public void DisplayCapturedSet(HashSet<ChessPiece> setOfPieces) {
            Console.Write("[");
            foreach (ChessPiece x in setOfPieces) {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }
    }
}
