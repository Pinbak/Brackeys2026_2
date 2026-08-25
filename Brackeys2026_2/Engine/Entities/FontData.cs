namespace Brackeys2026_2.Engine.Entities;

public sealed unsafe class FontData : IDisposable
{
    internal TTF.Font* Handle { get; private set; }
    
    internal FontData(TTF.Font* font)
    {
        Handle = font;
    }
    
    public void Dispose()
    {
        if (Handle != null)
            TTF.CloseFont(Handle);
        Handle = null;
    }
}