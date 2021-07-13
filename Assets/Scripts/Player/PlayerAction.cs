using Game.Framework;
using UnityEngine;

namespace Game
{
    class PlayerAction : State<Player>
    {
        float deltaTime = 0f;
        public PlayerAction(Player owner) : base(owner)
        {
            
        }

        public override void OnEntering()
        {
            deltaTime = 0f;
            this.owner.GetAnimator().Play("Attack");
            
            Debug.Log("player Action Entering");
        }

        public override void OnUpdating(float dt)
        {
            deltaTime += dt;
            
            if (this.owner.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                this.owner.GetSTateMachine().ChangeState((int)Player.PlayerState.PlayerDance);
            }
        }

        public override void OnExiting()
        {
            Debug.Log("player Action OnExiting");
        }
    }
}