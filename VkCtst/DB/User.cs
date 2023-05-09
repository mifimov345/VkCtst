namespace VkCtst.DB
{
    public class User
    {
        public int id { get; set; }
        public string? login { get; set; }
        public string? password { get; set; }
        public DateTime created_date { get; set; }
        public User_Group? user_group_id { get; set; }
        public User_State? user_state_id { get; set; }
    }
}
