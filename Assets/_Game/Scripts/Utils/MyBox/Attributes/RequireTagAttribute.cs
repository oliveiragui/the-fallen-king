using System;

namespace Utils.MyBox.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RequireTagAttribute : Attribute
    {
        public string Tag;

        public RequireTagAttribute(string tag)
        {
            Tag = tag;
        }
    }
}