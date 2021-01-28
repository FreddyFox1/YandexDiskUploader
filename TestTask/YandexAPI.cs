using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace TestTask
{
    /// <summary>
    /// Чтобы загрузить файл на Диск, необходимо:
    /// 1. Запросить URL для загрузки.
    /// 2. Загрузить файл по полученному адресу.
    /// </summary>
    interface Yandex
    {
        string getUploadUrl(string YandexDir, string FileName);
        bool UploadFile(string url, string FilePath);
    }

    public class YandexAPI : Yandex
    {
        //Токен авторизации для Yandex Disk
        private string AccessToken { get; set; } = "Поставить свой токен";

        //Получаем уникальную ссылку для загрузки файла
        public string getUploadUrl(string YandexDir, string FileName)
        {
            var request = WebRequest.Create("https://cloud-api.yandex.net/v1/disk/resources/upload?path=" + YandexDir + '/' + FileName);
            request.Headers["Authorization"] = "OAuth " + AccessToken;
            request.Method = "GET";

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                var url = JsonConvert.DeserializeObject<Upload>(reader.ReadToEnd());
                return url.href;
            }

        }
        //Отправляем файл на ЯД по указанной ссылке.
        public bool UploadFile(string url, string FilePath)
        {
            var request = WebRequest.Create(url);
            request.Method = "PUT";
            request.ContentType = "application/binary";
            try
            {
                using (Stream myReqStream = request.GetRequestStream())
                {
                    using (FileStream myFile = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader myReader = new BinaryReader(myFile))
                        {
                            byte[] buffer = myReader.ReadBytes(2048);
                            while (buffer.Length > 0)
                            {
                                myReqStream.Write(buffer, 0, buffer.Length);
                                buffer = myReader.ReadBytes(2048);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            try
            {
                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }


}

