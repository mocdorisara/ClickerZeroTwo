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
    public class UI_Test : UI_Scene
    {
        Sprite[] heads = null;

        enum GameObjects
        {
            Head0,
            Head1,
            Head2,
            Head3,
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
            heads = Resources.LoadAll<Sprite>("Characters/PartsTest/Parts/Head/head");

        }

        void BindObjects()
        {
            Bind<GameObject>(typeof(GameObjects));
            Bind<TextMeshProUGUI>(typeof(TextMeshPros));

            GameObject head0 = GetObject((int)GameObjects.Head0);
            AddUIEvent(head0, (evt) => {
                GameManagers.Player.SetHeadSprite(heads[0]);
            }, Define.UIEvent.Click);

            GameObject head1 = GetObject((int)GameObjects.Head1);
            AddUIEvent(head1, (evt) => { 
                GameManagers.Player.SetHeadSprite(heads[1]); 
            }, Define.UIEvent.Click);

            GameObject head2 = GetObject((int)GameObjects.Head2);
            AddUIEvent(head2, (evt) => {
                GameManagers.Player.SetHeadSprite(heads[2]);
            }, Define.UIEvent.Click);

            GameObject head3 = GetObject((int)GameObjects.Head3);
            AddUIEvent(head3, (evt) => {
                GameManagers.Player.SetHeadSprite(heads[3]);
            }, Define.UIEvent.Click);
        }

        public override void RefreshText()
        {
        }
    }
}

