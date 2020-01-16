namespace DownloadableProduct.Domain.Dto.Product
{
    public class SetImageDto
    {
        public SetImageDto(int id, string imageName)
        {
            Id = id;
            ImageName = imageName;
        }
        public int Id { get; set; }

        public string ImageName { get; set; }
    }
}
