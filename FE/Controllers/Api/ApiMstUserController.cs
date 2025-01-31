﻿using FE.DTO.Req;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Text.Json;

namespace PeerLandingFE.Controllers.api
{
    public class ApiMstUserController : Controller
    {
        private readonly HttpClient _httpClient;

        public ApiMstUserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("https://localhost:7196/rest/v1/users");

            if (response.IsSuccessStatusCode)
            {
                var responData = await response.Content.ReadAsStringAsync();
                return Ok(responData);
            }
            else
            {
                return BadRequest("Get All Users Failed");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("User id cannot be null or empty");
            }
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"https://localhost:7196/rest/v1/users/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responData = await response.Content.ReadAsStringAsync();
                return Ok(responData);
            }
            else
            {
                return BadRequest("Fetch User Failed");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] ReqMstUserDto reqMstUserDto)
        {
            if (reqMstUserDto == null)
            {
                return BadRequest("Invalid user data");
            }

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(reqMstUserDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"https://localhost:7196/rest/v1/users/{id}", content);
            Console.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                return Ok(jsonData);
            }
            else
            {
                return BadRequest("Fetch User Failed");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.DeleteAsync($"https://localhost:7196/rest/v1/users/{id}");

            if (response.IsSuccessStatusCode)
            {
                return Ok("User deleted successfully.");
            }
            else
            {
                return BadRequest("Failed to delete user.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] ReqMstUserDto reqMstUserDto)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(reqMstUserDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7196/rest/v1/users/add-user", content);

            if (response.IsSuccessStatusCode)
            {
                var responData = await response.Content.ReadAsStringAsync();
                return Ok(responData);
            }
            else
            {
                return BadRequest("Failed to add user.");
            }
        }
    }
}