using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public static class Proxy
    {
        static string baseUri = "http://localhost:2926/api/tasks";

        public static HttpResponseMessage CreateTaskRequest(TaskModel task)
        {
            var message = new HttpRequestMessage();
            var content = new MultipartFormDataContent();

            foreach (var file in task.Files)
            {
                var fs = new FileStream(file, FileMode.Open);
                var index = file.LastIndexOf(@"\");
                var fn = file.Substring(index + 1);
                fs.Position = 0;
                content.Add(new StreamContent(fs), "files", fn);
            }

            message.Method = HttpMethod.Post;
            message.Content = content;
            message.RequestUri = new Uri($"{baseUri}/createtask/{task.TaskName}");

            var client = new HttpClient();
            return client.SendAsync(message).Result;
        }

        public static void StartTaskRequest(string taskName, int threadCount)
        {
            var client = new HttpClient();

            client.GetAsync($"{baseUri}/StartTask/{taskName}/{threadCount}");
        }

        public static async Task<string> StopTaskRequest(string taskName)
        {
            var client = new HttpClient();
            var response = client.GetAsync($"{baseUri}/StopTask/{taskName}").Result;

            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        public static async Task<string> GetTaskStatusRequest(string taskName)
        {
            var client = new HttpClient();
            var response = client.GetAsync($"{baseUri}/GetTaskStatus/{taskName}").Result;

            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        public static async Task<string> DeleteTaskRequest(string taskName)
        {
            var client = new HttpClient();
            var response = client.DeleteAsync($"{baseUri}/DeleteTask/{taskName}").Result;

            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
