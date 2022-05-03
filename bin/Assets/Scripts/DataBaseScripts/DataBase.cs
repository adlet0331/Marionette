using UnityEngine;

public abstract class DataBase : ScriptableObject
{
    public abstract void LoadJson();
    public abstract void SaveJson();
}
