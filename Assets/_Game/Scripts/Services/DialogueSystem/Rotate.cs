using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Services.DialogueSystem;
using _Game.Scripts.Utils.MyBox.Extensions;
using Fluent;
using UnityEngine;
using UnityEngine.UIElements;

public class Rotate : MonoBehaviour
{
    public float velocity;
    public float distanceMin;
    public float distanceMax;
    public GameObject otherTalk;
    public GameObject playerColliderInteraction;

    Camera camera;
    SpriteRenderer m_SpriteRenderer;
    Collider talkCollider;
    private bool add = true;
    private bool collided = false;

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        talkCollider = GetComponent<Collider>();
        camera = Camera.main;
    }

    void Update()
    {
        AnimateIcon();

        if (FluentManager.Instance.FluentScripts.Count > 0 && collided)
        {
            m_SpriteRenderer.sprite = null;
        }
        else if (FluentManager.Instance.FluentScripts.Count == 0 && collided)
        {
            m_SpriteRenderer.sprite = Resources.Load<Sprite>("Images/icon_talk_close");
        }
    }

    void LateUpdate()
    {
        if (camera != null)
        {
            transform.LookAt(camera.transform);

            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }
    }

    private void AnimateIcon()
    {
        float currentY = transform.position.y;
        if (add)
        {
            if (currentY >= distanceMax)
            {
                add = false;
            }

            transform.Translate(new Vector2(0f, velocity) * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector2(0f, -velocity) * Time.deltaTime);
            add = currentY <= distanceMin;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (playerColliderInteraction != collider.gameObject)
        {
            return;
        }

        if (talkCollider is SphereCollider)
        {
            m_SpriteRenderer.sprite = Resources.Load<Sprite>("Images/icon_talk");
        }

        if (talkCollider is BoxCollider)
        {
            collided = true;
            if (otherTalk != null)
            {
                otherTalk.GetComponent<SpriteRenderer>().sprite = null;
            }
            m_SpriteRenderer.sprite = Resources.Load<Sprite>("Images/icon_talk_close");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (playerColliderInteraction != collider.gameObject)
        {
            return;
        }

        if (talkCollider is BoxCollider)
        {
            collided = false;

            if (otherTalk != null)
            {
                otherTalk.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/icon_talk");
            }
            m_SpriteRenderer.sprite = Resources.Load<Sprite>("Images/icon_talk_close");
        }
        m_SpriteRenderer.sprite = null;
    }
}
