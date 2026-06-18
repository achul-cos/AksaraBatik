using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Object dari CustomerDatabase yang menyimpan data-data customer 
/// </summary>
[CreateAssetMenu(fileName = "CustomersDatabase", menuName = "AksaraBatik/CustomersDatabase")]
public class CustomersDatabase : ScriptableObject
{
    /// <summary>
    /// List customer yang didaftarkan 
    /// </summary>
    public List<Customer> customers = new List<Customer>();

    /// <summary>
    /// Mengambil data customer berdasarkan ID nya
    /// </summary>
    /// <param name="id">Id Customer</param>
    /// <returns></returns>
    public Customer GetCustomerById(string id)
    {
        foreach (Customer customer in customers)
        {
            if (customer.customerId == id)
            {
                return customer;
            }
        }

        Debug.LogError($"CustomersDatabase [GetCustomerById] Error : Tidak ditemukan customer dengan Id {id}");
        return null;
    }

    /// <summary>
    /// Fungsi untuk 'Mengacha' Customer bedasarkan chance nya
    /// </summary>
    /// <param name="customerWealthMiskinChance">Kemungkinanan kedatangan customer bertipe kekayaan miskin</param>
    /// <param name="customerWealthBiasaChance">Kemungkinanan kedatangan customer bertipe kekayaan biasa</param>
    /// <param name="customerWealthKayaChance">Kemungkinanan kedatangan customer bertipe kekayaan kaya</param>
    /// <param name="customerWealthSultanChance">Kemungkinanan kedatangan customer bertipe kekayaan Sultan</param>
    /// <param name="customerPerfectDChance">Kemungkinanan kedatangan customer bertipe perfeksionis yaitu D (50%)</param>
    /// <param name="customerPerfectCChance">Kemungkinanan kedatangan customer bertipe perfeksionis yaitu C (60%)</param>
    /// <param name="customerPerfectBChance">Kemungkinanan kedatangan customer bertipe perfeksionis yaitu B (70%)</param>
    /// <param name="customerPerfectAChance">Kemungkinanan kedatangan customer bertipe perfeksionis yaitu A (80%)</param>
    /// <param name="customerPerfectSChance">Kemungkinanan kedatangan customer bertipe perfeksionis yaitu S (90%)</param>
    public (WealthType, PerfectType) GetCustomerWealthAndPerfectTypeRandom
    (
        float customerWealthMiskinChance = 1,
        float customerWealthBiasaChance = 1,
        float customerWealthKayaChance = 1,
        float customerWealthSultanChance = 1,
        float customerPerfectDChance = 1,
        float customerPerfectCChance = 1,
        float customerPerfectBChance = 1,
        float customerPerfectAChance = 1,
        float customerPerfectSChance = 1
    )
    {
        float customerWealthChanceTotal = (float)System.Math.Round((customerWealthMiskinChance + customerWealthBiasaChance + customerWealthKayaChance + customerWealthSultanChance), 1);

        float wealthMiskinChance = (float)System.Math.Round((customerWealthMiskinChance / customerWealthChanceTotal), 1);
        float wealthBiasaChance = (float)System.Math.Round((customerWealthBiasaChance / customerWealthChanceTotal), 1);
        float wealthKayaChance = (float)System.Math.Round((customerWealthKayaChance / customerWealthChanceTotal), 1);
        float wealthSultanChance = (float)System.Math.Round((customerWealthSultanChance / customerWealthChanceTotal), 1);

        float wealthRandomPoint = Random.Range(0.1f, 1f);
        List<WealthType> wealthTypesChoosen = new List<WealthType>();
        WealthType wealthTypeChoosen = WealthType.BIASA;

        while (wealthRandomPoint >= 0.1f)
        {
            if (wealthMiskinChance > 0f)
            {
                if (wealthRandomPoint <= wealthMiskinChance)
                {
                    wealthTypesChoosen.Add(WealthType.MISKIN);
                }

                wealthRandomPoint -= wealthMiskinChance;

                if (wealthRandomPoint < 0.1f && wealthTypesChoosen.Count > 0)
                {
                    int a = Random.Range(0, wealthTypesChoosen.Count);

                    wealthTypeChoosen = wealthTypesChoosen[a];

                    break;
                }
            }

            if (wealthBiasaChance > 0f)
            {
                if (wealthRandomPoint <= wealthBiasaChance)
                {
                    wealthTypesChoosen.Add(WealthType.BIASA);
                }

                wealthRandomPoint -= wealthBiasaChance;

                if (wealthRandomPoint < 0.1f && wealthTypesChoosen.Count > 0)
                {
                    int a = Random.Range(0, wealthTypesChoosen.Count);

                    wealthTypeChoosen = wealthTypesChoosen[a];

                    break;
                }
            }

            if (wealthKayaChance > 0f)
            {
                if (wealthRandomPoint < wealthKayaChance)
                {
                    wealthTypesChoosen.Add(WealthType.KAYA);
                }

                wealthRandomPoint -= wealthKayaChance;

                if (wealthRandomPoint < 0.1f && wealthTypesChoosen.Count > 0)
                {
                    int a = Random.Range(0, wealthTypesChoosen.Count);

                    wealthTypeChoosen = wealthTypesChoosen[a];

                    break;
                }
            }

            if (wealthSultanChance > 0f)
            {
                if (wealthRandomPoint <= wealthSultanChance)
                {
                    wealthTypesChoosen.Add(WealthType.SULTAN);
                }

                wealthRandomPoint -= wealthSultanChance;

                if (wealthRandomPoint <= 0.1f && wealthTypesChoosen.Count > 0)
                {
                    int a = Random.Range(0, wealthTypesChoosen.Count);

                    wealthTypeChoosen = wealthTypesChoosen[a];

                    break;
                }
            }

            if (wealthTypesChoosen.Count == 0)
            {
                if (wealthRandomPoint <= 0.1f)
                {
                    wealthTypesChoosen.Add(WealthType.MISKIN);
                    wealthTypesChoosen.Add(WealthType.BIASA);
                    wealthTypesChoosen.Add(WealthType.KAYA);
                    wealthTypesChoosen.Add(WealthType.SULTAN);

                    int o = Random.Range(0, 4);

                    wealthTypeChoosen = wealthTypesChoosen[o];

                    break;
                }

                wealthRandomPoint -= 0.1f;

                continue;
            }
            else if (wealthTypesChoosen.Count >= 1)
            {
                if (wealthTypesChoosen.Count == 1)
                {
                    wealthTypeChoosen = wealthTypesChoosen[0];

                    break;
                }
                else if (wealthTypesChoosen.Count > 1)
                {
                    int q = wealthTypesChoosen.Count;

                    int p = Random.Range(0, q);

                    wealthTypeChoosen = wealthTypesChoosen[p];

                    break;
                }
            }
        }

        float customerPerfectChanceTotal = (float)System.Math.Round((customerPerfectDChance + customerPerfectCChance + customerPerfectBChance + customerPerfectAChance + customerPerfectSChance), 1);

        float PerfectDChance = (float)System.Math.Round((customerPerfectDChance / customerPerfectChanceTotal), 1);
        float PerfectCChance = (float)System.Math.Round((customerPerfectCChance / customerPerfectChanceTotal), 1);
        float PerfectBChance = (float)System.Math.Round((customerPerfectBChance / customerPerfectChanceTotal), 1);
        float PerfectAChance = (float)System.Math.Round((customerPerfectAChance / customerPerfectChanceTotal), 1);
        float PerfectSChance = (float)System.Math.Round((customerPerfectSChance / customerPerfectChanceTotal), 1);

        float perfectRandomPoint = Random.Range(0.1f, 1f);
        List<PerfectType> perfectTypesChoosen = new List<PerfectType>();
        PerfectType perfectTypeChoosen = PerfectType.C;

        while (perfectRandomPoint >= 0.1f)
        {
            if (PerfectDChance > 0f)
            {
                if (perfectRandomPoint <= PerfectDChance)
                {
                    perfectTypesChoosen.Add(PerfectType.D);
                }

                perfectRandomPoint -= PerfectDChance;

                if (perfectRandomPoint < 0.1f && perfectTypesChoosen.Count > 0)
                {
                    int a = Random.Range(0, perfectTypesChoosen.Count);

                    perfectTypeChoosen = perfectTypesChoosen[a];

                    break;
                }
            }

            if (PerfectCChance > 0f)
            {
                if (perfectRandomPoint <= PerfectCChance)
                {
                    perfectTypesChoosen.Add(PerfectType.C);
                }

                perfectRandomPoint -= PerfectCChance;

                if (perfectRandomPoint < 0.1f && perfectTypesChoosen.Count > 0)
                {
                    int a = Random.Range(0, perfectTypesChoosen.Count);

                    perfectTypeChoosen = perfectTypesChoosen[a];

                    break;
                }
            }

            if (PerfectBChance > 0f)
            {
                if (perfectRandomPoint < PerfectBChance)
                {
                    perfectTypesChoosen.Add(PerfectType.B);
                }

                perfectRandomPoint -= PerfectBChance;

                if (perfectRandomPoint < 0.1f && perfectTypesChoosen.Count > 0)
                {
                    int a = Random.Range(0, perfectTypesChoosen.Count);

                    perfectTypeChoosen = perfectTypesChoosen[a];

                    break;
                }
            }

            if (PerfectAChance > 0f)
            {
                if (perfectRandomPoint <= PerfectAChance)
                {
                    perfectTypesChoosen.Add(PerfectType.A);
                }

                perfectRandomPoint -= PerfectAChance;

                if (perfectRandomPoint <= 0.1f && perfectTypesChoosen.Count > 0)
                {
                    int a = Random.Range(0, perfectTypesChoosen.Count);

                    perfectTypeChoosen = perfectTypesChoosen[a];

                    break;
                }
            }

            if (PerfectSChance > 0f)
            {
                if (perfectRandomPoint <= PerfectSChance)
                {
                    perfectTypesChoosen.Add(PerfectType.S);
                }

                perfectRandomPoint -= PerfectSChance;

                if (perfectRandomPoint <= 0.1f && perfectTypesChoosen.Count > 0)
                {
                    int a = Random.Range(0, perfectTypesChoosen.Count);

                    perfectTypeChoosen = perfectTypesChoosen[a];

                    break;
                }
            }

            if (perfectTypesChoosen.Count == 0)
            {
                if (perfectRandomPoint <= 0.1f)
                {
                    perfectTypesChoosen.Add(PerfectType.D);
                    perfectTypesChoosen.Add(PerfectType.C);
                    perfectTypesChoosen.Add(PerfectType.B);
                    perfectTypesChoosen.Add(PerfectType.A);
                    perfectTypesChoosen.Add(PerfectType.S);

                    int o = Random.Range(0, 5);

                    perfectTypeChoosen = perfectTypesChoosen[o];

                    break;
                }

                perfectRandomPoint -= 0.1f;

                continue;
            }
            else if (perfectTypesChoosen.Count >= 1)
            {
                if (perfectTypesChoosen.Count == 1)
                {
                    perfectTypeChoosen = perfectTypesChoosen[0];

                    break;
                }
                else if (perfectTypesChoosen.Count > 1)
                {
                    int q = perfectTypesChoosen.Count;

                    int p = Random.Range(0, q);

                    perfectTypeChoosen = perfectTypesChoosen[p];

                    break;
                }
            }
        }

        return (wealthTypeChoosen, perfectTypeChoosen);
    }


