using System;

namespace MainBoard.BoardExceptions {
    class BoardException : Exception {
        /*
         * Classe que fornece o tratamento de excecoes personalizados
         * para o contexto do tabuleiro
         */

        /*
         *  @BoardException -> Construtor da classe, Constroi uma excecao basica a ser lancada quando necessario
         */
        public BoardException(string msg) : base(msg) {}
    }
}
