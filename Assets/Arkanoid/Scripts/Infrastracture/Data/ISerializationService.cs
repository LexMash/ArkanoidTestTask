using Newtonsoft.Json;

namespace Arkanoid.Levels
{
    public interface ISerializationService
    {
        string Serialize(object obj);
        T Deserialize<T>(string json);
    }

    public class NewtonJsonSerializator : ISerializationService
    {
        public T Deserialize<T>(string json) => JsonConvert.DeserializeObject<T>(json);

        public string Serialize(object obj) => JsonConvert.SerializeObject(obj);
    }
}