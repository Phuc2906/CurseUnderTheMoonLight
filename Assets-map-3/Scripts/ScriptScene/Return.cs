using UnityEngine;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
    public void LoadGameScene()
    {
       SceneManager.LoadScene("MainMenu");
    }
}
