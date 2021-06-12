using System.Collections.Generic;
using Newtonsoft.Json;
using GOES.Problems;
using System.Net;
using System.IO;
using System;

namespace GOES.StatisticsSend {
    /// <summary>
    /// Класс, предназначенный для отправки статистики выполненного задания на сервер
    /// </summary>
    class StatisticsSender {
        // ----Поля
        private ServerConfig serverConfig;


        // ----Конструктор
        public StatisticsSender(ServerConfig serverConfig) {
            this.serverConfig = serverConfig;
        }

        /// <summary>
        /// Отправить статистику решения задачи на сервер
        /// </summary>
        /// <param name="problem">Дескриптор решённой задачи</param>
        /// <param name="example">Дескприптор решённого примера</param>
        /// <param name="statistics">Статистика решения</param>
        /// <param name="studentName">Имя студента</param>
        /// <param name="studentGroup">Группа (класс) студента</param>
        /// <param name="errorMessage">Сообщение об ошибке. Означивается, если произошла ошибка</param>
        /// <returns>Флаг успеха</returns>
        public bool Send(IProblemDescriptor problem, ProblemExample example, IProblemStatistics statistics, 
                string studentName, string studentGroup, out string errorMessage) {
            bool isSuccess = true;
            errorMessage = null;
            // Все отправляемые данные представляем в виде словаря пар <строка>-<строка> и сериализуем его в json
            Dictionary<string, string> data = new Dictionary<string, string> {
                { "student_name", studentName },
                { "student_group", studentGroup},
                { "problem_name", problem.Name },
                { "example_name", example.Name },
                { "example_description", example.Description },
                { "statistics_body", statistics.GetStatisticsText() },
                { "total_errors_count", statistics.TotalErrorsCount.ToString() },
                { "necessary_errors_count", statistics.TotalNecessaryErrorsCount.ToString() },
                { "mark", statistics.Mark.ToString() }
            };
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            // Готовим объект для совершения POST-запроса с JSON на сервер для отправки статистики
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"{serverConfig.ServerUri}/api/stats");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            // Совершаем запрос и получаем ответ
            try {
                // Пишем json в запрос
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
                    streamWriter.Write(json);
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                // Читаем ответ
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                    var responseString = streamReader.ReadToEnd();
                }
            }
            catch (Exception e) {
                isSuccess = false;
                errorMessage = e.Message;
            }
            return isSuccess;
        }
    }
}
