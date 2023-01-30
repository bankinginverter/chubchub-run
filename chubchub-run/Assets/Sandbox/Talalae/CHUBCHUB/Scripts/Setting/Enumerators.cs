public class Enumerators
{
    public enum AppState
    {
        NONE = 0,
        APP_INIT,
        APP_REGISTER,
        APP_MAINMENU,
        APP_PREPARING,
        APP_MATCH,
        APP_INVENTORY,
        APP_MAP,
        APP_KITCHEN_CHOOSE,
        APP_KITCHEN_COOK,
        APP_KITCHEN_RESULT,
        APP_LAUNCH,
        APP_GAMEPLAY,
        APP_ENDGAME,
        APP_RESET
    }

    public enum KichenState
    {
        NONE = 0,

        KITCHEN_INIT,
        CLEARSOUP_SEQUENCE_1,
        CLEARSOUP_SEQUENCE_2,
        CLEARSOUP_SEQUENCE_3,
        CLEARSOUP_SEQUENCE_4,
        STIRFRIED_SEQUENCE_1,
        STIRFRIED_SEQUENCE_2,
        STIRFRIED_SEQUENCE_3,
        STIRFRIED_SEQUENCE_4,
        FRIEDRICE_SEQUENCE_1,
        FRIEDRICE_SEQUENCE_2,
        FRIEDRICE_SEQUENCE_3

    }
}