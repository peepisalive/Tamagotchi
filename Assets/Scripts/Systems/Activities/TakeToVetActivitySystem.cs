using Modules.Navigation;
using Leopotam.Ecs;
using Components;
using UI.Popups;

namespace Systems.Activities
{
    public sealed class TakeToVetActivitySystem : ActivitySystem
    {
        protected override NavigationElementType Type => NavigationElementType.TakeToVetActivity;

        protected override void StartInteraction(NavigationElementType type, bool isEnable)
        {
            World.NewEntity().Replace(new ShowPopup
            {
                Settings = new PopupToShow<DefaultPopup>(new DefaultPopup
                {

                })
            });
        }
    }
}