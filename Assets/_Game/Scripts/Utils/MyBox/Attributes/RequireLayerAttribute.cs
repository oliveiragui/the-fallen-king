using System;

namespace _Game.Scripts.Utils.MyBox.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RequireLayerAttribute : Attribute
    {
        public readonly int LayerIndex = -1;
        public readonly string LayerName;

        public RequireLayerAttribute(string layer)
        {
            LayerName = layer;
        }

        public RequireLayerAttribute(int layer)
        {
            LayerIndex = layer;
        }
    }
}