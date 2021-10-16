namespace YandexDiskUploader
{
    /// <summary>
    /// Чтобы загрузить файл на Диск, необходимо:
    /// 1. Запросить URL для загрузки.
    /// 2. Загрузить файл по полученному адресу.
    /// </summary>
    interface IYandex
    {
        string GetUploadUrl(string YandexDir, string FileName);
        bool UploadFile(string Url, string FilePath);
    }
}
