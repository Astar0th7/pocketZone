using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Storage
{
    private string _filePath;
    private BinaryFormatter _binaryFormatter;

    public Storage()
    {
        var directory = Application.persistentDataPath + "/Saves";
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        
        _filePath = directory + "/GameSave.data";
        InitBinaryFormatter();
    }

    private void InitBinaryFormatter()
    {
        _binaryFormatter = new BinaryFormatter();
        var selector = new SurrogateSelector();

        var vector3Surrogate = new Vector3SerializationSurrogate();

        selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);

        _binaryFormatter.SurrogateSelector = selector;
    }
    
    public object Load(object saveDataByDefault)
    {
        if (!File.Exists(_filePath))
        {
            if (saveDataByDefault != null)
                Save(saveDataByDefault);

            return saveDataByDefault;
        }

        var file = File.Open(_filePath, FileMode.Open);
        var saveData = _binaryFormatter.Deserialize(file);
        file.Close();
        return saveData;
    }

    public void Save(object saveData)
    {
        var file = File.Create(_filePath);
        _binaryFormatter.Serialize(file, saveData);
        file.Close();
    }
}
