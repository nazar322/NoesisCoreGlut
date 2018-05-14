using System;
using System.Resources;
using System.IO;
using Noesis;

namespace NoesisApp
{
    public struct EmbeddedTexture
    {
        public string filename;
        public string resource;
    }

    public class EmbeddedTextureProvider : FileTextureProvider
    {
        public EmbeddedTextureProvider(EmbeddedTexture[] texures, ResourceManager resources)
        {
            _textures = texures;
            _resources = resources;
        }

        public override System.IO.Stream OpenStream(string filename)
        {
            foreach (EmbeddedTexture texture in _textures)
            {
                if (texture.filename == filename)
                {
                    byte[] content = (byte[])_resources.GetObject(texture.resource);
                    return new MemoryStream(content);
                }
            }

            return null;
        }

        private EmbeddedTexture[] _textures;
        private ResourceManager _resources;
    }
}