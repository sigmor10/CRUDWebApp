using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using FrontEnd.Models;

namespace FrontEnd.Pages
{
    public partial class ContactEditView : ComponentBase
    {
        [Parameter]
        public Guid id { get; set; }
        private GetContactResponse? ogContact; // original contact
        private UpdateContactRequest? contact; // new contact object
        private List<Category>? categories;
        private List<SubCategory>? subCats;
        private List<SubCategory> filteredSubCats = new(); // list of viable subcategories for current category
        private bool emailExists = false;

        private bool IsCustomSubCategory = false;
        // Idiomatic expressions for automatic changes in subcategory input/selection
        private string _selectedSubCategory;
        private string selectedSubCategory
        {
            get => _selectedSubCategory;
            set => _selectedSubCategory = value;
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
            var categoryId = categories
                            .Where(x => x.Name.Equals(ogContact.Category))
                            .Select(x => x.Id)
                            .First();

            contact = new UpdateContactRequest
            {
                Id = id,
                Name = ogContact.Name,
                Surname = ogContact.Surname,
                Email = ogContact.Email,
                Password = "", // keep it blank unless user sets it
                CategoryId = categoryId,
                SubCategory = ogContact.SubCategory,
                Phone = ogContact.Phone,
                BirthDate = ogContact.BirthDate
            };
            selectedCategory = categoryId;
            selectedSubCategory = ogContact.SubCategory;
        }

        /// <summary>
        /// Fetches paginated list from backend
        /// </summary>
        /// <returns></returns> 
        private async Task LoadContent()
        {
            // Fetches needed lists and entities from backend
            ogContact = await Http
                .GetFromJsonAsync<GetContactResponse>($"api/contacts/{id}");
            categories = await Http
                .GetFromJsonAsync<List<Category>>($"api/categories");
            subCats = await Http
                .GetFromJsonAsync<List<SubCategory>>($"api/subcategories");

            //Initializes contact used for request body
            if (ogContact != null && categories != null)
            {
                SetToDefaultValues();
                FilterSubCategories();
            }
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
        /// <returns></returns>
        private async Task<bool> ValidateEmael()
        {
            var encodedEmail = Uri.EscapeDataString(contact.Email);
            var result = await Http
                .GetFromJsonAsync<bool>($"api/email?email={encodedEmail}");

            emailExists = !ogContact.Email.Equals(contact.Email) && result;
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

            // Attaches token to the header and sends request
            var token = await LocalStorage.GetItemAsync<string>("authToken");
            var request = new HttpRequestMessage(HttpMethod.Patch, $"api/contacts/{id}/update");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = content;

            var response = await Http.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Navigation.NavigateTo($"/contacts/{id}");
            }
            else
            {
                Console.WriteLine("Failed to save changes");
            }
        }

        /// <summary>
        /// Redirects to Contact's edit view
        /// </summary>
        /// <param name="id">Contact Guid</param>
        private void RedirectToDetails(Guid id)
        {
            Navigation.NavigateTo($"contacts/{id}");
        }
    }
}
