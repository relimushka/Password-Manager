using System;
using System.Text;

namespace PasswordGenerator
{
    /*!
    \brief Класс, отвечающий генерацию пароля
    \author relimushka
    \version 1.0.0
    \date Июнь 2022 года
    */
    class GeneratePassword
    {
        /*!
        Генерирует новый пароль
        \param includeDigits При генерации использование цифр
        \param includeUppercase При генерации использование букв верхнего регистра
        \param includeSpecialCharacters При генерации использование спец. символов
        \param SliderValue Длина пароля
        \return Новый пароль
        */
        public static string Generate(bool? includeDigits, bool? includeUppercase, bool? includeSpecialCharacters, int SliderValue)
        {
            string chars = "abcdefghijklmnopqrstuvwxyz";

            const string digits = "0123456789";
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string specialCharacters = "!#$%&'()*+,-./;<=>?@[]^_`{|}~";

            if (includeDigits == true)
            {
                chars += digits;
            }

            if (includeUppercase == true)
            {
                chars += uppercase;
            }

            if (includeSpecialCharacters == true)
            {
                chars += specialCharacters;
            }

            StringBuilder password = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < SliderValue; i++)
            {
                int index = rnd.Next(chars.Length);
                password.Append(chars[index]);
            }

            return password.ToString();
        }
    }
}
