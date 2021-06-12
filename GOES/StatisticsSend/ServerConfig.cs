using System;
using Newtonsoft.Json;
using System.IO;

namespace GOES.StatisticsSend {
    /// <summary>
    /// Класс, представляющий структуру конфигурационного файла для соединения с сервером
    /// </summary>
    public class ServerConfig {
        /// <summary>
        /// Адрес, на котором развёрнут сервер (http://localhost:5000, и т.д.)
        /// </summary>
        public string ServerUri { get; set; }

        /// <summary>
        /// Путь до конфигурационного файла, содержащего настройки соединения с сервером
        /// </summary>
        private static readonly string configPath = "ServerConfig.json";

        /// <summary>
        /// Статический метод для получения объекта, представляющего текущую конфигурацию для связи с сервером.
        /// Объект десериализуется из json-файла конфигурации.
        /// </summary>
        /// <param name="errorMessage">Сообщение об ошибке, если получить объект не удалось (возвращается null-объект)</param>
        public static ServerConfig GetServerConfig(out string errorMessage) {
            ServerConfig config;
            StreamReader reader;
            errorMessage = null;
            try {
                reader = new StreamReader(configPath);
                config = JsonConvert.DeserializeObject<ServerConfig>(reader.ReadToEnd());
                reader.Close();
            }
            catch (Exception e) {
                errorMessage = e.Message;
                config = null;
            }
            return config;
        }

        /// <summary>
        /// Метод для сохранения объекта, представляющего конфигурацию для связи с сервером.
        /// Объект сериализуется в json-файл конфигурации.
        /// Возвращает флаг успеха
        /// </summary>
        public bool SaveServerConfig() {
            bool isSuccess = true;
            StreamWriter writer;
            try {
                writer = new StreamWriter(configPath);
                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                writer.Write(json);
                writer.Close();
            }
            catch {
                isSuccess = false;
            }
            return isSuccess;
        }
    }
}
