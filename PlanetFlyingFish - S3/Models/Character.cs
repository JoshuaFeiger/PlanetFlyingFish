﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanetFlyingFish.Models;

namespace PlanetFlyingFish.Models
{
    public class Character : AnyNoun
    {
        public enum Emotion
        {
            None,
            Happy,
            Hopeful,
            Energized,
            Ready,
            Excited,
            Sad,
            Angry,
            Lonely,
            Scared,
            Frustrated,
            Depressed,
            Rejected,
            Tired
        }

        public Character()
        {

        }

        public Character(int id, string name, int healthPoints)
        {
            _id = id;
            _name = name;
            _healthPoints = healthPoints;
        }

        public override void Battle(Player player)
        {
            
        }

        public override void DefaultInteract()
        {
            //Something something "{_name} doesn't respond. Maybe something's wrong..."
        }

        protected Emotion _emotionalState;

        public Emotion EmotionalState
        {
            get { return _emotionalState; }
            set { _emotionalState = value; }
        }

    }
}
