using UnityEngine;

namespace Game.Managers
{
    public class GameManagers : MonoBehaviour
    {
        Player player = null;
        void Start()
        {
            Object _rawObject = Resources.Load("Characters/Plunka/Prefabs/Plunkah");
            player = new Player((GameObject)GameObject.Instantiate(_rawObject));
        }

        // Update is called once per frame
        void Update()
        {
            if (player != null) player.OnUpdate(Time.deltaTime);

        }
    }

}
