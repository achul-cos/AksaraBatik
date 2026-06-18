using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PhasesConfigDatabases", menuName = "AksaraBatik/PhasesConfigDatabases")]
public class PhasesConfigDatabase : ScriptableObject
{
    public PhaseConfig[] phaseConfigs = PhasesConfigDefault.phaseConfigs;

    /// <summary>
    /// Fungsi yang mengambil nama hari (dayName) pada phaseConfigs,
    /// berdasarkan urutan hari yang diberikan.
    /// </summary>
    /// <param name="day">urutan hari</param>
    /// <returns>dayName atau nama hari</returns>
    public string GetDayName(int day)
    {
        // Memberikan urutan hari, misal 8
        // Mula-mula kita melakukan iterasi pada phaseConfigs, dan mencapai pada hari yang dinginkan

        // Validasi apakah phaseConfigs telah berisi atau tidak (kosong) atau panjang atau jumlah anggota nya tidak nol
        if (phaseConfigs == null || phaseConfigs.Length == 0)
        {
            Debug.LogError($"PhaseConfigDatabase [GetDayName] : Tidak dapat memberikan nama hari pada urutan {day} karena phaseConfigs bernilai kosong.");
            return null;
        }

        // Melakukan iterasi pada setiap phaseConfig pada _phaseConfig, serta phaseDay pada phaseConfig pada _phaseConfig,
        // untuk menemukan days pada urutan yang diminta oleh parameter

        int i = 0; // Index interasi
        int j = 0; // Index interasi phaseConfigs

        // Lakukan iterasi jika nilai i masih lebih kecil dari nilai day

        while (i < day)
        {
            while (j < phaseConfigs.Length)
            {
                // Setiap phaseConfigs yang kita iterasikan, kita mencari pada phaseConfigs ke berapa, phaseDay yang urutanya sama dengan parameter day, dia berada

                // Ini adalah jumlah hari yang berada pada phaseConfigs yang kita iterasikan
                int phaseDay = phaseConfigs[j].phaseDay.Length;

                // jika i yang kita tambahkan dengan phaseDay lebih kecil dari nilai day, maka itu bukan phaseConfigs yang kita maksud
                if ( i + phaseDay < day)
                {
                    i += phaseDay; // Maka tambahkan i dengan phaseDay, Biar langsung lanjut ke iterasi phaseConfigs selanjutnnya
                    j++;
                    continue;
                }

                // tetapi jika i yang ditambahkan dengan phaseDay tidak lebih kecil dari nilai, maka itu lah phaseConfigs yang kita maksudkan.
                string dayName = phaseConfigs[j].phaseDay[(day - i - 1)].dayName;

                // Mengembalikan nilai dayName berupa string, dimana itu adalah data nama hari pada urutan hari pada phaseConfigs
                return dayName;
            }
            break;
        }

        Debug.LogWarning($"PhaseConfigDatabase [GetDayName] : Tidak dapat memberikan nama hari pada hari ke-{day} yang diminta");
        return null;
    }

    /// <summary>
    /// Fungsi yang mengembalikan nilai day pertama pada phase yang diinput.
    /// </summary>
    /// <param name="phase">Phase/Fase yang ingin diminta nilai hari pertamanya</param>
    /// <returns>nilai day pertama dari phase yang diinput</returns>
    public int GetPhaseStartDay(int phase)
    {
        if (phase <= phaseConfigs.Length)
        {
            int phaseDays = 0;
            int i = 0;

            while (i < phase - 1)
            {
                phaseDays += phaseConfigs[i].phaseDay.Length;
                i++;
            }

            return phaseDays + 1;
        }
        else
        {
            Debug.LogWarning($"GameManager [GetPhaseStartDay] : Tidak ditemukan terdapat phase ke-{phase}");
            return 0;
        }
    }

    /// <summary>
    /// Fungsi yang mengembalikan nilai day terakhir pada phase yang diinput.
    /// </summary>
    /// <param name="phase">Phase/Fase yang ingin diminta nilai hari terakhirnya</param>
    /// <returns>nilai day tearkhir dari phase yang diinput</returns>
    public int GetPhaseEndDay(int phase)
    {
        if (phase <= phaseConfigs.Length)
        {
            int phaseDays = 0;
            int i = 0;

            while (i < phase)
            {
                phaseDays += phaseConfigs[i].phaseDay.Length;
                i++;
            }

            return phaseDays;
        }
        else
        {
            Debug.LogWarning($"GameManager [GetPhaseEndDay] : Tidak terdapat phase ke-{phase}");
            return 0;
        }
    }
}
