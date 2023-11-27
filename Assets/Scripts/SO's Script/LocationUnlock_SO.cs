using UnityEngine;
[CreateAssetMenu(fileName = "Location_", menuName = "Location")]
public class LocationUnlock_SO : ScriptableObject
{
    public LocationName locationName;
    public int playerLevelRequire;
    public bool locationUnlockedOnce;
}