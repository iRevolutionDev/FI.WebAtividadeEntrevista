﻿namespace FI.AtividadeEntrevista.DML
{
    /// <summary>
    ///     Classe de beneficiários que representa o registo na tabela Beneficiarios do Banco de Dados
    /// </summary>
    public class Beneficiario
    {
        /// <summary>
        ///     Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     IdCliente
        /// </summary>
        public long IdCliente { get; set; }

        /// <summary>
        ///     Nome
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        ///     CPF
        /// </summary>
        public long CPF { get; set; }
    }
}