using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PlanetFlyingFish.Models
{
    public abstract class Dialog : GameAction
    {
        protected string _text;

        private string _nameHeader;
        
        /// <summary>
        /// None of the default MessageText variable. Since Dialogs come in their own separate boxes,
        /// </summary>
        public string Text
        {
            get 
            {
                //The return value is changed here to add the NameHeader if you've set that to anything.
                if (_nameHeader == null)
                {
                    return _text;
                }
                else
                {
                    return ($"{_nameHeader}: \n   \"{_text}\"");
                }
            }
            set { _text = value; }
        }

        
        /// <summary>
        /// If you want to display your dialog as if a character is speaking it, you can just set this parameter!
        /// If it's null, the window just renders its text normally.
        /// </summary>
        public string NameHeader
        {
            get { return _nameHeader; }
            set { _nameHeader = value; }
        }

        /// <summary>
        /// This dialog just displays some simple text.
        /// </summary>
        public class BasicText : Dialog
        {
            public BasicText(string text)
            {
                _text = text;
            }
        }

        /// <summary>
        /// This dialog presents the user with a choice of "yes" or "no".
        /// </summary>
        public class YesOrNo : Dialog
        {
            private (bool yes, bool no) _closeOnYesOrNo;

            private bool _answer;

            private GameActionGroup _actionsOnYes;

            private GameActionGroup _actionsOnNo;

            private (string yesText, string noText) _buttonText = ("Yes", "No");

            /// <summary>
            /// This field/property allows the dialog to delete itself only when a certain option is chosen.
            /// For example, you can have the classic scenario where the character keeps asking you to do something when you approach them,
            /// until you eventually say "yes", at which point that dialog expires.
            /// </summary>
            public (bool yes, bool no) CloseOnYesOrNo
            {
                get { return _closeOnYesOrNo; }
                set { _closeOnYesOrNo = value; }
            }

            public bool Answer
            {
                get { return _answer; }
                set { _answer = value; }
            }

            public GameActionGroup ActionsOnYes
            {
                get { return _actionsOnYes; }
                set { _actionsOnYes = value; }
            }

            public GameActionGroup ActionsOnNo
            {
                get { return _actionsOnNo; }
                set { _actionsOnNo = value; }
            }

            /// <summary>
            /// This just makes it easier to execute the specific instructions based on whether "yes" or "no" was given,
            /// I thought this could be done in the class instead!
            /// </summary>
            public GameActionGroup ActionsToTake
            {
                get
                {
                    GameActionGroup actionsToTake = new GameActionGroup();
                    if (Answer == true)
                    {
                        if (!(_actionsOnYes == null))
                        {
                            actionsToTake = _actionsOnYes;
                        }
                    }
                    else
                    {
                        if (!(_actionsOnNo == null))
                        {
                            actionsToTake = _actionsOnNo;
                        }
                    }
                    return actionsToTake;
                }
            }

            /// <summary>
            /// This field/property allows you to set custom text for the buttons. By default they're "Yes" and "No", of course.
            /// </summary>
            public (string yesText, string noText) ButtonText
            {
                get { return _buttonText; }
                set { _buttonText = value; }
            }
        }
    }
}
