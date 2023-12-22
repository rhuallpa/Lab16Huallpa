using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using lab16Huallpa.Model; // Asegúrate de agregar la referencia al espacio de nombres de tu modelo-HUALLPA

namespace lab16Huallpa
{
    public partial class MainPage : ContentPage
    {
        private const string BaseUrl = "https://jsonplaceholder.typicode.com";

        public MainPage()
        {
            InitializeComponent();
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                HttpResponseMessage response = await client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    T result = JsonConvert.DeserializeObject<T>(json);
                    return result;
                }
                else
                {
                    throw new Exception($"Error en la solicitud: {response.StatusCode}");
                }
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await GetAsync<List<Post>>("/posts");


                dataListView.ItemsSource = result;

            }
            catch (Exception ex)
            {
                // Manejar errores
                _ = DisplayAlert("Error", $"Error: {ex.Message}", "OK");
            }
        }
    }
}
