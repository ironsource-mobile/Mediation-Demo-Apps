using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// Representation of a user segment
/// </summary>
public class IronSourceSegment
{
    [Obsolete("This method will be removed in version 9.0.0.")]
    /// <summary>
    /// Age of the segment
    /// </summary>
    public int age;

    [Obsolete("This method will be removed in version 9.0.0.")]
    /// <summary>
    /// Gender of the segment
    /// </summary>
    public string gender = null;

    /// <summary>
    /// level of the segment
    /// </summary>
    public int level;

    /// <summary>
    /// Is the segment paying
    /// </summary>
    public int isPaying;

    /// <summary>
    /// date at which the user was created
    /// </summary>
    public long userCreationDate;

    /// <summary>
    /// Number of in app purchases
    /// </summary>
    public double iapt;

    /// <summary>
    /// Name of the segment
    /// </summary>
    public string segmentName = null;

    /// <summary>
    /// Custom key-values for the segment
    /// </summary>
    public Dictionary<string, string> customs;


    /// <summary>
    /// Constructs a Segment with default values
    /// </summary>
    public IronSourceSegment()
    {
        customs = new Dictionary<string, string>();
        age = -1;
        level = -1;
        isPaying = -1;
        userCreationDate = -1;
        iapt = 0;
    }

    /// <summary>
    /// Add a custom key-value to the segment
    /// </summary>
    /// <param name="key">Key to be added</param>
    /// <param name="value">Value to be added</param>
    public void setCustom(string key, string value)
    {
        customs.Add(key, value);
    }

    /// <summary>
    /// Returns the segment as a Dictionary
    /// </summary>
    /// <returns>Dictionary representing the segment</returns>
    public Dictionary<string, string> getSegmentAsDict()
    {
        Dictionary<string, string> temp = new Dictionary<string, string>();
        if (age != -1)
            temp.Add("age", age + "");
        if (!string.IsNullOrEmpty(gender))
            temp.Add("gender", gender);
        if (level != -1)
            temp.Add("level", level + "");
        if (isPaying > -1 && isPaying < 2)
            temp.Add("isPaying", isPaying + "");
        if (userCreationDate != -1)
            temp.Add("userCreationDate", userCreationDate + "");
        if (!string.IsNullOrEmpty(segmentName))
            temp.Add("segmentName", segmentName);
        if (iapt > 0)
            temp.Add("iapt", iapt + "");

        Dictionary<string, string> result = temp.Concat(customs).GroupBy(d => d.Key).ToDictionary(d => d.Key, d => d.First().Value);

        return result;
    }
}
