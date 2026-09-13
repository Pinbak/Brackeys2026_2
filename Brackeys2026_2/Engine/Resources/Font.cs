using System.Runtime.InteropServices;

namespace Brackeys2026_2.Engine.Resources;

public sealed unsafe class Font : Resource, IDisposable
{
    internal TTF.Font* Handle { get; private set; }
    internal SDL.IoStream* Stream { get; set; }
    internal GCHandle GCHandle;
    private bool _isDisposed;
    
    public Font(string fontPath, float size = 24)
    {
        Stream = CreateStream(fontPath, out GCHandle);
        Handle = TTF.OpenFontIo(Stream, false, size);
        if (Handle == null)
        {
            throw new Exception($"Failed to load font '{fontPath}': {SDL.GetError()}");
        }
                
        // Apply SDF for scaling
        TTF.SetFontSdf(Handle, true);
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
        DestroyFont();
        _isDisposed = true;
    }

    public void DestroyFont()
    {
        if (Handle != null)
        {
            TTF.CloseFont(Handle);
            Handle = null;
        }

        if (GCHandle.IsAllocated)
        {
            GCHandle.Free();
        }

        if (Stream != null)
        {
            SDL.CloseIo(Stream);
        }
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