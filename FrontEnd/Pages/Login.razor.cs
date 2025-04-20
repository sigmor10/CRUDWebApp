using FrontEnd.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace FrontEnd.Pages
{
    public partial class Login : ComponentBase
    {
        private User user = new();

        /// <summary>
        /// Redirects logged in users to homepage
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            if (AuthService.IsLoggedIn)
            {
                Navigation.NavigateTo("/");
            }
        }

        /// <summary>
        /// Sends Login request
        /// </summary>
        /// <returns></returns>
        private async Task LogIn()
        {
            if (user == null) return;

            // Sends login request and parses response's body
            var response = await Http.PostAsJsonAsync($"api/auth/login", user);

            // Checks whether response contained Jwt Token, if so, saves it to local storage
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                await AuthService.SetToken(token);
                Navigation.NavigateTo($"/contacts");
            }
            else
            {
                Console.WriteLine("Failed to add contact");
            }
        }
    }
}
