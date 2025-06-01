namespace IziHardGames.AppConstructor
{
    /*
    Services - singletons 100%
    Singletons
    Transients
    Scoped
    Objects
    */

    public class IziModuleStatus
    {
        private bool loaded;
        private bool resolved;
        public bool Loaded => loaded;
        public bool Resolved => resolved;
        internal void SetAsLoaded()
        { 
            loaded = true;
        }
        internal void SetAsResolved()
        {
            resolved = true;
        }
    }
}
