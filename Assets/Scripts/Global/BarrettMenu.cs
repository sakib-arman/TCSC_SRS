using System.Collections.Generic;

using UnityEngine;

// Author Saiful Akhter
public class BarrettMenu : MonoBehaviour
{
    public static Dictionary<string, dynamic>
        info =
            new Dictionary<string, dynamic> {
                {
                    "General",
                    new Dictionary<string, dynamic> {
                        {
                            "Mic up/down keys",
                            new Dictionary<string, dynamic> {
                                { "Change Channel", "" },
                                { "Change Value", "" },
                                { "Disabled", "" }
                            }
                        },
                        {
                            "Internal Modem",
                            new Dictionary<string, dynamic> {
                                { "Enabled", "" },
                                { "Disabled", "" }
                            }
                        },
                        {
                            "Upload Pack",
                            new Dictionary<string, dynamic> {
                                { "SelCall ID-1", "" },
                                { "SelCall ID-2", "" },
                                { "Six Digit ID", "" }
                            }
                        },
                        {
                            "Security Level",
                            new Dictionary<string, dynamic> {
                                { "Standard", "" },
                                { "High", "" }
                            }
                        },
                        { "Secure Call Code", "" },
                        { "Hopping Pin", "" },
                        { "Option Installation", "" },
                        { "BITE Test", "" },
                        { "Set Date", "" },
                        { "Set Clock", "" },
                        {
                            "Channel Levels",
                            new Dictionary<string, dynamic> {
                                { "Search Entries", "" },
                                { "Add Entries", "" }
                            }
                        },
                        {
                            "Transmit Time Out",
                            new Dictionary<string, dynamic> {
                                { "Disabled", "" },
                                { "3 min", "" },
                                { "2 min", "" },
                                { "1 min", "" }
                            }
                        },
                        {
                            "Tx Over Beep",
                            new Dictionary<string, dynamic> {
                                { "Enabled", "" },
                                { "Disabled", "" }
                            }
                        }
                    }
                },
                {
                    "ALE Setting",
                    new Dictionary<string, dynamic> {
                        {
                            "ALE State",
                            new Dictionary<string, dynamic> {
                                { "Enabled", "" },
                                { "Disabled", "" }
                            }
                        },
                        { "Ber Threshold", "" },
                        { "Sinad Threshold", "" },
                        {
                            "Threshold Test",
                            new Dictionary<string, dynamic> {
                                { "Sinad", "" },
                                { "BER", "" },
                                { "None", "" },
                                { "Both", "" }
                            }
                        },
                        {
                            "LQA Decay Rate",
                            new Dictionary<string, dynamic> {
                                { "48 hours", "" },
                                { "24 hours", "" },
                                { "8 hours", "" },
                                { "4 hours", "" },
                                { "2 hours", "" },
                                { "1 hours", "" },
                                { "Disabled", "" }
                            }
                        },
                        {
                            "LQA Averaging",
                            new Dictionary<string, dynamic> {
                                { "(7*old + new)/8", "" },
                                { "(3*old + new)/4", "" },
                                { "(old + new)/2", "" },
                                { "No Averaging", "" }
                            }
                        },
                        {
                            "Exchange Mode",
                            new Dictionary<string, dynamic> {
                                { "Current LQA", "" },
                                { "Averaged LQA", "" }
                            }
                        },
                        {
                            "LQA Exchange",
                            new Dictionary<string, dynamic> {
                                { "Enabled", "" },
                                { "Disabled", "" }
                            }
                        },
                        {
                            "Sounding Address",
                            new Dictionary<string, dynamic> { { "CSG-1", "" } }
                        },
                        {
                            "Sounding Control",
                            new Dictionary<string, dynamic> {
                                { "Enabled", "" },
                                { "Disabled", "" },
                                { "Individually Preset", "" }
                            }
                        },
                        {
                            "Response Control",
                            new Dictionary<string, dynamic> {
                                { "Enabled", "" },
                                { "Disabled", "" },
                                { "Individually Preset", "" }
                            }
                        },
                        {
                            "Transmit Control",
                            new Dictionary<string, dynamic> {
                                { "Enabled", "" },
                                { "Disabled", "" }
                            }
                        },
                        {
                            "Scan List",
                            new Dictionary<string, dynamic> {
                                { "Label: DEFAULT", "" }
                            }
                        },
                        {
                            "Autofill",
                            new Dictionary<string, dynamic> {
                                { "Enabled", "" },
                                { "Disabled", "" }
                            }
                        }
                    }
                },
                {
                    "I/O Setting",
                    new Dictionary<string, dynamic> {
                        {
                            "RS232 Out",
                            new Dictionary<string, dynamic> {
                                { "Enabled", "" },
                                { "Disabled", "" }
                            }
                        },
                        {
                            "Line in Level",
                            new Dictionary<string, dynamic> {
                                { "0 dBm", "" },
                                { "-6 dBm", "" },
                                { "-12 dBm", "" },
                                { "-18 dBm", "" },
                                { "-24 dBm", "" }
                            }
                        },
                        {
                            "Line out Level",
                            new Dictionary<string, dynamic> {
                                { "0 dBm", "" },
                                { "-3 dBm", "" },
                                { "-6 dBm", "" },
                                { "+9 dBm", "" },
                                { "+6 dBm", "" },
                                { "+3 dBm", "" }
                            }
                        },
                        {
                            "Antenna Type",
                            new Dictionary<string, dynamic> {
                                { "Base Status", "" },
                                { "2017 Automatic Tuner", "" },
                                { "Liner with ATU", "" },
                                { "Loop Antenna", "" },
                                { "2019 Mobile Antenna", "" },
                                { "Liner Amplifier", "" },
                                { "911 Automatic Tuner", "" },
                                { "910 Mobile Antenna", "" }
                            }
                        },
                        {
                            "Extra Alarm Type",
                            new Dictionary<string, dynamic> {
                                { "Latched", "" },
                                { "Pulsed", "" }
                            }
                        } ,{
                            "GPS",
                            new Dictionary<string, dynamic> {
                                { "Enable", "" },
                                { "Disabled", "" }
                            }
                        }
                    }
                },
                {
                    "RF Setting",
                    new Dictionary<string, dynamic> {
                        {
                            "RX Preamp",
                            new Dictionary<string, dynamic> {
                                { "Enabled", "" },
                                { "Disabled", "" }
                            }
                        },
                        {
                            "AGC Hang",
                            new Dictionary<string, dynamic> {
                                { "Hang Off", "" },
                                { "Hang AGC", "" }
                            }
                        },
                        {
                            "Power Level",
                            new Dictionary<string, dynamic> {
                                { "10W", "" },
                                { "30W", "" },
                                { "125W", "" }
                            }
                        },
                        {
                            "Noise Blanker",
                            new Dictionary<string, dynamic> {
                                { "On", "" },
                                { "Off", "" }
                            }
                        },
                        {
                            "Clarify Range",
                            new Dictionary<string, dynamic> {
                                { "0Hz", "" },
                                { "50Hz", "" },
                                { "150Hz", "" },
                                { "1kHz", "" }
                            }
                        }
                    }
                },
                {
                    "Audio Setting",
                    new Dictionary<string, dynamic> {
                        {
                            "Audio Bandwidth",
                            new Dictionary<string, dynamic> {
                                { "300Hz-1.5kHz", "" },
                                { "300Hz-2.0kHz", "" },
                                { "300Hz-2.5kHz", "" },
                                { "300Hz-3.0kHz", "" }
                            }
                        },
                        {
                            "Noise Reduction",
                            new Dictionary<string, dynamic> {
                                { "Low", "" },
                                { "Medium", "" },
                                { "High", "" }
                            }
                        },
                        {
                            "Line Audio",
                            new Dictionary<string, dynamic> {
                                { "Unmuted", "" },
                                { "Follows Mute", "" }
                            }
                        },
                        {
                            "Tx Configuration",
                            new Dictionary<string, dynamic> {
                                { "Local", "" },
                                { "Remote", "" }
                            }
                        },
                        {
                            "Rx Configuration",
                            new Dictionary<string, dynamic> {
                                { "Internal Audio", "" },
                                { "External Audio", "" }
                            }
                        },
                        {
                            "Beep Level",
                            new Dictionary<string, dynamic> {
                                { "Off", "" },
                                { "High", "" },
                                { "Low", "" }
                            }
                        }
                    }
                },
                {
                    "Selcall Setting",
                    new Dictionary<string, dynamic> {
                        {
                            "Self IDs",
                            new Dictionary<string, dynamic> {
                                { "Search Entries", "" },
                                { "Add Entry", "" }
                            }
                        },
                        {
                            "OEM Privacy Key",
                            new Dictionary<string, dynamic> {
                                { "Off", "" },
                                { "On", "" }
                            }
                        },
                        { "TXCVR Lock", "" },

                        { "Preamble", "" },
                        {
                            "Rx Configuration",
                            new Dictionary<string, dynamic> {
                                { "Internal Audio", "" },
                                { "External Audio", "" }
                            }
                        },
                        {
                            "Beep Level",
                            new Dictionary<string, dynamic> {
                                { "Off", "" },
                                { "High", "" },
                                { "Low", "" }
                            }
                        },
                        {
                            "Set Audio In Tx",
                            new Dictionary<string, dynamic> {
                                { "Off", "" },
                                { "High", "" },
                                { "Low", "" }
                            }
                        },
                        {
                            "Selcall Alarm",
                            new Dictionary<string, dynamic> {
                                { "Off", "" },
                                { "On", "" }
                            }
                        },
                        { "Selcall MMSI", "" },
                        { "Selcall OEM2", "" },
                        { "Selcall OEM1", "" },
                        { "Selcall INT2", "" },
                        { "Selcall INT1", "" }
                    }
                },
                {
                    "Mute Setting",
                    new Dictionary<string, dynamic> {
                        {
                            "Syllabic Mute Sensitivity",
                            new Dictionary<string, dynamic> {
                                { "Low", "" },
                                { "Medium", "" },
                                { "High", "" }
                            }
                        },
                        {
                            "Signal Strength Mute Level",
                            new Dictionary<string, dynamic> {
                                { "Low", "" },
                                { "Medium", "" },
                                { "High", "" }
                            }
                        }
                    }
                },
                {
                    "Scan Setting",
                    new Dictionary<string, dynamic> {
                        {
                            "Scan Rate",
                            new Dictionary<string, dynamic> {
                                { "200 ms", "" },
                                { "500 ms", "" },
                                { "700 ms", "" },
                                { "1000 ms", "" },
                                { "1500 ms", "" },
                                { "2000 ms", "" },
                                { "5000 ms", "" }
                            }
                        },
                        {
                            "Scan Select",
                            new Dictionary<string, dynamic> {
                                { "Scan Table 1", "" },
                                { "Scan Table 2", "" },
                                { "Scan Table 3", "" },
                                { "Scan Table 4", "" },
                                { "Scan Table 5", "" },
                                { "Scan Table 6", "" },
                                { "Scan Table 7", "" },
                                { "Scan Table 8", "" }
                            }
                        },
                        {
                            "Scan Resume Time",
                            new Dictionary<string, dynamic> {
                                { "Off", "" },
                                { "1 min", "" },
                                { "2 min", "" },
                                { "3 min", "" },
                                { "5 min", "" },
                                { "10 min", "" },
                                { "15 min", "" },
                                { "20 min", "" },
                                { "30 min", "" }
                            }
                        }
                    }
                },
                {
                    "Scan Tables", new Dictionary<string, dynamic> {
                        {
                            "Scan Table 1", ""

                        },
                        {
                            "Scan Table 2", ""
                        },
                        {
                            "Scan Table 3", ""
                        },
                        {
                            "Scan Table 4", ""
                        },
                        {
                            "Scan Table 5", ""
                        },
                        {
                            "Scan Table 6666", ""
                        },
                        {
                            "Scan Table 7777", ""
                        },
                        {
                            "Scan Table 8888", ""
                        }
                    }
                }
            };


