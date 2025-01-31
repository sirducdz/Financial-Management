﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN231_FinalProject_Client.Models;

namespace PRN231_FinalProject_Client.Pages.Incomes
{
    public class DetailsModel : PageModel
    {
        HttpClient _httpClient;
        private readonly string BASE_URL = "https://localhost:7203";

        public DetailsModel()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

      public Income Income { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var response = await _httpClient.GetAsync(BASE_URL + $"/api/Incomes/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }
            var strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var income = JsonSerializer.Deserialize<Income>(strData, options);

            if (income == null)
            {
                return NotFound();
            }
            else 
            {
                Income = income;
            }
            return Page();
        }
    }
}
