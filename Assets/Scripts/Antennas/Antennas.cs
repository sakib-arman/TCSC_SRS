using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Antennas
{
    public RADIO supportedRadio = RADIO.BARRET_2090;
    public AntennaType type = AntennaType.MANPACK_ANTENNA;
    public DirectionType directionType = DirectionType.BIDIRECTIONAL;
    public double length;
    public double section;
    public double frequency;
    public bool isFaulty = false;
    public string antennaName = "Barret Antenna";
    public Vector3 direction = new Vector3();
    public Antennas(RADIO supportedRadio, AntennaType antennaType, DirectionType directionType, double length, double section, string antennaName)
    {
        this.supportedRadio = supportedRadio;
        this.type = antennaType;
        this.section = section;

        this.directionType = directionType;
        this.length = length;
        this.antennaName = antennaName;
    }


}
