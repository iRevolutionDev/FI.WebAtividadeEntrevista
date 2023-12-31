﻿using System.Collections.Generic;
using FI.AtividadeEntrevista.DAL;
using FI.AtividadeEntrevista.DML;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoCliente
    {
        /// <summary>
        ///     Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public long Incluir(Cliente cliente)
        {
            var cli = new DaoCliente();
            return cli.Incluir(cliente);
        }

        /// <summary>
        ///     Altera um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public void Alterar(Cliente cliente)
        {
            var cli = new DaoCliente();
            cli.Alterar(cliente);
        }

        /// <summary>
        ///     Consulta o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public Cliente Consultar(long id)
        {
            var cli = new DaoCliente();
            return cli.Consultar(id);
        }

        /// <summary>
        ///     Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            var cli = new DaoCliente();
            cli.Excluir(id);
        }

        /// <summary>
        ///     Lista os clientes
        /// </summary>
        public List<Cliente> Listar()
        {
            var cli = new DaoCliente();
            return cli.Listar();
        }

        /// <summary>
        ///     Lista os clientes
        /// </summary>
        public List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            var cli = new DaoCliente();
            return cli.Pesquisa(iniciarEm, quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        ///     VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF)
        {
            var cli = new DaoCliente();
            return cli.VerificarExistencia(CPF);
        }
    }
}