using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.AI;

public class Salamander : MonoBehaviour
{

    #region PathCreatorVar
    [Space(5)]
    [Header("Path Creator Var")]

    [Tooltip("Assign here the path component the monster shall follow")]
    public PathCreator salamanderPath;

    [HideInInspector]
    public float distanceTravelled;

    [Tooltip("Where in the Path the monster will stop Scouting to display Idle Animation")]
    public int pointToStop1, pointToStop2;
    int originalPoint1, originalPoint2;
    #endregion

    #region SalamanderVar
    public enum SalamanderState { Idle1, Idle2, Scouting, Chasing, Returning, Dead }
    Animator salamanderAnimator;

    [Space(5)]
    [Header("Salamander Var")]

    public SalamanderState salamanderCurrentState;

    [Range(5, 25)]
    public float salamanderSpeedOnPath;

    public bool isInsideMaxRange;

    public AudioSource fonteSom;
    public AudioClip somAvisoSalamandra;
    #endregion

    #region NavMeshVar
    NavMeshAgent mySalamanderAgent;
    Transform targetToAttack;

    [Space(5)]
    [Header("NavMesh Var")]

    public Transform positionToReturn;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        originalPoint1 = pointToStop1;
        originalPoint2 = pointToStop2;
        salamanderAnimator = GetComponent<Animator>();
        mySalamanderAgent = GetComponent<NavMeshAgent>();
        targetToAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        SalamanderActionInEachState();
        SalamanderAnimationInEachState();
    }

    #region SalamanderAnimation
    void SalamanderAnimationInEachState()
    {
        switch (salamanderCurrentState)
        {
            case SalamanderState.Idle1:
                salamanderAnimator.SetInteger("salamanderState", SalamanderIntStateToAnim());
                break;
            case SalamanderState.Idle2:
                salamanderAnimator.SetInteger("salamanderState", SalamanderIntStateToAnim());
                break;
            case SalamanderState.Scouting:
                salamanderAnimator.SetInteger("salamanderState", SalamanderIntStateToAnim());
                break;
            case SalamanderState.Chasing:
                salamanderAnimator.SetInteger("salamanderState", SalamanderIntStateToAnim());
                break;
            case SalamanderState.Returning:
                salamanderAnimator.SetInteger("salamanderState", SalamanderIntStateToAnim());
                break;
            case SalamanderState.Dead:
                salamanderAnimator.SetInteger("salamanderState", SalamanderIntStateToAnim());
                break;
            default:
                break;
        }
    }

    int SalamanderIntStateToAnim()
    {
        return (int)salamanderCurrentState;
    }
    #endregion

    #region SalamanderAction
    void SalamanderActionInEachState()
    {
        switch (salamanderCurrentState)
        {
            case SalamanderState.Idle1:
                SetSalamanderOnPath();
                break;
            case SalamanderState.Idle2:
                SetSalamanderOnPath();
                break;
            case SalamanderState.Scouting:
                SetSalamanderOnPath();
                break;
            case SalamanderState.Chasing:
                ChaseTarget();
                break;
            case SalamanderState.Returning:
                ReturnToScoutingPosition();
                break;
            case SalamanderState.Dead:
                break;
            default:
                break;
        }
    }        

    #region SalamanderIdleScouting
    void SetSalamanderOnPath()
    {
        // defino a uma variavel com a minha posição no eixo X (em valores inteiros)
        int myPositionX = (int)transform.localPosition.x;

        if (myPositionX == pointToStop1 || myPositionX == pointToStop2) // se essa minha posição for igual ao ponto de parada 1 ou ponto de parada 2
        {
            if (myPositionX == pointToStop1)
                salamanderCurrentState = SalamanderState.Idle1; // quer dizer que estou executando a animação Idle 1
            else if (myPositionX == pointToStop2)
                salamanderCurrentState = SalamanderState.Idle2; // quer dizer que estou executando a animação Idle 2

            StartCoroutine(StopIdle()); // paro de executar o Idle após 2 segundos -> Coroutine
        }
        else // se eu não estiver em um pont de parada
        {
            salamanderCurrentState = SalamanderState.Scouting; // quer dizer que estou patrulhando
            StartCoroutine(RecoverIdle());
            distanceTravelled += salamanderSpeedOnPath * Time.deltaTime; // percorro o caminho em velocidade normal
        }

        transform.position = salamanderPath.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Loop); // percorro o caminho em loop
        transform.rotation = salamanderPath.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Loop); // modifico minha rotação conforme o caminho
    }

    // coroutine para sair do estado Idle e voltar a patrulhar -> executada após 2s
    IEnumerator StopIdle()
    {
        yield return new WaitForSeconds(2.5f);
        pointToStop1 += 200;
        pointToStop2 += 200;
    }

    // coroutine para recuperar informações de onde o monster deve parar p/ executar Idle
    IEnumerator RecoverIdle()
    {
        yield return new WaitForSeconds(1f);
        if (pointToStop1 != originalPoint1)
            pointToStop1 = originalPoint1;

        if (pointToStop2 != originalPoint2)
            pointToStop2 = originalPoint2;
    }
    #endregion

    #region SalamanderChasing
    void ChaseTarget()
    {
        Vector3 direction = (targetToAttack.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1);
        mySalamanderAgent.SetDestination(targetToAttack.position);
    }
    #endregion

    #region SalamanderReturning
    void ReturnToScoutingPosition()
    {
        mySalamanderAgent.SetDestination(positionToReturn.position);
        bool reachedScoutPoint = (int)transform.position.x == (int)positionToReturn.position.x && (int)transform.position.z == (int)positionToReturn.position.z;
        if (reachedScoutPoint)
        {
            salamanderCurrentState = SalamanderState.Scouting;
        }
    }
    #endregion

    #region OnTriggerMethods
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bool onScoutRoute = salamanderCurrentState == SalamanderState.Scouting || salamanderCurrentState == SalamanderState.Idle1 || salamanderCurrentState == SalamanderState.Idle2;
            if (onScoutRoute)
            {
                positionToReturn.position = transform.position;
            }

            fonteSom.PlayOneShot(somAvisoSalamandra);
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isInsideMaxRange)
            {
                salamanderCurrentState = SalamanderState.Chasing;
            }
        }
    }
    #endregion

    #region OnCollisionMethod
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
        }
                
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("PunhoPlayer") || other.gameObject.CompareTag("PicaretaPlayer"))
        {
        }
    }
    #endregion

    #endregion // salamander action

}
