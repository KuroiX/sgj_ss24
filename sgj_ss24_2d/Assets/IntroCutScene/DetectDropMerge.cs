using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace IntroCutScene
{
    [RequireComponent(typeof(Collider2D))]
    public class DetectDropMerge : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Drop")) return;

            GameObject.Find("blitzii").GetComponent<Animator>().enabled = true;
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Lightning");
            StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            yield return null;
            yield return null;
            yield return null;
            yield return null;

            FindObjectOfType<SceneLoader>().LoadSceneByIndex(3);
        }
    }
}