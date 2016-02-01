﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    [Serializable]
    public class User
    {
        [JsonProperty("Username")]
        public string UserName;
        [JsonProperty("Password")]
        public string Password;
        [JsonProperty("Balance")]
        public double Balance;
    }
}
