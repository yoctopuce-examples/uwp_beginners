/*********************************************************************
 *
 * $Id: YWlanRecord.cs 29015 2017-10-24 16:29:41Z seb $
 *
 * Implements FindWlanRecord(), the high-level API for WlanRecord functions
 *
 * - - - - - - - - - License information: - - - - - - - - -
 *
 *  Copyright (C) 2011 and beyond by Yoctopuce Sarl, Switzerland.
 *
 *  Yoctopuce Sarl (hereafter Licensor) grants to you a perpetual
 *  non-exclusive license to use, modify, copy and integrate this
 *  file into your software for the sole purpose of interfacing
 *  with Yoctopuce products.
 *
 *  You may reproduce and distribute copies of this file in
 *  source or object form, as long as the sole purpose of this
 *  code is to interface with Yoctopuce products. You must retain
 *  this notice in the distributed source file.
 *
 *  You should refer to Yoctopuce General Terms and Conditions
 *  for additional information regarding your rights and
 *  obligations.
 *
 *  THE SOFTWARE AND DOCUMENTATION ARE PROVIDED 'AS IS' WITHOUT
 *  WARRANTY OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING
 *  WITHOUT LIMITATION, ANY WARRANTY OF MERCHANTABILITY, FITNESS
 *  FOR A PARTICULAR PURPOSE, TITLE AND NON-INFRINGEMENT. IN NO
 *  EVENT SHALL LICENSOR BE LIABLE FOR ANY INCIDENTAL, SPECIAL,
 *  INDIRECT OR CONSEQUENTIAL DAMAGES, LOST PROFITS OR LOST DATA,
 *  COST OF PROCUREMENT OF SUBSTITUTE GOODS, TECHNOLOGY OR
 *  SERVICES, ANY CLAIMS BY THIRD PARTIES (INCLUDING BUT NOT
 *  LIMITED TO ANY DEFENSE THEREOF), ANY CLAIMS FOR INDEMNITY OR
 *  CONTRIBUTION, OR OTHER SIMILAR COSTS, WHETHER ASSERTED ON THE
 *  BASIS OF CONTRACT, TORT (INCLUDING NEGLIGENCE), BREACH OF
 *  WARRANTY, OR OTHERWISE.
 *
 *********************************************************************/

using System;
using System.Threading.Tasks;

namespace com.yoctopuce.YoctoAPI
{

    //--- (generated code: YWlanRecord return codes)
//--- (end of generated code: YWlanRecord return codes)
    //--- (generated code: YWlanRecord class start)
/**
 * <summary>
 *   YWlanRecord Class: Description of a wireless network
 * <para>
 * </para>
 * <para>
 * </para>
 * </summary>
 */
public class YWlanRecord
{
//--- (end of generated code: YWlanRecord class start)
//--- (generated code: YWlanRecord definitions)
    protected string _ssid;
    protected int _channel = 0;
    protected string _sec;
    protected int _rssi = 0;

    //--- (end of generated code: YWlanRecord definitions)

    internal YWlanRecord(string json_str)
    {
        YJSONObject json = new YJSONObject(json_str);
        json.parse();
        _ssid = json.getString("ssid");
        _channel = json.getInt("channel");
        _sec = json.getString("sec");
        _rssi = json.getInt("rssi");
    }

    //--- (generated code: YWlanRecord implementation)
#pragma warning disable 1998

    public virtual string get_ssid()
    {
        return _ssid;
    }

    public virtual int get_channel()
    {
        return _channel;
    }

    public virtual string get_security()
    {
        return _sec;
    }

    public virtual int get_linkQuality()
    {
        return _rssi;
    }

#pragma warning restore 1998
    //--- (end of generated code: YWlanRecord implementation)
    }

}