using System;
using System.Threading;
using System.Threading.Tasks;
using static TestTask.FileManager;

namespace TestTask
{
    /// <summary>
    /// Реализовать консольное приложение, осуществляющее параллельную загрузку файлов 
    /// из указанной директории, путь расположения которой передается в качестве первого 
    /// параметра приложения, в указанное местоположение на Яндекс.Диске, сетевой адрес которого 
    /// передается в качестве второго параметра. В процессе работы приложение должно отображать 
    /// список файлов и статус загрузки: загрузка / загружено. API Яндекс.Диска: https://yandex.ru/dev/disk/poligon/#
    /// </summary>

    class Program
    {
        static void Main(string[] args)
        {
            YandexAPI yandexAPI = new YandexAPI();
            UserFile[] FileList;

            //Считываем путь до директории с файлами
            Console.Write("Укажите путь до директории с файлами: ");
            string Dir = Console.ReadLine();
            //Считываем путь до директории на Yandex Disk
            Console.Write("Укажите сетевой адрес местоположение на Яндекс диске: ");
            string YandexDir = Console.ReadLine();

            Console.WriteLine("\n");

            //Создаем список файлов в директории
            FileList = FileManager.CreateFileList(Dir);


            for (int i = 0; i < FileList.Length; i++)
            {
                string _FileName = FileList[i].FileName;
                string _LocalFilePath = FileList[i].LocalFilePath;
                int _Top = FileList[i].Top, _Left = FileList[i].Left;

                new Thread(() =>
                {
                    if (yandexAPI.UploadFile(yandexAPI.getUploadUrl(YandexDir, _FileName), _LocalFilePath))
                    {
                        UpdateStatus(_Top, _Left);
                    }
                }).Start();
            }

            Console.ReadLine();

        }

        private static void UpdateStatus(int Top, int Left)
        {
            Console.SetCursorPosition(Left, Top);
            Console.Write(" загружено");
        }
    }
}
