using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Channel
{

    public string channelNumber { set; get; } = "0001";
    public string channelLabel { set; get; } = "Undefinite";
    public double tx { set; get; } = 000000.000;
    public double rx { set; get; } = 000000.000;
    public Modulation modulation { set; get; } = Modulation.USB;
    public ChannelPowerMode power { set; get; } = ChannelPowerMode.LOW;
    public CallFormat callFormat { set; get; } = CallFormat.INTERNATIONAL;
    public Channel(string channelNumber, double tx, double rx, string channelLabel, Modulation modulation, ChannelPowerMode power, CallFormat callFormat)
    {
        this.channelNumber = channelNumber;
        this.tx = tx;
        this.rx = rx;
        this.channelLabel = channelLabel;
        this.modulation = modulation;
        this.power = power;
        this.callFormat = callFormat;
    }
}
