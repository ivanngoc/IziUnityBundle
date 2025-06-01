using System;
using System.Collections.Generic;
using System.Text;
using IziHardGames.Geometry.Domain.Vectors;

namespace IziHardGames.UserControl.Domain.Events.Screen
{
    public class OnPointerAtEdgeEvent : UserControlEvent
    {
        public EOctagonConfiguration Sides { get; set; }
    }
}
