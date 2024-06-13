using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
   public Slider slider;

   public void SetMaxHealth(int _health)
   {
       slider.maxValue = _health;
       slider.value = _health;
   }

   public void SetHealth(int _health)
   {
        slider.value = _health;
   }
}
