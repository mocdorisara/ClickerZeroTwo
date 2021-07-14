using Game.Framework;
using UnityEngine;

namespace Game
{
    class PlayerDance : State<Player>
    {
        float deltaTime = 0f;
        public PlayerDance(Player owner) : base(owner)
        {
            deltaTime = 0f;
        }

        public override void OnEntering()
        {
            deltaTime = 0f;
            this.owner.GetAnimator().Play("Dance");
            Debug.Log("player Dance Entering");
        }

        public override void OnUpdating(float dt)
        {
            //deltaTime += dt;
            //if (deltaTime > 1) this.owner.GetSTateMachine().ChangeState((int)Player.PlayerState.PlayerDie);
        }

        public override void OnExiting()
        {
            Debug.Log("player Dance OnExiting");
        }
    }
}