using System;

namespace FI.AtividadeEntrevista.Utils
{
    public static class CpfConverter
    {
        /// <summary>
        ///     Converte a string do CPF para long
        ///     <param name="cpf">CPF</param>
        /// </summary>
        public static long ToLong(string cpf)
        {
            var formattedCpf = cpf.Replace(".", "").Replace("-", "");
            return Convert.ToInt64(formattedCpf);
        }

        /// <summary>
        ///     Converte o long do CPF para string
        /// </summary>
        public static string ToString(long cpf)
        {
            return cpf.ToString();
        }

        /// <summary>
        ///     Converte o long para um CPF mais amigável
        /// </summary>
        public static string ToFriendlyString(long cpf)
        {
            return cpf.ToString(@"000\.000\.000\-00");
        }
    }
}