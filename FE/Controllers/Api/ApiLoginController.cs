﻿using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FE.Controllers.Api
{
    public class ApiLoginController : Controller
    {
        private readonly HttpClient _httpClient;

        public ApiLoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var json = JsonSerializer.Serialize(loginRequest);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7196/rest/v1/users/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return Ok(responseData);
            }
            else
            {
                return BadRequest("Login failed");
            }
        }
    }
}
