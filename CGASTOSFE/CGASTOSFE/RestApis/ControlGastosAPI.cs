using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
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
            _apiBaseUrl = options.Value.ApiBaseUrl;
            _authUser = options.Value.AuthUser;//ya no existiria
            _authPass = options.Value.AuthPass;//ya no existiria
        }

        public async Task<bool> AuthenticateAsync(LoginDto loginDto)//volver publico y consumirlo desde el login del frontend
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest("/Acceso/Login", Method.Post);
            request.AddJsonBody(new LoginDto
            {
                Correo = loginDto.Correo,///no seria necesario el username
                Clave = loginDto.Clave,///no seria necesario el password
            });

            var response = await client.ExecuteAsync<LoginResponseDto>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                _token = response.Data.Token;
                return true;
            }

            return false;
        }

        //private async Task EnsureTokenIsValid()
        //{
        //    if (_token == null || DateTime.UtcNow >= _tokenExpirationTime)
        //    {
        //        await AuthenticateAsync();
        //    }
        //}

        private RestRequest AddAuthentication(RestRequest request)
        {

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
            var request = new RestRequest("/Producto/Lista", Method.Get);
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
            var request = new RestRequest("/Proveedores", Method.Get);
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
            var request = new RestRequest($"Proveedores/{id}", Method.Get);
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
            var request = new RestRequest("Proveedores", Method.Put);
            request.AddJsonBody(proveedorDto);
            AddAuthentication(request);

            var response = await client.ExecuteAsync(request);

            return response.IsSuccessful;
        }

        public async Task<bool> PostProveedoresAsync(ProveedorDto proveedorDto)
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest("Proveedores", Method.Post);
            request.AddJsonBody(proveedorDto);
            AddAuthentication(request);

            var response = await client.ExecuteAsync(request);

            return response.IsSuccessful;
        }

        public async Task<bool> DeleteProveedoresAsync(int id)
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest($"Proveedores/{id}", Method.Delete);
            AddAuthentication(request);

            var response = await client.ExecuteAsync(request);

            return response.IsSuccessful;
        }

        //******************************************************INVENTARIODTO******************************************************//

        public async Task<List<InventarioDto>> GetInventariosAsync()
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest("Inventario/Lista", Method.Get);
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
