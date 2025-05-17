using System;

namespace IziHardGames.DependencyInjection.Attributes
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
