using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game_3
{
    public class Shark : MonoBehaviour
    {
        public Image sharkImg;
        public GameObject basket;
        public GameObject fin;
        public GameObject shark2;
        public CapsuleCollider2D basketColl;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("Obstacle"))
            {
                Game3.instance.TouchObstacle(collision.transform.position);

                if (collision.gameObject.name.StartsWith("Stone"))
                    SoundManager.instance.PlaySound(3);
                else
                    SoundManager.instance.PlaySound(2);

                UseLife();
            }
        }

        private void UseLife()
        {
            sharkImg.enabled = false;
            basket.SetActive(false);
            fin.SetActive(false);
            shark2.SetActive(true);
            GetComponent<CapsuleCollider2D>().enabled = false;
            basketColl.enabled = false;

            if (Game3.instance.life > 0)
            {
                Container.life--;
                Game3.instance.life--;

                transform.DOLocalMoveX(-1300f, 2f).SetEase(Ease.InFlash).OnComplete(() =>
                {
                    sharkImg.enabled = true;
                    basket.SetActive(true);
                    fin.SetActive(true);
                    shark2.SetActive(false);

                    Container.showGame3Intro = false;
                    StartCoroutine(GlobalScript.instance.ChangeScene(SceneManager.GetActiveScene().buildIndex, 0f));
                });
            }
            else
            {
                transform.DOLocalMoveX(-1300f, 2f).SetEase(Ease.InFlash);
            }
        }
    }
}