using System;
using System.Collections.Generic;
using System.Text;
using EFT;
using UnityEngine;

namespace KillsAndHitAndDeathSounds
{
    internal class Utils
    {
        public static GameWorld World;

        public static bool InRaid;

        private static Player _localPlayer;

        public static Player LocalPlayer
        {
            get
            {
                if (World != null && InRaid)
                {
                    foreach (Player player in World.RegisteredPlayers)
                    {
                        if (player != null && player.IsYourPlayer)
                            return player;
                    }
                }

                return _localPlayer;
            }
            set => _localPlayer = value;
        }
    }
}