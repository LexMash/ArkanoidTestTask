using Newtonsoft.Json;

namespace Arkanoid.Levels
{
    public class NewtonJsonSerializator : ISerializationService
    {
        public T Deserialize<T>(string json) => JsonConvert.DeserializeObject<T>(json);

        public string Serialize(object obj) => JsonConvert.SerializeObject(obj);
    }
}