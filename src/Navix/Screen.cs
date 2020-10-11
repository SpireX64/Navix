namespace Spx.Navix
{
    /// <summary>
    ///     Class identifying screen in the system
    /// </summary>
    public abstract class Screen
    {
        protected Screen()
        {
            Name = GetType().Name;
        }

        public virtual string Name { get; }
    }
}