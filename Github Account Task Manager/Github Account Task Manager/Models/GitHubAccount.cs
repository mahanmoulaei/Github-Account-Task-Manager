using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Github_Account_Task_Manager.Models
{
    internal class GitHubAccount
    {
        [JsonProperty(PropertyName = "mahanmoulaei")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "avatar_url")]
        public string Avatar { get; set; }
    }
}
