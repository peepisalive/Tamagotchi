using System.Collections.Generic;

namespace UI.Settings
{
    public sealed class DropdownSettings
    {
        public string Title;
        public List<DropdownContent> DropdownContent;
    }


    public sealed class DropdownContent<T> : DropdownContent
    {
        public T Value;
    }


    public class DropdownContent
    {
        public string Title;
    }
}