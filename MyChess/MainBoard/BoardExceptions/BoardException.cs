using System;

namespace MainBoard.BoardExceptions {
    class BoardException : Exception {
        /*
         * Classe que fornece o tratamento de excecoes personalizados
         * para o contexto do tabuleiro
         */
        public BoardException(string msg) : base(msg) {}
    }
}
