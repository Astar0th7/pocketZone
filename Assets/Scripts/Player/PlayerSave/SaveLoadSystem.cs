using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    private Storage _storage;
    private PlayerSaveData _playerSaveData;
    private Player _player;
    
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _storage = new Storage();
        Load();
    }

    public void Save()
    {
        _playerSaveData.Speed = _player.Speed;
        _playerSaveData.Health = _player.DefaultHealth;
        _playerSaveData.Position = _player.transform.position;
        _playerSaveData.Scale = _player.transform.localScale;
        
        _storage.Save(_playerSaveData);
    }
    
    public void Load()
    {
        _playerSaveData = (PlayerSaveData) _storage.Load(new PlayerSaveData());
        _player.SetSpeed(_playerSaveData.Speed);
        _player.SetHealth(_playerSaveData.Health);
        _player.transform.position = _playerSaveData.Position;
        _player.transform.localScale = _playerSaveData.Scale;
    }
}