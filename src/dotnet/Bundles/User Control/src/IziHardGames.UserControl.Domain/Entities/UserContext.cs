using System;
using System.Collections.Generic;
using IziHardGames.UserControl.Contracts;

namespace IziHardGames.UserControl.Domain.Models
{
    public class UserContext
    {
        protected readonly Dictionary<Type, IUserMode> modes = new Dictionary<Type, IUserMode>();
        public bool IsModeEnebled<T>()
        {
            if (modes.TryGetValue(typeof(T), out var mode))
            {
                return mode.IsEnabled;
            }
            return false;
        }
    }
}
