﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestTask
{
    public class FileManager
    {
        //Создаем структуру для удобства использования файлов
        public class UserFile
        {
            public string LocalFilePath { get; set; }
            public string FileName { get; set; }
            public string UploadURL { get; set; }
            public int Top { get; set; }
            public int Left { get; set; }
        }

        public static UserFile[] Files;

        /// <summary>
        /// Создаем список всех файлов в указанном пути
        /// </summary>
        /// <param name="dir">
        ///     Путь до директории на ПК выбранной пользователем
        /// </param>

        public static UserFile[] CreateFileList(string dir)
        {
            var AllFiles = Directory.GetFiles(dir);
            Files = new UserFile[AllFiles.Length];

            for (int i = 0; i < AllFiles.Length; i++)
            {
                Files[i] = new UserFile
                {
                    LocalFilePath = AllFiles[i],
                    FileName = Path.GetFileName(AllFiles[i]),
                };

                PrintFile(i);
                //Console.Write($"Файл {i} " + ':' + $" {Files[i].FileName} : ");
                //Files[i].Left = Console.CursorLeft;
                //Files[i].Top = Console.CursorTop;
                //Console.SetCursorPosition(Files[i].Left, Files[i].Top);
                //Console.Write(" загрузка\n");

            }
            return Files;
        }


        private static void PrintFile(int FileIndex)
        {
            Console.Write($"Файл {FileIndex} " + ':' + $" {Files[FileIndex].FileName} : ");
            Files[FileIndex].Left = Console.CursorLeft;
            Files[FileIndex].Top = Console.CursorTop;
            Console.SetCursorPosition(Files[FileIndex].Left, Files[FileIndex].Top);
            Console.Write(" загрузка\n");
        }




    }
}
