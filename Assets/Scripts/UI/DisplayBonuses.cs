using UnityEngine.UI;
using UnityEngine;


namespace GB.UI
{
    public sealed class DisplayBonuses
    {
        private Text _text;

        public DisplayBonuses()
        {
            _text = Object.FindObjectOfType<Text>();
        }

        public void Display(int value)
        {
            _text.text = $"Вы набрали {value} бонусов";
        }
    }
}
