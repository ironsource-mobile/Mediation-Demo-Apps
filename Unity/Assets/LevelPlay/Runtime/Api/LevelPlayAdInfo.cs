using System;
using System.Collections.Generic;
using System.Globalization;
using JetBrains.Annotations;
using UnityEngine;

namespace com.unity3d.mediation
{
    /// <summary>
    /// Contains detailed information about a LevelPlay advertisement, including its dimensions, placement, and performance metrics.
    /// </summary>
    public class LevelPlayAdInfo
    {
        // Constants for JSON keys
        const string AdUnitIdKey = "adUnitId";
        const string AdUnitNameKey = "adUnitName";
        const string AdSizeKey = "adSize";
        const string AdFormatKey = "adFormat";
        const string PlacementNameKey = "placementName";
        const string AuctionIdKey = "auctionId";
        const string CountryKey = "country";
        const string AbKey = "ab";
        const string SegmentNameKey = "segmentName";
        const string AdNetworkKey = "adNetwork";
        const string InstanceNameKey = "instanceName";
        const string InstanceIdKey = "instanceId";
        const string RevenueKey = "revenue";
        const string PrecisionKey = "precision";
        const string EncryptedCpmKey = "encryptedCPM";

        const string AdSizeDescriptionKey = "description";
        const string AdSizeWidthKey = "width";
        const string AdSizeHeightKey = "height";

        [Obsolete("adUnitId will be replaced by AdUnitId in version 9.0.0.")]
        public readonly string adUnitId;
        [Obsolete("adUnitName will be replaced by AdUnitName in version 9.0.0.")]
        public readonly string adUnitName;
        [Obsolete("adSize will be replaced by AdSize in version 9.0.0.")]
        [CanBeNull] public readonly LevelPlayAdSize adSize;
        [Obsolete("adFormat will be replaced by AdFormat in version 9.0.0.")]
        public readonly string adFormat;
        [Obsolete("placementName will be replaced by PlacementName in version 9.0.0.")]
        public readonly string placementName;
        [Obsolete("auctionId will be replaced by AuctionId in version 9.0.0.")]
        public readonly string auctionId;
        [Obsolete("country will be replaced by Country in version 9.0.0.")]
        public readonly string country;
        [Obsolete("ab will be replaced by Ab in version 9.0.0.")]
        public readonly string ab;
        [Obsolete("segmentName will be replaced by SegmentName in version 9.0.0.")]
        public readonly string segmentName;
        [Obsolete("adNetwork will be replaced by AdNetwork in version 9.0.0.")]
        public readonly string adNetwork;
        [Obsolete("instanceName will be replaced by InstanceName in version 9.0.0.")]
        public readonly string instanceName;
        [Obsolete("instanceId will be replaced by InstanceId in version 9.0.0.")]
        public readonly string instanceId;
        [Obsolete("revenue will be replaced by Revenue in version 9.0.0.")]
        public readonly double? revenue;
        [Obsolete("precision will be replaced by Precision in version 9.0.0.")]
        public readonly string precision;
        [Obsolete("encryptedCPM will be replaced by EncryptedCPM in version 9.0.0.")]
        public readonly string encryptedCPM;

        public readonly string AdUnitId;
        public readonly string AdUnitName;
        [CanBeNull] public readonly LevelPlayAdSize AdSize;
        public readonly string AdFormat;
        public readonly string PlacementName;
        public readonly string AuctionId;
        public readonly string Country;
        public readonly string Ab;
        public readonly string SegmentName;
        public readonly string AdNetwork;
        public readonly string InstanceName;
        public readonly string InstanceId;
        public readonly double? Revenue;
        public readonly string Precision;
        public readonly string EncryptedCPM;

        internal LevelPlayAdInfo(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return;
            }

