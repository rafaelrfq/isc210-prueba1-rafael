using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliciaNPC : MonoBehaviour
{
    float _speed = 1f;
    float _direccion = 1;
    float _fuerza = 100f;
    int vida = 100;
    Vector3 _deltaPos = new Vector3();
    Animator currentAnimator;
    SpriteRenderer _renderer;
    Time time;

    public GameObject monedaPrefab;

    private void Awake()
    {
        currentAnimator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_deltaPos.x != 0)
        {
            currentAnimator.SetBool("isWalking", true);
        }
        else
        {
            currentAnimator.SetBool("isWalking", false);
        }
        _renderer.flipX = _direccion == 1;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        _direccion *= -1;
        _deltaPos.x = (_speed*2) * Time.deltaTime * _direccion;
        gameObject.transform.Translate(_deltaPos);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        GameObject objeto = collision.gameObject;

        _deltaPos.x = _speed * Time.deltaTime * _direccion;
        float posicionRelativaFinal = gameObject.transform.position.x + (gameObject.transform.localScale.x / 2);
        float longitudSueloFinal = (objeto.transform.localScale.x / 2) + objeto.transform.position.x;
        float posicionRelativaInicial = gameObject.transform.position.x - (gameObject.transform.localScale.x / 2);
        float longitudSueloInicial = objeto.transform.position.x - (objeto.transform.localScale.x / 2);

        if (posicionRelativaFinal >= longitudSueloFinal) {
                _direccion = -1;
        }
        if (posicionRelativaInicial <= longitudSueloInicial) {
            _direccion = 1;
        }


        gameObject.transform.Translate(_deltaPos);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    public void hacerDano(int dano)
    {
        vida -= dano;
        if (vida <= 0)
        {
            Instantiate(monedaPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}

