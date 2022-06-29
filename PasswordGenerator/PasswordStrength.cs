using System.Text.RegularExpressions;

namespace PasswordGenerator
{
    /*!
    \brief Класс, отвечающий за определение надежности пароля
    \author relimushka
    \version 1.0.0
    \date Июнь 2022 года
    */
    public class PasswordStrength
    {
        /*!
        Определяет уровень надежности пароля
        \param password Пароль
        \return Уровень надежности пароля
        */
        public static string Check(string password)
        {
            int score = 0;

            var hasNumber = new Regex(@"[0-9]+");
            var hasUppercase = new Regex(@"[A-Z]+");
            var hasLowercase = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (password.Length == 0)
            {
                return "Уровень пароля";
            }

            if (password.Length >= 8)
            {
                score++;
            }

            if (password.Length >= 12)
            {
                score++;
            }

            if (hasNumber.IsMatch(password))
            {
                score++;
            }

            if (hasLowercase.IsMatch(password) && hasUppercase.IsMatch(password))
            {
                score++;
            }

            if (hasSymbols.IsMatch(password))
            {
                score++;
            }

            switch (score)
            {
                case 1:
                    return "Очень слабый";

                case 2:
                    return "Слабый";

                case 3: 
                    return "Средний";

                case 4:
                    return "Сильный";

                case 5:
                    return "Очень сильный";

                default:
                    return "Ошибка";
            }
        }
    }
}
