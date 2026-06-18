using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhasesConfigDefault
{
    public readonly static PhaseConfig[] phaseConfigs = new PhaseConfig[]
    {
        new PhaseConfig
        {
            phaseName = "Chapter/Phase Pertama",
            phaseDay = new DayConfig[]
            {
                new DayConfig
                {
                    dayName = "Hari Pertama",
                    dayWeather = WeatherType.Cerah,
                    dayCustomers = 5,
                    dayWealthCustomersChances = new WealthCustomerChance[]
                    {
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Miskin", wealthType = WealthType.MISKIN, wealthChance = 1.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Biasa", wealthType = WealthType.BIASA, wealthChance = 0.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Kaya Anjay", wealthType = WealthType.KAYA, wealthChance = 0.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Sultan Bejir", wealthType = WealthType.SULTAN, wealthChance = 0.0f},
                    },
                    dayPerfectCustomerChances = new PerfectCustomerChance[]
                    {
                        new PerfectCustomerChance{perfectCustomerName = "(D) Gold Flag, 50% aja", perfectType = PerfectType.D, perfectChance = 1.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(C) Green Flag, 60% aja", perfectType = PerfectType.C, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(B) Gray Flag, 70% baru cukup", perfectType = PerfectType.B, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(A) Red Flag, 80% baru puas jir", perfectType = PerfectType.A, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(S) BLACK FLAG, 90% standar perfek", perfectType = PerfectType.S, perfectChance = 0.0f},
                    },
                },
                new DayConfig
                {
                    dayName = "Hari Kedua",
                    dayWeather = WeatherType.Cerah,
                    dayCustomers = 10,
                    dayWealthCustomersChances = new WealthCustomerChance[]
                    {
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Miskin", wealthType = WealthType.MISKIN, wealthChance = 1.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Biasa", wealthType = WealthType.BIASA, wealthChance = 0.5f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Kaya Anjay", wealthType = WealthType.KAYA, wealthChance = 0.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Sultan Bejir", wealthType = WealthType.SULTAN, wealthChance = 0.0f},
                    },
                    dayPerfectCustomerChances = new PerfectCustomerChance[]
                    {
                        new PerfectCustomerChance{perfectCustomerName = "(D) Gold Flag, 50% aja", perfectType = PerfectType.D, perfectChance = 1.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(C) Green Flag, 60% aja", perfectType = PerfectType.C, perfectChance = 0.5f},
                        new PerfectCustomerChance{perfectCustomerName = "(B) Gray Flag, 70% baru cukup", perfectType = PerfectType.B, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(A) Red Flag, 80% baru puas jir", perfectType = PerfectType.A, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(S) BLACK FLAG, 90% standar perfek", perfectType = PerfectType.S, perfectChance = 0.0f},
                    },
                },
                new DayConfig
                {
                    dayName = "Hari Ketiga",
                    dayWeather = WeatherType.Cerah,
                    dayCustomers = 15,
                    dayWealthCustomersChances = new WealthCustomerChance[]
                    {
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Miskin", wealthType = WealthType.MISKIN, wealthChance = 1.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Biasa", wealthType = WealthType.BIASA, wealthChance = 1.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Kaya Anjay", wealthType = WealthType.KAYA, wealthChance = 0.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Sultan Bejir", wealthType = WealthType.SULTAN, wealthChance = 0.0f},
                    },
                    dayPerfectCustomerChances = new PerfectCustomerChance[]
                    {
                        new PerfectCustomerChance{perfectCustomerName = "(D) Gold Flag, 50% aja", perfectType = PerfectType.D, perfectChance = 1.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(C) Green Flag, 60% aja", perfectType = PerfectType.C, perfectChance = 1.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(B) Gray Flag, 70% baru cukup", perfectType = PerfectType.B, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(A) Red Flag, 80% baru puas jir", perfectType = PerfectType.A, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(S) BLACK FLAG, 90% standar perfek", perfectType = PerfectType.S, perfectChance = 0.0f},
                    },
                }
            },
            phaseTarget = new Target[]
            {
                new Target{targetName = TargetType.Customers.ToString(), targetType = TargetType.Customers, targetValue = 20},
                new Target{targetName = TargetType.Income.ToString(), targetType = TargetType.Income, targetValue = 0}
            }
        },
        new PhaseConfig
        {
            phaseName = "Chapter/Phase Kedua",
            phaseDay = new DayConfig[]
            {
                new DayConfig
                {
                    dayName = "Hari Keempat",
                    dayWeather = WeatherType.Berawan,
                    dayCustomers = 10,
                    dayWealthCustomersChances = new WealthCustomerChance[]
                    {
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Miskin", wealthType = WealthType.MISKIN, wealthChance = 0.5f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Biasa", wealthType = WealthType.BIASA, wealthChance = 1.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Kaya Anjay", wealthType = WealthType.KAYA, wealthChance = 0.5f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Sultan Bejir", wealthType = WealthType.SULTAN, wealthChance = 0.0f},
                    },
                    dayPerfectCustomerChances = new PerfectCustomerChance[]
                    {
                        new PerfectCustomerChance{perfectCustomerName = "(D) Gold Flag, 50% aja", perfectType = PerfectType.D, perfectChance = 0.5f},
                        new PerfectCustomerChance{perfectCustomerName = "(C) Green Flag, 60% aja", perfectType = PerfectType.C, perfectChance = 1.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(B) Gray Flag, 70% baru cukup", perfectType = PerfectType.B, perfectChance = 0.8f},
                        new PerfectCustomerChance{perfectCustomerName = "(A) Red Flag, 80% baru puas jir", perfectType = PerfectType.A, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(S) BLACK FLAG, 90% standar perfek", perfectType = PerfectType.S, perfectChance = 0.0f},
                    },
                },
                new DayConfig
                {
                    dayName = "Hari Kelima",
                    dayWeather = WeatherType.Hujan,
                    dayCustomers = 7,
                    dayWealthCustomersChances = new WealthCustomerChance[]
                    {
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Miskin", wealthType = WealthType.MISKIN, wealthChance = 0.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Biasa", wealthType = WealthType.BIASA, wealthChance = 0.6f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Kaya Anjay", wealthType = WealthType.KAYA, wealthChance = 1.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Sultan Bejir", wealthType = WealthType.SULTAN, wealthChance = 0.4f},
                    },
                    dayPerfectCustomerChances = new PerfectCustomerChance[]
                    {
                        new PerfectCustomerChance{perfectCustomerName = "(D) Gold Flag, 50% aja", perfectType = PerfectType.D, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(C) Green Flag, 60% aja", perfectType = PerfectType.C, perfectChance = 1.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(B) Gray Flag, 70% baru cukup", perfectType = PerfectType.B, perfectChance = 1.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(A) Red Flag, 80% baru puas jir", perfectType = PerfectType.A, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(S) BLACK FLAG, 90% standar perfek", perfectType = PerfectType.S, perfectChance = 0.0f},
                    },
                },
                new DayConfig
                {
                    dayName = "Hari Keenam",
                    dayWeather = WeatherType.Badai,
                    dayCustomers = 5,
                    dayWealthCustomersChances = new WealthCustomerChance[]
                    {
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Miskin", wealthType = WealthType.MISKIN, wealthChance = 0.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Biasa", wealthType = WealthType.BIASA, wealthChance = 0.2f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Kaya Anjay", wealthType = WealthType.KAYA, wealthChance = 1.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Sultan Bejir", wealthType = WealthType.SULTAN, wealthChance = 0.8f},
                    },
                    dayPerfectCustomerChances = new PerfectCustomerChance[]
                    {
                        new PerfectCustomerChance{perfectCustomerName = "(D) Gold Flag, 50% aja", perfectType = PerfectType.D, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(C) Green Flag, 60% aja", perfectType = PerfectType.C, perfectChance = 0.5f},
                        new PerfectCustomerChance{perfectCustomerName = "(B) Gray Flag, 70% baru cukup", perfectType = PerfectType.B, perfectChance = 1.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(A) Red Flag, 80% baru puas jir", perfectType = PerfectType.A, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(S) BLACK FLAG, 90% standar perfek", perfectType = PerfectType.S, perfectChance = 0.0f},
                    },
                },
                new DayConfig
                {
                    dayName = "Hari Ketujuh",
                    dayWeather = WeatherType.Cerah,
                    dayCustomers = 15,
                    dayWealthCustomersChances = new WealthCustomerChance[]
                    {
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Miskin", wealthType = WealthType.MISKIN, wealthChance = 0.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Biasa", wealthType = WealthType.BIASA, wealthChance = 0.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Kaya Anjay", wealthType = WealthType.KAYA, wealthChance = 1.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Sultan Bejir", wealthType = WealthType.SULTAN, wealthChance = 0.8f},
                    },
                    dayPerfectCustomerChances = new PerfectCustomerChance[]
                    {
                        new PerfectCustomerChance{perfectCustomerName = "(D) Gold Flag, 50% aja", perfectType = PerfectType.D, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(C) Green Flag, 60% aja", perfectType = PerfectType.C, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(B) Gray Flag, 70% baru cukup", perfectType = PerfectType.B, perfectChance = 1.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(A) Red Flag, 80% baru puas jir", perfectType = PerfectType.A, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(S) BLACK FLAG, 90% standar perfek", perfectType = PerfectType.S, perfectChance = 0.0f},
                    },
                },
            },
            phaseTarget = new Target[]
            {
                new Target{targetName = TargetType.Customers.ToString(), targetType = TargetType.Customers, targetValue = 20},
                new Target{targetName = TargetType.Income.ToString(), targetType = TargetType.Income, targetValue = 1000000}
            }
        },
        new PhaseConfig
        {
            phaseName = "Chapter/Phase Ketiga",
            phaseDay = new DayConfig[]
            {
                new DayConfig
                {
                    dayName = "Hari Kedelapan",
                    dayWeather = WeatherType.Cerah,
                    dayCustomers = 12,
                    dayWealthCustomersChances = new WealthCustomerChance[]
                    {
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Miskin", wealthType = WealthType.MISKIN, wealthChance = 0.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Biasa", wealthType = WealthType.BIASA, wealthChance = 0.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Kaya Anjay", wealthType = WealthType.KAYA, wealthChance = 1.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Sultan Bejir", wealthType = WealthType.SULTAN, wealthChance = 1.0f},
                    },
                    dayPerfectCustomerChances = new PerfectCustomerChance[]
                    {
                        new PerfectCustomerChance{perfectCustomerName = "(D) Gold Flag, 50% aja", perfectType = PerfectType.D, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(C) Green Flag, 60% aja", perfectType = PerfectType.C, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(B) Gray Flag, 70% baru cukup", perfectType = PerfectType.B, perfectChance = 1.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(A) Red Flag, 80% baru puas jir", perfectType = PerfectType.A, perfectChance = 0.5f},
                        new PerfectCustomerChance{perfectCustomerName = "(S) BLACK FLAG, 90% standar perfek", perfectType = PerfectType.S, perfectChance = 0.0f},
                    },
                },
                new DayConfig
                {
                    dayName = "Hari Kesembilan",
                    dayWeather = WeatherType.Badai,
                    dayCustomers = 8,
                    dayWealthCustomersChances = new WealthCustomerChance[]
                    {
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Miskin", wealthType = WealthType.MISKIN, wealthChance = 0.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Biasa", wealthType = WealthType.BIASA, wealthChance = 0.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Kaya Anjay", wealthType = WealthType.KAYA, wealthChance = 1.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Sultan Bejir", wealthType = WealthType.SULTAN, wealthChance = 1.0f},
                    },
                    dayPerfectCustomerChances = new PerfectCustomerChance[]
                    {
                        new PerfectCustomerChance{perfectCustomerName = "(D) Gold Flag, 50% aja", perfectType = PerfectType.D, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(C) Green Flag, 60% aja", perfectType = PerfectType.C, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(B) Gray Flag, 70% baru cukup", perfectType = PerfectType.B, perfectChance = 0.8f},
                        new PerfectCustomerChance{perfectCustomerName = "(A) Red Flag, 80% baru puas jir", perfectType = PerfectType.A, perfectChance = 0.5f},
                        new PerfectCustomerChance{perfectCustomerName = "(S) BLACK FLAG, 90% standar perfek", perfectType = PerfectType.S, perfectChance = 0.2f},
                    },
                },
                new DayConfig
                {
                    dayName = "Hari Kesepuluh",
                    dayWeather = WeatherType.Cerah,
                    dayCustomers = 15,
                    dayWealthCustomersChances = new WealthCustomerChance[]
                    {
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Miskin", wealthType = WealthType.MISKIN, wealthChance = 0.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Biasa", wealthType = WealthType.BIASA, wealthChance = 0.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Kaya Anjay", wealthType = WealthType.KAYA, wealthChance = 1.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Sultan Bejir", wealthType = WealthType.SULTAN, wealthChance = 1.0f},
                    },
                    dayPerfectCustomerChances = new PerfectCustomerChance[]
                    {
                        new PerfectCustomerChance{perfectCustomerName = "(D) Gold Flag, 50% aja", perfectType = PerfectType.D, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(C) Green Flag, 60% aja", perfectType = PerfectType.C, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(B) Gray Flag, 70% baru cukup", perfectType = PerfectType.B, perfectChance = 0.5f},
                        new PerfectCustomerChance{perfectCustomerName = "(A) Red Flag, 80% baru puas jir", perfectType = PerfectType.A, perfectChance = 0.5f},
                        new PerfectCustomerChance{perfectCustomerName = "(S) BLACK FLAG, 90% standar perfek", perfectType = PerfectType.S, perfectChance = 0.5f},
                    },
                }
            },
            phaseTarget = new Target[]
            {
                new Target{targetName = TargetType.Customers.ToString(), targetType = TargetType.Customers, targetValue = 30},
                new Target{targetName = TargetType.Income.ToString(), targetType = TargetType.Income, targetValue = 1500000}
            }
        },
        new PhaseConfig
        {
            phaseName = "Chapter/Phase Final",
            phaseDay = new DayConfig[]
            {
                new DayConfig
                {
                    dayName = "Hari Kesebelas / Terakhir",
                    dayWeather = WeatherType.Berawan,
                    dayCustomers = 1,
                    dayWealthCustomersChances = new WealthCustomerChance[]
                    {
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Miskin", wealthType = WealthType.MISKIN, wealthChance = 0.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Biasa", wealthType = WealthType.BIASA, wealthChance = 0.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Kaya Anjay", wealthType = WealthType.KAYA, wealthChance = 1.0f},
                        new WealthCustomerChance{wealthName = "Pelanggan Orang Sultan Bejir", wealthType = WealthType.SULTAN, wealthChance = 1.0f},
                    },
                    dayPerfectCustomerChances = new PerfectCustomerChance[]
                    {
                        new PerfectCustomerChance{perfectCustomerName = "(D) Gold Flag, 50% aja", perfectType = PerfectType.D, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(C) Green Flag, 60% aja", perfectType = PerfectType.C, perfectChance = 0.0f},
                        new PerfectCustomerChance{perfectCustomerName = "(B) Gray Flag, 70% baru cukup", perfectType = PerfectType.B, perfectChance = 0.5f},
                        new PerfectCustomerChance{perfectCustomerName = "(A) Red Flag, 80% baru puas jir", perfectType = PerfectType.A, perfectChance = 0.5f},
                        new PerfectCustomerChance{perfectCustomerName = "(S) BLACK FLAG, 90% standar perfek", perfectType = PerfectType.S, perfectChance = 0.5f},
                    }
                }
            },
            phaseTarget = new Target[]
            {
                new Target{targetName = TargetType.Customers.ToString(), targetType = TargetType.Customers, targetValue = 1},
                new Target{targetName = TargetType.Income.ToString(), targetType = TargetType.Income, targetValue = 0}
            }
        }
    };

}
