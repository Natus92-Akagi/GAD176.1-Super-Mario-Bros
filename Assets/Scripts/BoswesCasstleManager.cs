using UnityEngine;
using System.Collections;
using FMODUnity;

public class BoswesCasstleManager : MonoBehaviour
{

    public float minWait = 4f;
    public float maxWait = 12f;

    public EventReference thunderSFX;

    private LightingFlash[] allWindows;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        allWindows = FindObjectsByType<LightingFlash>(FindObjectsSortMode.None);
        StartCoroutine(LightingLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LightingLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
            RuntimeManager.PlayOneShot(thunderSFX);

            foreach (var window in allWindows)
            {
                window.TriggerFlash();
            }
        }
    }
}
