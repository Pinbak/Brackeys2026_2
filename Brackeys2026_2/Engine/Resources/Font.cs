using System.Runtime.InteropServices;

namespace Brackeys2026_2.Engine.Resources;

public sealed unsafe class Font : Resource, IDisposable
{
    internal TTF.Font* Handle { get; private set; }
    private bool _isDisposed;
    
    public Font(string fontPath, float size = 24)
    {
        GCHandle gcHandle = default;
        TTF.Font* font;
        try
        {
            var stream = CreateStream(fontPath, out gcHandle);
            font = TTF.OpenFontIo(stream, true, size);
            if (font == null)
            {
                SDL.CloseIo(stream);
                throw new Exception($"Failed to load font '{fontPath}': {SDL.GetError()}");
            }
                
            // Apply SDF for scaling
            TTF.SetFontSdf(font, true);
        }
        finally
        {
            if (gcHandle.IsAllocated)
                gcHandle.Free();
        }

        Handle = font;
    }

    ~Font()
    {
        Release();
    }
    
    public void Dispose()
    {
        Release();
        GC.SuppressFinalize(this);
    }
    
    private void Release()
    {
        if (_isDisposed) return;
        if (Handle != null)
            TTF.CloseFont(Handle);
        Handle = null;
        _isDisposed = true;
    }

    public void SetAlignmentLeft()
    {
        TTF.SetFontWrapAlignment(Handle, TTF.HorizontalAlignment.Left);
    }
    public void SetAlignmentRight()
    {
        TTF.SetFontWrapAlignment(Handle, TTF.HorizontalAlignment.Right);
    }
    public void SetAlignmentCentre()
    {
        TTF.SetFontWrapAlignment(Handle, TTF.HorizontalAlignment.Center);
    }
}