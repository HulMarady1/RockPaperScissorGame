using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Draw : MonoBehaviour
{
   public void PlayAgain ()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
   }

   public void Exit ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
    }
}
