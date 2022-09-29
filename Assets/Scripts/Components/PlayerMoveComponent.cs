using UnityEngine;
namespace Client
{
    struct PlayerMoveComponent
    {
        public Vector3 touchMoveVector;
        public Vector3 moveVector;
        public float speed;
        public bool Backwards;
        public Animation anim;
        public AnimationClip jumpAnim;
    }
}

