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


        /// <summary>
        ///     Checks if the CPF is valid
        /// </summary>
        public static bool IsValid(string cpf)
        {
            var formattedCpf = cpf.Replace(".", "").Replace("-", "");
            if (formattedCpf.Length != 11)
                return false;

            var firstDigit = Convert.ToInt32(formattedCpf.Substring(9, 1));
            var secondDigit = Convert.ToInt32(formattedCpf.Substring(10, 1));

            var sum = 0;
            for (var i = 0; i < 9; i++)
                sum += Convert.ToInt32(formattedCpf.Substring(i, 1)) * (10 - i);

            var firstDigitResult = sum % 11;
            if (firstDigitResult < 2)
                firstDigitResult = 0;
            else
                firstDigitResult = 11 - firstDigitResult;

            if (firstDigitResult != firstDigit)
                return false;

            sum = 0;
            for (var i = 0; i < 10; i++)
                sum += Convert.ToInt32(formattedCpf.Substring(i, 1)) * (11 - i);

            var secondDigitResult = sum % 11;
            if (secondDigitResult < 2)
                secondDigitResult = 0;
            else
                secondDigitResult = 11 - secondDigitResult;

            return secondDigitResult == secondDigit;
        }

        public static bool IsValid(long cpf)
        {
            return IsValid(cpf.ToString());
        }
    }
}