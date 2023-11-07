using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.Utils;

namespace FI.AtividadeEntrevista.DAL.Beneficiarios
{
    internal class DaoBeneficiario : AcessoDados
    {
        internal long Incluir(Beneficiario beneficiario)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("IdCliente", beneficiario.IdCliente),
                new SqlParameter("Nome", beneficiario.Nome),
                new SqlParameter("CPF", beneficiario.CPF)
            };

            var ds = Consultar("FI_SP_IncBeneficiario", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        internal void Alterar(Beneficiario beneficiario)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("IdCliente", beneficiario.IdCliente),
                new SqlParameter("Nome", beneficiario.Nome),
                new SqlParameter("CPF", beneficiario.CPF)
            };

            Executar("FI_SP_AltBeneficiario", parametros);
        }

        internal void Excluir(long id)
        {
            var parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("Id", id));

            Executar("FI_SP_DeleteBeneficiario", parametros);
        }

        internal List<Beneficiario> Listar(long idCliente)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("IdCliente", idCliente)
            };

            var ds = Consultar("FI_SP_ConsBeneficiario", parametros);
            var beneficiarios = Converter(ds);

            return beneficiarios;
        }

        public static List<Beneficiario> Converter(DataSet ds)
        {
            return (from DataRow row in ds.Tables[0].Rows
                select new Beneficiario
                {
                    Id = row.Field<long>("Id"),
                    IdCliente = row.Field<long>("IdCliente"),
                    Nome = row.Field<string>("Nome"),
                    CPF = CpfConverter.ToLong(row.Field<string>("CPF"))
                }).ToList();
        }

        public bool VerificarExistencia(string cpf)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("CPF", cpf)
            };

            var ds = Consultar("FI_SP_VerifyExistenceBenefic", parametros);
            var beneficiarios = Converter(ds);

            return beneficiarios.Count > 0;
        }
    }
}