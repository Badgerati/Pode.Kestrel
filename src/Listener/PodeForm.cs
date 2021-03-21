using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace PodeKestrel
{
    public class PodeForm
    {
        public IList<PodeFormFile> Files { get; private set; }
        public IList<PodeFormData> Data { get; private set; }

        public PodeForm(IFormCollection form)
        {
            Files = new List<PodeFormFile>();
            Data = new List<PodeFormData>();

            foreach (var file in form.Files)
            {
                Files.Add(new PodeFormFile(file));
                Data.Add(new PodeFormData(file));
            }

            foreach (var item in form)
            {
                Data.Add(new PodeFormData(item));
            }
        }
    }
}