using System;
using System.Resources;
using System.IO;
using Noesis;

namespace NoesisApp
{
    public struct EmbeddedXaml
    {
        public string filename;
        public string resource;
    }

    public class EmbeddedXamlProvider : XamlProvider
    {
        public EmbeddedXamlProvider(EmbeddedXaml[] xamls, ResourceManager resources)
        {
            _xamls = xamls;
            _resources = resources;
        }

        public override Stream LoadXaml(string filename)
        {
            foreach (EmbeddedXaml xaml in _xamls)
            {
                if (xaml.filename == filename)
                {
                    byte[] content = (byte[])_resources.GetObject(xaml.resource);
                    return new MemoryStream(content);
                }
            }

            return null;
        }

        private EmbeddedXaml[] _xamls;
        private ResourceManager _resources;
    }
}
