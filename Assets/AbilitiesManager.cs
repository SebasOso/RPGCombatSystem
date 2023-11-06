using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
    [SerializeField] private GameObject tornadoPrefab;
    private InputReader inputReader;

    void Start()
    {
        inputReader = GetComponent<InputReader>();
        inputReader.Tornado += TornadoAnim;
    }
    public void TornadoAnim()
    {
        GetComponent<PlayerStateMachine>().IsTornado = true;
    }
    public void CastTornado()
    {
        print("TORNADO");
        GameObject tornado = Instantiate(tornadoPrefab, transform.position, Quaternion.Euler(new Vector3(0,0,0)));
        
        StartCoroutine(Tornado(tornado));
    }
    private IEnumerator Tornado(GameObject tornado)
    {
        yield return new WaitForSeconds(9f);
        inputReader.IsCasting = false;
        Destroy(tornado);
    }
}
