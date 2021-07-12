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
            Debug.Log("player Idle entering");
        }

        public override void OnUpdating(float dt)
        {
            deltaTime += dt;
            if (deltaTime > 1) this.owner.GetSTateMachine().ChangeState((int)Player.PlayerState.PlayerDie);

            Debug.Log("player Idle updating");
        }

        public override void OnExiting()
        {
            Debug.Log("player Dance OnExiting");
        }
    }
}