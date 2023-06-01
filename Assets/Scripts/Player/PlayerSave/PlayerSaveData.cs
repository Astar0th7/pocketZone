using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    public float Speed;
    public float Health;
    public Vector3 Position;
    public Vector3 Scale;

    public PlayerSaveData()
    {
        Speed = 10;
        Health = 100;
        Position = new Vector3(0, 0, 0);
        Scale = new Vector3(1, 1, 1);
    }
}
