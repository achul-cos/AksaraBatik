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

        if (CustomerManager.Instance.CustomerCurrent == null)
        {
            // Dan dia harus bisa trigger next custommer di customer manager
            Customer currentCustomer = CustomerManager.Instance.GetNextCustomer();

            // jika current customer ada, maka mari kita pindahkan scene ini menjadi scene dialog
            if (currentCustomer != null)
            {
                GameManager.Instance.LoadGameScene(GameState.Dialog);
            }
        }

        Debug.Log("Bell Ditekan");

        yield return new WaitForSeconds(1.5f);

        _isCooldown = false;
    }
}