    public static Dictionary<string, dynamic> menuShortPress = new Dictionary<string, dynamic> {
                {
                    "Identification",
                    new Dictionary<string, dynamic> {
                        { "TXCVR Type", "" },
                        { "S/N", "" },
                        { "Option", "" },
                    }
                },
                {
                    "Address Book",
                    new Dictionary<string, dynamic> {
                        {
                                "Selcall ID Book",
                            new Dictionary<string, dynamic> {
                                { "Search Entries", "" },
                                { "Add Entry", "" },
                            }
                        },
                        {
                            "Autofill Book",
                            new Dictionary<string, dynamic> {
                                { "Disabled", "" },
                                { "Enabled", "" }
                            }
                        },
                        {
                            "Phone Book",
                            new Dictionary<string, dynamic> {
                                { "Search Entry", "" },
                                { "Add Entry", "" },
                            }
                        },
                    }
                },
                {
                    "Call History",
                    new Dictionary<string, dynamic> {
                        { "New Calls", "" },
                        { "Outbox", "" },
                        { "Inbox", "" },
                    }
                },
                {
                    "Display Option",
                    new Dictionary<string, dynamic> {
                        {
                            "Backlight Level",
                            new Dictionary<string, dynamic> {
                                { "High", "" },
                                { "Medium", "" },
                                { "Low", "" },
                            }
                        },
                        {
                            "Backlight Time Out",
                            new Dictionary<string, dynamic> {
                                { "Always On", "" },
                                { "Long Time Out", "" },
                                { "Short Time Out", "" },
                            }
                        },
                    }
                },
            };
    public static Modulation[] ModulationList = { Modulation.USB, Modulation.LSB, Modulation.AM, Modulation.CW, Modulation.FSK };
    public static ChannelPowerMode[] ChannelPowerModeList = { ChannelPowerMode.HIGH, ChannelPowerMode.MEDIUM, ChannelPowerMode.LOW };
    public static CallFormat[] CallFormatList = { CallFormat.INTERNATIONAL, CallFormat.OEM };
    public static int IdentificationIndex = 0;
    public static string[] IndentificationText = new string[]
    {
        "Txcvr Type:2090 \n S/N:20900 \n Options:123",
        "Software Version: 2.01 \n DSP Version:2.00 \n Core Version: 2.00",
        "ATU Version: 1.04 \n Antenna: Whip/L.Wire",
        "Sellcall IDs INT1: 1234 OME1:9876 /n INT2: 123456 OME2:876745",
        "Battery RX: 15.2 \n Battery TX: 14.2 \n PA Temparature: 20°",
        "Charge: 86% \n Estimated Charging Time 1hrs 57 mins",
        "GPS Corrdinates \n Lat: 32°05.727S  \n long: 115° 48.043E \n"
    };

    public static int CallIndex = 0;
    public static string[] CallText = new string[]
    {
        "SelCall",
        "TeleCall",
        "Hang Up",
        "Page Call",
        "GPS Request",
        "Status Request",
        "Secure Call",
        "Beacon",
    };

    public static int scanListIndexSelected = 0;
    public static ScanTable[] scanListNames = new ScanTable[]
    {
        new ScanTable( "Scan Table 1"),
        new ScanTable( "Scan Table 2"),
        new ScanTable( "Scan Table 3"),
        new ScanTable( "Scan Table 4"),
        new ScanTable( "Scan Table 5"),
        new ScanTable( "Scan Table 6"),
        new ScanTable( "Scan Table 7"),
        new ScanTable( "Scan Table 8")
    };
    public static string[] scanListOptions = new string[]
    {
        "Search Entry",
        "Add Entry",
        "Change Label",
    };

}
