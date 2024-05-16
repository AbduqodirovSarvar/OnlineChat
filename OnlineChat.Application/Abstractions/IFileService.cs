using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.Abstractions
{
    public interface IFileService
    {
        Task<string?> SaveFileAsync(IFormFile? uri);
        Task RemoveFileAsync(string? fileName);
        Stream? GetFileByFileName(string fileName);
        public string GetFilePath(string fileName);
    }
}
