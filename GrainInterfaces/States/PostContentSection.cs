namespace GrainInterfaces.States
{
    [GenerateSerializer]
    public class PostContentSection
    {
        [Id(0)]
        public string ContentType { get; set; }
        [Id(1)]
        public string Body { get; set; }
    }
}