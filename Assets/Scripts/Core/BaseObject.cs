namespace Core
{
    public abstract class BaseObject
    {
        public string Id { get; private set; }

        public BaseObject(string id)
        {
            Id = id;
        }
    }
}