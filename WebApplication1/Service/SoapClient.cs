using System.Net.Http;
using System.Text;
using System.Xml;

namespace WebApplication1.Service
{
    public class SoapClient { 
     private readonly HttpClient _httpClient;

    public SoapClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

        public async Task<int> Sumar(int valor1, int valor2)
        {
            var soapRequest = $@"
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

            var content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");

            var response = await _httpClient.PostAsync("http://www.dneonline.com/calculator.asmx", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error en la llamada al servicio SOAP");

            var responseXml = await response.Content.ReadAsStringAsync();

            var doc = new XmlDocument();
            doc.LoadXml(responseXml);

            // Crear un XmlNamespaceManager para manejar el espacio de nombres
            var nsManager = new XmlNamespaceManager(doc.NameTable);
            nsManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            nsManager.AddNamespace("web", "http://tempuri.org/");

            // Buscar el nodo AddResult dentro de la respuesta de Add
            var resultadoNode = doc.SelectSingleNode("//soap:Body/web:AddResponse/web:AddResult", nsManager);

            if (resultadoNode == null)
            {
                throw new Exception("No se encontró el nodo AddResult en la respuesta del servicio SOAP.");
            }

            var resultado = resultadoNode.InnerText;

            if (!int.TryParse(resultado, out var resultadoFinal))
            {
                throw new Exception("El resultado recibido no es un número válido.");
            }

            return resultadoFinal;
        }

        public async Task<int> Restar(int valor1, int valor2)
        {
            var soapRequest = $@"
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

            var content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
            var response = await _httpClient.PostAsync("http://www.dneonline.com/calculator.asmx", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error en la llamada al servicio SOAP");

            var responseXml = await response.Content.ReadAsStringAsync();
            var doc = new XmlDocument();
            doc.LoadXml(responseXml);

            // Usar un XmlNamespaceManager
            var nsManager = new XmlNamespaceManager(doc.NameTable);
            nsManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            nsManager.AddNamespace("web", "http://tempuri.org/");

            // Buscar el nodo SubtractResult
            var resultadoNode = doc.SelectSingleNode("//soap:Body/web:SubtractResponse/web:SubtractResult", nsManager);
            var resultado = resultadoNode.InnerText;

            if (!int.TryParse(resultado, out var resultadoFinal))
            {
                throw new Exception("El resultado recibido no es un número válido.");
            }

            return resultadoFinal;
        }

        public async Task<int> Multiplicar(int valor1, int valor2)
        {
            var soapRequest = $@"
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

            var content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
            var response = await _httpClient.PostAsync("http://www.dneonline.com/calculator.asmx", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error en la llamada al servicio SOAP");

            var responseXml = await response.Content.ReadAsStringAsync();
            var doc = new XmlDocument();
            doc.LoadXml(responseXml);

            // Usar un XmlNamespaceManager
            var nsManager = new XmlNamespaceManager(doc.NameTable);
            nsManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            nsManager.AddNamespace("web", "http://tempuri.org/");

            // Buscar el nodo MultiplyResult
            var resultadoNode = doc.SelectSingleNode("//soap:Body/web:MultiplyResponse/web:MultiplyResult", nsManager);
            var resultado = resultadoNode.InnerText;

            if (!int.TryParse(resultado, out var resultadoFinal))
            {
                throw new Exception("El resultado recibido no es un número válido.");
            }

            return resultadoFinal;
        }


        public async Task<int> Dividir(int valor1, int valor2)
        {
            var soapRequest = $@"
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

            var content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
            var response = await _httpClient.PostAsync("http://www.dneonline.com/calculator.asmx", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error en la llamada al servicio SOAP");

            var responseXml = await response.Content.ReadAsStringAsync();
            var doc = new XmlDocument();
            doc.LoadXml(responseXml);

            // Usar un XmlNamespaceManager
            var nsManager = new XmlNamespaceManager(doc.NameTable);
            nsManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            nsManager.AddNamespace("web", "http://tempuri.org/");

            // Buscar el nodo DivideResult
            var resultadoNode = doc.SelectSingleNode("//soap:Body/web:DivideResponse/web:DivideResult", nsManager);
            var resultado = resultadoNode.InnerText;

            if (!int.TryParse(resultado, out var resultadoFinal))
            {
                throw new Exception("El resultado recibido no es un número válido.");
            }

            return resultadoFinal;
        }

    }
}