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
        if (phase >= 1 && phase <= phaseConfigs.Length)
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
            Debug.LogWarning($"PhaseConfigDatabase [GetPhaseStartDay] : Tidak ditemukan terdapat phase ke-{phase}");
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
    
    /// <summary>
    /// Mengambil data-data day atau hari yang berjalan didalam game pada dayConfig,
    /// berdasarkan pada phase dan hari keberapa
    /// </summary>
    /// <param name="phase">Phase ke berapa di game</param>
    /// <param name="day">Hari keberapan di phase tersebut</param>
    /// <returns>Objek Dayconfig yang berisi data-data yang berjalan pada hari itu</returns>
    public DayConfig GetDayConfigByPhase(int phase, int day)
    {
        // Validasi bahwa jika phase yang berikan itu lebih kecil dari 1,
        // atau lebih besar dari jumlah phase didalam game
        // maka kembalikan null atau batalkan.
        if (phase < 1 || phase > phaseConfigs.Length)
        {
            Debug.LogError($"PhasesConfigDatabase [GetDayConfig] : Phase ke - {phase} itu tidak valid karena lebih kecil dari 1 (negatif atau nol), atau memang tidak ada phase ke - {phase} pada database.");
            return null;
        }

        // Ambil objek phaseConfig pada urutan index yang disesuikan dengan urutan phase pada parameter
        PhaseConfig phaseConfig = phaseConfigs[phase - 1];

        // day pada parameter menunjukkan urutan hari pada phase yang dituju
        // misal phase ke-2, hari ke-2
        // Maka data hari atau dayConfig berada di variabel phaseDay pada phaseConfig
        // dengan index ke urutan (nilai day pada parameter) dikurangi 1.

        // validasi bahwa nilai day yang dimasukkan pada parameter
        // tidak lebih besar dari pada jumlah hari pada phase
        // jika tidak maka kembalikan null dan console error
        if (day < 1 || day > phaseConfig.phaseDay.Length)
        {
            Debug.LogError($"PhasesConfigDatabase [GetDayConfig] : Day ke-{day} pada Phase ke-{phase} itu tidak valid. Bisa jadi karena lebih kecil dari satu atau negatif, atau memang tidak ada hari ke-{day} pada phase ke-{phase}");
            return null;
        }

        DayConfig dayConfig = phaseConfig.phaseDay[day - 1];

        return dayConfig;
    }

    /// <summary>
    /// Mendapatkan data dayconfig berdasarkan paramter hari ke berapan di game
    /// </summary>
    /// <param name="day">Pada hari keberapa di game</param>
    /// <returns>Data hari dayconfig yang dituju</returns>
    public DayConfig GetDayConfig(int day)
    {
        // Validasi bahwa day tidak boleh lebih kecil dari 1 atau negatif, serta tidak boleh lebih banyak dari jumlah hari yang ada didalam database
        if (day < 1 || day > GetDayLenghth())
        {
            Debug.LogError("PhaseConfigDatabase [GetDayConfig] Error : Tidak dapat memberikan data dayConfig karena day yang didaftarkan pada parameter bernilai invalid. Bernilai lebih kecil dari satu atau negatif, atau lebih banyak dari jumlah hari yang ada di dalam database.");
            return null;
        }

        // Memberikan urutan hari, misal 8
        // Mula-mula kita melakukan iterasi pada phaseConfigs, dan mencapai pada hari yang dinginkan

        // Validasi apakah phaseConfigs telah berisi atau tidak (kosong) atau panjang atau jumlah anggota nya tidak nol
        if (phaseConfigs == null || phaseConfigs.Length == 0)
        {
            Debug.LogError($"PhaseConfigDatabase [GetDayConfig] : Tidak dapat memberikan data hari pada urutan {day} karena phaseConfigs bernilai kosong.");
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
                if (i + phaseDay < day)
                {
                    i += phaseDay; // Maka tambahkan i dengan phaseDay, Biar langsung lanjut ke iterasi phaseConfigs selanjutnnya
                    j++;
                    continue;
                }

                // tetapi jika i yang ditambahkan dengan phaseDay tidak lebih kecil dari nilai, maka itu lah phaseConfigs yang kita maksudkan.
                DayConfig dayConfig = phaseConfigs[j].phaseDay[(day - i - 1)];

                // Mengembalikan nilai dayName berupa string, dimana itu adalah data nama hari pada urutan hari pada phaseConfigs
                return dayConfig;
            }
            break;
        }

        Debug.LogWarning($"PhaseConfigDatabase [GetDayConfig] : Tidak dapat memberikan nama hari pada hari ke-{day} yang diminta");
        return null;
    }

    /// <summary>
    /// Memberikan berapa banyak hari pada suatu phase
    /// </summary>
    /// <param name="phase">Pada phase ke berapa yang ingin dicari berapa banyak harinya</param>
    /// <returns>Jumlah hari pada pada phase itu</returns>
    public int GetPhaseLengthDay(int phase)
    {
        // Validasi bahwa jika phase yang berikan itu lebih kecil dari 1,
        // atau lebih besar dari jumlah phase didalam game
        // maka kembalikan null atau batalkan.
        if (phase < 1 || phase > phaseConfigs.Length)
        {
            Debug.LogError($"PhasesConfigDatabase [GetPhaseLengthDay] : Phase ke - {phase} itu tidak valid karena lebih kecil dari 1 (negatif atau nol), atau memang tidak ada phase ke - {phase} pada database.");
            return 0;
        }

        return phaseConfigs[phase - 1].phaseDay.Length;
    }

    /// <summary>
    /// Memberikan jumlah phase didalam game
    /// </summary>
    /// <returns>Jumlah phase didalam game</returns>
    public int GetPhaseLength()
    {
        return phaseConfigs.Length;
    } 

    /// <summary>
    /// Memberikan data phase berupa PhaseConfig berdasarkan day yang diberikan
    /// </summary>
    /// <param name="day">Pada hari keberapa</param>
    /// <returns>Data phase pada day yang ditentukan</returns>
    public PhaseConfig GetPhaseConfigByDay(int day)
    {
        // Validasi jika day bernilai lebih kecil dari satu atau negatif
        // Maka kembalikan null
        if (day < 1)
        {
            return null;
        }

        int phases = phaseConfigs.Length;

        int i = 0;
        int j = 0;

        // Untuk mencari tahu pada phase keberapa yang memiliki hari dengan urutan yang sama dengan urutan hari pada parameter (day)
        // Maka kita akan melakukan iterasi.
        // Mula-mula kita akan membuat permisalan, jika DAY lebih kecil dari jumlah total hari dari keseluruhan phase sebelumnya
        // pada iterasi, maka pada phase pada index itu lah phase yang dicari.

        while (i < phases)
        {
            if (day <= j + phaseConfigs[i].phaseDay.Length)
            {
                return phaseConfigs[i];
            }

            j += phaseConfigs[i].phaseDay.Length;

            i++;
        }

        // Jika pada iterasi belum menghasilkan return phaseConfigs, maka asumsinya day yang diberikan melebihi dari jumlah keseluruhan day pada phase yang ada di database
        // Maka berikan console error, dan return null
        Debug.LogError($"PhasesConfigDatabase [GetPhaseConfigByDay] Error : Day ke-{day} tidak valid pada phase apapun. Kemungkinan jumlah hari pada phase-phase lebih kecil daripada Day ke-{day} yang dinput di parameter.");
        return null;
    }

    /// <summary>
    /// Memberikan data pahase berupaa PhaseConfig berdasarkan phase yang diberikan
    /// </summary>
    /// <param name="phase"></param>
    /// <returns></returns>
    public PhaseConfig GetPhaseConfigByPhase(int phase)
    {
        if(phase < GetPhaseLength() + 2 && phase > 0)
        {
            return phaseConfigs[phase - 1];
        }
        else
        {
            Debug.LogError($"PhasesConfigDatabase [GetPhaseConfigByPhase] : Tidak dapat memberikan data phaseCOnfig pada ke-{phase} karena lebih besar dari jumlah phases didalam game {GetPhaseLength() + 1 } atau lebih kecil dari 1");
            return null;
        }
    }

    /// <summary>
    /// Memberikan urutan phase berdasarkan day yang diberikan
    /// </summary>
    /// <param name="day">Pada hari keberapa</param>
    /// <returns>urutan phase</returns>
    public int GetPhaseFromDay(int day)
    {
        // Validasi jika day bernilai lebih kecil dari satu atau negatif
        // Maka kembalikan null
        if (day < 1)
        {
            return 0;
        }

        int phases = phaseConfigs.Length;

        int i = 0;
        int j = 0;

        // Untuk mencari tahu pada phase keberapa yang memiliki hari dengan urutan yang sama dengan urutan hari pada parameter (day)
        // Maka kita akan melakukan iterasi.
        // Mula-mula kita akan membuat permisalan, jika DAY lebih kecil dari jumlah total hari dari keseluruhan phase sebelumnya
        // pada iterasi, maka pada phase pada index itu lah phase yang dicari.

        while (i < phases)
        {
            if (day <= j + phaseConfigs[i].phaseDay.Length)
            {
                return i + 1;
            }

            j += phaseConfigs[i].phaseDay.Length;

            i++;
        }

        // Jika pada iterasi belum menghasilkan return phaseConfigs, maka asumsinya day yang diberikan melebihi dari jumlah keseluruhan day pada phase yang ada di database
        // Maka berikan console error, dan return null
        Debug.LogError($"PhasesConfigDatabase [GetPhaseConfigByDay] Error : Day ke-{day} tidak valid pada phase apapun. Kemungkinan jumlah hari pada phase-phase lebih kecil daripada Day ke-{day} yang dinput di parameter.");
        return 0;
    }

    public int GetDayLenghth()
    {
        if (GetPhaseLength() < 1)
        {
            Debug.LogWarning("PhasesConfigDatabase [GetDayLenghth] : Tidak dapat memberikan jumlah hari didalam database, dikarenkana belum ada phases didalam database.");
            return 0;
        }

        int i = 0;

        foreach (PhaseConfig p in phaseConfigs)
        {
            i += p.phaseDay.Length;
        }

        return i;
    }
}
