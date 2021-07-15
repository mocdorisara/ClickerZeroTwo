using Game.Utils;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI
{

    public abstract class UI_Base : MonoBehaviour
    {
        public bool IsFull { get; set; } = false;
        public bool IsEscAble { get; set; } = true;
        protected bool IsInit { get; set; } = false;

        protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

        public abstract void Init();

        protected virtual void OnDestroy()
        {
            _objects.Clear();
        }

        protected void Bind<T>(Type type) where T : UnityEngine.Object
        {
            string[] names = Enum.GetNames(type);
            UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];

            _objects.Add(typeof(T), objects);

            for (int i = 0; i < names.Length; i++)
            {
                if (typeof(T) == typeof(GameObject))
                {
                    objects[i] = Util.FindChild(gameObject, names[i], true);
                }
                else
                {
                    objects[i] = Util.FindChild<T>(gameObject, names[i], true);
                }

                if (objects[i] == null)
                {
                    Debug.Log($"Failed to bind({names[i]})");
                }
            }
        }

        protected GameObject GetObject(int idx)
        {
            return Get<GameObject>(idx);
        }

        protected TextMeshProUGUI GetTextMeshPro(int idx)
        {
            return Get<TextMeshProUGUI>(idx);
        }


        protected T Get<T>(int idx) where T : UnityEngine.Object
        {
            UnityEngine.Object[] objects = null;
            if (_objects.TryGetValue(typeof(T), out objects) == false)
            {
                return null;
            }

            return objects[idx] as T;
        }

        public static void AddUIEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type)
        {
            UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

            switch (type)
            {
                case Define.UIEvent.Click:
                    evt.OnClickHandler -= action;
                    evt.OnClickHandler += action;
                    break;
                case Define.UIEvent.Drag:
                    evt.OnDragHandler -= action;
                    evt.OnDragHandler += action;
                    break;
            }
        }

        public static void AddUIEventWithScrollbar(GameObject go, Action<PointerEventData> action, Define.UIEvent type, ScrollRect scrollRect)
        {
            UI_ScrollbarEventHandler evt = Util.GetOrAddComponent<UI_ScrollbarEventHandler>(go);
            evt.SetScrollRect(scrollRect);

            switch (type)
            {
                case Define.UIEvent.Click:
                    evt.OnClickHandler -= action;
                    evt.OnClickHandler += action;
                    break;
                case Define.UIEvent.Drag:
                    evt.OnDragHandler -= action;
                    evt.OnDragHandler += action;
                    break;
            }
        }

        public static void RemoveUIEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type)
        {
            UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);
            switch (type)
            {
                case Define.UIEvent.Click:
                    evt.OnClickHandler -= action;
                    break;
                case Define.UIEvent.Drag:
                    evt.OnDragHandler -= action;
                    break;
            }
        }

        public virtual void RefreshText() { }


        public virtual void OnFocus() { }

        public virtual void CastMessage(string message) { }
    }

}