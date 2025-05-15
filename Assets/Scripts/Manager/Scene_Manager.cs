using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public void P_Reset()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void P_Reset(float delay)
    {
        Invoke("P_Reset", delay);
    }
}
