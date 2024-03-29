﻿using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Text;
using NeuralNetwork.TurnBasedBoardGameTrainerStuff.Enums;

using TikTakToe.DrawStuff;

namespace TikTakToe.PlayerTypes
{
    public abstract class Player
    {
        public Players PlayerID { get; set; }
        public Player(Players playerID)
        {
            PlayerID = playerID;
        }

        public Dictionary<int, Dictionary<Players, int>> GetPlayerValue { get; set; }
        public abstract (int y, int x) SelectTile(Node<Board> CurrentTree);
    }
}
