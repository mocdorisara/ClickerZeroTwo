using Game.Framework;
using UnityEngine;

namespace Game
{
    class PlayerDance : State<Player>
    {
        public PlayerDance(Player owner) : base(owner)
        {
        }

        public override void OnEntering()
        {
            this.owner.GetAnimator().Play("Dance");
            Debug.Log("player Dance Entering");
        }

        public override void OnUpdating(float dt)
        {
        }

        public override void OnExiting()
        {
            Debug.Log("player Dance OnExiting");
        }
    }
}