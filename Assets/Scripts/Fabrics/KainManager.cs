using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KainManager : MonoBehaviour
{
    public List<Transform> posisiKains = new List<Transform>(6);

    public GameObject[] kains = new GameObject[6];

    private void Start()
    {
        foreach (GameObject kain in kains)
        {
            // Random ambil anggota dalam posisiKains

            int jumlahPosisi = posisiKains.Count;

            int posisiRandom = Random.Range(0, jumlahPosisi);

            kain.transform.position = posisiKains[posisiRandom].position;

            posisiKains.RemoveAt(posisiRandom);
        }
    }
}
