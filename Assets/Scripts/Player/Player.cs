using Game.Framework;
using UnityEngine;

namespace Game {
    public class Player
    {
        public enum PlayerState
        {
            PlayerIdle = 0,
            PlayerDance = 1,
        }

        StateMachine<Player> stateMachine;
        GameObject playerObject;
        GameObject headObject;

        Animator playerAnimator;

        public Animator GetAnimator() => playerAnimator;
        
        public Player()
        {

            Debug.Log("Player");
            Object _object = Resources.Load("Characters/PartsTest/Prefabs/PartTest");

            this.playerObject = (GameObject)GameObject.Instantiate(_object);
            this.playerObject.transform.position = new Vector3Int(0, -5, 0);
            headObject = this.playerObject.transform.Find("head").gameObject;

            playerAnimator = this.playerObject.GetComponent<Animator>();
            
            stateMachine = new StateMachine<Player>();
            stateMachine.AddState((int)PlayerState.PlayerIdle, new PlayerIdle(this));
            stateMachine.AddState((int)PlayerState.PlayerDance, new PlayerDance(this));

            stateMachine.SetState((int)PlayerState.PlayerIdle);
        }

        public StateMachine<Player> GetSTateMachine() => stateMachine;

        public void OnUpdate(float dt)
        {
            stateMachine.OnUpdate(dt);
        }

        public void SetHeadSprite(Sprite headSprite)
        {
            headObject.GetComponent<SpriteRenderer>().sprite = headSprite;
        }

    }
}


