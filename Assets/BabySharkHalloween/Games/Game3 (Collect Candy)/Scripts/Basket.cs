using UnityEngine;
using UnityEngine.UI;

namespace Game_3
{
    public class Basket : MonoBehaviour
    {
        [SerializeField] private GameObject tapEffect;
        [SerializeField] private Image basketImg;
        [SerializeField] private Sprite[] baskets;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name.Equals("Candy"))
            {
                TapEffect(collision.transform.position);
                Destroy(collision.transform.parent.gameObject);
                Game3.instance.catchCandy++;
                SoundManager.instance.PlaySound(0);

                int catchCandy = Game3.instance.catchCandy;
                basketImg.gameObject.SetActive(true);
                if (catchCandy <= 10)
                    basketImg.sprite = baskets[0];
                else if (catchCandy >= 11 && catchCandy <= 15)
                    basketImg.sprite = baskets[1];
                else if (catchCandy >= 16 && catchCandy <= 20)
                    basketImg.sprite = baskets[2];
                else if (catchCandy >= 21 && catchCandy <= 25)
                    basketImg.sprite = baskets[3];
                else if (catchCandy >= 26)
                    basketImg.sprite = baskets[4];
            }
            else if (collision.gameObject.name.Equals("Finish"))
            {
                Game3.instance.ShowGameWin();
            }
        }

        private void TapEffect(Vector2 pos)
        {
            _ = Instantiate(tapEffect, pos, Quaternion.identity, null);
        }
    }
}