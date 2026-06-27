using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// Konfigurasi suatu day
/// </summary>
[System.Serializable]
public class DayConfig
{
    /// <summary>
    /// Nama day/hari. Biasanya untuk text atau title.
    /// </summary>
    public string dayName;

    /// <summary>
    /// Cuaca pada hari ini.
    /// </summary>
    public WeatherType dayWeather;

    /// <summary>
    /// Jumlah pelanggan yang datang pada hari ini.
    /// </summary>
    public int dayCustomers;

    /// <summary>
    /// Peluang untuk kedantangan customer berdasarkan kekayaannya
    /// </summary>
    public WealthCustomerChance[] dayWealthCustomersChances = new WealthCustomerChance[]
    {
        new WealthCustomerChance{wealthName = "Pelanggan Orang Miskin", wealthType = WealthType.MISKIN, wealthChance = 1.0f},
        new WealthCustomerChance{wealthName = "Pelanggan Orang Biasa", wealthType = WealthType.BIASA, wealthChance = 1.0f},
        new WealthCustomerChance{wealthName = "Pelanggan Orang Kaya Anjay", wealthType = WealthType.KAYA, wealthChance = 1.0f},
        new WealthCustomerChance{wealthName = "Pelanggan Orang Sultan Bejir", wealthType = WealthType.SULTAN, wealthChance = 1.0f},
    };

    /// <summary>
    /// Peluang untuk kedatangan customer bedasarkan perfeksionismenya
    /// </summary>
    public PerfectCustomerChance[] dayPerfectCustomerChances = new PerfectCustomerChance[]
    {
        new PerfectCustomerChance{perfectCustomerName = "(D) Gold Flag, 50% aja", perfectType = PerfectType.D, perfectChance = 1.0f},
        new PerfectCustomerChance{perfectCustomerName = "(C) Green Flag, 60% aja", perfectType = PerfectType.C, perfectChance = 1.0f},
        new PerfectCustomerChance{perfectCustomerName = "(B) Gray Flag, 70% baru cukup", perfectType = PerfectType.B, perfectChance = 1.0f},
        new PerfectCustomerChance{perfectCustomerName = "(A) Red Flag, 80% baru puas jir", perfectType = PerfectType.A, perfectChance = 1.0f},
        new PerfectCustomerChance{perfectCustomerName = "(S) BLACK FLAG, 90% standar perfek", perfectType = PerfectType.S, perfectChance = 1.0f},
    };

    /// <summary>
    /// Customer special yang diprioritaskan untuk hadir dan dapat ditentukan siapa customernya dan pada posisi ke berapa
    /// </summary>
    public SpecialCustomer[] daySpecialCustomers;

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("========== DAY CONFIG ==========");
        sb.AppendLine($"Day Name      : {dayName}");
        sb.AppendLine($"Weather       : {dayWeather}");
        sb.AppendLine($"Customers     : {dayCustomers}");

        sb.AppendLine();
        sb.AppendLine("=== Wealth Chances ===");

        foreach (var wealth in dayWealthCustomersChances)
        {
            sb.AppendLine(
                $"- {wealth.wealthName} | " +
                $"Type : {wealth.wealthType} | " +
                $"Chance : {wealth.wealthChance}"
            );
        }

        sb.AppendLine();
        sb.AppendLine("=== Perfect Chances ===");

        foreach (var perfect in dayPerfectCustomerChances)
        {
            sb.AppendLine(
                $"- {perfect.perfectCustomerName} | " +
                $"Type : {perfect.perfectType} | " +
                $"Chance : {perfect.perfectChance}"
            );
        }

        sb.AppendLine();
        sb.AppendLine("=== Special Customers ===");

        if (daySpecialCustomers == null || daySpecialCustomers.Length == 0)
        {
            sb.AppendLine("None");
        }
        else
        {
            foreach (var special in daySpecialCustomers)
            {
                sb.AppendLine(special.ToString());
            }
        }

        sb.AppendLine("==============================");

        return sb.ToString();
    }
}