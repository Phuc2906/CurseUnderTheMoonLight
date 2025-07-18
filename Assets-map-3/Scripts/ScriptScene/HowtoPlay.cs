using UnityEngine;
using UnityEngine.SceneManagement;

public class Help : MonoBehaviour
{
    public void LoadGameScene()
    {
       SceneManager.LoadScene("HowtoPlay");
    }
}
