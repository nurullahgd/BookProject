namespace BookProject.Data.Entities
{
    public class AccountHashes
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}