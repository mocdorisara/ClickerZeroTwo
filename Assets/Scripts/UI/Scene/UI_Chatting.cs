using Game.Managers;
using Game.Utils;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;

namespace Game.UI.Popup
{
    class UIChattingMessage: UIMessage
    {
        public string Id { get; set; }
        public string Message { get; set; }
    }

    public class UI_Chatting : UI_Scene
    {
        enum GameObjects
        {
            ChattingArea
        }

        enum TextMeshPros
        {
            ViewerCounter
        }

        void Start()
        {
            Init();

        }

        public override void Init()
        {
            base.Init();

            BindObjects();
            BindFunc();
        }

        void BindFunc()
        {
            castFunction.Add("AddMessage", AddMessage);
        }

        GameObject chattingArea = null;
        void BindObjects()
        {
            Bind<GameObject>(typeof(GameObjects));

            chattingArea = GetObject((int)GameObjects.ChattingArea);

        }

        Dictionary<string, Action<UIMessage>> castFunction = new Dictionary<string, Action<UIMessage>>();

        public override void CastMessage(UIMessage message)
        {
            castFunction.TryGetValue(message.action, out Action<UIMessage> action);
            if (action != null) action.Invoke(message);
        }

        public void AddMessage(UIMessage uiMessage)
        {
            UIChattingMessage uiChattingMessage = uiMessage as UIChattingMessage;
            string id = uiChattingMessage.Id;
            string message = uiChattingMessage.Message;

            UnityEngine.Object _object = Resources.Load("Prefabs/UI/Component/Chatting");

            GameObject chattingObject = (GameObject)GameObject.Instantiate(_object);
            chattingObject.transform.position = new Vector3(0.0f, UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(0, 100));
            chattingObject.transform.Find("Message").GetComponent<TextMeshProUGUI>().text = $"{id}, {message}";
            chattingObject.transform.SetParent(chattingArea.transform, false);
        }
    }
}

