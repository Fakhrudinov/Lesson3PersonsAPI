namespace DataLayer.Abstraction.Entityes
{
    public sealed class AuthResponse
    {
        public string Password { get; set; }
        public RefreshToken LatestRefreshToken { get; set; }
    }
}
