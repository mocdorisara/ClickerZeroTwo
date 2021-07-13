using Game.UI.Popup;
using UnityEngine;

namespace Game.Managers
{
    public class GameManagers : MonoBehaviour
    {
        Player player = null;

        public static UIManager UI = new UIManager();
        public static ResourceManager Resource = new ResourceManager();
        

        void Start()
        {
            Object _rawObject = Resources.Load("Characters/Plunka/Prefabs/Plunkah");
            player = new Player((GameObject)GameObject.Instantiate(_rawObject));

            UI.ShowPopupUI<UI_Test>();
        }

        // Update is called once per frame
        void Update()
        {
            if (player != null) player.OnUpdate(Time.deltaTime);
        }
    }

}
