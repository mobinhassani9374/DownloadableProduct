namespace DownloadableProduct.Domain.Dto.Product
{
    public class SetFileDto
    {
        public SetFileDto(int id, string fileName,long length)
        {
            Id = id;
            FileName = fileName;
            Length = length;
        }
        public int Id { get; set; }
        public string FileName { get; set; }
        public long Length { get; set; }
    }
}
