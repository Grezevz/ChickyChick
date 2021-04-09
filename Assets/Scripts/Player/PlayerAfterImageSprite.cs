using UnityEngine;

public class PlayerAfterImageSprite  : MonoBehaviour
{
    [SerializeField] private float activeTime = 0.1f;
    private float timeActived;
    private float alpha;
    [SerializeField] private float alphaSet = 0.8f;
    private float alphaMultiplier = 0.92f;

    private Transform player;
    private SpriteRenderer SR;
    private SpriteRenderer playerSR;

    private Color color;

    private void OnEnable() {
        SR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerSprite").transform;
        playerSR = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        SR.sprite = playerSR.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        transform.localScale = player.localScale;
        timeActived = Time.time;
    }

    private void Update() {
        alpha *= alphaMultiplier;
        color = new Color(1f, 1f, 1f, alpha);
        SR.color = color;

        if (Time.time > timeActived + activeTime) {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
