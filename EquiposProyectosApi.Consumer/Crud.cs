using Newtonsoft.Json;
using System.Text;

namespace EquiposProyectosApi.Consumer
{
   
        public static class Crud<T>
        {

            public static string EndPoint { get; set; }

            public static List<T> GetAll()
            {
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync(EndPoint).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var json = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<T>>(json);
                    }
                    else
                    {
                        throw new Exception($"Error: {response.StatusCode}");
                    }
                }
            }

            public static T GetById(int id)
            {
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync($"{EndPoint}/{id}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var json = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<T>(json);
                    }
                    else
                    {
                        throw new Exception($"Error: {response.StatusCode}");
                    }
                }
            }
            public static List<T> GetBy(String libreria, int id)
            {
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync($"{EndPoint}/{libreria}/{id}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var json = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<T>>(json);
                    }
                    else
                    {
                        throw new Exception($"Error: {response.StatusCode}");
                    }

                }
            }

            public static T Create(T item)
            {
                using (var client = new HttpClient())
                {
                    var response = client.PostAsync(
                        EndPoint,
                        new StringContent(
                            JsonConvert.SerializeObject(item),
                            Encoding.UTF8,
                            "application/json"
                        )
                    ).Result;

                    var responseContent = response.Content.ReadAsStringAsync().Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<T>(responseContent);
                    }
                    else
                    {
                        // Mostrar el cuerpo del error (por ejemplo, detalles del 500)
                        throw new Exception($"Error HTTP {(int)response.StatusCode} - {response.StatusCode}:\n{responseContent}");
                    }
                }
            }

            public static bool Update(int id, T item)
            {
                using (var client = new HttpClient())
                {
                    var response = client.PutAsync(
                            $"{EndPoint}/{id}",
                            new StringContent(
                                JsonConvert.SerializeObject(item),
                                Encoding.UTF8,
                                "application/json"
                            )
                        ).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception($"Error: {response.StatusCode}");
                    }
                }
            }

            public static bool Delete(int id)
            {
                using (var client = new HttpClient())
                {
                    var response = client.DeleteAsync($"{EndPoint}/{id}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception($"Error: {response.StatusCode}");
                    }
                }
            }
        }
    }

