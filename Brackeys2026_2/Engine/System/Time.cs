namespace Brackeys2026_2.Engine.System;

public static unsafe class Time
{
    public static float DeltaTime => _deltaTime;
    public static float Fps => _fps;
    
    private static long _last = SDL.GetPerformanceCounter();
    private static long _now = SDL.GetPerformanceCounter();
    private static long _frequency = SDL.GetPerformanceFrequency();
    private static float _deltaTime;

    private static float _fpsLimit;
    private static float _fps;
    private static bool _isLimited;
    
    internal static void Setup()
    {
        _last = SDL.GetPerformanceCounter();
        _frequency = SDL.GetPerformanceFrequency();
        _now = SDL.GetPerformanceCounter();
    }

    internal static void FrameStart()
    {
        _now = SDL.GetPerformanceCounter();
        _deltaTime = (float)(_now - _last) / _frequency;
        _last = _now;
        var instantFps = _deltaTime > .0f ? 1.0f / _deltaTime : .0f;
        const float smoothing = 1f;
        _fps += (instantFps - _fps) * smoothing;
    }

    internal static void FrameEnd()
    {
        if (!_isLimited) return;
        var targetFps = 1.0 / _fpsLimit;
        var end = SDL.GetPerformanceCounter();
        var elapsed = (double)(end - _now) / _frequency;
        var remaining = targetFps - elapsed;
        if (remaining > 0)
        {
            SDL.DelayPrecise((ulong)(remaining * 1_000_000_000.0));
        }
    }

    public static void SetFramerateLimit(int limit)
    {
        if (limit <= 0) return;
        _fpsLimit = limit;
        _isLimited = true;
    }

    public static void EnabledVsync()
    {
        SDL.SetRenderVSync(Renderer.Handle, 1);
    }
}