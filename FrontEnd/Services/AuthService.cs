using Blazored.LocalStorage;
using FrontEnd.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;

namespace FrontEnd.Services
{
    /// <summary>
    /// Authentication and jwt token handling class
    /// </summary>
    public class AuthService
    {
        private readonly ILocalStorageService _localStorage;
        public event Action? OnChange;
        public bool IsLoggedIn { get; private set; }
        public string? UserEmail { get; private set; }

        public AuthService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        /// <summary>
        /// Initializes token and authorization mechanics
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            // Checks if token was properly fownloaded
            if (!string.IsNullOrWhiteSpace(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
                var emailClaim = jwtToken?.Claims.FirstOrDefault(c =>
                    c.Type == ClaimTypes.Email || c.Type == "email");

                UserEmail = emailClaim?.Value;
                IsLoggedIn = true;
            }
            else
            {
                IsLoggedIn = false;
                UserEmail = null;
            }

            NotifyStateChanged();
        }

        /// <summary>
        /// Puts token in local storage
        /// </summary>
        /// <param name="token">Jwt token</param>
        /// <returns></returns>
        public async Task SetToken(string token)
        {
            await _localStorage.SetItemAsync("authToken", token);
            await InitializeAsync();
        }

        /// <summary>
        /// Logs user out and deletes token
        /// </summary>
        /// <returns></returns>
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            IsLoggedIn = false;
            UserEmail = null;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

}
