using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace PasswordGenerator
{
    /*!
    \brief Класс, отвечающий за взаимодействие аккаунтов
    \author relimushka
    \version 1.0.0
    \date Июнь 2022 года
    */
    public class PasswordDatabase
    {
        /*!
        Получает данные о всех хранящихся аккаунтах
        \return Коллекция всех аккаунтов
        */
        public static ObservableCollection<Data> GetData()
        {
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            ObservableCollection<Data> data = new(); ;

            if (!Directory.Exists(directory + "\\PasswordManager"))
            {
                Directory.CreateDirectory(directory + "\\PasswordManager");
            }

            var files = Directory.GetFiles(directory + "\\PasswordManager", "*.rel");

            foreach (var file in files)
            {
                var z = File.ReadAllBytes(file);
                byte[] originalData = null;

                if (Encryption.Unprotect(z) != null)
                {
                    originalData = Encryption.Unprotect(z);
                }

                string password = Encoding.Default.GetString(bytes: originalData);

                var tempfile = file[(file.LastIndexOf("\\") + 1) ..^4];
                var index = tempfile.IndexOf("-");
                var service = tempfile[..index];
                var login = tempfile.Substring(index + 1);

                data.Add(new Data() { Service = service, Login = login, Password = password });
            }
            return data;
        }

        /*!
        Создает новый аккаунт
        \param service Название сервиса
        \param login Имя аккаунта
        \param password Пароль аккаунта
        */
        public static void Create(string service, string login, string password)
        {
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string fileName = directory + "\\PasswordManager\\" + service + "-" + login + ".rel";
            byte[] str = Encoding.Default.GetBytes(password);
            byte[] encryptedSecret = null;

            if (Encryption.Protect(str) != null)
            {
                encryptedSecret = Encryption.Protect(str);
            }

            File.WriteAllBytes(fileName, bytes: encryptedSecret);
        }

        /*!
        Изменяет существующий аккаунт
        \param oldFilename Название старого файла
        \param service Новое название сервиса
        \param login Новое имя аккаунта
        \param password Новый пароль аккаунта
        */
        public static void Edit(string oldFilename, string service, string login, string password)
        {
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            File.Delete(directory + "\\PasswordManager\\" + oldFilename + ".rel");
            string fileName = directory + "\\PasswordManager\\" + service + "-" + login + ".rel";
            byte[] str = Encoding.Default.GetBytes(password);

            byte[] encryptedSecret = null;

            if (Encryption.Protect(str) != null)
            {
                encryptedSecret = Encryption.Protect(str);
            }

            File.WriteAllBytes(fileName, bytes: encryptedSecret);
        }

        /*!
        Удаляет аккаунт
        \param oldFilename Название старого файла
        \param service Название сервиса
        \param login Имя аккаунта
        */
        public static void Delete(string service, string login)
        {
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            File.Delete(directory + "\\PasswordManager\\" + service + "-" + login + ".rel");
        }

        /*!
        Осуществляет поиск сервиса
        \param data Коллекция всех аккаунтов
        \param findingService Название искомого сервиса
        */
        public static void Search(ObservableCollection<Data> data, string findingService)
        {
            List<Data> TempFiltered;

            TempFiltered = data.Where(contact => contact.Service.Contains(findingService, StringComparison.InvariantCultureIgnoreCase)).ToList();

            for (int i = data.Count - 1; i >= 0; i--)
            {
                var item = data[i];
                if (!TempFiltered.Contains(item))
                {
                    data.Remove(item);
                }
            }

            foreach (var item in TempFiltered)
            {
                if (!data.Contains(item))
                {
                    data.Add(item);
                }
            }
        }
    }
}