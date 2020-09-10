using Pieces;
using MainBoard.BoardExceptions;

namespace MainBoard {
    class Board {
        
        /*
         *  Classe Board, contem a estrutura do tabuleiro em linhas e colunas e o conjunto de pecas
         *  e suas respectivas posicoes no tabuleiro
         */

        public int Lines { get; set; }
        public int Columns { get; set; }
        public ChessPiece[,] Pieces { get; set; }

        /*
         *  @Board -> Construtor da classe, Constroi um tabuleiro com estrutura de linhas e colunas (X, Y)
         */
        public Board(int lines, int columns) {
            Lines = lines;
            Columns = columns;
            Pieces = new ChessPiece[lines, columns];
        }

        /*
        *  @InsertPiece -> Inserir uma peca na posicao especificada
        */

        public void InsertPiece(ChessPiece piece, Position2D pos) {
            
            Pieces[pos.Line, pos.Column] = piece;
            piece.Position = pos;
        }

        /*
         *  @RemovePiece -> Remover uma peca indicando uma posicao especifica
         */

        public ChessPiece RemovePiece(Position2D pos) {
            ChessPiece PieceToRemove = FindPiece(pos);
            Pieces[pos.Line, pos.Column] = null;
            return PieceToRemove;
        }


        /*
         *  @FindPiece -> Encontrar uma peca na posicao especificada
         */

        private ChessPiece FindPiece(Position2D pos) {

            if (Pieces[pos.Line, pos.Column] == null) {
                return null;
            }

            ChessPiece PieceToFind = Pieces[pos.Line, pos.Column];
            Pieces[pos.Line, pos.Column].Position = null;
            Pieces[pos.Line, pos.Column] = null;
            return PieceToFind;
        }

        /*
         *  @ValidatePosition -> Verifica se uma posicao do tabuleiro possui ou nao uma peca  
         */

        public bool ValidatePosition(Position2D pos) {
            if (pos.Line < 0 || pos.Line >= Lines || pos.Column < 0 || pos.Column >= Columns) {
                return false;
            }
            else {
                return true;
            }
        }

        /*
         *  @GetPiece -> Captura uma peca baseado em uma posicao do tabuleiro
         */
        public ChessPiece GetPiece(Position2D pos) {
            ChessPiece p = Pieces[pos.Line, pos.Column];
            if (p == null) {
                return null;
            }
            else {
                return p;
            }
        }

        /*
         *  Retorna a estrutura do tabuleiro em linhas e colunas (x, y)
         */
        public override string ToString() {
            return "(" + Lines + ", " + Columns + ")";
        }

    }
}
