using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public RawImage healthBarRed;
    public RawImage healthBarWhite;

    private RectTransform redTransform;
    private RectTransform whiteTransform;

    private float originalWidth;
    private Vector2 originalPosition;

    private float maxHP = 100f;
    private float currentHP; // HP hiện tại
    private float targetHP;  // HP mục tiêu để cập nhật UI từ từ

    private bool isDead = false;
    private float reduceSpeed = 100f; // Tốc độ tụt/tăng từ từ

    void Start()
    {
        redTransform = healthBarRed.GetComponent<RectTransform>();
        whiteTransform = healthBarWhite.GetComponent<RectTransform>();

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

        // Cập nhật thanh máu đỏ (tăng hoặc giảm từ từ)
        if (!Mathf.Approximately(redTransform.sizeDelta.x, redTargetWidth))
        {
            float newWidth = Mathf.MoveTowards(
                redTransform.sizeDelta.x,
                redTargetWidth,
                reduceSpeed * Time.deltaTime
            );
            redTransform.sizeDelta = new Vector2(newWidth, redTransform.sizeDelta.y);

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
        if (isDead) return;

        targetHP = Mathf.Max(currentHP - damage, 0f); // Cập nhật targetHP khi nhận sát thương
        currentHP = targetHP; // Đồng bộ hóa ngay lập tức
        Debug.Log($"Player bị mất máu! HP còn lại: {currentHP}");
    }

    public void Heal(float amount)
    {
        if (isDead) return;

        currentHP = Mathf.Min(currentHP + amount, maxHP);
        targetHP = currentHP; // Đồng bộ targetHP với currentHP khi hồi máu
        Debug.Log($"Hồi máu: {amount}, HP hiện tại: {currentHP}");
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
        whiteTransform.sizeDelta = new Vector2(0, whiteTransform.sizeDelta.y);
        redTransform.anchoredPosition = originalPosition;
        whiteTransform.anchoredPosition = originalPosition;

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