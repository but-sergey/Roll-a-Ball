﻿using UnityEngine;

namespace Model
{
    public interface IPlayerModel
    {
        public GameObject Prefab { get; }
        public float Speed { get; }
        public Vector3 Position { get; }
        public string Name { get; }

    }
}
