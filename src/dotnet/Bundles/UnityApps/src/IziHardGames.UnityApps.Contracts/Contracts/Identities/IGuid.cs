using System;

namespace IziHardGames.UnityApps.Contracts.Identities
{
    public interface IGuid : IUnique
    {
		public Guid Guid { get; set; }
	}
}