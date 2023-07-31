using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureSwitcher2 : MonoBehaviour
{
    [SerializeField] private Texture2D _texture;
    private Material _material;
    private int _id;
    
    void Start()
    {
        Invoke(nameof(SwitchTexture),.1f);
    }


    private void SwitchTexture(){
        var collection = transform.parent.parent.parent.GetComponent<CharacterCollection2>();
        _id = collection.UserId;
        if(collection.UserId == 0)return;
        _material = GetComponent<SkinnedMeshRenderer>().material;
        _material.SetTexture("_MainTex",_texture);
    }

    
}
