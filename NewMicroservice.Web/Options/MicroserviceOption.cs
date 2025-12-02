namespace NewMicroservice.Web.Options
{
    public class MicroserviceOption
    {
        public required MicroserviceOptionItem CatalogMicroservice { get; set; }
    }
    public class MicroserviceOptionItem
    {

        public required string BaseUrl { get; set; }
    }
}
