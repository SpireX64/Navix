namespace Spx.Navix.Abstractions
{
    /// <summary>
    /// Used navigator container
    /// </summary>
    public interface INavigatorHolder
    {
        /// <summary>
        /// Reference of current navigator
        /// </summary>
        public Navigator? Navigator { get; }
        
        /// <summary>
        /// Specifies the navigator to use
        /// </summary>
        /// <param name="navigator">Navigator reference</param>
        public void SetNavigator(Navigator navigator);
        
        /// <summary>
        /// Removes the navigator from operation
        /// </summary>
        public void RemoveNavigator();
    }
}