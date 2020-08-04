using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlakeAnimation : MonoBehaviour
{

    public Animator blakeAnimator;

    BlakeController blakeStateToAnim;

    // idle var
    int sortIdle;
    float timerIdle;

    // Start is called before the first frame update
    void Start()
    {
        blakeStateToAnim = GetComponent<BlakeController>();
    }

    // Update is called once per frame
    void Update()
    {
        Animation();
    }
    
    void Animation()
    {
        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        switch (blakeStateToAnim.blakeCurrentState)
        {
            case BlakeController.BlakeState.Idle:
                ValuesToAnimate(0, 0);
                AnimationIdle(4, 3); // 1º = num de variações... 2º = tempo para mudar as variações
                break;
            case BlakeController.BlakeState.Walking:
                ValuesToAnimate(hor, vert);
                break;
            case BlakeController.BlakeState.Running:
                ValuesToAnimate(hor, vert);
                break;
            case BlakeController.BlakeState.Jumping:
                ValuesToAnimate(0, 0);
                break;
            case BlakeController.BlakeState.IdleCrouching:
                ValuesToAnimate(0, 0);
                break;
            case BlakeController.BlakeState.Crouching:
                ValuesToAnimate(hor, vert);
                break;
            case BlakeController.BlakeState.Sliding:
                ValuesToAnimate(0, 0);
                break;
            case BlakeController.BlakeState.AttackingHand:
                ValuesToAnimate(0, 0);
                break;
            case BlakeController.BlakeState.IdleWeapon:
                ValuesToAnimate(0, 0);
                break;
            case BlakeController.BlakeState.AttackingWeapon:
                ValuesToAnimate(0, 0);
                break;
            case BlakeController.BlakeState.WalkingWeapon:
                ValuesToAnimate(hor, vert);
                break;
            case BlakeController.BlakeState.Falling:
                ValuesToAnimate(0, 0);
                break;
            case BlakeController.BlakeState.Landing:
                ValuesToAnimate(0, 0);
                break;
            default:                
                break;
        }
    }

    void ValuesToAnimate(float h, float v)
    {
        blakeAnimator.SetFloat("hor", h);
        blakeAnimator.SetFloat("vert", v);
        blakeAnimator.SetFloat("blakeState", (float)blakeStateToAnim.blakeCurrentState);
    }

    // animações Idle's do Blake
    void AnimationIdle(int idleVariations, float timeToChange)
    {
        if (blakeAnimator.GetInteger("idleSort") == 0) // se a animação atual for a Idle padrão e o personagem estiver no modo Idle
        {
            timerIdle += Time.deltaTime; // soma tempo a variavel timerIdle
            if (timerIdle >= timeToChange)
                sortIdle = Random.Range(1, idleVariations + 1); // sorteia numero da próxima animação
        }
        else // se estiver executando qualquer outra animação que não seja a Idle padrão
        {
            sortIdle = 0; // volta o sorteIdle para 0 (padrão)
            timerIdle = 0; // reseta a contagem de tempo
        }
        blakeAnimator.SetInteger("idleSort", sortIdle); // define o parametro de Idle para o número sorteado         
    }

}
