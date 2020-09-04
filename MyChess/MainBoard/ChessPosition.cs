using System;
using Pieces;
using MainBoard;

namespace MyChess.MainBoard {
    class ChessPosition {

        /*
         *  Classe que trabalha a conversao do formato padrao alfanumerico do xadrez para matriz
         */

        public int Line { get; set; }
        public char Column { get; set; }

        /*
         *  Classe construtora que define a estrutura na forma padrao do xadrez
         */

        public ChessPosition(char column, int line) {
            Line = line;
            Column = column;
        }


        /*
         *  @ToChessPosition -> Converte uma peca em formato alfanumero do xadrez para formato de matriz 
         */

        public Position2D ToChessPosition() {
            return new Position2D(8 - Line, Column - 'a');
        }
    }
}
