using System;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;

namespace PodeKestrel
{
    public class PodeFormData
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public PodeFormData(KeyValuePair<string, StringValues> value)
        {
            Key = value.Key;
            Value = value.Value[0];
        }
        public PodeFormData(IFormFile file)
        {
            Key = file.Name;
            Value = file.FileName;
        }
    }
}