            object obj;
            double parsedDouble;
            try
            {
                CultureInfo invCulture = CultureInfo.InvariantCulture;
                Dictionary<string, object> jsonDic =
                    IronSourceJSON.Json.Deserialize(json) as Dictionary<string, object>;
                if (jsonDic.TryGetValue(AdUnitIdKey, out obj) && obj != null)
                {
                    adUnitId = obj.ToString();
                    AdUnitId = obj.ToString();
                }

                if (jsonDic.TryGetValue(AdUnitNameKey, out obj) && obj != null)
                {
                    adUnitName = obj.ToString();
                    AdUnitName = obj.ToString();
                }

                if (jsonDic.TryGetValue(AdSizeKey, out obj) && obj != null)
                {
                    adSize = GetAdSize(obj.ToString());
                    AdSize = GetAdSize(obj.ToString());
                }

                if (jsonDic.TryGetValue(AdFormatKey, out obj) && obj != null)
                {
                    adFormat = obj.ToString();
                    AdFormat = obj.ToString();
                }

                if (jsonDic.TryGetValue(PlacementNameKey, out obj) && obj != null)
                {
                    placementName = obj.ToString();
                    PlacementName = obj.ToString();
                }

                if (jsonDic.TryGetValue(AuctionIdKey, out obj) && obj != null)
                {
                    auctionId = obj.ToString();
                    AuctionId = obj.ToString();
                }

                if (jsonDic.TryGetValue(CountryKey, out obj) && obj != null)
                {
                    country = obj.ToString();
                    Country = obj.ToString();
                }

                if (jsonDic.TryGetValue(AbKey, out obj) && obj != null)
                {
                    ab = obj.ToString();
                    Ab = obj.ToString();
                }

                if (jsonDic.TryGetValue(SegmentNameKey, out obj) && obj != null)
                {
                    segmentName = obj.ToString();
                    SegmentName = obj.ToString();
                }

                if (jsonDic.TryGetValue(AdNetworkKey, out obj) && obj != null)
                {
                    adNetwork = obj.ToString();
                    AdNetwork = obj.ToString();
                }

                if (jsonDic.TryGetValue(InstanceNameKey, out obj) && obj != null)
                {
                    instanceName = obj.ToString();
                    InstanceName = obj.ToString();
                }

                if (jsonDic.TryGetValue(InstanceIdKey, out obj) && obj != null)
                {
                    instanceId = obj.ToString();
                    InstanceId = obj.ToString();
                }

                if (jsonDic.TryGetValue(RevenueKey, out obj) && obj != null && double.TryParse(
                    string.Format(invCulture, "{0}", obj), NumberStyles.Any, invCulture, out parsedDouble))
                {
                    revenue = parsedDouble;
                    Revenue = parsedDouble;
                }

                if (jsonDic.TryGetValue(PrecisionKey, out obj) && obj != null)
                {
                    precision = obj.ToString();
                    Precision = obj.ToString();
                }

                if (jsonDic.TryGetValue(EncryptedCpmKey, out obj) && obj != null)
                {
                    encryptedCPM = obj.ToString();
                    EncryptedCPM = obj.ToString();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("error parsing LevelPlayAdInfo" + ex.ToString());
            }
        }

        /// <summary>
        /// Retrieves the ad size from a JSON string describing the ad size.
        /// </summary>
        /// <param name="adSizeJson">The JSON string describing the ad size.</param>
        /// <returns>An instance of <see cref="LevelPlayAdSize"/> or null if parsing fails.</returns>
        static LevelPlayAdSize GetAdSize(string adSizeJson)
        {
            string description = "";
            int width = 0;
            int height = 0;
            if (!string.IsNullOrEmpty(adSizeJson))
            {
                try
                {
                    object obj;
                    Dictionary<string, object> jsonDic =
                        IronSourceJSON.Json.Deserialize(adSizeJson) as Dictionary<string, object>;
                    if (jsonDic.TryGetValue(AdSizeDescriptionKey, out obj) && obj != null)
                    {
                        description = obj.ToString();
                    }

                    if (jsonDic.TryGetValue(AdSizeWidthKey, out obj) && obj != null)
                    {
                        width = Int32.Parse(obj.ToString());
                    }

                    if (jsonDic.TryGetValue(AdSizeHeightKey, out obj) && obj != null)
                    {
                        height = Int32.Parse(obj.ToString());
                    }

                    return new LevelPlayAdSize(description, width, height);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return $"adUnitId: {adUnitId}, adUnitName: {adUnitName}, adSize: {adSize}, adFormat: {adFormat}, placementName: {placementName}, auctionId: {auctionId}, country: {country}, ab: {ab}, segmentName: {segmentName}, adNetwork: {adNetwork}, instanceName: {instanceName}, instanceId: {instanceId}, revenue: {revenue}, precision: {precision}, encryptedCPM: {encryptedCPM}";
        }
    }
}
