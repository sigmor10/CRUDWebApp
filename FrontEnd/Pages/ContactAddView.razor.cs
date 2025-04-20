using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using FrontEnd.Models;
using FrontEnd.Services;

namespace FrontEnd.Pages
{
    /// <summary>
    /// Code behind of ContactAddView
    /// </summary>
    public partial class ContactAddView : ComponentBase
    {
        private CreateContactRequest? contact = new();
        private List<Category>? categories;
        private List<SubCategory>? subCats;
        private List<SubCategory> filteredSubCats = new();
        private bool emailExists = false;

        private bool IsCustomSubCategory = false;

        // Idiomatic expressions for automatic changes in subcategory input/selection
        private string _selectedSubCategory;
        private string selectedSubCategory
        {
            get => _selectedSubCategory;
            set
            {
                if (_selectedSubCategory != value)
                {
                    _selectedSubCategory = value;
                    contact.SubCategory = selectedSubCategory;
                }
            }
        }
        private int _selectedCategory;
        public int selectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    FilterSubCategories();
                }
            }
        }

        /// <summary>
        /// Redirects unauthorized users to the login page
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            if (!AuthService.IsLoggedIn)
            {
                Navigation.NavigateTo("/login");
            }
        }

        /// <summary>
        /// Sets Entity and form to default values
        /// </summary> 
        private void SetToDefaultValues()
        {
            contact = new();
            selectedCategory = categories.Select(c => c.Id).First();

            contact.CategoryId = selectedCategory;
            contact.SubCategory = subCats
                .Where(x => x.CategoryId == selectedCategory)
                .Select(c => c.Name)
                .First();
        }

        /// <summary>
        /// Fetches paginated list from backend
        /// </summary>
        /// <returns></returns> 
        private async Task LoadContent()
        {
            categories = await Http
                .GetFromJsonAsync<List<Category>>($"api/categories");
            subCats = await Http
                .GetFromJsonAsync<List<SubCategory>>($"api/subcategories");
            selectedCategory = categories.Select(c => c.Id).First();

            contact.CategoryId = selectedCategory;
            contact.SubCategory = subCats
                .Where(x => x.CategoryId == selectedCategory)
                .Select(c => c.Name)
                .First();
        }

        /// <summary>
        /// Initializes contact list
        /// </summary>
        /// <returns></returns> 
        protected override async Task OnParametersSetAsync()
        {
            await LoadContent();
        }

        /// <summary>
        /// Filters SubCategories to select only one viable for given category
        /// </summary> 
        private void FilterSubCategories()
        {
            filteredSubCats = subCats
                .Where(x => x.CategoryId == selectedCategory)
                .ToList();

            IsCustomSubCategory = filteredSubCats.Count == 0;

            if (filteredSubCats.Count != 0)
                selectedSubCategory = filteredSubCats.Select(x => x.Name).First();

            StateHasChanged();
        }

        /// <summary>
        /// Checks if given email is already in use by other user
        /// </summary>
        /// <returns>bool saying whether email is valid and not taken</returns> 
        private async Task<bool> ValidateEmael()
        {
            var encodedEmail = Uri.EscapeDataString(contact.Email);
            var result = await Http
                .GetFromJsonAsync<bool>($"api/email?email={encodedEmail}");

            emailExists = result;
            return emailExists;
        }

        /// <summary>
        /// Saves edited contact
        /// </summary>
        /// <returns></returns> 
        private async Task SaveContact()
        {
            if (contact == null) return;

            if (await ValidateEmael())
            {
                StateHasChanged();
                return;
            }

            contact.CategoryId = selectedCategory;
            contact.SubCategory = selectedSubCategory;

            // Generates request body
            var json = JsonSerializer.Serialize(contact);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Adding token to the header and sending the request
            var token = await LocalStorage.GetItemAsync<string>("authToken");
            var request = new HttpRequestMessage(HttpMethod.Post, $"api/contacts/add");
            request.Content = content;
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await Http.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Navigation.NavigateTo($"/contacts");
            }
            else
            {
                Console.WriteLine("Failed to add new contact");
            }
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
