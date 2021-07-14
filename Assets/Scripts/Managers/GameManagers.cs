using Game.UI.Popup;
using System;
using UnityEngine;

namespace Game.Managers
{
    public class GameManagers : MonoBehaviour
    {
        public static Player Player = null;
        public static UIManager UI = new UIManager();
        public static ResourceManager Resource = new ResourceManager();


        void Start()
        {
            Player = new Player();
            UI.ShowPopupUI<UI_Test>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Player != null) Player.OnUpdate(Time.deltaTime);
        }

        
    }

}
