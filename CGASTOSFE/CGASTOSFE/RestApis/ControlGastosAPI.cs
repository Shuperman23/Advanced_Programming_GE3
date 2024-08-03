using CGASTOSFE.DTOs;
using Microsoft.Extensions.Options;
using RestSharp;

namespace CGASTOSFE.RestApis
{
    public class ControlGastosAPI
    {
        private readonly string _apiBaseUrl;
        private readonly string _authUser;
        private readonly string _authPass;
        private string? _token;
        private DateTime _tokenExpirationTime = DateTime.MinValue;
        private readonly object _tokenLock = new object();


        public ControlGastosAPI(IOptions<ControlGastosApiSettingsDto> options)
        {
            _apiBaseUrl = options.Value.ApiBaseurl;
            _authUser = options.Value.AuthUser;
            _authPass = options.Value.AuthPass;

            AuthenticateAsync().GetAwaiter().GetResult();
        }

        private async Task<bool> AuthenticateAsync()
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest("Auth", Method.Post);
            request.AddJsonBody(new LoginDto
            {
                Username = _authUser,
                Password = _authPass
            });

            var response = await client.ExecuteAsync<LoginResponseDto>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                _token = response.Data.Token;
                _tokenExpirationTime = DateTime.UtcNow.AddMinutes(5);
                return true;
            }

            return false;
        }

        private async Task EnsureTokenIsValid()
        {
            if (_token == null || DateTime.UtcNow >= _tokenExpirationTime)
            {
                await AuthenticateAsync();
            }
        }

        private RestRequest AddAuthentication(RestRequest request)
        {
            EnsureTokenIsValid().GetAwaiter().GetResult();

            if (_token != null)
            {
                request.AddHeader("Authorization", $"Bearer {_token}");
            }

            return request;
        }

        //******************************************************PRODUCTODTO******************************************************//
        public async Task<List<ProductoDto>> GetProductosAsync()
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest("Producto", Method.Get);
            AddAuthentication(request);

            var response = await client.ExecuteAsync<List<ProductoDto>>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
            }
            throw new Exception(response.ErrorMessage);
        }

        public async Task<ProductoDto> GetProductosAsync(int id)
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest($"Producto/{id}", Method.Get);
            AddAuthentication(request);

            var response = await client.ExecuteAsync<ProductoDto>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
            }
            throw new Exception(response.ErrorMessage);
        }

        public async Task<bool> PutProductosAsync(ProductoDto productoDto)
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest("Producto", Method.Put);
            request.AddJsonBody(productoDto);
            AddAuthentication(request);

            var response = await client.ExecuteAsync(request);

            return response.IsSuccessful;
        }

        public async Task<bool> PostProductosAsync(ProductoDto productoDto)
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest("Producto", Method.Post);
            request.AddJsonBody(productoDto);
            AddAuthentication(request);

            var response = await client.ExecuteAsync(request);

            return response.IsSuccessful;
        }

        public async Task<bool> DeleteProductosAsync(int id)
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest($"Producto/{id}", Method.Delete);
            AddAuthentication(request);

            var response = await client.ExecuteAsync(request);

            return response.IsSuccessful;
        }

        //******************************************************PROVEEDORDTO******************************************************//

        public async Task<List<ProveedorDto>> GetProveedoresAsync()
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest("Proveedor", Method.Get);
            AddAuthentication(request);

            var response = await client.ExecuteAsync<List<ProveedorDto>>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
            }
            throw new Exception(response.ErrorMessage);
        }

        public async Task<ProveedorDto> GetProveedoresAsync(int id)
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest($"Proveedor/{id}", Method.Get);
            AddAuthentication(request);

            var response = await client.ExecuteAsync<ProveedorDto>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
            }
            throw new Exception(response.ErrorMessage);
        }

        public async Task<bool> PutProveedoresAsync(ProveedorDto proveedorDto)
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest("Proveedor", Method.Put);
            request.AddJsonBody(proveedorDto);
            AddAuthentication(request);

            var response = await client.ExecuteAsync(request);

            return response.IsSuccessful;
        }

        public async Task<bool> PostProveedoresAsync(ProveedorDto proveedorDto)
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest("Proveedor", Method.Post);
            request.AddJsonBody(proveedorDto);
            AddAuthentication(request);

            var response = await client.ExecuteAsync(request);

            return response.IsSuccessful;
        }

        public async Task<bool> DeleteProveedoresAsync(int id)
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest($"Proveedor/{id}", Method.Delete);
            AddAuthentication(request);

            var response = await client.ExecuteAsync(request);

            return response.IsSuccessful;
        }

        //******************************************************INVENTARIODTO******************************************************//

        public async Task<List<InventarioDto>> GetInventariosAsync()
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest("Inventario", Method.Get);
            AddAuthentication(request);

            var response = await client.ExecuteAsync<List<InventarioDto>>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
            }
            throw new Exception(response.ErrorMessage);
        }

        public async Task<InventarioDto> GetInventariosAsync(int id)
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest($"Inventario/{id}", Method.Get);
            AddAuthentication(request);

            var response = await client.ExecuteAsync<InventarioDto>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
            }
            throw new Exception(response.ErrorMessage);
        }

        public async Task<bool> PutInventariosAsync(InventarioDto inventarioDto)
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest("Inventario", Method.Put);
            request.AddJsonBody(inventarioDto);
            AddAuthentication(request);

            var response = await client.ExecuteAsync(request);

            return response.IsSuccessful;
        }   

        public async Task<bool> PostInventariosAsync(InventarioDto inventarioDto)
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest("Inventario", Method.Post);
            request.AddJsonBody(inventarioDto);
            AddAuthentication(request);

            var response = await client.ExecuteAsync(request);

            return response.IsSuccessful;
        }

        public async Task<bool> DeleteInventariosAsync(int id)
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest($"Inventario/{id}", Method.Delete);
            AddAuthentication(request);

            var response = await client.ExecuteAsync(request);

            return response.IsSuccessful;
        }

        //************************************************************************************************************//
    }

}
