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
    public class UIStatusBarMessage : UIMessage
    {

    }

    public class UI_StatusBar : UI_Scene
    {
        enum GameObjects
        {
            
        }

        enum TextMeshPros
        {
            ViewerCounter
        }


        TextMeshProUGUI viewerCounter = null;

        void Start()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();

            BindObjects();
            BindFunc();
            RefreshText();
        }

        void BindFunc()
        {
            castFunction.Add("RefreshText", RefreshText);
        }

        void BindObjects()
        {
            Bind<GameObject>(typeof(GameObjects));
            Bind<TextMeshProUGUI>(typeof(TextMeshPros));

            viewerCounter = GetTextMeshPro((int)TextMeshPros.ViewerCounter);
        }

        Dictionary<string, Action> castFunction = new Dictionary<string, Action>();

        public override void CastMessage(UIMessage message)
        {
            castFunction.TryGetValue(message.action, out Action action);
            if (action != null) action.Invoke();
        }

        public override void RefreshText()
        {
            viewerCounter.text = GameManagers.Chatting.GetUserCount().ToString();
        }
    }
}

