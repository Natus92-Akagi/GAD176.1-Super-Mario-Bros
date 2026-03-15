using UnityEngine;
using UnityEngine.Playables;
using FMODUnity;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;


public class BrotherMasterAndInput : MonoBehaviour
{
    public GameObject small;
    public GameObject super;
    public GameObject fire;

    public PlayableDirector brosDirector;
    public TimelineAsset marioGrown;
    public TimelineAsset marioShrunk;
    public TimelineAsset marioBurnShrunk;
    public TimelineAsset marioSpinyShrunk;
    public TimelineAsset[] marioFireTramformatios;
    public TimelineAsset marioDeath;
    public TimelineAsset transition;
    public TimelineAsset EndofLevel;

    public EventReference jump;
    public EventReference headBump;
    public EventReference swim;
    public EventReference Fireball;
    public EventReference powerUpPickUp;
    public EventReference climb;
    public EventReference starmanTheme;
    public EventReference starmanPickUp;
    public EventReference playerDown;

    public SpriteRenderer[] spriteRenderers;
    private Color[] starmanColors;

    PlayerInput playerInput;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      DefaultMario();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DefaultMario() 
    { 
        small.SetActive(true);
        super.SetActive(false);
        fire.SetActive(false);
    }
    public void SuperMario()
    {
       small.SetActive(false);
       super.SetActive(true);
       fire.SetActive(false);
    }
    public void FireMario()
    {
        small.SetActive(false);
        super.SetActive(false);
        fire.SetActive(true);
    }
    public void MarioDeath()
    {
        brosDirector.Play(marioDeath);
    }
    public void OnEnable()
    {
       
    }
    public void OnDisable()
    {
       
    }
    public void StarManForm()
    {

    }

}
