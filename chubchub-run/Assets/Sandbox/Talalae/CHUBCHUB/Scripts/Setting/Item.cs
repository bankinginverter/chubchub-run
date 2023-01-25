[System.Serializable]

public class Item
{
    #region Class Declarations

        public enum ItemType
        {
            INGREDIANT_BASIL,
            INGREDIANT_CABBAGE,
            INGREDIANT_RICE,
            INGREDIANT_PORK,
            INGREDIANT_PEAS,
            INGREDIANT_CHILI,
            INGREDIANT_CELERY,
            INGREDIANT_CHICKEN,
            INGREDIANT_CORN,
            INGREDIANT_EGG,
            INGREDIANT_CARROT,
            INGREDIANT_GALIC,
            INGREDIANT_CUCUMBER,
            INGREDIANT_TOFU
        }

        public ItemType itemType;

        public int amount;

    #endregion
}
