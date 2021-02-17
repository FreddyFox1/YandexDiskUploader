using System;
using System.Collections.Generic;
using System.Text;

namespace YandexDiskUploader
{
    /// <summary>
    /// Чтобы загрузить файл на Диск, необходимо:
    /// 1. Запросить URL для загрузки.
    /// 2. Загрузить файл по полученному адресу.
    /// </summary>
    interface IYandex
    {
        string getUploadUrl(string YandexDir, string FileName);
        bool UploadFile(string url, string FilePath);
    }
}
