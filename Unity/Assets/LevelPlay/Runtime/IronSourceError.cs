using System;

/// <summary>
/// Represents an error received from LevelPlay
/// </summary>
public class IronSourceError
{
    private string description;
    private int code;

    /// <summary>
    /// Returns the numerical error code.
    /// </summary>
    /// <returns>Error code</returns>
    public int getErrorCode()
    {
        return code;
    }

    /// <summary>
    /// Returns the error description
    /// </summary>
    /// <returns>Description of the error</returns>
    public string getDescription()
    {
        return description;
    }

    /// <summary>
    /// Returns the numerical error code.
    /// </summary>
    /// <returns>Error code</returns>
    public int getCode()
    {
        return code;
    }

    /// <summary>
    /// Constructs an IronSourceError
    /// </summary>
    /// <param name="errorCode">Numerical error code</param>
    /// <param name="errorDescription">Description of the error</param>
    [Obsolete("This function will be made private in a future version.", false)]
    public IronSourceError(int errorCode, string errorDescription)
    {
        code = errorCode;
        description = errorDescription;
    }

    /// <summary>
    /// Returns a string that represents the error
    /// </summary>
    /// <returns>String representation of the error</returns>
    public override string ToString()
    {
        return code + " : " + description;
    }
}
