using System;

namespace Editor.Scripts.MyBox.Attributes
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