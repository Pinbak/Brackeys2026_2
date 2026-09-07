using System.Reflection;
using System.Runtime.InteropServices;

namespace Brackeys2026_2.Engine.Resources;

/*
Adapted from code written by Lloyd J Howarth, under MIT license:

MIT License

Copyright (c) Lloyd J Howarth

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

public abstract unsafe class Resource
{
    private static readonly Dictionary<string, (Assembly assembly, string fullpath)> EmbeddedResources = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    ///     Imports all embedded resources in the project and adds them to <see cref="EmbeddedResources"/>.
    /// </summary>
    internal static void ImportAllEmbedded()
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
    
    /// <summary>
    ///     Given a path of an embedded resource, gets a stream for it.
    /// </summary>
    /// <param name="path">This needs to be a path to an embedded resource</param>
    /// <param name="handle">Ignored</param>
    /// <returns>An <see cref="SDL.IoStream"/> that can be read from with SDL functions.</returns>
    /// <exception cref="Exception">If the resource is not found. Usually if the resource is not embedded, or doesn't exist</exception>
    internal SDL.IoStream* CreateStream(string path, out GCHandle handle)
    {
        // Find Resource
        if (!EmbeddedResources.TryGetValue(path.Replace('\\', '.').Replace('/', '.'), out var resource))
        {
            throw new Exception($"Resource '{path}' not found.");
        }

        // Load bytes from resource stream
        using Stream stream = resource.assembly.GetManifestResourceStream(resource.fullpath) ?? throw new Exception($"Failed to open '{resource.fullpath}' resource.");
        var data = new byte[stream.Length];
        stream.ReadExactly(data, 0, (int)stream.Length);

        // Create Stream
        handle = GCHandle.Alloc(data, GCHandleType.Pinned);
        return SDL.OpenIo((void*)handle.AddrOfPinnedObject(), (nuint)data.Length);
    }
}