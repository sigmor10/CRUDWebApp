using FrontEnd.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace FrontEnd.Pages
{
    public partial class ContactList : ComponentBase
    {
        private List<Contact>? contacts;
        private int skip = 0;
        private int take = 7;

        /// <summary>
        /// Fetches paginated list from backend
        /// </summary>
        /// <returns></returns>
        private async Task LoadContactList()
        {
            contacts = await Http
                .GetFromJsonAsync<List<Contact>>($"api/contacts/{skip}/{take}");
        }

        /// <summary>
        /// Initializes contact list
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await LoadContactList();
        }

        /// <summary>
        /// Next list page
        /// </summary>
        /// <returns></returns>
        private async Task IncrementSkip()
        {
            skip += take;
            await LoadContactList();
            StateHasChanged();
        }

        /// <summary>
        /// Previous list page
        /// </summary>
        /// <returns></returns>
        private async Task DecrementSkip()
        {
            skip -= take;

            if (skip < 0) skip = 0;

            await LoadContactList();
            StateHasChanged();
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
                if (contacts == null) return;
                // Attaches token to the request header and sends the request
                var token = await LocalStorage.GetItemAsync<string>("authToken");
                var request = new HttpRequestMessage(HttpMethod.Delete, $"api/contacts/{id}/delete");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await Http.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var contact = contacts.First(x => x.Id == id);
                    if (contact != null)
                    {
                        contacts.Remove(contact);
                        StateHasChanged();
                    }
                }
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
        /// Redirects to Contact's details view
        /// </summary>
        /// <param name="id">Contact's Guid</param>
        private void RedirectToDetails(Guid id)
        {
            Navigation.NavigateTo($"contacts/{id}");
        }

        /// <summary>
        /// Redirects to add contact view
        /// </summary>
        private void RedirectToAdd()
        {
            if (AuthService.IsLoggedIn)
                Navigation.NavigateTo($"contacts/add");
        }
    }
}
