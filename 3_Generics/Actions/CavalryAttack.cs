using System;
using System.Collections.Generic;
using System.Text;
using StrategyGame.Warriors.Abstractions;
using StrategyGame.Warriors.Interfaces;
using StrategyGame.Warriors.Models;
using static ITEA_Collections.Common.Extensions;
using System.Linq;

namespace StrategyGame.Actions
{
    public class CavalryAttack<T> : Battle<Knight, T> where T : IRangeUnit
    {
        protected override int CountPoints(IEnumerable<CombatUnit> army)
        {

            int total = 0;
            int count = 0;
            foreach (var item in army)
            {
                count++;
                if (item.UnitType == CombatUnitType.Melee)
                    item.Strength = item.Strength*2;
                total += item.Health + item.Strength;
            }
            ToConsole($"Total army count: {count}", ConsoleColor.DarkYellow);
            ToConsole($"Total army strength: {total}", ConsoleColor.DarkYellow);
            return total;
        }
    }

}
