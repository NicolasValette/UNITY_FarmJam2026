using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FarmJam2026
{
    public class SceneLoader : MonoBehaviour
    {
        //[SerializeField]
        //private Animator _transition;
        //[SerializeField]
        //private float _transitionTime = 1;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

           
        public void GoToScene(string sceneName)
        {
            StartCoroutine(LoadScene(sceneName));
        }
        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private IEnumerator LoadScene(string sceneName)
        {
            //_transition.SetTrigger("StartTransition");

            //yield return new WaitForSeconds(_transitionTime);
            yield return null;
            SceneManager.LoadScene(sceneName);
        }



    }
    
}
