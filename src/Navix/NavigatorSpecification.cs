namespace Navix
{
    public readonly struct NavigatorSpecification
    {
        public NavigatorSpecification(
            bool backToScreenSupported,
            bool backToRootSupported, 
            bool replaceScreenSupported)
        {
            BackToScreenSupported = backToScreenSupported;
            BackToRootSupported = backToRootSupported;
            ReplaceScreenSupported = replaceScreenSupported;
        }
        
        public bool BackToScreenSupported { get; }
        public bool BackToRootSupported { get; }
        public bool ReplaceScreenSupported { get; }
    }
}