using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BoilingManager : MonoBehaviour
{
    public Button MulaiButton;
    public Button StopButton;

    public Slider BoilingSlider;

    public float YellowBoilingTime = 3.0f;
    public float GreenBoilingTime = 6.0f;
    public float RedBoilingTime = 10.0f;

    public float time = 0f;

    private Coroutine timerCoroutine;
    private bool isBoiling = false;

    public GameObject pot;
    public GameObject fire;

    public Sprite waterPot;
    public Sprite NonwaterPot;

    private void Start()
    {
        MulaiButton.onClick.AddListener(StartBoiling);
        StopButton.onClick.AddListener(StopBoiling);

        StopButton.gameObject.SetActive(false);

        BoilingSlider.minValue = 0;
        BoilingSlider.maxValue = RedBoilingTime;
        BoilingSlider.value = 0;

        fire.SetActive(false);
    }


    private void StartBoiling()
    {
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);

        timerCoroutine = StartCoroutine(handleTimer());

        MulaiButton.gameObject.SetActive(false);
        StopButton.gameObject.SetActive(true);

        pot.GetComponent<SpriteRenderer>().sprite = waterPot;

        fire.SetActive(true);
    }


    private void StopBoiling()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }

        isBoiling = false;
        time = 0;

        BoilingSlider.value = 0;

        MulaiButton.gameObject.SetActive(true);
        StopButton.gameObject.SetActive(false);

        fire.SetActive(false);

        NextDrying();

        Debug.Log("Boiling stopped");
    }

    public void NextDrying()
    {
        GameManager.Instance.NextDrying();
    }


    public IEnumerator handleTimer()
    {
        isBoiling = true;

        while (isBoiling)
        {
            time += Time.deltaTime;

            BoilingSlider.value = time;


            if (time < YellowBoilingTime)
            {
                Debug.Log("Status : Kuning");
            }
            else if (time < GreenBoilingTime)
            {
                Debug.Log("Status : Hijau");
            }
            else if (time < RedBoilingTime)
            {
                Debug.Log("Status : Merah");
            }
            else
            {
                Debug.Log("Boiling selesai");

                isBoiling = false;

                MulaiButton.gameObject.SetActive(true);
                StopButton.gameObject.SetActive(false);
            }


            yield return null;
        }
    }
}