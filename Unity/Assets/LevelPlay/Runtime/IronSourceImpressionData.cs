using System;
using System.Collections.Generic;
using System.Globalization;
using Unity.Services.LevelPlay;
using UnityEngine;

/// <summary>
/// Represents an impression event's data
/// </summary>
public class IronSourceImpressionData
{
    public readonly string auctionId;
    [Obsolete("This parameter will be removed in version 9.0.0. Please use the AdFormat parameter instead.")]
    public readonly string adUnit;
    public readonly string adFormat;
    public readonly string mediationAdUnitName;
    public readonly string mediationAdUnitId;
    public readonly string country;
    public readonly string ab;
    public readonly string segmentName;
    public readonly string placement;
    public readonly string adNetwork;
    public readonly string instanceName;
    public readonly string instanceId;
    public readonly double? revenue;
    public readonly string precision;
    [Obsolete("This parameter will be removed in version 9.0.0.")]
    public readonly double? lifetimeRevenue;
    public readonly string encryptedCPM;
    public readonly int? conversionValue;
    public readonly string allData;


    [Obsolete("This function will be made private in a future version.", false)]
    public IronSourceImpressionData(string json)
    {
        if (json != null)
        {
            try
            {
                object obj;
                double parsedDouble;
                int parsedInt;
                allData = json;
                // Retrieve a CultureInfo object.
                CultureInfo invCulture = CultureInfo.InvariantCulture;
                Dictionary<string, object> jsonDic = IronSourceJSON.Json.Deserialize(json) as Dictionary<string, object>;
                if (jsonDic.TryGetValue(IronSourceConstants.IMPRESSION_DATA_KEY_AUCTION_ID, out obj) && obj != null)
                {
                    auctionId = obj.ToString();
                }
                if (jsonDic.TryGetValue(IronSourceConstants.IMPRESSION_DATA_KEY_AD_UNIT, out obj) && obj != null)
                {
                    adUnit = obj.ToString();
                }
                if (jsonDic.TryGetValue(IronSourceConstants.IMPRESSION_DATA_KEY_AD_FORMAT, out obj) && obj != null)
                {
                    adFormat = obj.ToString();
                }
                if (jsonDic.TryGetValue(IronSourceConstants.IMPRESSION_DATA_KEY_MEDIATION_AD_UNIT_NAME, out obj) && obj != null)
                {
                    mediationAdUnitName = obj.ToString();
                }
                if (jsonDic.TryGetValue(IronSourceConstants.IMPRESSION_DATA_KEY_MEDIATION_AD_UNIT_ID, out obj) && obj != null)
                {
                    mediationAdUnitId = obj.ToString();
                }
                if (jsonDic.TryGetValue(IronSourceConstants.IMPRESSION_DATA_KEY_COUNTRY, out obj) && obj != null)
                {
                    country = obj.ToString();
                }
                if (jsonDic.TryGetValue(IronSourceConstants.IMPRESSION_DATA_KEY_ABTEST, out obj) && obj != null)
                {
                    ab = obj.ToString();
                }
                if (jsonDic.TryGetValue(IronSourceConstants.IMPRESSION_DATA_KEY_SEGMENT_NAME, out obj) && obj != null)
                {
                    segmentName = obj.ToString();
                }
                if (jsonDic.TryGetValue(IronSourceConstants.IMPRESSION_DATA_KEY_PLACEMENT, out obj) && obj != null)
                {
                    placement = obj.ToString();
                }
                if (jsonDic.TryGetValue(IronSourceConstants.IMPRESSION_DATA_KEY_AD_NETWORK, out obj) && obj != null)
                {
                    adNetwork = obj.ToString();
                }
                if (jsonDic.TryGetValue(IronSourceConstants.IMPRESSION_DATA_KEY_INSTANCE_NAME, out obj) && obj != null)
                {
                    instanceName = obj.ToString();
                }
                if (jsonDic.TryGetValue(IronSourceConstants.IMPRESSION_DATA_KEY_INSTANCE_ID, out obj) && obj != null)
                {
                    instanceId = obj.ToString();
                }
                if (jsonDic.TryGetValue(IronSourceConstants.IMPRESSION_DATA_KEY_PRECISION, out obj) && obj != null)
                {
                    precision = obj.ToString();
                }
                if (jsonDic.TryGetValue(IronSourceConstants.IMPRESSION_DATA_KEY_ENCRYPTED_CPM, out obj) && obj != null)
                {
                    encryptedCPM = obj.ToString();
                }

                if (jsonDic.TryGetValue(IronSourceConstants.IMPRESSION_DATA_KEY_REVENUE, out obj) && obj != null && double.TryParse(string.Format(invCulture, "{0}", obj), NumberStyles.Any, invCulture, out parsedDouble))
                {
                    revenue = parsedDouble;
                }

                if (jsonDic.TryGetValue(IronSourceConstants.IMPRESSION_DATA_KEY_LIFETIME_REVENUE, out obj) && obj != null && double.TryParse(string.Format(invCulture, "{0}", obj), NumberStyles.Any, invCulture, out parsedDouble))
                {
                    lifetimeRevenue = parsedDouble;
                }

                if (jsonDic.TryGetValue(IronSourceConstants.IMPRESSION_DATA_KEY_CONVERSION_VALUE, out obj) && obj != null && int.TryParse(string.Format(invCulture, "{0}", obj), NumberStyles.Any, invCulture, out parsedInt))
                {
                    conversionValue = parsedInt;
                }
            }
            catch (Exception ex)
            {
                LevelPlayLogger.Log("error parsing impression " + ex.ToString());
            }
        }
    }

    public override string ToString()
    {
        return "IronSourceImpressionData{" +
            "auctionId='" + auctionId + '\'' +
            ", adUnit='" + adUnit + '\'' +
            ", adFormat='" + adFormat + '\'' +
            ", mediationAdUnitName='" + mediationAdUnitName + '\'' +
            ", mediationAdUnitId='" + mediationAdUnitId + '\'' +
            ", country='" + country + '\'' +
            ", ab='" + ab + '\'' +
            ", segmentName='" + segmentName + '\'' +
            ", placement='" + placement + '\'' +
            ", adNetwork='" + adNetwork + '\'' +
            ", instanceName='" + instanceName + '\'' +
            ", instanceId='" + instanceId + '\'' +
            ", revenue=" + revenue +
            ", precision='" + precision + '\'' +
            ", lifetimeRevenue=" + lifetimeRevenue +
            ", encryptedCPM='" + encryptedCPM + '\'' +
            ", conversionValue=" + conversionValue +
            '}';
    }
}
