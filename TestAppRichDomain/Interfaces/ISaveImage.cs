using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TestAppRichDomain.Core.Interfaces
{
    public interface ISaveImage
    {
        Task<string> SaveImage(string path, string folder, string filename, Stream image);
    }
}
