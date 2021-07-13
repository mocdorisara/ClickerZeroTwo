using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI
{

    public class UI_EventHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerClickHandler, IEndDragHandler
    {
        public Action<PointerEventData> OnBeginDragHandler = null;
        public Action<PointerEventData> OnDragHandler = null;
        public Action<PointerEventData> OnClickHandler = null;        

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            if (OnBeginDragHandler != null)
            {
                OnBeginDragHandler.Invoke(eventData);
            }
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (OnDragHandler != null)
            {
                OnDragHandler.Invoke(eventData);
            }
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            // throw new NotImplementedException();
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (OnClickHandler != null)
            {
                OnClickHandler.Invoke(eventData);
            }
        }

    }
}