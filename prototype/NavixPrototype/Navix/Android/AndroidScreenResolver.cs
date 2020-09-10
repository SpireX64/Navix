using NavixPrototype.Stubs.Android;

namespace NavixPrototype.Navix.Android
{
    public class AndroidScreenResolver<TScreen>: ScreenResolver<TScreen> where TScreen: Screen
    {
        public TScreen IncomingScreen { get; set; }
        
        public virtual Intent GetActivityIntent() => null;
        public virtual Fragment GetFragment() => null;
    }
}