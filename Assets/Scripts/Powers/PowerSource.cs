using UnityEngine;

public class PowerSource : MonoBehaviour
{
    public double voltage = 12; // unit V
    public double current = 5; // current unit Ah
    public double watt = 100; // unit w
    public double powerOutput = 0; // unit w
    public bool isAC = false;
    public double interval = 1;
    public double health = 100; // 100%
    public int chargeRemain;
    double time = 0;
    private void Update()
    {
        if (isAC)
        {
            powerConsumption();
        }
    }
    private void powerConsumption()
    {

        if (time >= interval)
        {
            current -= ((powerOutput / voltage) / 3600)*interval; // per Interval
            chargeRemain=System.Convert.ToInt32((current * 100) / 100);
            //Logger.Log(this, chargeRemain + "  %  ");
            time = 0;
        }
        time += Time.deltaTime *GLOBALTAG.TIMESCALE;


    }



    public double powerCharging()
    {
        powerOutput = voltage * current;
        return powerOutput++;
    }
}
