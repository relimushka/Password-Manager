using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PasswordGenerator
{
    /*!
    \brief Класс, отвечающий за шифрование и расшифрование паролей
    \author relimushka
    \version 1.0.0
    \date Июнь 2022 года
    */
    public class Encryption
    {
        /*!
        Шифрует пароль
        \param password Незашифрованный пароль
        \return Зашифрованный пароль
        */
        public static byte[] Protect(byte[] password)
        {
            try
            {
                return ProtectedData.Protect(password, null, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException)
            {
                return null;
            }
        }

        /*!
        Расшифровывает пароль
        \param password Зашифрованный пароль
        \return Расшифрованный пароль
        */
        public static byte[] Unprotect(byte[] password)
        {
            try
            {
                return ProtectedData.Unprotect(password, null, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException)
            {
                return null;
            }
        }
    }
}
