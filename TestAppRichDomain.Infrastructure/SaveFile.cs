using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TestAppRichDomain.Core.Interfaces;

namespace TestAppRichDomain.Repository
{
    public class SaveFile : ISaveImage
    {
        public async Task<string> SaveImage(string path, string folder, string filename, Stream image)
        {
            string absolutePath = Path.Combine(path, folder, filename);
            if (File.Exists(absolutePath))
            {
                string fileType = Path.GetExtension(filename);
                absolutePath = Path.Combine(path, folder, $"{Guid.NewGuid().ToString()}{fileType}");
            }
            if (!Directory.Exists(Path.Combine(path, folder)))
                Directory.CreateDirectory(Path.Combine(path, folder));
            using (var fileStream = new FileStream(absolutePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "\\" + Path.Combine(folder, filename);
        }
    }
}