    public Customer GetCustomerRandomByWealthAndPerfectType
    (
        (WealthType, PerfectType) x,
        float customerWealthMiskinChance,
        float customerWealthBiasaChance,
        float customerWealthKayaChance,
        float customerWealthSultanChance,
        float customerPerfectDChance,
        float customerPerfectCChance,
        float customerPerfectBChance,
        float customerPerfectAChance,
        float customerPerfectSChance
    )
    {
        // Kumpulan Customers yang memiliki wealthtype dan perfecttype yang sama yang akan digacha
        List<Customer> CustomersRandom = new List<Customer>();

        Customer customerChoosen;

        // Iterasikan semua customer didalam customersdatabase untuk mencari customer yang memiliki wealthtype dan perfecttype yang sama
        foreach (Customer customer in customers)
        {
            if (customer.customerWealth == x.Item1 && customer.customerPerfect == x.Item2)
            {
                CustomersRandom.Add(customer);
            }
        }

        // Jika tidak ada customer yang memiliki wealthtype dan perfecttype yang sama, maka gacha lagi ampe maksimal 5 kali ampe nemu
        if (CustomersRandom.Count == 0)
        {
            int i = 5;

            while (i > 0)
            {
                (WealthType, PerfectType) CutomerWealthAndPerfectTypeRandom = GetCustomerWealthAndPerfectTypeRandom
                (
                    customerWealthMiskinChance: customerWealthMiskinChance,
                    customerWealthBiasaChance: customerWealthBiasaChance,
                    customerWealthKayaChance: customerWealthKayaChance,
                    customerWealthSultanChance: customerWealthSultanChance,
                    customerPerfectDChance: customerPerfectDChance,
                    customerPerfectCChance: customerPerfectCChance,
                    customerPerfectBChance: customerPerfectBChance,
                    customerPerfectAChance: customerPerfectAChance,
                    customerPerfectSChance: customerPerfectSChance
                );

                foreach (Customer customer in customers)
                {
                    if (customer.customerWealth == CutomerWealthAndPerfectTypeRandom.Item1 && customer.customerPerfect == CutomerWealthAndPerfectTypeRandom.Item2)
                    {
                        CustomersRandom.Add(customer);
                    }
                }

                if (CustomersRandom.Count == 0)
                {
                    if (i == 1)
                    {
                        (WealthType, PerfectType) CustomerDefaultRandom = GetCustomerWealthAndPerfectTypeRandom
                        (
                            customerWealthMiskinChance: 0,
                            customerWealthBiasaChance: 1,
                            customerWealthKayaChance: 0,
                            customerWealthSultanChance: 0,
                            customerPerfectDChance: 0,
                            customerPerfectCChance: 1,
                            customerPerfectBChance: 0,
                            customerPerfectAChance: 0,
                            customerPerfectSChance: 0
                        );

                        foreach (Customer customer in customers)
                        {
                            if (customer.customerWealth == CustomerDefaultRandom.Item1 && customer.customerPerfect == CustomerDefaultRandom.Item2)
                            {
                                CustomersRandom.Add(customer);
                            }
                        }

                        int randomCustomer2 = Random.Range(0, CustomersRandom.Count);
                        customerChoosen = CustomersRandom[randomCustomer2];
                        return customerChoosen;
                    }

                    i--;
                    continue;
                }
                else if (CustomersRandom.Count > 0)
                {
                    int randomCustomer = Random.Range(0, CustomersRandom.Count);
                    customerChoosen = CustomersRandom[randomCustomer];
                    return customerChoosen;
                }
            }
        }
        else
        {
            int randomCustomer3 = Random.Range(0, CustomersRandom.Count);
            customerChoosen = CustomersRandom[randomCustomer3];
            return customerChoosen;
        }

        return customers[0];
    }

