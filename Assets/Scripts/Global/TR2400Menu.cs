using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TR2400Menu : MonoBehaviour
{
    public static Dictionary<string, dynamic>
        trMenu =
            new Dictionary<string, dynamic> {
                {
                    "1. Operator Setup",
                    new Dictionary<string, dynamic> {
                        {
                            "1. Light Level",
                            new Dictionary<string, dynamic> {
                                { "Low", "" },
                                { "Medium", "" },
                                { "High", "" }
                            }
                        },
                        {
                            "2. Light Timeout",
                            new Dictionary<string, dynamic> {
                                { "10s", "" },
                                { "70s", "" }
                            }
                        },
                        {
                            "3. Freq. Offset",
                            new Dictionary<string, dynamic> {
                                { "+00Hz", "" },

                            }
                        },
                        {
                            "4. Zero dBm",
                            new Dictionary<string, dynamic> {
                                { "0dBm", "" },
                            }
                        },
                        {
                            "5. Time",
                            new Dictionary<string, dynamic> {
                                { "Add Time", "" },
                            }
                        },
                        {
                            "6. Whisper Mode",
                            new Dictionary<string, dynamic> {
                                { "Normal",
                                new Dictionary<string, dynamic>{
                                    {"TX Whisper" ,""},
                                    {"RX Whisper" ,""}
                                 }
                                },
                                { "Damped", "" },
                                { "Hi Sens", "" },
                                { "Sens", "" }
                            }
                        },
                        {
                            "7. Reciter Sens",
                            new Dictionary<string, dynamic> {
                                { "Normal", "" },
                            }
                        },
                        {
                            "8. Audioable Alerm",
                            new Dictionary<string, dynamic> {
                                { "ON", "" },
                                { "OFF", "" },
                            }
                        },
                        {
                            "9. Diaple Cut",""
                        },
                         {
                            "10. Auto Tune",
                            new Dictionary<string, dynamic> {
                                { "ON", "" },
                                { "OFF", "" },
                            }
                        },
                        {
                            "11. Restore Default",
                            new Dictionary<string, dynamic> {
                                { "1. ALE", "" },
                                { "2. Modem", "" },
                                { "3. DLP", "" },
                            }
                        },
                        {
                            "12. Redio Id Entry",
                            new Dictionary<string, dynamic> {
                                { "1. Edit", "" },
                                { "2. Exit", "" },
                            }
                        },
                    }
                },
                {
                    "2. Built In Test",
                    new Dictionary<string, dynamic> {
                        {
                            "1. Initate BITE",""
                        },
                        {
                            "2. Status",
                            new Dictionary<string, dynamic> {
                                { "1. Battery", "" },
                                { "2. PA Temp", "" },
                                { "3. FP Temp", "" },
                                { "4. RX Time", "" },
                                { "5. TX Time", "" },
                                { "6. TCXO Offs", "" },
                                { "7. Exit Status", "" },

                            }
                        },
                        {
                            "3. Version",
                            new Dictionary<string, dynamic> {
                                { "1. Ver. Radio", "" },
                                { "2. Ver. Host Proc.", "" },
                                { "3. Ver. Front Panel", "" },
                                { "4. Ver. Synthesiser", "" },
                                { "5. Ver. DSP J", "" },
                                { "6. Ver. DSP K", "" },
                                { "7. Ver. Vocoder 2400", "" },
                                { "8. Ver. Vocoder 800", "" },
                                { "9. Ver. Crypto Algo", "" },
                                { "10. Ver. ECCM", "" },
                                { "11. Ver. AUX 386", "" },
                                { "12 Ver. Ext PA", "" },
                                { "13. Ver. Ext At2223", "" },
                                { "14. Ver. Ext Cloc", "" },
                                { "15. Exit Version", "" },

                            }
                        },
                        {
                            "4. Maintainance BIT",
                            new Dictionary<string, dynamic> {
                                { "1. Key band Test", "" },
                                { "2. Volume ctl Test", "" },
                                { "3. Display Test", "" },
                                { "4. Dipole Test", "" },
                                { "5. Production BIT", "" },
                                { "6. Development BIT", "" },
                                { "7. Exit Main BIT", "" },

                            }
                        },
                        {
                            "5. Peripharal BIT",""
                        },
                        {
                            "6. Exit BIT",""
                        },
                    }
                },
                {
                    "3. Program Channel",
                    new Dictionary<string, dynamic> {
                        {"Channel",""},
                        {"RX Freq",""},
                        {"TX Freq",""},
                        {"RX Modulation",""},
                        {"TX Modulation",""},

                    }
                },
                {
                    "4. Configure ALE",
                    new Dictionary<string, dynamic> {
                        {"1. Channel Sounding",""},
                        {"2. Self Address",""},
                        {"3. Other Address",""},
                        {"4. Net Address",""},
                        {"5. ALE Scan List",""},
                        {"6. SMS Message",""},
                        {"7. ALE operation parm.",""},
                        {"8. Clear ALE parm",""},
                        {"9. Exit ALE Parm",""},

                    }
                },
                {
                    //Decription Needed
                    "5. Configure Net",
                    ""
                },
                {
                    "6. Configure DLP",
                    new Dictionary<string, dynamic> {
                        {
                            "1. Nodes Setup",
                            new Dictionary<string, dynamic> {
                                { "1. Local Node Address","" },
                                { "2. Group Node Address","" },
                                { "3. Cross Reference ALE","" },
                                { "4. Exit Node Setup","" },
                            }
                        },
                        {"2. Operational Setup",""},
                        { "3. Exit DLP Configuration", "" },

                    }
                },


            };
}
