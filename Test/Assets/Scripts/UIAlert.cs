using System.Collections;
using UnityEngine;
using TMPro;

public abstract class UIAlert : MonoBehaviour
{
    [SerializeField] protected TMP_Text _text;

    protected CanvasGroup _canvasGroup;

  

    protected abstract void Alert(string text, Transform spawnPoint);


    protected abstract void UpAlpha();



    protected abstract IEnumerator ZeroAlpha();

}
