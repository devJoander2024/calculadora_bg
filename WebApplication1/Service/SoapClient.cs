using System.Net.Http;
using System.Text;
using System.Xml;

namespace WebApplication1.Service
{
    public class SoapClient
    {
        private readonly HttpClient _httpClient;

        public SoapClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> RealizarOperacion(string operacion, int valor1, int valor2)
        {
            // Validación de operación
            if (string.IsNullOrWhiteSpace(operacion) ||
                !(operacion.Equals("sumar", StringComparison.OrdinalIgnoreCase) ||
                  operacion.Equals("restar", StringComparison.OrdinalIgnoreCase) ||
                  operacion.Equals("multiplicar", StringComparison.OrdinalIgnoreCase) ||
                  operacion.Equals("dividir", StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("Operación no válida.");
            }

            // Validación de división por cero
            if (operacion.Equals("dividir", StringComparison.OrdinalIgnoreCase) && valor2 == 0)
            {
                throw new ArgumentException("No se puede dividir por cero.");
            }

            // Realizar la operación correspondiente
            switch (operacion.ToLower())
            {
                case "sumar":
                    return await Sumar(valor1, valor2);
                case "restar":
                    return await Restar(valor1, valor2);
                case "multiplicar":
                    return await Multiplicar(valor1, valor2);
                case "dividir":
                    return await Dividir(valor1, valor2);
                default:
                    throw new ArgumentException("Operación no válida.");
            }
        }

        private async Task<int> Sumar(int valor1, int valor2)
        {
            string soapRequest = $@"
            <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'
                              xmlns:web='http://tempuri.org/'>
                <soapenv:Header/>
                <soapenv:Body>
                    <web:Add>
                        <web:intA>{valor1}</web:intA>
                        <web:intB>{valor2}</web:intB>
                    </web:Add>
                </soapenv:Body>
            </soapenv:Envelope>";

            StringContent content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
            HttpResponseMessage response = await _httpClient.PostAsync("http://www.dneonline.com/calculator.asmx", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error en la llamada al servicio SOAP");

            string responseXml = await response.Content.ReadAsStringAsync();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(responseXml);

            XmlNamespaceManager nsManager = new XmlNamespaceManager(doc.NameTable);
            nsManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            nsManager.AddNamespace("web", "http://tempuri.org/");

            XmlNode resultadoNode = doc.SelectSingleNode("//soap:Body/web:AddResponse/web:AddResult", nsManager);

            if (resultadoNode == null)
            {
                throw new Exception("No se encontró el nodo AddResult en la respuesta del servicio SOAP.");
            }

            string resultado = resultadoNode.InnerText;

            if (!int.TryParse(resultado, out int resultadoFinal))
            {
                throw new Exception("El resultado recibido no es un número válido.");
            }

            return resultadoFinal;
        }

        private async Task<int> Restar(int valor1, int valor2)
        {
            string soapRequest = $@"
            <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'
                              xmlns:web='http://tempuri.org/'>
                <soapenv:Header/>
                <soapenv:Body>
                    <web:Subtract>
                        <web:intA>{valor1}</web:intA>
                        <web:intB>{valor2}</web:intB>
                    </web:Subtract>
                </soapenv:Body>
            </soapenv:Envelope>";

            StringContent content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
            HttpResponseMessage response = await _httpClient.PostAsync("http://www.dneonline.com/calculator.asmx", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error en la llamada al servicio SOAP");

            string responseXml = await response.Content.ReadAsStringAsync();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(responseXml);

            XmlNamespaceManager nsManager = new XmlNamespaceManager(doc.NameTable);
            nsManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            nsManager.AddNamespace("web", "http://tempuri.org/");

            XmlNode resultadoNode = doc.SelectSingleNode("//soap:Body/web:SubtractResponse/web:SubtractResult", nsManager);
            string resultado = resultadoNode.InnerText;

            if (!int.TryParse(resultado, out int resultadoFinal))
            {
                throw new Exception("El resultado recibido no es un número válido.");
            }

            return resultadoFinal;
        }

        private async Task<int> Multiplicar(int valor1, int valor2)
        {
            string soapRequest = $@"
            <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'
                              xmlns:web='http://tempuri.org/'>
                <soapenv:Header/>
                <soapenv:Body>
                    <web:Multiply>
                        <web:intA>{valor1}</web:intA>
                        <web:intB>{valor2}</web:intB>
                    </web:Multiply>
                </soapenv:Body>
            </soapenv:Envelope>";

            StringContent content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
            HttpResponseMessage response = await _httpClient.PostAsync("http://www.dneonline.com/calculator.asmx", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error en la llamada al servicio SOAP");

            string responseXml = await response.Content.ReadAsStringAsync();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(responseXml);

            XmlNamespaceManager nsManager = new XmlNamespaceManager(doc.NameTable);
            nsManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            nsManager.AddNamespace("web", "http://tempuri.org/");

            XmlNode resultadoNode = doc.SelectSingleNode("//soap:Body/web:MultiplyResponse/web:MultiplyResult", nsManager);
            string resultado = resultadoNode.InnerText;

            if (!int.TryParse(resultado, out int resultadoFinal))
            {
                throw new Exception("El resultado recibido no es un número válido.");
            }

            return resultadoFinal;
        }

        private async Task<int> Dividir(int valor1, int valor2)
        {
            string soapRequest = $@"
            <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'
                              xmlns:web='http://tempuri.org/'>
                <soapenv:Header/>
                <soapenv:Body>
                    <web:Divide>
                        <web:intA>{valor1}</web:intA>
                        <web:intB>{valor2}</web:intB>
                    </web:Divide>
                </soapenv:Body>
            </soapenv:Envelope>";

            StringContent content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
            HttpResponseMessage response = await _httpClient.PostAsync("http://www.dneonline.com/calculator.asmx", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error en la llamada al servicio SOAP");

            string responseXml = await response.Content.ReadAsStringAsync();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(responseXml);

            XmlNamespaceManager nsManager = new XmlNamespaceManager(doc.NameTable);
            nsManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            nsManager.AddNamespace("web", "http://tempuri.org/");

            XmlNode resultadoNode = doc.SelectSingleNode("//soap:Body/web:DivideResponse/web:DivideResult", nsManager);
            string resultado = resultadoNode.InnerText;

            if (!int.TryParse(resultado, out int resultadoFinal))
            {
                throw new Exception("El resultado recibido no es un número válido.");
            }

            return resultadoFinal;
        }
    }
}
