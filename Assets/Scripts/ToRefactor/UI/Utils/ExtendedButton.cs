using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ToRefactor.UI.Utils
{
    public class ExtendedButton : Button
    {
        public BaseEvent onSelected = new BaseEvent();
        public BaseEvent onSubmit = new BaseEvent();

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            onSelected.Invoke(eventData);
            Sons.SomDeSelecionar.Play();
        }

        public override void OnSubmit(BaseEventData eventData)
        {
            base.OnSubmit(eventData);
            onSubmit.Invoke(eventData);
            Sons.SomDeConfirmacao.Play();
        }
    }

    [Serializable]
    public class BaseEvent : UnityEvent<BaseEventData> { }
}