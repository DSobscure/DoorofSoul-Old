using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace DoorofSoul.Server.Config
{
    public class VersionConfiguration
    {
        [XmlElement]
        public string ServerVersion { get; set; }
        [XmlElement]
        public string ClientVersion { get; set; }

        public VersionConfiguration() { }
        public static VersionConfiguration Load(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(VersionConfiguration));
            if (File.Exists(filePath))
            {
                using (XmlReader reader = XmlReader.Create(filePath))
                {
                    if (serializer.CanDeserialize(reader))
                    {
                        return (VersionConfiguration)serializer.Deserialize(reader);
                    }
                    else
                    {
                        Application.Log.Fatal("version configuration can't be serialized!");
                        return null;
                    }
                }
            }
            else
            {
                VersionConfiguration versionConfiguration = new VersionConfiguration
                {
                    ServerVersion = "not set",
                    ClientVersion = "not set"
                };
                using (XmlWriter writer = XmlWriter.Create(filePath))
                {
                    serializer.Serialize(writer, versionConfiguration);
                }
                return versionConfiguration;
            }
        }
    }
}
