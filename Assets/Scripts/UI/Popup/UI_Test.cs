using Game.Managers;
using Game.Utils;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI.Popup
{
    public class UI_Test : UI_Popup
    {
        enum GameObjects
        {
            CloseButton,
        }

        enum TextMeshPros
        {
            TitleText,
        }

        void Start()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();

            BindObjects();
            RefreshText();

        }

        void BindObjects()
        {
            Bind<GameObject>(typeof(GameObjects));
            Bind<TextMeshProUGUI>(typeof(TextMeshPros));

            GameObject closeButton = GetObject((int)GameObjects.CloseButton);
            AddUIEvent(closeButton, (evt) => { GameManagers.UI.ClosePopupUI(); }, Define.UIEvent.Click);
        }

        public override void RefreshText()
        {
        }
    }
}

