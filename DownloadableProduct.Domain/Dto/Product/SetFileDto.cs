namespace DownloadableProduct.Domain.Dto.Product
{
    public class SetFileDto
    {
        public SetFileDto(int id, string fileName)
        {
            Id = id;
            FileName = fileName;
        }
        public int Id { get; set; }
        public string FileName { get; set; }
    }
}
