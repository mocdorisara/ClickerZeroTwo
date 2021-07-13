using Game.Framework;
using UnityEngine;

namespace Game
{
    
    class PlayerDie : State<Player>
    {
        float deltaTime = 0f;
        public PlayerDie(Player owner) : base(owner)
        {

        }

        public override void OnEntering()
        {
            deltaTime = 0f;
            this.owner.GetAnimator().Play("Die");
            Debug.Log("player Die Entering");
        }

        public override void OnUpdating(float dt)
        {
            deltaTime += dt;
            if (deltaTime > 1) this.owner.GetSTateMachine().ChangeState((int)Player.PlayerState.PlayerIdle);
        }

        public override void OnExiting()
        {
            Debug.Log("player Die OnExiting");
        }
    }
}