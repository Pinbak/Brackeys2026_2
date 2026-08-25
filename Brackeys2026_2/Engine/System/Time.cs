namespace Brackeys2026_2.Engine.System;

public static unsafe class Time
{
    public static float DeltaTime => _deltaTime;
    public static float Fps => _fps;
    
    private static long _last = SDL.GetPerformanceCounter();
    private static long _frequency = SDL.GetPerformanceFrequency();
    private static float _deltaTime;
    private static float _fps;
    
    internal static void Setup()
    {
        _last = SDL.GetPerformanceCounter();
        _frequency = SDL.GetPerformanceFrequency();
    }

    internal static void Update()
    {
        var now = SDL.GetPerformanceCounter();
        _deltaTime = (float)(now - _last) / _frequency;
        _last = now;
        var instantFps = _deltaTime > .0f ? 1.0f / _deltaTime : .0f;
        const float smoothing = .01f;
        _fps += (instantFps - _fps) * smoothing;
    }
}