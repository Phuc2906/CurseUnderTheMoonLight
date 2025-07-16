using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public RawImage healthBarRed;
    public RawImage healthBarWhite;

    private RectTransform redTransform;
    private RectTransform whiteTransform;

    private float originalWidth;
    private Vector2 originalPosition; // Lưu vị trí gốc của thanh máu

    private float maxHP = 100f;
    private float currentHP;
    private float targetHP;

    private bool isDead = false;
    private float reduceSpeed = 50f; // Tốc độ tụt từ từ

    void Start()
    {
        // Lấy RectTransform của health bars
        redTransform = healthBarRed.GetComponent<RectTransform>();
        whiteTransform = healthBarWhite.GetComponent<RectTransform>();

        // Lưu vị trí và kích thước gốc
        originalWidth = redTransform.sizeDelta.x;
        originalPosition = redTransform.anchoredPosition;

        currentHP = maxHP;
        targetHP = maxHP;

        redTransform.sizeDelta = new Vector2(originalWidth, redTransform.sizeDelta.y);
        whiteTransform.sizeDelta = new Vector2(originalWidth, whiteTransform.sizeDelta.y);
    }

    void Update()
    {
        if (isDead) return;

        float redTargetWidth = originalWidth * (targetHP / maxHP);

        if (redTransform.sizeDelta.x > redTargetWidth)
        {
            float newWidth = Mathf.MoveTowards(
                redTransform.sizeDelta.x,
                redTargetWidth,
                reduceSpeed * Time.deltaTime
            );
            redTransform.sizeDelta = new Vector2(newWidth, redTransform.sizeDelta.y);

            // Điều chỉnh anchoredPosition để giữ thanh máu ở vị trí cố định
            float offset = (originalWidth - newWidth) / 2f;
            redTransform.anchoredPosition = new Vector2(
                originalPosition.x + offset,
                originalPosition.y
            );
        }

        if (targetHP <= 0 && redTransform.sizeDelta.x <= 0.01f && !isDead)
        {
            Debug.Log("Gọi hàm GameOver vì HP = 0");
            GameOver();
        }
    }

    public void TakeDamage(float damage)
    {
        targetHP = Mathf.Max(targetHP - damage, 0f);
        Debug.Log($"Player bị mất máu! HP còn lại: {targetHP}");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.collider.CompareTag("Obstacle"))
        {
            Debug.Log("Va chạm Obstacle!");
            GameOver();
        }
    }

    void GameOver()
    {
        isDead = true;
        redTransform.sizeDelta = new Vector2(0, redTransform.sizeDelta.y);
        redTransform.anchoredPosition = originalPosition; // Đặt lại vị trí gốc khi chết

        // Gọi GameOver từ GameManager
        if (GameManager.instance != null)
        {
            GameManager.instance.GameOver();
            Debug.Log("Đã gọi GameManager.instance.GameOver()");
        }
        else
        {
            Debug.LogError("GameManager.instance không tồn tại!");
        }
    }
}