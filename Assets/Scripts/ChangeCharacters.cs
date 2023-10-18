
using UnityEngine;

public class ChangeCharacters : MonoBehaviour
{
    [SerializeField] AnimatorOverrideController mariaController;
    [SerializeField] AnimatorOverrideController johnController;
    [SerializeField] Avatar johnAvatar;
    [SerializeField] Avatar mariaAvatar;
    [SerializeField] GameObject maria;
    [SerializeField] GameObject john;
    public bool canChange = true;
    private void Start() 
    {
        GetComponent<Animator>().runtimeAnimatorController = johnController;
        GetComponent<Animator>().avatar = johnAvatar;
        maria.SetActive(false);
        john.SetActive(true);
    }
    public void Jonh()
    {
        if(!canChange)return;
        GetComponent<Animator>().runtimeAnimatorController = johnController;
        GetComponent<Animator>().avatar = johnAvatar;
        john.SetActive(true);
        maria.SetActive(false);
    }
    public void Maria()
    {
        if(!canChange)return;
        GetComponent<Animator>().runtimeAnimatorController = mariaController;
        GetComponent<Animator>().avatar = mariaAvatar;
        maria.SetActive(true);
        john.SetActive(false);
    }
}
