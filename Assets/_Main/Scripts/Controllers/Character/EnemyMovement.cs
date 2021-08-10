using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Control enemy AI and behaviour
/// </summary>
public class EnemyMovement : BaseCharacter
{
    [SerializeField] private NavMeshAgent _enemyNavMesh = default;
    [SerializeField] private Transform _pointA = default;
    [SerializeField] private Transform _pointB = default;

    private GameObject _objectToChase = default;
    private bool _isACoroutineActive = false;
    private Action onObjectCaught = null;

    #region Base Character Inheritance

    public void SetupCharacter(GameObject playerObjectReference, Action onPlayerCaught)
    {
        base.SetupCharacter();
        _objectToChase = playerObjectReference;
        characterCanMove = true;
        onObjectCaught = onPlayerCaught;
        _enemyNavMesh.speed = 6f;
        _enemyNavMesh.SetDestination(transform.position);
        _isACoroutineActive = false;
        playerIsReady = true;
    }

    protected override void MoveCharacter()
    {
        if (!characterCanMove)
        {
            SetSpeedValueAnimation(0);
            _enemyNavMesh.SetDestination(transform.position);

            if (_isACoroutineActive)
            {
                StopAllCoroutines();
                _isACoroutineActive = false;
            }
            return;
        }

        if ((transform.position - _objectToChase.transform.position).magnitude < 10f)
        {
            if (_isACoroutineActive)
            {
                StopAllCoroutines();
                _isACoroutineActive = false;
            }

            _enemyNavMesh.SetDestination(_objectToChase.transform.position);

            if ((transform.position - _objectToChase.transform.position).magnitude < 2f)
            {
                onObjectCaught?.Invoke();
            }
        }
        else
        {

            if (!_isACoroutineActive)
                StartCoroutine(MovePatrol());
        }

        SetSpeedValueAnimation(_enemyNavMesh.speed);
    }

    #endregion

    private void Update()
    {
        if(playerIsReady)
            MoveCharacter();
    }

    /// <summary>
    /// Enemy moves around some object and determined points
    /// </summary>
    /// <returns></returns>
    private IEnumerator MovePatrol()
    {
        _isACoroutineActive = true;
        while (true)
        {
            _enemyNavMesh.SetDestination(_pointA.position);
            yield return new WaitUntil(() => (transform.position - _pointA.position).magnitude < 2f);
            _enemyNavMesh.SetDestination(_pointB.position);
            yield return new WaitUntil(() => (transform.position - _pointB.position).magnitude < 2f);
        }
    }
}
