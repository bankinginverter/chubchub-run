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
        APP_COSTUME,
        APP_LAUNCH,
        APP_GAMEPLAY,
        APP_ENDGAME,
        APP_SETTING
    }

    public enum GameplayState
    {
        NONE = 0,
        GAMEPLAY_INIT,
        GAMEPLAY_PREPARE,
        GAMEPLAY_START,
        GAMEPLAY_STOP
    }
}