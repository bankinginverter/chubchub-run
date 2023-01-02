[System.Serializable]

public class Item
{
    #region Class Declarations

        public enum ItemType
        {
            INGREDIANT_BREAD,
            INGREDIANT_HAM,
            INGREDIANT_FISH
        }

        public ItemType itemType;

        public int amount;

    #endregion
}
