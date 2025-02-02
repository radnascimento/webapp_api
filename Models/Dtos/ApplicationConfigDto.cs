namespace Api.Models.Dtos
{
    public class ApplicationConfigDto
    {
        public int IdApplicationConfig { get; set; }
        public string Name { get; set; }
        public string JsonContent { get; set; }

        public DateTime OperationDate { get; set; }
    }
}
