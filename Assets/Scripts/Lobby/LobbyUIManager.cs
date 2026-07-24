using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class LobbyUIManager : MonoBehaviour
{
    public Button nextCustomerButton;
    public GameObject dialogPanel;
    public TextMeshProUGUI nameChara;
    public RawImage charaImage;
    public TextMeshProUGUI dialogChara;
    public Button nextDialog;
    public int indexDialog = 0;

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
        DisplayNextCustomerButton();
    }

    private void OnCustomerServed(Customer customer)
    {
        RestartDisplay();
        DisplayNextCustomerButton();
    }

    public void debug()
    {
        Debug.Log("Ping");
    }

    private void Start()
    {
        RestartDisplay();
        DisplayNextCustomerButton();

        nextCustomerButton.onClick.AddListener(HandleDialogCustomer);
        nextDialog.onClick.AddListener(HandleCustomerDialog);
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

    public void HandleDialogCustomer()
    {
        if (GameManager.Instance.NextDialog() == true)
        {
            dialogPanel.SetActive(true);
            nameChara.text = CustomerManager.Instance.CustomerCurrent.customerName;
            charaImage.texture = CustomerManager.Instance.CustomerCurrent.customerImage;
            HandleCustomerDialog();
        }
    }

    public IEnumerator TypeText(TextMeshProUGUI text, float typingSpeed = 0.05f)
    {
        text.maxVisibleCharacters = 0;

        for (int i = 0; i <= text.text.Length; i++)
        {
            text.maxVisibleCharacters = i;

            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public IEnumerator HandleCooldownNextDialogButton(GameObject button, string DialogText, float ReadFast = 0.05f)
    {
        button.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).SetEase(Ease.OutCubic);
        button.GetComponent<Button>().interactable = false;

        yield return new WaitForSeconds(ReadFast * DialogText.Length);

        button.GetComponent<CanvasGroup>().DOFade(1f, 0.5f).SetEase(Ease.OutCubic);
        button.GetComponent<Button>().interactable = true;
    }

    public void HandleCustomerDialog()
    {
        if (indexDialog < CustomerManager.Instance.CustomerCurrent.customerDialogList.Count)
        {
            dialogChara.text = CustomerManager.Instance.CustomerCurrent.customerDialogList[indexDialog];

            StartCoroutine(TypeText(dialogChara));

            StartCoroutine(HandleCooldownNextDialogButton(nextDialog.transform.gameObject, CustomerManager.Instance.CustomerCurrent.customerDialogList[indexDialog]));

            indexDialog ++;

            return;
        }
        else
        {
            indexDialog = 0;

            // Jalankan fungsi pindah ke scene ngebatik
            GameManager.Instance.LoadGameScene(GameState.ChoosingFabric);

            return;
        }
    }

    public void DisplayNextCustomerButton()
    {
        if (CustomerManager.Instance.CustomerCurrent == null && CustomerManager.Instance.CustomerQueueCount >= 1)
        {
            Debug.Log("Tombol Ambil Pesanan Muncul");
            GameObject goNextCustomerButton = nextCustomerButton.transform.gameObject;
            goNextCustomerButton.transform.DOLocalMove(new Vector3(721f, -384f), 1.5f).SetEase(Ease.OutCubic);
        }
        else
        {
            Debug.Log("Tombol Ambil Pesanan Menghilang");
            GameObject goNextCustomerButton = nextCustomerButton.transform.gameObject;
            goNextCustomerButton.transform.DOLocalMove(new Vector3(721f, -764f), 1.5f).SetEase(Ease.OutCubic);
        }
    }
}

[System.Serializable]
public class CustomerQueueSlot
{
    public GameObject chair;
    public Customer customer;
    public Sprite customerSprite;
    public Vector3 startPosition;
    public Vector3 endPosition;
    public bool isCustomer = false;

    public void SetCustomer(Customer cust)
    {
        customer = cust;

        SpriteRenderer spchair = chair.GetComponent<SpriteRenderer>();

        spchair.sprite = cust.customerSprite ? cust.customerSprite : CustomerManager.Instance.DefaultCustomerSprite;

        if (isCustomer == false)
        {
            chair.transform.position = startPosition;

            chair.transform.DOMove(endPosition, 2f).SetEase(Ease.OutCubic);

            isCustomer = true;
        }
        
    }

    public void ClearDisplay()
    {

        if (isCustomer == false)
        {
            customer = null;

            chair.GetComponent<SpriteRenderer>().sprite = CustomerManager.Instance.DefaultChairSprite;

            chair.transform.position = endPosition;

            chair.transform.DOMove(startPosition, 2f).SetEase(Ease.OutCubic);
        }

    }
}