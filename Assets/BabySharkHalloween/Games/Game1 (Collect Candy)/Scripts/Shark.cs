using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shark : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Camera _camera;
    [SerializeField] private ParticleSystem tapEffect;
    [SerializeField] private GameObject hand;
    [SerializeField] private Image basketImg;
    [SerializeField] private Sprite[] baskets;
    private Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        ShowHand();
    }

    private void ShowHand()
    {
        hand.SetActive(Container.game1Hand);
        Container.game1Hand = false;
    }

    private void OnMouseDown()
    {
        offset = transform.position - _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition + offset;
    }

    private void OnMouseUp()
    {
        offset = Vector2.zero;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - _camera.ScreenToWorldPoint(Input.mousePosition);

        if (hand != null)
            hand.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        offset = Vector2.zero;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        tapEffect.Play();
        Game1.instance.catchCandy++;
        SoundManager.instance.PlaySound(0);

        int candy = Game1.instance.catchCandy;
        if (candy > 5)
        {
            basketImg.gameObject.SetActive(true);

            if (candy > 25)
                basketImg.sprite = baskets[3];
            else if (candy > 15 && candy <= 25)
                basketImg.sprite = baskets[2];
            else if (candy > 10 && candy <= 15)
                basketImg.sprite = baskets[1];
        }
    }
}