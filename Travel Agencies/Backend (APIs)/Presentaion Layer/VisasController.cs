﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalamaTravelDTA;
using SalamaTravelBL;

namespace SalamaTravelApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisasController : ControllerBase
    {
        [HttpGet("GetAllVisas")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<VisasDAL.VisaDTO>>> GetVisas()
        {
         
            var visas = await SalamaTravelBL.VisasBL.GetVisasAsync();
            if (visas == null || visas.Count == 0)
                return NotFound("No visas available for issuance.");
            return Ok(visas);
        }
        [HttpPost("PostVisa")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> AddNewVisa([FromBody] VisasDAL.VisaDTO dto,  string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Invalid token.");
            }

            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            // استدعاء الدالة غير المتزامنة من طبقة الـ Business Logic لإضافة الفيزا
            var visaId = await SalamaTravelBL.VisasBL.AddNewVisaAsync(dto, token);

            if (visaId > 0) // إذا تم إضافة الفيزا بنجاح
            {
                return Ok(new { message = "Visa added successfully.", visaId });
            }

            return BadRequest("Failed to add visa.");
        }
        [HttpPut("UpdateVisaInfo")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateVisa([FromBody] VisasDAL.VisaDTO dto,  string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("invalid Token");
            }
            if (dto == null || dto.VisaId <= 0)
                return BadRequest("Invalid data.");

            var result = await SalamaTravelBL.VisasBL.PutVisaAsync(dto, token);
            if (result)
                return Ok("Visa updated successfully.");
            return BadRequest("Failed to update visa.");
        }
        [HttpPost("UploadVisaImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UploadVisaImage(IFormFile imageFile, string Token)
        {
            if (string.IsNullOrEmpty(Token))
            {
                return Unauthorized("invalid Token");
            }
            // Check if no file is uploaded
            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("No file uploaded.");

            // Directory where files will be uploaded
            var uploadDirectory = @"C:\VisaImages";
            // Generate a unique filename
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadDirectory, fileName);

            // Ensure the uploads directory exists, create if it doesn't
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            // Save the file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Return the file path as a response without curly braces
            return Ok(filePath);
        }
        [HttpPut("UpdateVisaImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateVisaImage(IFormFile imageFile,  string Token,int VisaId)
        {
            if (string.IsNullOrEmpty(Token))
            {
                return Unauthorized("invalid Token");
            }
            if(! await VisasBL.DeleteLastVisaImageAsync(VisaId))
            {
                Console.WriteLine("error occurred");
            }
            // Check if no file is uploaded
            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("No file uploaded.");
            // Directory where files will be uploaded
            var uploadDirectory = @"C:\VisaImages";

            // Generate a unique filename
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadDirectory, fileName);

            // Ensure the uploads directory exists, create if it doesn't
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            // Save the file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Return the file path as a response
            return    await VisasDAL.UpdateVisaImagePathAsync(VisaId,filePath)?Ok(new { filePath }):BadRequest("An error");
        }

    }
}
    