using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public int crystalCount;
    public Text crystalCountText;
    public static Inventory instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory dans la scène");
            return;
        }
        instance = this;
    }

    public void AddCrystal(int count)
    {
        crystalCount += count;
        crystalCountText.text = crystalCount.ToString();

    }
}
