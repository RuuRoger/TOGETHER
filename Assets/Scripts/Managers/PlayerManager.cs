using System;
using System.Collections.Generic;
using Assets.Scripts.Players;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        // =================================== FIELDS ===================================
        private BasePlayer m_human;
        private BasePlayer m_dog;

        public void PlayersInitialation()
        {
            GameObject humanObject = GameObject.FindGameObjectWithTag("Player");
            GameObject dogObject = GameObject.FindGameObjectWithTag("DogPlayer");
            m_human = humanObject.GetComponent<BasePlayer>();
            m_dog = dogObject.GetComponent<BasePlayer>();
        }
    }
}