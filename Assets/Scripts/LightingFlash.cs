using UnityEngine;


public class LightingFlash : MonoBehaviour
{
    public Animator windowAnimator;

    void Awake()
    {
      windowAnimator = GetComponent<Animator>();
    }
    public void TriggerFlash()
    {
        Debug.Log("hope this works");
        windowAnimator.Play("lighting flash");
    }


}
