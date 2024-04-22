using ChizhWPF.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChizhWPF.API
{
    class Client
    {
        HttpClient httpClient = new HttpClient();
        public Client()
        {
            httpClient.BaseAddress = new Uri(@"https://localhost:7049/api/");
        }

        public async Task<UserDTO> UserLogin(UserDTO user, string name, string password)
        {

           
            {
                var loginUser = new UserDTO
                {
                    Name = name,
                    Password = password
                };
                var jsonContent = JsonConvert.SerializeObject(loginUser);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("User/UserLogin", httpContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Неправильный логин или пароль");
                }
                var answerUser = JsonConvert.DeserializeObject<UserDTO>(await response.Content.ReadAsStringAsync());
                return answerUser;
            }
        }

        public async Task<List<TrainDTO>> GetTrains()

        {
            try
            {
                var response = await httpClient.GetAsync("Train");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TrainDTO>>(content);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                // Обработка исключений
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public async Task<List<PozeDTO>> GetPoze()

        {
            try
            {
                var response = await httpClient.GetAsync("Poze/Poze");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<PozeDTO>>(content);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                // Обработка исключений
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public async Task<AdminDTO> AdminLogin(AdminDTO admin, string name, string password)
        {


            {
                var loginAdmin = new AdminDTO
                {
                    AdmName = name,
                    AdmPassword = password
                };
                var jsonContent = JsonConvert.SerializeObject(loginAdmin);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("Admin/AdminLogin", httpContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Неправильный логин или пароль");
                }
                var answerAdmin = JsonConvert.DeserializeObject<AdminDTO>(await response.Content.ReadAsStringAsync());
                return answerAdmin;
            }
        }

        static Client instance = new();
        public static Client Instance
        {
            get
            {
                if (instance == null)
                    instance = new Client();
                return instance;
            }
        }

    }
}
