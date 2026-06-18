using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object Data yang menampung data save game, yang terdiri dari phase, day, balance dan timestamp
/// </summary>
[System.Serializable]
public class SaveData
{
    public int phase;
    public int day;
    public long balance;
    public string timeStamp;
}