using Game.Framework;
using UnityEngine;

namespace Game {
    public class Player
    {
        public enum PlayerState
        {
            PlayerIdle = 1,
            PlayerAction = 2,
            PlayerDance = 3,
            PlayerDie = 4,
        }

        StateMachine<Player> stateMachine;
        GameObject playerObject;

        Animator playerAnimator;

        public Animator GetAnimator() => playerAnimator;
        
        public Player(GameObject playerObject)
        {
            
            this.playerObject = playerObject;
            playerAnimator = this.playerObject.GetComponent<Animator>();
            
            stateMachine = new StateMachine<Player>();
            stateMachine.AddState((int)PlayerState.PlayerIdle, new PlayerIdle(this));
            stateMachine.AddState((int)PlayerState.PlayerAction, new PlayerAction(this));
            stateMachine.AddState((int)PlayerState.PlayerDance, new PlayerDance(this));
            stateMachine.AddState((int)PlayerState.PlayerDie, new PlayerDie(this));

            stateMachine.SetState((int)PlayerState.PlayerIdle);
        }

        public StateMachine<Player> GetSTateMachine() => stateMachine;

        public void OnUpdate(float dt)
        {
            stateMachine.OnUpdate(dt);
        }

    }
}


