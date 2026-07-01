using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUIManager : MonoBehaviour
{
    public GameObject _nextCustomerButton;
}

public class CustomerQueueSlot
{
    public Transform chair;
    public Customer customer;
}
