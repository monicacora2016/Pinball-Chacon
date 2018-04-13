using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class BumperBehavior : MonoBehaviour
{
    [SerializeField]
    private float multiplier = 1.0f;
    [SerializeField]
    private float lightFlash = 0.1f;
    [SerializeField]
    private int points = 1;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            other.gameObject.GetComponent<Rigidbody>().velocity *= multiplier;
            SetLights(true);
            StartCoroutine(TurnOff());
        }
    }

    private void SetLights(bool state)
    {
        foreach (Light l in GetComponentsInChildren<Light>(true))
        {
            l.gameObject.SetActive(state);
        }
    }

    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(lightFlash);
        SetLights(false);
    }
}
