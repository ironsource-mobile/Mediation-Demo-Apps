using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.unity3d.mediation
{
    /// <summary>
    /// Represents the reward from LevelPlay Rewarded Ad, including a name and the amount
    /// </summary>
    public class LevelPlayReward
    {
        // The name of the reward.
        public string Name { get; }

        // The amount of the reward.
        public int Amount { get; }

        internal LevelPlayReward(string name, int amount)
        {
            Name = name;
            Amount = amount;
        }
    }

}
