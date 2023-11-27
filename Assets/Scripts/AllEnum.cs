public enum SettingState
{
    None,
    EnterInput,
    SettingInput,
    DoneInput
}
public enum PromptType
{
    Cat,
    Mineral,
    Geometry,
    Food,
    Liquor,
    BossAttack,
    Wood,
}
public enum GameState
{
    SpawnPrompt,
    PlayerInput,
    WrongInput,
    BossRush,
    BossDealtDamage,
    LeavingLevel
}
public enum ItemCondition
{
    None,
    PromptCanNotBeAnItem,
    PlayerCanNotGetThisItemYet,
    PlayerInventoryIsFull,
    ItemHasDescendedToByProduct,
    PlayerGetThisItem
}
public enum MerchantPage
{
    Trade,
    Weapon,
    Spell,
    Repair,
    Deposit
}
public enum SceneName
{
    None,
    Mine,
    Mine2,
    Mine3,
    Market,
    Catland,
    Garden,
    MeatFarm,
    Tavern,
    Boss,
    Forest
}
public enum MissionRequestItem
{
    None,
    HeartStone,
    AquaStone,
    ThinGreenCrystal,
    SquareStone
}
public enum LocationName
{
    None,
    BOROOM,
    LUDAPHIN,
    DARKVILLAGE,
    MARKET,
    BOSS
}
public enum AllItem
{
    None = 0,
    AquaStone = 1, ThinGreenCrystal = 2, HeartStone = 3, SquareStone = 4,
    BlackCat = 5, BlueCat = 6, BrownCat = 7, WhiteCat = 8,
    Mica = 9, OrangeLuster = 10, PoisonRock = 11, Turquoise = 12,
    FireBomb = 13,
    Apple = 14, Avocado = 15, GoldenKiwi = 16, Watermelon = 17,
    Falchion = 18, GothicSword = 19, RubyDagger = 20, Spatha = 21,
    Chicken = 22, Fish = 23, PorkRibs = 24, Steak = 25,
    Agate = 26, BloodAgate = 27, BloodTopaz = 28, Topaz = 29,
    Beer = 30, Moonshine = 31, Whiskey = 32, Wine = 33,
    Log = 34, SmallLogs = 35,
    BrokenSword = 36, Gladius = 37, LazuliSword = 38,
}
public enum MarketPage
{
    TRADE,
    WEAPON,
    SPELL,
    REPAIR,
    QUEST
}
public enum CraftPlacePage
{
    CRAFT,
    DEPOSIT
}
public enum EquipmentType
{
    NOT_RESTRICTED,
    MAIN,
    SECONDARY
}