using Game.UI;
using Game.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers
{
    public class UIManager
    {
        Stack<UI_Popup> popupStack = new Stack<UI_Popup>();
        
        UI_Scene sceneUI = null;

        public void SetCanvas(GameObject go, bool sort = true)
        {
            Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.overrideSorting = true;

            canvas.sortingOrder = sort ? popupStack.Count + 1 : 0;
        }

        public GameObject Root
        {
            get
            {
                GameObject root = GameObject.Find("@UI_Root");
                if (root == null)
                {
                    root = new GameObject { name = "@UI_Root" };
                }
                return root;
            }
        }

        public T ShowSceneUI<T>(string name = null) where T : UI_Scene
        {
            if (string.IsNullOrEmpty(name))
            {
                name = typeof(T).Name;
            }

            GameObject go = GameManagers.Resource.Instantiate($"UI/Scene/{name}");

            if (go == null)
            {
                Debug.Log($"ShowScene Error{name}");
            }

            T sceneUI = Util.GetOrAddComponent<T>(go);
            this.sceneUI = sceneUI;

            go.transform.SetParent(Root.transform);

            return sceneUI;
        }

        public T ShowPopupUI<T>() where T : UI_Popup
        {
            string name = typeof(T).Name;

            if (GameObject.Find($"{name}(Clone)"))
                return null;

            GameObject go = GameManagers.Resource.Instantiate($"UI/Popup/{name}");

            if (go == null)
            {
                Debug.Log($"ShowPopupUI Error {name}");
            }

            T popup = Util.GetOrAddComponent<T>(go);

            popupStack.Push(popup);

            go.transform.SetParent(Root.transform);

            return popup;
        }

        public void ClosePopupUI(UI_Popup popup)
        {
            if (popupStack.Count == 0)
            {
                return;
            }

            if (popupStack.Peek() != popup)
            {
                Debug.Log("close failed");
                return;
            }

            ClosePopupUI();
        }

        public void ClosePopupUI()
        {

            UI_Popup uiPopup = popupStack.Peek();
            if (!uiPopup.IsEscAble)
            {
                return;
            }

            UI_Popup popup = popupStack.Pop();

            popup.Destory();

            GameManagers.Resource.Destroy(popup.gameObject);

            EnablePopupStack();
        }

        public void CloseAllPopupUI()
        {
            popupStack.Clear();
        }

        public void RefreshTextAll()
        {
            foreach (UI_Base uiBase in popupStack)
            {
                uiBase.RefreshText();
            }

            if (sceneUI != null)
            {
                sceneUI.RefreshText();
            }
        }

        public GameObject FindPopupUI(string popupName)
        {
            GameObject go = null;
            foreach (UI_Base ui in popupStack)
            {
                if (popupName.Equals(ui.name))
                {
                    go = ui.transform.gameObject;
                }
            }

            return go;
        }


        // TODO: 이전 스택에 있는 유아이를 가져와서 처리하면 되지 않을까?
        private void DisablePopupStack()
        {
        }

        private void EnablePopupStack()
        {
        }
    }

}