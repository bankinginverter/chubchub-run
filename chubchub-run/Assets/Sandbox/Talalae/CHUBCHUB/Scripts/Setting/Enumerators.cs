public class Enumerators
{
    public enum AppState
    {
        NONE = 0,
        APP_INIT,
        APP_REGISTER,
        APP_MAINMENU,
        APP_LAUNCH,
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