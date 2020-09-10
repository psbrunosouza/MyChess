namespace MainBoard {
    class Position2D {

        /*
         *  Classe Position2D, define a posicao de uma peca no tabuleiro, baseado em linha e coluna
         */

        public int Line { get; set; }
        public int Column { get; set; }

        /*
         *  @ -> Construtor da classe, define posicao X,Y
         */

        public Position2D(int line, int column) {
            Line = line;
            Column = column;
        }

        /*
        *  @GetPosition -> define posicao em X,Y
        */

        public void GetPosition(int line, int column) {
            Line = line;
            Column = column;
        }
    }
}
