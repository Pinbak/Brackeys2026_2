using System.Runtime.InteropServices;

public static unsafe partial class SDL
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct CommonEvent
    {
        internal uint Type;
        internal uint Reserved;
        internal ulong TimeStamp;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    internal struct ClipboardEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal bool Owner;
        internal int NumberOfMimeTypes;
        internal byte** CurrentMimeTypes;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    internal struct DisplayEvent
    {
        internal EventType Type;                        // SDL_EVENT_DISPLAY_
        internal uint Reserved;
        internal ulong TimeStamp;                       // In nanoseconds, populated using SDL_GetTicksNS()
        internal uint DisplayID;                        // The associated display
        private int Data1;                              // Event dependent data
        private int Data2;                              // Event dependent data
    }
    
    [StructLayout(LayoutKind.Sequential)]
    internal struct QuitEvent
    {
        internal SDL.EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    internal struct RenderEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal uint WindowID;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    internal struct TextEditingCandidatesEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal uint WindowID;
        internal byte** Candidates;
        internal int NumberOfCandidates;
        internal int SelectedCandidate;
        internal bool Horizontal;
        private byte Padding1;
        private byte Padding2;
        private byte Padding3;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    internal struct TextEditingEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal uint WindowID;
        internal byte* Text;
        internal int Start;
        internal int Length;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    internal struct TextInputEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal uint WindowID;
        internal byte* Text;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowEvent
    {
        internal EventType Type;
        internal uint Reserved;
        internal ulong TimeStamp;
        internal uint WindowID;
        private int Data1;
        private int Data2;
    }
}