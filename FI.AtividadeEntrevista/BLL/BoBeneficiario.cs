using System.Collections.Generic;
using FI.AtividadeEntrevista.DAL.Beneficiarios;
using FI.AtividadeEntrevista.DML;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        ///     Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto do beneficiario</param>
        public long Incluir(Beneficiario beneficiario)
        {
            var cli = new DaoBeneficiario();
            return cli.Incluir(beneficiario);
        }

        /// <summary>
        ///     Altera um beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto do beneficiario</param>
        /// <returns></returns>
        public void Alterar(Beneficiario beneficiario)
        {
            var cli = new DaoBeneficiario();
            cli.Alterar(beneficiario);
        }

        /// <summary>
        ///     Excluir o beneficiario pelo id
        /// </summary>
        /// <param name="id">id do beneficiario</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            var cli = new DaoBeneficiario();
            cli.Excluir(id);
        }

        /// <summary>
        ///     Lista os beneficiarios
        /// </summary>
        public List<Beneficiario> Listar(long idCliente)
        {
            var cli = new DaoBeneficiario();
            return cli.Listar(idCliente);
        }

        /// <summary>
        ///     Verifica se o CPF já está cadastrado
        /// </summary>
        public bool VerificarExistencia(string CPF)
        {
            var cli = new DaoBeneficiario();
            return cli.VerificarExistencia(CPF);
        }
    }
}