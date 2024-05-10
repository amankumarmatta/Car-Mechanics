using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Controller : NetworkBehaviour
{
    public float speed;
    public float delay;

    [SerializeField] private GameObject shield;

    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    private void Awake()
    {
        shield.SetActive(false);
    }

    void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        float xPos = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float zPos = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.position += new Vector3(xPos, 0, zPos);
    }

    IEnumerator OnSpeedBoostCollected()
    {
        float defaultSpeed = speed;
        speed = speed * 4;
        yield return new WaitForSeconds(3);
        speed = defaultSpeed;
    }

    IEnumerator OnShieldTriggered()
    {
        shield.SetActive(true);
        yield return new WaitForSeconds(3);
        shield.SetActive(false); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Speed")
        {
            StartCoroutine(OnSpeedBoostCollected());
            other.gameObject.SetActive(false);
        }
        
        if (other.tag == "Shield")
        {
            StartCoroutine(OnShieldTriggered());
            other.gameObject.SetActive(false);
        }
    }
}
