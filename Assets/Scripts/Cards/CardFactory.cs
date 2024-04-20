using System;
using System.Reflection;

namespace com.deathbox.jam
{
    public static class CardFactory
    {
        private static Random random = new Random();

        public static ACard CreateRandomCard()
        {
            // Get all types derived from ACard
            Type[] cardTypes = Assembly.GetAssembly(typeof(ACard)).GetTypes();
            Type cardType = null;

            // Find a random type derived from ACard
            while (cardType == null || cardType == typeof(ACard) || !typeof(ACard).IsAssignableFrom(cardType))
            {
                int randomIndex = random.Next(cardTypes.Length);
                cardType = cardTypes[randomIndex];
            }

            // Instantiate and return the selected card type
            return (ACard)Activator.CreateInstance(cardType);
        }
    }
}