    public Customer GetCustomerRandom
    (
        float customerWealthMiskinChance = 0,
        float customerWealthBiasaChance = 1,
        float customerWealthKayaChance = 0,
        float customerWealthSultanChance = 0,
        float customerPerfectDChance = 0,
        float customerPerfectCChance = 1,
        float customerPerfectBChance = 0,
        float customerPerfectAChance = 0,
        float customerPerfectSChance = 0
    )
    {
        (WealthType, PerfectType) GetRandomWealthAndPerfectType = GetCustomerWealthAndPerfectTypeRandom
        (
            customerWealthMiskinChance: customerWealthMiskinChance,
            customerWealthBiasaChance: customerWealthBiasaChance,
            customerWealthKayaChance: customerWealthKayaChance,
            customerWealthSultanChance: customerWealthSultanChance,
            customerPerfectDChance: customerPerfectDChance,
            customerPerfectCChance: customerPerfectCChance,
            customerPerfectBChance: customerPerfectBChance,
            customerPerfectAChance: customerPerfectAChance,
            customerPerfectSChance: customerPerfectSChance
        );

        Customer customer = GetCustomerRandomByWealthAndPerfectType
        (
            x: GetRandomWealthAndPerfectType,
            customerWealthMiskinChance: customerWealthMiskinChance,
            customerWealthBiasaChance: customerWealthBiasaChance,
            customerWealthKayaChance: customerWealthKayaChance,
            customerWealthSultanChance: customerWealthSultanChance,
            customerPerfectDChance: customerPerfectDChance,
            customerPerfectCChance: customerPerfectCChance,
            customerPerfectBChance: customerPerfectBChance,
            customerPerfectAChance: customerPerfectAChance,
            customerPerfectSChance: customerPerfectSChance
        );

        return customer;
    }
}
