using System;
using System.Collections.Generic;
using System.Globalization;
using Unity.Services.LevelPlay;

/// <summary>
/// Defines the information relative to an ad
/// </summary>
public class IronSourceAdInfo
{
    /// <summary>
    /// Auction ID.
    /// </summary>
    public readonly string auctionId;

    /// <summary>
    /// Ad Unit.
    /// </summary>
    public readonly string adUnit;

    /// <summary>
    /// Country where the ad is presented.
    /// </summary>
    public readonly string country;

    /// <summary>
    /// AB testing group.
    /// </summary>
    public readonly string ab;

    /// <summary>
    /// Name of the segment the ad belongs to.
    /// </summary>
    public readonly string segmentName;

    /// <summary>
    /// Ad Network to which the ad belongs.
    /// </summary>
    public readonly string adNetwork;

    /// <summary>
    /// Name of the instance
    /// </summary>
    public readonly string instanceName;

    /// <summary>
    /// Instance ID
    /// </summary>
    public readonly string instanceId;

    /// <summary>
    /// Revenue generated
    /// </summary>
    public readonly double? revenue;

    /// <summary>
    /// Precision for the revenue
    /// </summary>
    public readonly string precision;

    /// <summary>
    /// Lifetime revenue
    /// </summary>
    public readonly double? lifetimeRevenue;

    /// <summary>
    /// Encrypted CPM
    /// </summary>
    public readonly string encryptedCPM;


    [Obsolete("This constructor will soon be made private. Please rely on the ad events to provide an AdInfo object", false)]
    public IronSourceAdInfo(string json)
    {
        if (json != null && json != IronSourceConstants.EMPTY_STRING)
        {
            try
            {
                object obj;
                double parsedDouble;

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
            }
            catch (Exception ex)
            {
                LevelPlayLogger.Log("error parsing ad info " + ex.ToString());
            }
        }
    }

    /// <summary>
    /// Returns a string that represents the ad info.
    /// </summary>
    /// <returns>Ad infos as a string</returns>
    public override string ToString()
    {
        return "IronSourceAdInfo {" +
            "auctionId='" + auctionId + '\'' +
            ", adUnit='" + adUnit + '\'' +
            ", country='" + country + '\'' +
            ", ab='" + ab + '\'' +
            ", segmentName='" + segmentName + '\'' +
            ", adNetwork='" + adNetwork + '\'' +
            ", instanceName='" + instanceName + '\'' +
            ", instanceId='" + instanceId + '\'' +
            ", revenue=" + revenue +
            ", precision='" + precision + '\'' +
            ", lifetimeRevenue=" + lifetimeRevenue +
            ", encryptedCPM='" + encryptedCPM + '\'' +
            '}';
    }
}
