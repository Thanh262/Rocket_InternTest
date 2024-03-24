using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRb;

    public GameObject view ;

    public Material playerMaterial;
    private Color ballColor;
    public GameManager gameManager;
    public float moveSpeed = 600f;

    public float turnSpeed = 50f;
    public float horizontalInput ;
    public float verticalInput ;

    //public List<string> missingBall ;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerMaterial = GetComponent<MeshRenderer>().material;

        view = GameObject.Find("View");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput   = Input.GetAxis("Vertical");
        
        view.transform.Rotate(Vector3.up, horizontalInput * Time.deltaTime * turnSpeed);
        playerRb.AddForce(view.transform.forward * verticalInput * Time.deltaTime * moveSpeed );
        
    }

    private void OnCollisionEnter(Collision other) {
        if( gameManager.isGameActive && gameManager.win == false){
        if ( playerMaterial.color == other.gameObject.GetComponent<MeshRenderer>().material.color ){
            
            NewBallColor();       

            KeepScore(); 

            CreateNewBall(other); 

            Destroy(other.gameObject); 
   
        }else if(other.gameObject.tag == "Platform") {
            
        }else{
            gameManager.isGameActive = false;
            gameManager.GameOver();
        }
    }}

    
    Color GetRandomColor(){
        Color[] colors = {Color.red, Color.green, Color.blue};

        return colors[Random.Range( 0, colors.Length )];
    }
     
    public void NewBallColor(){
        do{
             ballColor = GetRandomColor();
        }while(ballColor == playerMaterial.color);
        playerMaterial.color = ballColor;
    }

    public void KeepScore(){
        gameManager.UpdateScore(1);         
    }

    public void CreateNewBall(Collision other){
        gameManager.ballRespawn.Add(other.gameObject.tag);
        gameManager.Look4ball();
    }
}
