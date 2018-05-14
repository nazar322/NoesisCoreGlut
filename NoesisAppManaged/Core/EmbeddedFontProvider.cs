using System;
using System.Resources;
using System.IO;
using Noesis;

namespace NoesisApp
{
    public struct EmbeddedFont
    {
        public string folder;
        public string resource;
    }

    public class EmbeddedFontProvider : FontProvider
    {
        public EmbeddedFontProvider(EmbeddedFont[] fonts, ResourceManager resources)
        {
            _fonts = fonts;
            _resources = resources;
        }

        public override void ScanFolder(string folder)
        {
            for (int i = 0; i < _fonts.Length; ++i)
            {
                EmbeddedFont font = _fonts[i];
                if (font.folder == folder)
                {
                    RegisterFont(folder, i.ToString());
                }
            }
        }

        public override System.IO.Stream OpenFont(string folder, string id)
        {
            for (int i = 0; i < _fonts.Length; ++i)
            {
                EmbeddedFont font = _fonts[i];
                if (font.folder == folder && i.ToString() == id)
                {
                    byte[] content = (byte[])_resources.GetObject(font.resource);
                    return new MemoryStream(content);
                }
            }

            return null;
        }

        private EmbeddedFont[] _fonts;
        private ResourceManager _resources;
    }
}