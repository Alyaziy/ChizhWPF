using ChizhWPF.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public async Task<List<MuscleDTO>> GetMuscle()

        {
            try
            {
                var response = await httpClient.GetAsync("Muscle/Muscle");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<MuscleDTO>>(content);
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

        public async Task AddTrainAsync(TrainDTO train)
        {
            using (var client = new HttpClient())
            {
                var jsonContent = JsonConvert.SerializeObject(train);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("Train/AddTrain", httpContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Не удалось добавить треню.");
                }
            }
        }

        public async Task AddPozeAsync(PozeDTO poze)
        {
            using (var client = new HttpClient())
            {
                var jsonContent = JsonConvert.SerializeObject(poze);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("Poze/AddPoze", httpContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Не удалось добавить позу.");
                }
            }
        }

        public async Task<TrainDTO> EditTrain(TrainDTO train, int id)
        {
            var shit = System.Text.Json.JsonSerializer.Serialize(train);
            using StringContent jsonContent = new(
                   shit ,
                   Encoding.UTF8,
                   "application/json");
            using HttpResponseMessage response = await httpClient.PutAsync("Train/" + train.Id, jsonContent);
            response.EnsureSuccessStatusCode();
            // MessageBox.Show(response.StatusCode.ToString());
            return train;
        }

        public async Task<PozeDTO> EditPoze(MuscleDTO muscle, PozeDTO poze, int id)
        {
            //var shit = System.Text.Json.JsonSerializer.Serialize(poze);
            //using StringContent jsonContent = new(shit,Encoding.UTF8,"application/json");
            //using HttpResponseMessage response = await httpClient.PutAsync("Poze/" + poze.Id, jsonContent);


            poze.IdMuscle = poze.Id;
            poze.Muscle = poze.Muscle;
            poze.Tittle = poze.Tittle;  
            poze.Time = poze.Time;  
            poze.Description = poze.Description;
            poze.Image = poze.Image;

            //400 Bad regrest
            using HttpResponseMessage response = await httpClient.PutAsJsonAsync($"Poze/Edit/{poze.Id}", muscle.Id);
            //response - 400 Bad regrest

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Данные успешно изменены в базе данных", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
               
            }
            else
            {
                MessageBox.Show("Ошибка при изменении данных в базу данных", "Неудача", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return null;
            //response.EnsureSuccessStatusCode();
            // MessageBox.Show(response.StatusCode.ToString());
            //return poze;
        }

        public async Task DeleteTrain(int id)
        {
            using HttpResponseMessage response = await httpClient.DeleteAsync("Train/" + id);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePoze(int id)
        {
            using HttpResponseMessage response = await httpClient.DeleteAsync("Poze/" + id);
            response.EnsureSuccessStatusCode();
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
