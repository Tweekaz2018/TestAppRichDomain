using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TestAppRichDomain.Core.Interfaces;

namespace TestApp.FunctionalTests.Helper
{
    class SaveImageMock : ISaveImage
    {
        public async Task<string> SaveImage(string path, string folder, string filename, Stream image)
        {
            return await Task.Run(() => $"\\{folder}\\{filename}");
        }
    }
}
