public enum PowerMode
{
    MANPACK_LOW,
    MANPACK_MEDIUM,
    MANPACK_HIGH,
    BASESTATION_LOW,
    BASESTATION_MEDIUM,
    BASESTATION_HIGH,
    VEHICLE_LOW,
    VEHICLE_MEDIUM,
    VEHICLE_HIGH,
}

public enum RADIO
{
    BARRET_2090,
    BARRET_2050,
    TR_2400,
    CODAN_2110M,
    TRC_3730,
    Q_MAC_90M,
    XV_3088,
    RF_1350,
    RF_13,
    FSG_90HI_71,
    SALEX_MH_313_XE,
}
public enum AntennaType
{
    MANPACK_ANTENNA,
    BASESTATION_ANTENNA,
    VEHICLE_ANTENNA
}
public enum DirectionType
{
    BIDIRECTIONAL,
    OMNIDIRECTIONAL,
    DIRECTIONAL,
}
public enum AntennaBodyType
{

}
public enum StationType
{
    MANPACK,
    BASESTATION,
    VEHICLE,
}
public enum Modulation
{
    USB,
    LSB,
    AM,
    CW,
    FSK,
}
public enum ChannelPowerMode
{
    HIGH,
    MEDIUM,
    LOW,
}
public enum CallFormat
{
    INTERNATIONAL,
    OEM,
}

public enum BacklightTimeout
{
    ALWAYS_ON,
    LONG_TIMEOUT,
    SHORT_TIMEOUT
}
public enum BacklightLavel
{
    HIGH,
    MEDIUM,
    LOW,
    //PFX 3208 backlight label
    ZERO,
    ONE,
    TWO,
    THREE
}

public enum TRFunctionSelector
{
    NET,
    CHAN,
    FREQ,
    MOD,
    SQL,
    PWR
}
public enum xv3088ChannelPowerMode
{
    HIGH,
    LOW,
}
public enum rf1350ChannelPowerMode
{
    HIGH,
    LOW,
}
;