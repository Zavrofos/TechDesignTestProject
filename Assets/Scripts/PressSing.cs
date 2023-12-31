using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PressSing : MonoBehaviour
{
    private Animator _birdAnimator;
    [SerializeField] private Animator _singAnimator;
    [SerializeField] private Button _buttonNextScene;
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private int indexToNextScene;

    private void Start()
    {
        _birdAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _buttonNextScene.onClick.AddListener(ToNextScene);
    }

    private void OnDisable()
    {
        _buttonNextScene.onClick.RemoveListener(ToNextScene);
    }

    private void OnMouseDown()
    {
        StartCoroutine(OpenSingCoroutine());
    }

    public void ToNextScene()
    {
        StartCoroutine(NextSceneCoroutine());
    }

    public IEnumerator NextSceneCoroutine()
    {
        _buttonNextScene.gameObject.SetActive(false);
        _singAnimator.SetTrigger("close_sing");
        yield return new WaitForSeconds(1);
        _birdAnimator.SetTrigger("fly_right");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(indexToNextScene);
    }

    public IEnumerator OpenSingCoroutine()
    {
        _singAnimator.SetTrigger("start");
        yield return new WaitForSeconds(1);
        _buttonNextScene.GetComponent<RectTransform>().position = _singAnimator.transform.position;
        _buttonNextScene.GetComponent<RectTransform>().rotation = _singAnimator.transform.rotation;
        RectTransform buttonTextRect = _buttonText.GetComponent<RectTransform>();
        buttonTextRect.rotation = Quaternion.Euler(Vector3.zero);
        _buttonNextScene.gameObject.SetActive(true);
    }
}
