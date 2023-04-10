namespace Api.Dto
{
    public class PictureUpdateDto
    {
        public bool RemovePicture { get; set; }
        public IFormFile? Picture { get; set; }
    }
}
