using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuartetCrank : Interactable
{
    [Range(0,100)]
    public float charge;
    [Range(0.1f, 2f)]
    [SerializeField] 
    private float ChargeDrainSpeed;
    [SerializeField] 
    private static float TimeToChargeFromZero = 5f;
    private static float ChargeSpeed = (100 / TimeToChargeFromZero);
    [SerializeField] 
    private Renderer WallHeadRenderer;
    Color color;
    [SerializeField] 
    public int level;

    private bool charging;
    public bool locked;
    [SerializeField] 
    private float LockLimit;
    [SerializeField] 
    private float LockedTimer;

    public override void OnInteraction() {
        charging = true;
        charge += ChargeSpeed * Time.deltaTime;
        charge = Mathf.Clamp(charge, 0, 100);
        locked = true;
    }

    public override void OnInteractionExit() {
        charging = false;
        LockLimit = Random.Range(10 + ((20 - level) / 2), 20 + ((20 - level)/ 2));
        LockLimit += Random.Range(0, 100) / 100; // Add Decimals to timer;
        LockLimit *= charge / 100;
    }

    private void Update() {
        if (LockedTimer >= LockLimit) {
            locked = false;
            LockedTimer = 0;
        }
            
        if (locked) {
            LockedTimer += Time.deltaTime;
            return;
        }
        if (!charging)
            charge -= ChargeDrainSpeed * Time.deltaTime;
        charge = Mathf.Clamp(charge, 0, 100);
        color = new Color(charge / 100f, charge / 100f, charge / 100f);
        WallHeadRenderer.material.color = color;
        transform.gameObject.GetComponent<Renderer>().material.color = color;
    }
    // Start is called before the first frame update
    void Start()
    {
        level = NightInformation.AiLevels[3];
        ChargeDrainSpeed = (level != 0) ? ChargeDrainSpeed * (Mathf.Log(level + 1) / 2) + (1 - (Mathf.Log(2) / 2)) : 0.5f;
        //ChargeDrainSpeed *= (Mathf.Log(GameObject.Find("Reggie Rhino").GetComponent<ReggieAI>().level + 1) / 2) + (1 - (Mathf.Log(2) / 2));
    }
}
