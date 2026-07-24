using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour
{
    private bool _isCooldown = false;

    // Ketika Bell ditekan
    private void OnMouseDown()
    {
        if (_isCooldown == false)
        {
            StartCoroutine(HandleOnMouseDown());

            _isCooldown = true;
        }
    }

    private IEnumerator HandleOnMouseDown()
    {
        // Dia harus mengeluarkan bunyi
        // AudioManager.Instance.PlaySFXName();

        GameManager.Instance.NextDialog();

        Debug.Log("Bell Ditekan");

        yield return new WaitForSeconds(1.5f);

        _isCooldown = false;
    }
}
