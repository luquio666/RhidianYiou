using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleIntro : Module
{

    public Animator Anim;
    public string IntroAnimName = "IntroAnim";
    public string LoopAnimName = "IntroLoop";
    public float ReplayIntroTimer = 30f;
    [Space]
    public GameObject MainMenu;

    private Coroutine _replayIntroCo;

    public void PlayIntro()
    {
        Anim.Play(IntroAnimName, 0, 0f);
        _replayIntroCo = StartCoroutine(ReplayIntroCo());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
        {
            if (Anim.GetCurrentAnimatorStateInfo(0).IsName(IntroAnimName))
            {
                Anim.Play(LoopAnimName, 0, 0f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Anim.GetCurrentAnimatorStateInfo(0).IsName(IntroAnimName))
            {
                Anim.Play(LoopAnimName, 0, 0f);
            }
            else
            {
                GoToMainMenu();
            }
        }
    }

    private void GoToMainMenu()
    {
        if (_replayIntroCo != null)
            StopCoroutine(_replayIntroCo);

        MainMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public IEnumerator ReplayIntroCo()
    {
        yield return new WaitForSeconds(ReplayIntroTimer);
        ReplayIntro();
    }

    private void ReplayIntro()
    {
        Anim.Play(IntroAnimName, 0, 0f);
        _replayIntroCo = StartCoroutine(ReplayIntroCo());
    }

}
