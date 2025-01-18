using System;

namespace com.unity3d.mediation
{
    /// <summary>
    /// Represents detailed error information about an issue that occurred during the display of a LevelPlay advertisement.
    /// </summary>
    public class LevelPlayAdDisplayInfoError
    {
        public LevelPlayAdInfo DisplayLevelPlayAdInfo { get; private set; }
        public LevelPlayAdError LevelPlayError { get; private set; }

        [Obsolete("The constructor of LevelPlayAdDisplayInfoError will be removed in version 9.0.0.")]
        /// <summary>
        /// Initializes a new instance of the <see cref="LevelPlayAdDisplayInfoError"/> class with the specified advertisement information and error details.
        /// </summary>
        /// <param name="levelPlayAdInfo">The AdInfo associated with the error.</param>
        /// <param name="error">The error encountered during the advertisement display.</param>
        public LevelPlayAdDisplayInfoError(LevelPlayAdInfo levelPlayAdInfo, LevelPlayAdError error)
        {
            DisplayLevelPlayAdInfo = levelPlayAdInfo;
            LevelPlayError = error;
        }

        /// <summary>
        /// Returns a string that represents the current <see cref="LevelPlayAdDisplayInfoError"/>.
        /// </summary>
        /// <returns>A string that contains the AdInfo and error details.</returns>
        public override string ToString()
        {
            return $"LevelPlayAdDisplayInfoError: {DisplayLevelPlayAdInfo.ToString()}, {LevelPlayError.ToString()}";
        }
    }
}
