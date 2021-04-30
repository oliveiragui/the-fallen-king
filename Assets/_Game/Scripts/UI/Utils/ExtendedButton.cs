using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Game.Scripts.UI.Utils
{
    public class ExtendedButton : Selectable, ISubmitHandler, IPointerClickHandler
    {
        [SerializeField] bool usePointerToSelect;
        public UnityEvent onClick;
        public BaseUIEvent onSelect;

        #region pointerMethods

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            if (usePointerToSelect) Select();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick.Invoke();
        }

        // public override void OnPointerExit(PointerEventData eventData)
        // {
        //     base.OnPointerExit(eventData);
        //     if (usePointerToSelect) OnDeselect(eventData);
        // }

        #endregion

        #region selectableMethods

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            onSelect.Invoke(eventData);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            onClick.Invoke();
        }

        #endregion
    }

    [Serializable]
    public class BasePointerEvent : UnityEvent<PointerEventData> { }

    [Serializable]
    public class BaseUIEvent : UnityEvent<BaseEventData> { }
}