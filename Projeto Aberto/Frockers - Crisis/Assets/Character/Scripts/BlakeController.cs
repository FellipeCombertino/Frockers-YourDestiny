using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlakeController : MonoBehaviour
{

    [Range(5, 50)]
    public int blakeSpeed;
    int originalSpeed, speedOnAir;

    [Range(1, 10)]
    public int blakeJumpForce;

    Rigidbody blakeRB;

    Vector3 movement;
    //                         0       1        2      3          4           5           6         7             8              9               10            11       12
    public enum BlakeState { Idle, Walking, Running, Jumping, IdleCrouching, Crouching, Sliding, AttackingHand, IdleWeapon, AttackingWeapon, WalkingWeapon, Falling, Landing }
    public BlakeState blakeCurrentState;
    bool usingWeapon, foundPlaceToLand;

    CapsuleCollider blakeCollider;
    public LayerMask groundLayer;
    public float timeOnAir;

    void Start()
    {
        blakeRB = GetComponent<Rigidbody>();
        blakeCollider = GetComponent<CapsuleCollider>();
        originalSpeed = blakeSpeed;
    }

    void Update()
    {        
        BlakeMovement();
    }   

    // para movimentar o rigidbody em fixedDeltaTime
    private void FixedUpdate()
    {        
        blakeRB.MovePosition(blakeRB.position + movement * blakeSpeed * Time.fixedDeltaTime);
        BlakeJumpUp();        
    }

    // controle de movimentos do Blake (andar, correr) -> Update
    void BlakeMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        BlakeWalking(hor, vert);
        BlakeRunning();
        BlakeCrouching();
        BlakeChangeWeapon();
        BlakeOnMiddleAir();

        ManageCharacterStates(hor, vert);
    }
    
    void ManageCharacterStates(float h, float v)
    {

        //se estiver no chão
        if (IsGrounded()) 
        {
            if (h == 0 && v == 0) // se estiver parado
            {
                if (blakeSpeed == originalSpeed) // caso esteja no speed original
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (!usingWeapon)
                            blakeCurrentState = BlakeState.AttackingHand;
                        else
                            blakeCurrentState = BlakeState.AttackingWeapon;
                        Invoke("BlakeStopAttacking", 1);
                    }                    
                    else if (blakeCurrentState != BlakeState.AttackingHand && blakeCurrentState != BlakeState.AttackingWeapon)
                    {
                        if (usingWeapon)
                        {
                            blakeCurrentState = BlakeState.IdleWeapon;
                        }
                        else
                        {
                            blakeCurrentState = BlakeState.Idle;
                        }
                    }
                }
                else if (blakeSpeed < originalSpeed) // caso esteja abaixo do speed original
                    blakeCurrentState = BlakeState.IdleCrouching;
            }
            else if (h != 0 || v != 0) // se estiver se mexendo
            { 
                if (blakeSpeed == originalSpeed) // caso esteja no speed original
                {
                    if (usingWeapon)
                    {
                        blakeCurrentState = BlakeState.WalkingWeapon;
                    }
                    else
                    {
                        blakeCurrentState = BlakeState.Walking; // está andando
                    }
                }
                else if (blakeSpeed > originalSpeed) // caso esteja acima do speed original
                {
                    if (Input.GetKeyDown(KeyCode.C) && v > 0)
                    {
                        BlakeSliding(); // transforma o blakeState em Sliding
                        Invoke("BlakeStopSliding", 0.5f); // transforma o blakeState em Idle
                    }
                    if (blakeCurrentState != BlakeState.Sliding)
                    {
                        blakeCurrentState = BlakeState.Running;
                    }
                }
                else if (blakeSpeed < originalSpeed) // caso esteja abaixo do speed original
                {
                    blakeCurrentState = BlakeState.Crouching; // está agachando
                }
            }
        } // se estiver no chão ^
        else // se estiver no ar
        {            
            if (blakeRB.velocity.y > 0) // se estiver indo pra cima
            {
                blakeCurrentState = BlakeState.Jumping;
            }
            else if (!foundPlaceToLand) // se estiver indo pra baixo e não achou um lugar para pousar
                blakeCurrentState = BlakeState.Falling;
            else // se achou um lugar para pousar
                blakeCurrentState = BlakeState.Landing;
        }

    }

    // booleana para checar se o Blake está tocando no chão
    private bool IsGrounded()
    {
        return Physics.CheckCapsule(blakeCollider.bounds.center, new Vector3(blakeCollider.bounds.center.x, blakeCollider.bounds.min.y, blakeCollider.bounds.center.z), blakeCollider.radius * 0.9f, groundLayer);
    }

    // deslocar o Blake com rigidbody -> BlakeMovement
    void BlakeWalking(float h, float v)
    {
        movement = new Vector3(h, 0, v); // atribui os inputs ao vector3
        movement = Camera.main.transform.TransformDirection(movement); // transforma o vector3 p/ o world space com base na camera
        movement = movement.normalized; // retorna o vector com magnitude de 1
        movement.y = 0; // fixa o y em 0 para não andar pra cima
    }    

    // fazer o Blake correr -> BlakeMovement
    void BlakeRunning ()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && blakeCurrentState == BlakeState.Walking) // apertei shift e estou andando
            blakeSpeed = blakeSpeed * 2; // eu dobro a vel para correr
        else if (Input.GetKeyUp(KeyCode.LeftShift) && (blakeCurrentState == BlakeState.Running || blakeCurrentState == BlakeState.Sliding)) // soltei shift e estou correndo
            blakeSpeed = blakeSpeed / 2; // eu volto para vel original
    }

    // fazer o Blake agachar -> BlakeMovement
    void BlakeCrouching()
    {
        if (Input.GetKeyDown (KeyCode.C) && IsGrounded() && !usingWeapon) // apertei c e estou no chão sem arma
        {
            if (blakeSpeed == originalSpeed) // se estiver andando ou parado
            {
                blakeSpeed = blakeSpeed / 2; // diminuo a velocidade para agachar
                if (blakeCollider.height > 4) // se a altura do personagem for maior que 4
                {
                    blakeCollider.height /= 2; // diminuo o tamanho do collider
                    blakeCollider.center = new Vector3(blakeCollider.center.x, blakeCollider.center.y / 2, blakeCollider.center.z); // diminuo o centro do collider
                }
            }
            else if (blakeSpeed < originalSpeed) // se estiver agachado
            {
                blakeSpeed = originalSpeed; // volto para velocidade normal
                if (blakeCollider.height < 4) // se a altura do personagem for menor que 4
                {
                    blakeCollider.height *= 2; // volto o tamanho do collider
                    blakeCollider.center = new Vector3(blakeCollider.center.x, blakeCollider.center.y * 2, blakeCollider.center.z); // volto o centro do collider
                }
            }
        }
    }
    
    // fazer o blake Pular -> FixedUpdate
    void BlakeJumpUp()
    {
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            speedOnAir = blakeSpeed / 2;
            if ((blakeSpeed == originalSpeed || blakeSpeed > originalSpeed) && ((float)blakeCurrentState < 3)) // se estiver andando ou correndo e seu estado for idle, andando ou correndo
            {
                blakeRB.AddForce(Vector3.up * blakeJumpForce, ForceMode.Impulse); 
            }
            else if (blakeSpeed < originalSpeed) // se estiver agachado
            {
                blakeSpeed = originalSpeed; // volto para velocidade normal
                if (blakeCollider.height < 4) // se o seu collider estiver abaixo da altura original
                {
                    blakeCollider.height *= 2; // volto o tamanho do collider
                    blakeCollider.center = new Vector3(blakeCollider.center.x, blakeCollider.center.y * 2, blakeCollider.center.z); // volto o centro do collider
                }
            }            
        }
    }

    // fazer o Blake perder speed enquanto estiver no ar -> BlakeMovement
    void BlakeOnMiddleAir()
    {        
        if (!IsGrounded())
        {
            timeOnAir += Time.deltaTime;
            if (blakeRB.velocity.y < 0)
            {
                Collider[] jumpCheck = Physics.OverlapSphere(gameObject.transform.position - Vector3.up * 0.8f, 0.5f); // isso está sendo desenhado no método OnDrawGizmos            
                for (int i = 0; i < jumpCheck.Length; i++)
                {
                    if (!foundPlaceToLand)
                    {
                        if (jumpCheck[i].gameObject.layer == 9 && timeOnAir > 1) // se encontrar um collider com a layer de ground e estiver no ar ao menos por + de 1s
                        {
                            foundPlaceToLand = true; // achou um lugar para pousar
                        }
                    }                    
                }
            }
            if (!foundPlaceToLand)
            {
                if (speedOnAir != 0)
                    blakeSpeed = speedOnAir;
                else
                    blakeSpeed = originalSpeed;                
            }
            else
            {
                if (speedOnAir != 0)
                    blakeSpeed = speedOnAir * 2;
                else
                    blakeSpeed = originalSpeed;
            }            
        }
    }

    // faz com que o tempo no ar resete todavez que ele entre em contato com o chão
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            timeOnAir = 0;
        }
    }

    // faz com que a variável foundPlaceToLand seja falsa toda vez que o Blake sair do chão
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            foundPlaceToLand = false;
        }
    }

    // desenhar no editor a esfera de overlap que está sendo criada no método BlakeOnMiddleAir
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - Vector3.up * 0.8f, 0.5f);
    }

    // fazer Blake começar a deslizar -> ManageCharacterStates
    void BlakeSliding()
    {
        blakeCurrentState = BlakeState.Sliding;
        blakeCollider.height /= 2; // diminuo o tamanho do collider
        blakeCollider.center = new Vector3(blakeCollider.center.x, blakeCollider.center.y / 2, blakeCollider.center.z); // diminuo o centro do collider
    }

    // fazer o Blake parar de deslizar -> ManageCharacterStates
    void BlakeStopSliding()
    {
        blakeCurrentState = BlakeState.Idle;
        blakeCollider.height *= 2; // volto o tamanho do collider
        blakeCollider.center = new Vector3(blakeCollider.center.x, blakeCollider.center.y * 2, blakeCollider.center.z); // volto o centro do collider
    }

    // fazer o Blake parar de atacar -> ManageCharacterStates
    void BlakeStopAttacking()
    {
        blakeCurrentState = BlakeState.Idle;
    } 

    // fazer o Blake pegar ou guardar a arma -> BlakeMovement
    void BlakeChangeWeapon()
    {
        if (Input.mouseScrollDelta.y > 0 && blakeCurrentState == BlakeState.Idle)
        {
            usingWeapon = true;
        }
        else if (Input.mouseScrollDelta.y <0 && blakeCurrentState == BlakeState.IdleWeapon)
        {
            usingWeapon = false;
        }
    }
       
}
