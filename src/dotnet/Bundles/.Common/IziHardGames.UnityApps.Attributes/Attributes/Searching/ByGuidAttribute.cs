using System;

namespace IziHardGames.UnityApps.Attributes.Searching
{
    public class ByGuidAttribute : Attribute
    {
        private string guid;
        public string GuidAsString => guid;
        public Guid Guid => Guid.Parse(guid);

        public ByGuidAttribute(string guid)
        {
            this.guid = guid;
        }
    }
}
