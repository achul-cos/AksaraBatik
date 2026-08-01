using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class SaveDataNew
{
    public List<PhaseSaveData> phaseSaveDatas = new List<PhaseSaveData>();
    public int phaseIndex;
}

[System.Serializable]
public class PhaseSaveData
{
    public PhaseConfig phaseConfig;
    public List<DaySaveData> daySaveDatas = new List<DaySaveData>();
    public Time timestamp;
    public int dayIndex;
}

[System.Serializable]
public class DaySaveData
{
    public DayConfig DayConfig;
    public Time timestamp;
    public int customerServed;
    public double balance;
}

public static class SaveDataNewDefault
{
    public static SaveDataNew Create()
    {
        var saveData = new SaveDataNew
        {
            phaseIndex = 0,
            phaseSaveDatas = new List<PhaseSaveData>()
        };

        foreach (var phase in PhasesConfigDefault.phaseConfigs)
        {
            var phaseSaveData = new PhaseSaveData
            {
                phaseConfig = phase,
                dayIndex = 0,
                timestamp = new Time(),
                daySaveDatas = new List<DaySaveData>()
            };

            // Membuat save data untuk setiap hari pada phase
            foreach (var day in phase.phaseDay)
            {
                phaseSaveData.daySaveDatas.Add(new DaySaveData
                {
                    DayConfig = day,
                    timestamp = new Time(),
                    customerServed = 0,
                    balance = 0
                });
            }

            saveData.phaseSaveDatas.Add(phaseSaveData);
        }

        return saveData;
    }
}