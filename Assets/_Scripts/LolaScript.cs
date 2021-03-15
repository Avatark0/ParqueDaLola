using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LolaScript : MonoBehaviour
{
    //Componentes
    private Camera cam;
    private Rigidbody corpoRigido;
    //Variáveis
    private bool tocandoSuperficie; //Colisão com chão
    private bool movimentando;      //pressionando os botões AWSD 
    private bool temPulo;           //Carga do pulo
    private bool pausaNosControles; //Tira o controle do usuário, para permitir animação e controle da câmera

    public static int pontos;
    //Controles
    public float velRot = 88f;      //Velocidade de rotação
    public float velChao = 36f;     //Velocidade linear no chao
    public float velAr = 18f;       //Velocidade linear no ar
    public float alturaPulo = 9f;   //Altura do pulo
    private float maxAngularSpeedAndando = 8f;  //Velocidade de rotação máxima normal
    private float maxLinearSpeedAr = 12f;       //Velocidade de movimentação no ar

    public float MovDirAr=0.09f; //Controla quando da aceleração linear pode ser orientada para cima/baixo com a camêra. Caso a força obtida na vertical seja maior que a gravidade, permite que a Lola voe.
    
    void Start(){
        cam = Camera.main;
        corpoRigido = GetComponent<Rigidbody>();
        corpoRigido.maxAngularVelocity=maxAngularSpeedAndando;//Inicia a velocidade angular limite andando.
        pausaNosControles=false;
        pontos=0;
    }

    void Update(){
        if(!pausaNosControles)movimentacao();
    }

    void OnCollisionEnter(Collision col){
        tocandoSuperficie=true;
        if(!temPulo)temPulo=true;
    }

    void OnCollisionExit(Collision col){
        tocandoSuperficie=false;
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag=="Pessego")Pessego(col.gameObject);
        else if(col.gameObject.tag=="PessegoGig")PessegoGig(col.gameObject);
        if(col.gameObject.tag=="Arco"){
            Debug.Log("Entrou!");
        }
    }

    void OnTriggerExit(Collider col){
        if(col.gameObject.tag=="Arco"){
            Debug.Log("Saiu!");
        }
    }

    private void movimentacao(){
        //Controles
        Vector3 transR = cam.transform.right;
        transR.y=0;
        transR = transR.normalized;
        Vector3 transF = cam.transform.forward;
        transF.y=0;
        transF = transF.normalized;
        Vector3 transRlinear = cam.transform.right;
        if(transRlinear.y>MovDirAr)transRlinear.y=MovDirAr;
        else if(transRlinear.y<0)transRlinear.y=0;
        transRlinear = transRlinear.normalized;
        Vector3 transFlinear = cam.transform.forward;
        if(transFlinear.y>MovDirAr)transFlinear.y=MovDirAr;
        //else if(transFlinear.y<0)transFlinear.y=0;
        transFlinear = transFlinear.normalized;

        //Andar - AWSD
        movimentando=false;
        if (Input.GetKey(KeyCode.W)){
            corpoRigido.angularVelocity += transR * velRot * Time.deltaTime;
            movimentando=true;
        }
        if (Input.GetKey(KeyCode.S)){
            corpoRigido.angularVelocity -= transR * velRot * Time.deltaTime;
            movimentando=true;
        }
        if (Input.GetKey(KeyCode.A)){
            corpoRigido.angularVelocity += transF * velRot * Time.deltaTime;
            movimentando=true;
        }
        if (Input.GetKey(KeyCode.D)){
            corpoRigido.angularVelocity -= transF * velRot * Time.deltaTime;
            movimentando=true;
        }

        //Pulo - Space
        if(Input.GetKey(KeyCode.Space) && temPulo){
            temPulo=false;
            if(tocandoSuperficie)corpoRigido.velocity += Vector3.up * alturaPulo;
            else corpoRigido.velocity += Vector3.up * alturaPulo*0.5f;
        }

        //Aceleração - Ativado quando em colisão
        //Adiciona velociade Linear ao personagem no sentido de seu movimento
        if(tocandoSuperficie){
            if (Input.GetKey(KeyCode.W)){
                corpoRigido.velocity += transFlinear * velChao * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S)){
                corpoRigido.velocity -= transFlinear * velChao * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D)){
                corpoRigido.velocity += transRlinear * velChao * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A)){
                corpoRigido.velocity -= transRlinear * velChao * Time.deltaTime;
            }
        }

        //Movimentação no ar - Ativado quando não está em colisão.
        //Adiciona velociade Linear ao personagem no sentido de seu movimento
        //Ajusta diretamente a velocidade do objeto, permitindo o controle aéreo.
        if(!tocandoSuperficie && corpoRigido.velocity.magnitude<maxLinearSpeedAr){
            if (Input.GetKey(KeyCode.W)){
                corpoRigido.velocity += transFlinear * velAr * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S)){
                //corpoRigido.velocity -= transFlinear * velAr * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D)){
                corpoRigido.velocity += transRlinear * velAr * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A)){
                corpoRigido.velocity -= transRlinear * velAr * Time.deltaTime;
            }
        }

        //Arraste - Controle automático
        //O arraste é zerado no ar para dar mais controle ao jogador e permitir colisões perfeitamente elásticas.
        //No chão é o arraste que permite a mudança rápida de direção, quebrando o efeito da inércia.
        //O arraste é mantido em zero ao segurar o LeftShift (controle de elasticidade).
        if(tocandoSuperficie){
                corpoRigido.drag=3;
                corpoRigido.angularDrag=2f;
            }
        else{
            corpoRigido.drag=0f;
            corpoRigido.angularDrag=0f;
        }

        //Elasticidade - LeftShift
        //Pode ser auterada entre 0.2 e 0.96. 
        //Em max permite que o jogador kique pelo mundo conservando energia nas colisões.
        //Em min garante perda de energia nas colisões e melhor aderência nas superfícies.
        //O arraste é mantido em 0 durante o pressionar da skill.
        if(Input.GetKey(KeyCode.LeftShift)){
            Resources.Load<PhysicMaterial>("Materials/LolaMaterial").bounciness=0.90f;
            corpoRigido.drag=0f;
            corpoRigido.angularDrag=0f;
        }
        else{
            Resources.Load<PhysicMaterial>("Materials/LolaMaterial").bounciness=0.2f;
        }
    }

    private void Pessego(GameObject instancia){
        Destroy(instancia);
        pontos+=5;
    }

    private void PessegoGig(GameObject instancia){
        Destroy(instancia);
        pontos+=50;
    }

    private void Arco(){
        
    }

    public void PausaNosControles(bool pausar){
        if(pausar)pausaNosControles=true;
        else pausaNosControles=false;
    }
}
