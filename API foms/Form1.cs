using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;


namespace API_foms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string patron = @"^-?\d*(\.\d+)?$"; // Esta expresión regular permite solo números

            // Crea un objeto Regex
            Regex regex = new Regex(patron);

            // Realiza la validación
            bool esValido = regex.IsMatch(textBox1.Text);
            bool esValido1 = regex.IsMatch(textBox2.Text);

            // Muestra el resultado
            if (esValido & esValido1)
            {
                string apiUrl = $"http://localhost/Php_API/API.Php?x={textBox1.Text}&y={textBox2.Text}";

                // Crear una instancia de HttpClient (debe ser reutilizada en toda la aplicación)
                using (HttpClient httpClient = new HttpClient())
                {
                    try
                    {
                        // Realizar la solicitud GET a la API
                        HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                        // Verificar si la solicitud fue exitosa (código de estado 200)
                        if (response.IsSuccessStatusCode)
                        {
                            // Leer el contenido de la respuesta como una cadena
                            string responseBody = await response.Content.ReadAsStringAsync();

                            // Deserializar la respuesta JSON en un objeto
                            var responseObject = JsonConvert.DeserializeObject<MyApiResponse>(responseBody);

                            // Acceder al valor de "data" en el objeto
                            string data = responseObject.data;

                            // Puedes trabajar con el valor de "data" aquí
                            label3.Text=$"Resultado: {data}";

                        }
                        else
                        {
                            label3.Text=($"Error en la solicitud. Código de estado: {response.StatusCode}");
                        }
                    }
                    catch (Exception ex)
                    {
                        label3.Text=($"Error en la solicitud: {ex.Message}");
                    }
                }
            }
            else
            {
                label3.Text=("La cadena no es válida o contiene caracteres que no son números.");
            }
        }
    }
        public class MyApiResponse
        {
            public string data { get; set; }
        }
    }
