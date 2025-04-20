using FrontEnd.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace FrontEnd.Pages
{
    public partial class ContactDetails :ComponentBase
    {
        [Parameter]
        public Guid id { get; set; }
        private GetContactResponse? contact;

        /// <summary>
        /// Fetches paginated list from backend
        /// </summary>
        /// <returns></returns> 
        private async Task LoadContactList()
        {
            contact = await Http
                .GetFromJsonAsync<GetContactResponse>($"api/contacts/{id}");
        }

        /// <summary>
        /// Initializes contact list
        /// </summary>
        /// <returns></returns> 
        protected override async Task OnParametersSetAsync()
        {
            await LoadContactList();
        }

        /// <summary>
        /// Sends delete request
        /// </summary>
        /// <param name="id">Contact's Guid</param>
        /// <returns></returns>
        private async Task DeleteContact(Guid id)
        {
            if (AuthService.IsLoggedIn)
            {
                if (contact == null) return;
                var token = await LocalStorage.GetItemAsync<string>("authToken");
                var request = new HttpRequestMessage(HttpMethod.Delete, $"api/contacts/{id}/delete");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await Http.SendAsync(request);
                Navigation.NavigateTo("/");
            }
        }

        /// <summary>
        /// Redirects to Contact's edit view
        /// </summary>
        /// <param name="id">Contact's Guid</param>
        private void RedirectToEdit(Guid id)
        {
            if (AuthService.IsLoggedIn)
                Navigation.NavigateTo($"contacts/{id}/edit");
        }

        /// <summary>
        /// Redirects to contact list view
        /// </summary>
        private void RedirectToList()
        {
            Navigation.NavigateTo("contacts");
        }
    }
}
