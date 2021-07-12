using Game.Framework;
using UnityEngine;

namespace Game
{
    class PlayerIdle : State<Player>
    {
        float deltaTime;
        public PlayerIdle(Player owner) : base(owner)
        {
            float deltaTime = 0f;
        }

        public override void OnEntering()
        {
            this.owner.GetAnimator().Play("Idle");
            deltaTime = 0f;


        }

        public override void OnUpdating(float dt)
        {
            deltaTime += dt;
            if (deltaTime > 1) this.owner.GetSTateMachine().ChangeState((int)Player.PlayerState.PlayerAction);
            

            Debug.Log($"player Idle updating {deltaTime}");
        }

        public override void OnExiting()
        {
            Debug.Log("player Idle OnExiting");
        }
    }
}