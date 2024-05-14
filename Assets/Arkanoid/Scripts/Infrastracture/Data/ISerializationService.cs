namespace Arkanoid.Levels
{
    public interface ISerializationService
    {
        string Serialize(object obj);
        T Deserialize<T>(string json);
    }
}