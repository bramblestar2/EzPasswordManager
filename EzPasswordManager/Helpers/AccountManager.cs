using DynamicData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzPasswordManager.Helpers
{
    public static class AccountManager
    {
        public static readonly string DefaultPasswordDirectory = Path.Combine(Directory.GetCurrentDirectory(), "EZPassMangr\\");

        public static void CreateLogin(string username, string password, string? directory = null)
        {
            CheckDirectory(ref directory);

            if (!Path.Exists(Path.Combine(directory, ".logins")))
                File.Create(Path.Combine(directory, ".logins")).Close();

            if (Path.Exists(Path.Combine(directory, ".logins")))
            {
                string hashedPassword = TextEncryption.EncryptSHA(password);
                bool existingLogin = UserLoginExist(username, hashedPassword);


                if (!existingLogin)
                {
                    File.AppendAllText(Path.Combine(directory, ".logins"), username + " " + hashedPassword + "\n");
                    File.Create(Path.Combine(directory, username)).Close();
                }
            }
        }


        public static bool UserLoginExist(string username, string password, string? directory = null)
        {
            CheckDirectory(ref directory);

            if (!Path.Exists(Path.Combine(directory, ".logins")))
                File.Create(Path.Combine(directory, ".logins")).Close();

            using (var fs = File.OpenRead(Path.Combine(directory, ".logins")))
            {
                using (var sr = new StreamReader(fs, Encoding.UTF8, true))
                {
                    string hashedPassword = TextEncryption.EncryptSHA(password);
                    string? line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line is not null)
                        {
                            string[] strings = line.Split(" ");
                            if (strings.Length > 1)
                            {
                                string user = strings[0],
                                       pass = strings[1];

                                if (username.Equals(user) && hashedPassword.Equals(pass))
                                    return true;
                            }
                        }
                    }
                }
            }

            return false;
        }


        public static bool DoesUserFileExist(string username, string password, string? directory = null)
        {
            CheckDirectory(ref directory);

            foreach (var file in Directory.GetFiles(directory))
            {
                try
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    if (username.Equals(fileName))
                        return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return false;
        }


        public static void CheckDirectory(ref string? directory)
        {
            if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory))
            {
                directory = DefaultPasswordDirectory;
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
            }
        }


        public static void RemoveUser(string username, string? directory = null)
        {
            CheckDirectory(ref directory);

            string path = Path.Combine(directory, ".logins");

            if (!Path.Exists(path))
                File.Create(path).Close();

            //Delete user login
            string[] file = File.ReadAllLines(path);
            for (var i = 0; i < file.Length; i++)
            {
                string user = file[i].Split(' ')[0];

                if (user.Equals(username))
                {
                    var list = file.ToList();
                    list.RemoveAt(i);
                    file = list.ToArray();

                    i--;
                }
            }
            File.WriteAllLines(path, file);

            //Delete user file
            if (File.Exists(Path.Combine(directory, username)))
                File.Delete(Path.Combine(directory, username));
        }


        public static int? FindLineOfLogin(string username, string? directory = null)
        {
            CheckDirectory(ref directory);

            if (!Path.Exists(Path.Combine(directory, ".logins")))
                File.Create(Path.Combine(directory, ".logins")).Close();

            using (var fs = File.OpenRead(Path.Combine(directory, ".logins")))
            {
                using (var sr = new StreamReader(fs, Encoding.UTF8, true))
                {
                    string? line;
                    int count = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line is not null)
                        {
                            string[] strings = line.Split(" ");
                            if (strings.Length > 1)
                            {
                                string user = strings[0],
                                       pass = strings[1];

                                if (username.Equals(user))
                                    return count;
                            }
                        }

                        count++;
                    }
                }
            }

            return null;
        }
    }
}
