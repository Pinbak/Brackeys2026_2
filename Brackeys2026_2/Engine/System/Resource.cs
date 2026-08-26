using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Brackeys2026_2.Engine.System;

internal static unsafe class Resources
{
    private static readonly Dictionary<string, (Assembly assembly, string fullpath)> EmbeddedResources = new(StringComparer.OrdinalIgnoreCase);

    internal static void GatherResources()
    {
        // DebugLog();
        
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            var name = assembly.GetName().Name;

            if (name == null) continue;
            foreach (var resource in assembly.GetManifestResourceNames())
            {
                var path = resource;

                if (path.StartsWith(name + "."))
                {
                    path = path[(name.Length + 1)..];
                }

                EmbeddedResources[path] = (assembly, resource);
            }
        }
    }

    private static void DebugLog()
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            Console.WriteLine($"ASSEMBLY: {assembly.GetName().Name}");
        
            foreach (var resource in assembly.GetManifestResourceNames())
            {
                Console.WriteLine($"  RESOURCE: {resource}");
            }
        }
    }
        
    internal static SDL.IoStream* CreateStream(string path, out GCHandle handle)
    {
        // Find Resource
        if (!EmbeddedResources.TryGetValue(path.Replace('\\', '.').Replace('/', '.'), out var resource))
        {
            throw new Exception($"Resource '{path}' not found.");
        }

        // Load bytes from resource stream
        using Stream stream = resource.assembly.GetManifestResourceStream(resource.fullpath) ?? throw new Exception($"Failed to open '{resource.fullpath}' resource.");
        using MemoryStream ms = new MemoryStream();
        stream.CopyTo(ms);
        var data = ms.ToArray();

        // Create Stream
        handle = GCHandle.Alloc(data, GCHandleType.Pinned);
        return SDL.OpenIo((void*)handle.AddrOfPinnedObject(), (nuint)data.Length);
    }
}