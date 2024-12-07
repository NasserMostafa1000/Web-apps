using SalamaTravelDTA;

namespace SalamaTravelBA
{

    public class ClientsBL
    {
        public class GetByIdReq
        {
            public int ClientId { get; set; }
            public string Token { get; set; }
        }
        public static async Task<bool> DeleteImageAsync(string imagePath)
        {
            try
            {
                // التحقق من وجود الملف
                if (File.Exists(imagePath))
                {
                    // تشغيل الحذف كعملية غير متزامنة
                    await Task.Run(() => File.Delete(imagePath)); // حذف الملف
                    return true; // تم الحذف بنجاح
                }
                else
                {
                    Console.WriteLine("File not found.");
                    return false; // الملف غير موجود
                }
            }
            catch (Exception ex)
            {
                // معالجة أي خطأ يحدث أثناء عملية الحذف
                Console.WriteLine($"Error deleting file: {ex.Message}");
                return false;
            }
        }
    
        public class ClientsDTO
        {
            public int ClientID { get; set; }
            public string FirstName { get; set; }
            public string MidName { get; set; }
            public string LastName { get; set; }

            public string Email { get; set; }
            public string Password { get; set; }
            public string PhoneNumber { get; set; }
        }
        public class PutClient
        {
            public int ClientID { get; set; }
            public string FirstName { get; set; }
            public string MidName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public string Token { get; set; }

        }

        public static async Task<List<SalamaTravelDTA.Clients.ClientsDTO>>  GetAllClientsAsync(string Token,int ClientId)
        {
            List<SalamaTravelDTA.Clients.ClientsDTO> Clients =await SalamaTravelDTA.Clients.GetAllClientsAsync(Token,ClientId);
            
            return Clients.Count>0?Clients:null;
        }
        public static async Task<int>PostClientAsync(ClientsDTO Client)
        {
            try
            {
                int Result = await SalamaTravelDTA.Clients.PostClientAsync(Client.FirstName, Client.MidName, Client.Email, Client.PhoneNumber, Client.LastName, Client.Password);
                return Result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        public static async Task<bool>PutClientAsync(ClientsBL.PutClient Client)
        {
            return await SalamaTravelDTA.Clients.PutClientASync(Client.ClientID, Client.Token, Client.FirstName, Client.MidName, Client.LastName, Client.PhoneNumber);
        }
        public static async Task<bool>DeleteLastPersonalImageAsync(int ClientId)
        {
            string FilePath = await Clients.GetPersonalImageWithClientIdAsync(ClientId);
            if(string.IsNullOrEmpty(FilePath))
            {
                return true;
            }
            return await DeleteImageAsync(FilePath);

        }
        public static async Task<bool> DeleteLastPassportImageAsync(int ClientId)
        {
            string FilePath = await Clients.GetPassportImageWithClientIdAsync(ClientId);
            if (string.IsNullOrEmpty(FilePath))
            {
                return true;
            }
            return await DeleteImageAsync(FilePath);

        }
    }
}
