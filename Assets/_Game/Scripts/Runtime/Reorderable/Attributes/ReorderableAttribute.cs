using System;
using UnityEngine;

namespace Runtime.Reorderable.Attributes
{
    public class ReorderableAttribute : PropertyAttribute
    {
        public bool add;
        public bool draggable;
        public string elementIconPath;
        public string elementNameOverride;
        public string elementNameProperty;
        public bool labels;
        public int pageSize;
        public bool paginate;
        public bool remove;
        public bool singleLine;
        public bool sortable;
        public string surrogateProperty;
        public Type surrogateType;

        public ReorderableAttribute()
            : this(null) { }

        public ReorderableAttribute(string elementNameProperty)
            : this(true, true, true, elementNameProperty, null, null) { }

        public ReorderableAttribute(string elementNameProperty, string elementIconPath)
            : this(true, true, true, elementNameProperty, null, elementIconPath) { }

        public ReorderableAttribute(string elementNameProperty, string elementNameOverride, string elementIconPath)
            : this(true, true, true, elementNameProperty, elementNameOverride, elementIconPath) { }

        public ReorderableAttribute(
            bool add, bool remove, bool draggable, string elementNameProperty = null, string elementIconPath = null
        )
            : this(add, remove, draggable, elementNameProperty, null, elementIconPath) { }

        public ReorderableAttribute(
            bool add, bool remove, bool draggable, string elementNameProperty = null, string elementNameOverride = null,
            string elementIconPath = null
        )
        {
            this.add = add;
            this.remove = remove;
            this.draggable = draggable;
            this.elementNameProperty = elementNameProperty;
            this.elementNameOverride = elementNameOverride;
            this.elementIconPath = elementIconPath;

            sortable = true;
            labels = true;
        }
    }
}