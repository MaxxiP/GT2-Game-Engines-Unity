﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public string charName;
    [SerializeField] private GameObject spell;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Camera cam;
    private Movement_Controller _movementController;
    public CharData_Controller _charDataController;
    private Animator _animator;
    public bool isLoading;
    //public LoadingScreen _LoadingScreen;

    public void Init(){
        _charDataController = new CharData_Controller(charName);
        _movementController = new Movement_Controller(gameObject.GetComponent<Rigidbody2D>());
        _animator = GetComponent<Animator>();
        spell.GetComponent<BaseSpell>().Init();
        FindObjectOfType<UIManager>().SetMaxHP(_charDataController._CharData.MAXHp);
    }

    // Update is called once per frame
    void Update()
    {
        isLoading = LoadingScreen.isLoading;
        //Movement
        if (!isLoading)
        {
            _movementController.move(_charDataController._CharData.MoveSpeed, cam);
            _animator.SetBool("isWalking", _movementController.isWalking);

            //Shooting
            if (Input.GetButtonDown("Fire1"))
            {
                _animator.SetBool("isShooting", true);
                spell.GetComponent<BaseSpell>().Shoot(firePoint);
            }
            else
            {
                _animator.SetBool("isShooting", false);
            }
        }
    }
}
