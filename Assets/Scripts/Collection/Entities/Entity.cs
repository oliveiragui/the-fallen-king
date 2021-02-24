using System;
using System.Collections;
using Collection.Entities.Animation;
using Collection.Entities.Audio;
using Collection.Entities.Mesh;
using Collection.Entities.Particle;
using Collection.Entities.Physics;
using Components.Move;
using UnityEngine;
using Utils.Extension;

namespace Collection.Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] new EntityAnimation animation;
        [SerializeField] new EntityAudio audio;
        [SerializeField] EntityMesh mesh;
        [SerializeField] EntityParticle particle;
        [SerializeField] EntityPhysics physics;
        [SerializeField] EntityMove movement;

        void Start()
        {
            movement.Speed = 1;
            movement.MoveTo(new Vector3(0, 0, 0));
        }

        void Update()
        {
            ProcessaInput();
        }

        void ProcessaInput()
        {
            var speed = new Vector2(Input.GetAxisRaw("P1KHorizontal"), Input.GetAxisRaw("P1KVertical"));
            if (speed.magnitude > 0.1)
            {
                movement.Speed = 7;
                movement.Move(speed.ToDegree() + 45);
                animation.Run(1);
            }
            else
            {
                animation.StopRun();
                movement.Stop();
            }

            if (Input.GetButtonDown("P1KAtaque1")) animation.UsaHabilidade(1, true, 1);
            if (Input.GetButtonUp("P1KAtaque1")) animation.ParaDeConjurar();
            
            if (Input.GetButtonDown("P1KAtaque2")) animation.UsaHabilidade(2, false, 2);
        }
    }
}