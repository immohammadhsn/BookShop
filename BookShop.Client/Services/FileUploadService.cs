namespace BookShop.Client.Services
{
    using Microsoft.AspNetCore.Components.Forms;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class FileUploadService(HttpClient _httpClient)
    {
        public async Task<string?> UploadFile(IBrowserFile file)
        {
            if (file != null)
            {
                var buffer = new byte[file.Size];
                await file.OpenReadStream().CopyToAsync(new MemoryStream(buffer));

                var content = new MultipartFormDataContent
                {
                    { new ByteArrayContent(buffer), "file", file.Name }
                };

                var response = await _httpClient.PostAsync("/api/FileUpload", content);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
            throw new Exception();
        }
    }

}
