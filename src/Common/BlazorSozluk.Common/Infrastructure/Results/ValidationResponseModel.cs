﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Infrastructure.Results
{
    public class ValidationResponseModel
    {
        public ValidationResponseModel(IEnumerable<string> errors)
        {
            Errors = errors;
        }
        public ValidationResponseModel(string message):this(new List<string>() { message })
        {
            
        }

        public IEnumerable<string> Errors { get; set; }

        [JsonIgnore]
        public string FlattenError => Errors != null
            ? string.Join(Environment.NewLine, Errors) 
            : string.Empty;
    }
}