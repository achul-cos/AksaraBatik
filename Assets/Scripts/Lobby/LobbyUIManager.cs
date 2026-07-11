using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUIManager : MonoBehaviour
{
    public GameObject _nextCustomerButton;

    [SerializeField] private CustomerQueueSlot[] _customerQueueSlots = new CustomerQueueSlot[3];

    private void OnEnable()
    {
        if (CustomerManager.Instance)
        {
            CustomerManager.Instance.CustomerArrived += OnCustomerArrived;
            CustomerManager.Instance.CustomerServed += OnCustomerServed;
        }
    }

    private void OnDisable()
    {
        if (CustomerManager.Instance)
        {
            CustomerManager.Instance.CustomerArrived -= OnCustomerArrived;
            CustomerManager.Instance.CustomerServed -= OnCustomerServed;
        }
    }

    private void OnCustomerArrived(Customer customer)
    {
        RestartDisplay();
    }

    private void OnCustomerServed(Customer customer)
    {
        RestartDisplay();
    }

    private void Start()
    {
        RestartDisplay();
    }

    /// <summary>
    /// Fungsi untuk melakukan restart display ada customer didalam scene
    /// </summary>
    private void RestartDisplay()
    {
        // Setiap customerqueueslot dikosongkan dulu
        for (int i = 0; i < _customerQueueSlots.Length; i++)
        {
            _customerQueueSlots[i].ClearDisplay();
        }

        // Mengambil data queue dari customer manager
        Queue<Customer> queue = CustomerManager.Instance.CustomerQueue;

        // Jika ada yang ngantri, maka masukkan antrian customer ke list customerqueueslot
        if (queue.Count > 0)
        {
            // Berikan nomor antrian pada setiap iterasi
            int indexAntrian = 0;
            foreach (Customer cust in queue)
            {
                // Jika nomor antrian sekarang lebih besar sama dengan jumlah slot di antrian
                // maka hentikan iterasi
                if (indexAntrian >= _customerQueueSlots.Length) break;

                // Tetapkan nilai customer pada slot antrian ini
                _customerQueueSlots[indexAntrian].SetCustomer(cust);

                // Setelahnya next antrian selanjutnya
                indexAntrian++;
            }
        }

        Debug.Log($"LobbyUIManager [RestartDisplay] : Melakukan restart slot antrian dengan jumlah antrian customer yaitu {queue.Count}");
    }

    /// <summary>
    /// Fungsi yang dijalankan ketika tombol next customer dipencet
    /// </summary>
    public void NextCustomer()
    {
        CustomerManager.Instance.GetNextCustomer();
    }
}

[System.Serializable]
public class CustomerQueueSlot
{
    public GameObject chair;
    public Customer customer;
    public Sprite customerSprite;

    public void SetCustomer(Customer cust)
    {
        customer = cust;

        SpriteRenderer spchair = chair.GetComponent<SpriteRenderer>();

        spchair.sprite = cust.customerSprite ? cust.customerSprite : CustomerManager.Instance.DefaultCustomerSprite;

    }

    public void ClearDisplay()
    {
        customer = null;

        chair.GetComponent<SpriteRenderer>().sprite = CustomerManager.Instance.DefaultChairSprite;
    }
}
