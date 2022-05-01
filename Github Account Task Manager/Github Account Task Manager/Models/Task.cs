using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Github_Account_Task_Manager.Models
{
    public class Task
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string Description { get; set; }
        public string Assigned { get; set; }
        public string Deadline { get; set; }
    }
}
