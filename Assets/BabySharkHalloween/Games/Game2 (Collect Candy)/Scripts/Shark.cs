using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game_2
{
    public class Shark : MonoBehaviour
    {
        public Image sharkImg;
        public GameObject basket;
        public GameObject fin;
        public GameObject shark2;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("Obstacle"))
            {
                Game2.instance.TouchObstacle(collision.transform.position);
                SoundManager.instance.PlaySound(2);

                sharkImg.enabled = false;
                basket.SetActive(false);
                fin.SetActive(false);
                shark2.SetActive(true);

                transform.DOLocalMoveX(-1300f, 2f).SetEase(Ease.InFlash);
            }
            else if (collision.gameObject.name.Equals("Bottom"))
            {
                Game2.instance.ShowGameOver();
            }
        }
    }
}