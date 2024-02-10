using System.Collections.Generic;
using Settings.Activities;
using Leopotam.Ecs;
using UI.Settings;
using Components;
using UI.Popups;

namespace Systems.Activities
{
    public sealed class BallGameActivitySystem : ActivitySystem<BallGameActivitySettings>
    {
        protected override void StartActivity(bool isEnable)
        {
            World.NewEntity().Replace(new ShowPopup
            {
                Settings = new PopupToShow<DefaultPopup>(new DefaultPopup
                {
                    Title = Settings.Type.ToString(), // to do: edit this
                    ButtonSettings = new List<TextButtonSettings>
                    {
                        new TextButtonSettings
                        {
                            Title = "Close", // to do: use localization system
                            Action = () =>
                            {
                                World.NewEntity().Replace(new HidePopup());
                            }
                        },
                        new TextButtonSettings
                        {
                            Title = "Start the ball game", // to do: use localization system
                            Action = () =>
                            {

                            }
                        }
                    }
                })
            });
        }
    }
}