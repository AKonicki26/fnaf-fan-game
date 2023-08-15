using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReggieAI : Animatronic
{
    [Range(0, 100)]
    public float EllieCharge;
    [Range(0, 100)]
    public float BillieCharge;
    [Range(0, 100)]
    public float WillieCharge;

    [SerializeField] private bool[] locked = new bool[3];
    public List<QuartetCrank> QuartetCranks = new List<QuartetCrank>(3);

    enum AggressionMarkers {
        Retracting = -2,
        Neutral = 0,
        Upset = 1,
        Angry = 2,
        Killdozer = 3,
    }

    private int[] AgressionSwitchValues = new int[] { 20, 40, 66, 85 };

    [SerializeField]
    private  AggressionMarkers aggressionMarker;

    [SerializeField]
    private float aggressionProgress;
    [SerializeField]
    

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        foreach (QuartetCrank crank in QuartetCranks)
            crank.charge = 100 - (3 * Level / 2f);
        aggressionProgress = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Level == 0)
            return;

        if (aggressionProgress <= 0) {
            NavMeshAgent.SetDestination(Player.transform.position);
            foreach (QuartetCrank crank in QuartetCranks)
                crank.enabled = false;
            KillEnabled = true;
        }
            
        EllieCharge = QuartetCranks[0].charge;
        BillieCharge = QuartetCranks[1].charge;
        WillieCharge = QuartetCranks[2].charge;

        locked[0] = QuartetCranks[0].locked;
        locked[1] = QuartetCranks[1].locked;
        locked[2] = QuartetCranks[2].locked;

        double WeightedAverage = (10000 - ((Mathf.Pow(EllieCharge, 2) + Mathf.Pow(BillieCharge, 2) + Mathf.Pow(WillieCharge, 2)) / 3f)) * (100f / 10000f);

        if (WeightedAverage < AgressionSwitchValues[0]) aggressionMarker = AggressionMarkers.Retracting;
        else if (WeightedAverage < AgressionSwitchValues[1]) aggressionMarker = AggressionMarkers.Neutral;
        else if (WeightedAverage < AgressionSwitchValues[2]) aggressionMarker = AggressionMarkers.Upset;
        else if (WeightedAverage < AgressionSwitchValues[3]) aggressionMarker = AggressionMarkers.Angry;
        else aggressionMarker = AggressionMarkers.Killdozer;

        // If we're in retract mode or none of them are locked
        if (aggressionMarker == AggressionMarkers.Retracting || !QuartetCranks.Where(charger => charger.locked).Any())
            aggressionProgress -= ((int)aggressionMarker * Time.deltaTime);
        aggressionProgress = Mathf.Clamp(aggressionProgress, 0, 100);
    }

    public override void SetAiLevel(int level) {
        this.Level = level;
        foreach (QuartetCrank crank in QuartetCranks)
            crank.level = level;
    }
}
