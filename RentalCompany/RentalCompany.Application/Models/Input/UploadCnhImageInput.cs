using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace RentalCompany.Application.Models.Input
{
    public class UploadCnhImageInput
    {
        public string CnhNumber { get; set; }
        public IFormFile CnhImage { get; set; }
    }
}
