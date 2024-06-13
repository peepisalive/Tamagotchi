namespace Modules.Network
{
    public sealed class User : Entity
    {
        public int Rating { get; set; }

        public string Name { get; set; }
        public string AdditionalData { get; set; }
    }
}