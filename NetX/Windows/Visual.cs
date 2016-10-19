
namespace NetX.Windows
{
    //todo : implement
    public abstract class Visual
    {
        public virtual void Draw()
        {
            // System draws the window for us
            if(this is NetX.XWindows.XWindow)
                return;
        }
    }
}