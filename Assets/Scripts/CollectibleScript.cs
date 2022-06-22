using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CollectibleScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Inventory inv;
    private bool collected = false;
    public int objectType;
    public Sequence collSeq;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerMovement>();

        collSeq = DOTween.Sequence();


        collSeq.Append(transform.DOScale(1.1f, 0.75f)).Append(transform.DOScale(0, 0.75f)).OnComplete(deactivateObj);
        collSeq.Pause();
        collSeq.SetAutoKill(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerAttackBox")
        {
            if (!collected)
            {

                transform.parent = player.transform;
                transform.localPosition = new Vector3(0, 2, 0);
                collSeq.Restart();
                collected = true;
                inv.addToInventory(objectType);
                
            }
            
        }
        
    }

    public void deactivateObj()
    {
        gameObject.SetActive(false);
    }
    
}