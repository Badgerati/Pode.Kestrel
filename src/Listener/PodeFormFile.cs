using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace PodeKestrel
{
    public class PodeFormFile
    {
        public string ContentType => FormFile.ContentType;
        public string FileName => FormFile.FileName;
        public string Name => FormFile.Name;

        public byte[] Bytes
        {
            get => GetBytes();
        }

        private IFormFile FormFile;

        public PodeFormFile(IFormFile file)
        {
            FormFile = file;
        }

        private byte[] GetBytes()
        {
            using (var stream = new MemoryStream())
            {
                FormFile.CopyTo(stream);
                return stream.ToArray();
            }
        }

        public void Save(string path)
        {
            using (var file = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                FormFile.CopyTo(file);
            }
        }
    }
}