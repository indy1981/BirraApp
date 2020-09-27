using System;
using System.Collections.Generic;
using System.Text;

namespace BirrasApp.Services.Tools
{
    public class BirrasLogic
    {
        private static readonly int BEERS_PACK = 6;

        public static int GetTotalPacksByTemperatureAndPeople(float temperature, int people)
        {
            float beerModifierByTemperature = 1.0f;

            switch (temperature)
            {
                case var n when (n < 20f):
                    beerModifierByTemperature *= 0.75f;  // 0.75 birras por persona
                    break;
                case var n when (n > 24f):
                    beerModifierByTemperature += 2f; // se toman dos birras mas por persona
                    break;
                default:
                    break;
            }

            // Que sobre y no que falte
            var totalBeers = Math.Ceiling(beerModifierByTemperature * people);
            var packsToBuy = (int)Math.Ceiling(totalBeers / BEERS_PACK);

            return packsToBuy;
        }
    }
}
