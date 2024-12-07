using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SalamaTravelDAL;

namespace SalamTravelDAL
{
    public static class ClientsDAL
    {
        public class ClientsDTO
        {
            public int ClientID { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public decimal balance { get; set; }
            public string PhoneNumber { get; set; }
           public string PersonalImagePath { get; set; }
            public string PassportImagePath { get; set; }

        }
        public static async Task<List<ClientsDTO>> FetchClientsDataAsync(string token, int clientId)
        {
            try
            {
                // تحديد عنوان URL الخاص بـ API
                string url = $"Clients/FetchClientsData?Token={Uri.EscapeDataString(token)}&ClientId={clientId}";

                // إرسال طلب GET للحصول على بيانات العملاء
                var response = await Settings.HttpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // قراءة البيانات المسترجعة من API
                    var content = await response.Content.ReadAsStringAsync();

                    // تحويل البيانات من JSON إلى قائمة من الكائنات ClientsDTO
                    return JsonConvert.DeserializeObject<List<ClientsDTO>>(content);
                }
                else
                {
                    // إذا كانت استجابة API غير ناجحة
                    return null;
                }
            }
            catch (Exception ex)
            {
                // التعامل مع الأخطاء
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
        public static async Task<int> PostClientAsync(ClientsDTO dto)
        {
            try
            {
                // تحديد عنوان URL
                string url = "Clients/PostNewClient";

                // تحويل الـ DTO إلى JSON
                var jsonContent = JsonConvert.SerializeObject(dto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // إرسال طلب POST مع بيانات العميل
                var response = await Settings.HttpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var id = JsonConvert.DeserializeObject<int>(result);
                    return id; // إرجاع الـ ID
                }
                else
                {
                    // إذا كانت الاستجابة غير ناجحة
                    return -1;
                }
            }
            catch (Exception ex)
            {
                // التعامل مع الأخطاء
                Console.WriteLine($"Error: {ex.Message}");
                return -1;
            }
        }
        public static async Task<bool> PutClientAsync(ClientsDTO dto, string token)
        {
            try
            {
                string url = "Clients/PutClient";

                var requestData = new
                {
                    Dto = dto,
                    Token = token
                };

                var jsonContent = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await Settings.HttpClient.PutAsync(url, content);
                return response.IsSuccessStatusCode; 
            }
            catch (Exception ex)
            {
                // التعامل مع الأخطاء
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }


        }
    }
    }

