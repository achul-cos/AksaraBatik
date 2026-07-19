using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class NextButton : MonoBehaviour
{
    [SerializeField] private GameObject _komikImage;
    public List<Komik> komiks = new List<Komik>();
    public TextMeshProUGUI textNextButton;
    public int index = 0;
    public int indexUrutan = 0;
    public int indexPosisi = 0;
    public float coolDown = 1f;
    public bool isCooldown = false;

    private void Start()
    {
        if (komiks.Count == 0) return;

        RawImage rImage = _komikImage.GetComponent<RawImage>();
        rImage.texture = komiks[0].gambarKomik;
        Vector3 posisiAwal = _komikImage.transform.position;
        _komikImage.transform.position = new Vector3(posisiAwal.x, komiks[0].posisiUrutan[0], posisiAwal.z);
    }

    public void Next()
    {
        if (isCooldown == true) return;

        StartCoroutine(handleCooldow());
    }

    public IEnumerator handleCooldow()
    {
        if (isCooldown == true) yield return null;

        isCooldown = true;

        StartCoroutine(handleNext());

        yield return new WaitForSeconds(coolDown);

        isCooldown = false;
    }

    public IEnumerator handleNext()
    {
        if (komiks.Count == 0) yield return null;

        if (komiks.Count > indexUrutan + 1)
        {
            if (komiks[indexUrutan + 1].posisiUrutan.Length > indexPosisi + 1)
            {
                Vector3 posisiAwal = _komikImage.transform.position;
                _komikImage.transform.DOMove(new Vector3(posisiAwal.x, komiks[indexUrutan + 1].posisiUrutan[indexPosisi + 1], posisiAwal.z), 0.5f).SetEase(Ease.InOutCubic);
                // _komikImage.transform.position = new Vector3(posisiAwal.x, komiks[indexUrutan + 1].posisiUrutan[indexPosisi + 1], posisiAwal.z);
                indexPosisi += 1;
            }   
            else
            {
                Vector3 posisiAwal = _komikImage.transform.position;
                _komikImage.transform.DOMove(new Vector3(posisiAwal.x, komiks[indexUrutan + 1].posisiUrutan[0], posisiAwal.z), 0.5f).SetEase(Ease.InOutCubic);
                //_komikImage.transform.position = new Vector3(posisiAwal.x, komiks[indexUrutan + 1].posisiUrutan[0], posisiAwal.z);
                yield return new WaitForSeconds(0.25f);
                RawImage rImage = _komikImage.GetComponent<RawImage>();
                rImage.texture = komiks[indexUrutan + 1].gambarKomik;

                indexPosisi = 0;
                indexUrutan += 1;
                index++;
            }
        }
        else
        {
            if (komiks[indexUrutan - 1].posisiUrutan.Length > indexPosisi + 1)
            {
                Vector3 posisiAwal = _komikImage.transform.position;
                _komikImage.transform.DOMove(new Vector3(posisiAwal.x, komiks[indexUrutan - 1].posisiUrutan[indexPosisi + 1], posisiAwal.z), 0.5f).SetEase(Ease.InOutCubic);
                // _komikImage.transform.position = new Vector3(posisiAwal.x, komiks[indexUrutan - 1].posisiUrutan[indexPosisi + 1], posisiAwal.z);
                indexPosisi += 1;

                if (indexPosisi + 1 <= komiks[indexUrutan - 1].posisiUrutan.Length)
                {
                    textNextButton.text = "PLAY";  
                }
            }
            else
            {
                // Debug.Log("Mentok");

                GameManager.Instance.Play();

                yield return null;
            }
        }
    }
}

[System.Serializable]
public class Komik
{
    public Texture2D gambarKomik;
    public float[] posisiUrutan;
}
