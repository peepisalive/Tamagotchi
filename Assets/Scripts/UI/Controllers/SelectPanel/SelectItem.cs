namespace UI
{
    public class SelectItem
    {
        public string Title { get; private set; }

        public SelectItem(string title)
        {
            Title = title;
        }
    }


    public sealed class SelectItem<T> : SelectItem
    {
        public T Item { get; private set; }

        public SelectItem(T item, string title) : base(title)
        {
            Item = item;
        }
    }
}