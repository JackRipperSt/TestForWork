using System.Collections;
using UnityEngine;
using TMPro;

public class AFirstStructure : UIAlert
{

    [SerializeField] private FirstStructure _firstStructure;

    private void OnEnable()
    {
        _firstStructure.StorageLimitCreateResource += Alert;
    }

    private void OnDisable()
    {
        _firstStructure.StorageLimitCreateResource -= Alert;

    }

    protected override void Alert(string text, Transform spawnPoint)
    {
        _text.text = text;
        TMP_Text createText = Instantiate(_text, spawnPoint);
        _canvasGroup = spawnPoint.GetComponent<CanvasGroup>();
        UpAlpha();
        Destroy(createText.gameObject, 3);
    }

    protected override void UpAlpha()
    {
        _canvasGroup.alpha = 0.9f;
        StartCoroutine(ZeroAlpha());
    }

    protected override IEnumerator ZeroAlpha()
    {
        yield return new WaitForSeconds(3);
        _canvasGroup.alpha = 0;
    }
}
