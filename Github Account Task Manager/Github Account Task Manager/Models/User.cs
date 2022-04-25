using SQLite;

namespace Github_Account_Task_Manager.Models
{
    internal class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
