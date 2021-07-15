using Game.Framework;
using UnityEngine;

namespace Game
{
    class PlayerIdle : State<Player>
    {
        public PlayerIdle(Player owner) : base(owner)
        {
        }

        public override void OnEntering()
        {
            this.owner.GetAnimator().Play("Idle");
            
            Debug.Log("player Idle OnEnetring");
        }

        public override void OnUpdating(float dt)
        {
            //if (deltaTime > 1) this.owner.GetSTateMachine().ChangeState((int)Player.PlayerState.PlayerAction);
        }

        public override void OnExiting()
        {
            Debug.Log("player Idle OnExiting");
        }
    }
}