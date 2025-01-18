namespace com.unity3d.mediation
{
    /// <summary>
    /// Represents dimensions and descriptions for different types of advertisement sizes.
    /// </summary>
    public class LevelPlayAdSize
    {
        private int width;
        private int height;
        private string description;
        private int customWidth = -1;

        /// <summary>
        /// Standard banner size
        /// </summary>
        public static LevelPlayAdSize BANNER = new LevelPlayAdSize("BANNER");

        /// <summary>
        /// Standard large size
        /// </summary>
        public static LevelPlayAdSize LARGE = new LevelPlayAdSize("LARGE");

        /// <summary>
        /// Standard mrec size
        /// </summary>
        public static LevelPlayAdSize MEDIUM_RECTANGLE = new LevelPlayAdSize("MEDIUM_RECTANGLE");

        /// <summary>
        /// Standard leaderboard size
        /// </summary>
        public static LevelPlayAdSize LEADERBOARD = new LevelPlayAdSize("LEADERBOARD");

        private LevelPlayAdSize(string description)
        {
            this.description = description;
        }

        internal LevelPlayAdSize(string description, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.description = description;
        }

        /// <summary>
        /// Creates a custom banner size with specified dimensions.
        /// </summary>
        /// <param name="width">The width of the custom banner in pixels.</param>
        /// <param name="height">The height of the custom banner in pixels.</param>
        /// <returns>A new instance of <see cref="LevelPlayAdSize"/> representing the custom size.</returns>
        public static LevelPlayAdSize CreateCustomBannerSize(int width, int height)
        {
            return new LevelPlayAdSize("CUSTOM", width, height);
        }

        /// <summary>
        /// Creates an adaptive banner with default screen width.
        /// The default screen width is used if the custom width is not specified. Specify the custom width if necessary.
        /// </summary>
        /// <param name="customWidth">Custom width of the adaptive banner container.
        /// On Android, it is measured in DP(density-independent pixels), and on IOS, it is in measured in Points.</param>
        /// <returns>A new instance of <see cref="LevelPlayAdSize"/> representing the Adaptive size.</returns>
        public static LevelPlayAdSize CreateAdaptiveAdSize(int customWidth = -1)
        {
            var adaptiveBanner = new LevelPlayAdSize("ADAPTIVE") { customWidth = customWidth };
            return adaptiveBanner;
        }

        /// <summary>
        /// Description for the banner
        /// </summary>
        public string Description { get { return description; } }

        /// <summary>
        /// Width of the banner
        /// </summary>
        public int Width { get { return width; } }

        /// <summary>
        /// Height of the banner
        /// </summary>
        public int Height { get { return height; } }

        /// <summary>
        /// Custom width of the banner in DP
        /// </summary>
        public int CustomWidth { get { return customWidth; } }

        public override string ToString()
        {
            return string.Format("Description: {0}, Width: {1}, Height: {2}", description, width, height);
        }
    }
}
