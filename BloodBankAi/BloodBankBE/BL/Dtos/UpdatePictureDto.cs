

using Microsoft.AspNetCore.Http;

namespace BL.Dtos
{
    public class UpdatePictureDto
    {
        public bool RemovePicture{ get; set; }
        public IFormFile? Picture { get; set; }
    }
}
