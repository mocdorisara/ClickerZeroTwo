using Game.Managers;
using System;

namespace Game.UI
{
    public class UI_Popup : UI_Base
    {
        public Action Action { get; private set; } = null;

        public override void Init()
        {
            IsInit = true;
            GameManagers.UI.SetCanvas(gameObject);
        }

        public virtual void ClosePopupUI()
        {
            GameManagers.UI.ClosePopupUI();
        }                                       

        public virtual void Destory()
        {
            Action?.Invoke();
        }


        public void SetDestroyAction(Action action)
        {
            this.Action = action;
        }
    }
}