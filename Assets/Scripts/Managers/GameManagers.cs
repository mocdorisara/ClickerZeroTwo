using Game.UI;
using Game.UI.Popup;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers
{
    public class UICommandMessage
    {
        public string TargetUID { get; set; }
        public UIMessage Message { get; set; }
        public UICommandMessage(string targetUID, UIMessage message)
        {
            TargetUID = targetUID;
            Message = message;
        }        
    }

    public class GameManagers : MonoBehaviour
    {
        public static Player Player = null;
        public static Chatting Chatting = null;

        public static UIManager UI = new UIManager();
        public static ResourceManager Resource = new ResourceManager();

        static Queue<UICommandMessage> uiCommandMessageQueue = new Queue<UICommandMessage>();
        Dictionary<string, UI_Base> sceneDictionary = new Dictionary<string, UI_Base>();

        bool isInitialised = false;

        void Start()
        {
            // Player = new Player();
            Chatting = new Chatting();

            InitUI();

            isInitialised = true;
            
        }

        void InitUI()
        {
            UI_StatusBar uiStatusBar = UI.ShowSceneUI<UI_StatusBar>();
            UI_Chatting uiChatting= UI.ShowSceneUI<UI_Chatting>();

            sceneDictionary.Add(typeof(UI_StatusBar).Name, uiStatusBar);
            sceneDictionary.Add(typeof(UI_Chatting).Name, uiChatting);
        }

        // Update is called once per frame
        void Update()
        {
            if (!isInitialised) return;

            // if (Player != null) Player.OnUpdate(Time.deltaTime);
            if (Chatting != null) Chatting.OnUpdate(Time.deltaTime);

            if(uiCommandMessageQueue.Count > 0)
            {
                UICommandMessage uiCommandMessage = uiCommandMessageQueue.Dequeue();
                sceneDictionary.TryGetValue(uiCommandMessage.TargetUID, out UI_Base uiBase);

                if (uiBase) CastMessage(uiBase, uiCommandMessage.Message);
                else uiCommandMessageQueue.Enqueue(uiCommandMessage);
            }
        }

        public static void PushCastMessage(UICommandMessage commandMessage)
        {
            uiCommandMessageQueue.Enqueue(commandMessage);
        }


        void CastMessage(UI_Base uiScene, UIMessage message)
        {
            uiScene.CastMessage(message);
        }
    }

}
