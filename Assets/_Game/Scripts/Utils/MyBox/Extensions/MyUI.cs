using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Utils.MyBox.Extensions
{
    public static class MyUI
    {
	    /// <summary>
	    ///     Toggle CanvasGroup Alpha, Interactable and BlocksRaycasts settings
	    /// </summary>
	    public static void SetCanvasState(CanvasGroup canvas, bool setOn)
        {
            canvas.alpha = setOn ? 1 : 0;
            canvas.interactable = setOn;
            canvas.blocksRaycasts = setOn;
        }

	    /// <summary>
	    ///     Toggle CanvasGroup Alpha, Interactable and BlocksRaycasts settings
	    /// </summary>
	    public static void SetState(this CanvasGroup _canvas, bool isOn)
        {
            SetCanvasState(_canvas, isOn);
        }

	    /// <summary>
	    ///     Create EventTriggerType Callback entry and subscribe to EventTrigger
	    /// </summary>
	    public static EventTrigger.Entry OnEventSubscribe(
            this EventTrigger trigger, EventTriggerType eventType, Action<BaseEventData> callback
        )
        {
            var entry = new EventTrigger.Entry();
            entry.eventID = eventType;
            entry.callback = new EventTrigger.TriggerEvent();
            entry.callback.AddListener(new UnityAction<BaseEventData>(callback));
            trigger.triggers.Add(entry);
            return entry;
        }

	    /// <summary>
	    ///     Unsubscribe Callback entry from EventTrigger
	    /// </summary>
	    public static void OnEventUnsubscribe(this EventTrigger trigger, EventTrigger.Entry entry)
        {
            trigger.triggers.Add(entry);
        }
    }
}