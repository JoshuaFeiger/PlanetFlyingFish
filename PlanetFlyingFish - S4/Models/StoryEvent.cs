using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;

namespace PlanetFlyingFish.Models
{
	public class StoryEvent
	{
		public StoryEvent(string eventName)
		{
			_eventName = eventName;
		}

		private string _eventName;

		public string EventName
		{
			get { return _eventName; }
			set { _eventName = value; }
		}


		private List<string> _affectedAreaIDs;

		public List<string> AffectedAreaIDs
		{
			get 
            {
                List<string> affectedAreaIDs = new List<string>();
                foreach (EffectAreaInstructions areaEffect in _effectAreas)
                {
                    affectedAreaIDs.Add(areaEffect.AreaID);
                }
                return affectedAreaIDs; 
            }
		}

		private List<EffectAreaInstructions> _effectAreas;

		public List<EffectAreaInstructions> EffectAreas
		{
			get { return _effectAreas; }
			set { _effectAreas = value; }
		}

		private GameActionGroup _effectPlayer;

		public GameActionGroup EffectPlayer
		{
			get { return _effectPlayer; }
			set { _effectPlayer = value; }
		}

	}
	public class EffectAreaInstructions
	{
		private string _areaID;

		public string AreaID
		{
			get { return _areaID; }
			set { _areaID = value; }
		}

		private List<AreaEffect> _areaEffects;

		public List<AreaEffect> AreaEffects
		{
			get { return _areaEffects; }
			set { _areaEffects = value; }
		}

	}

	public class AreaEffect
	{
		public class AddNoun : AreaEffect
		{
			private AnyNoun _nounToAdd;

			public AnyNoun NounToAdd
			{
				get { return _nounToAdd; }
				set { _nounToAdd = value; }
			}
		}

		public class MoveNoun : AreaEffect
		{
			private int _nounToMoveID;

			public int NounToMoveID
			{
				get { return _nounToMoveID; }
				set { _nounToMoveID = value; }
			}

			private string _areaToMoveToID;

			public string AreaToMoveToID
			{
				get { return _areaToMoveToID; }
				set { _areaToMoveToID = value; }
			}
		}

		public class RemoveNoun : AreaEffect
		{
			private int _nounToRemoveID;

			public int NounToRemoveID
			{
				get { return _nounToRemoveID; }
				set { _nounToRemoveID = value; }
			}
		}

		public class UnlockTravel : AreaEffect
		{
			public UnlockTravel(string unlockedAreaID)
			{
				_unlockedAreaID = unlockedAreaID;
			}

			private string _unlockedAreaID;

			public string UnlockedAreaID
			{
				get { return _unlockedAreaID; }
				set { _unlockedAreaID = value; }
			}
		}

		public class EditProperty : AreaEffect
		{
			private int _objectID;

			public int ObjectID
			{
				get { return _objectID; }
				set { _objectID = value; }
			}

			private PropertyInfo _propertyID;

			public PropertyInfo PropertyID
			{
				get { return _propertyID; }
				set { _propertyID = value; }
			}

			private object _valueEdited;

			public object ValueEdited
			{
				get { return _valueEdited; }
				set { _valueEdited = value; }
			}
		}

		public class PerformMethod : AreaEffect
		{
			private int _objectID;

			public int ObjectID
			{
				get { return _objectID; }
				set { _objectID = value; }
			}

			private MethodInfo _methodID;

			public MethodInfo MethodID
			{
				get { return _methodID; }
				set { _methodID = value; }
			}
		}
	}
}
