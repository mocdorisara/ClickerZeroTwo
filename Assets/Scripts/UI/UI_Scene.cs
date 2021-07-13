using Game.Managers;

namespace Game.UI
{
    public class UI_Scene : UI_Base
    {
        public override void Init()
        {
            GameManagers.UI.CloseAllPopupUI();
            GameManagers.UI.SetCanvas(gameObject, false);
        }
    }
}