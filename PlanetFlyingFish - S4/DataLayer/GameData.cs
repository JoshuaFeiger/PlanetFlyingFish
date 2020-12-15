using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlanetFlyingFish.Models;

namespace PlanetFlyingFish.DataLayer
{
    public class GameData
    {
        //----------------------------------------------
        #region ----------------------------MAIN

        public static List<string> PriorityMessageData()
        {
            return DefaultPriorityMessages();
        }

        public static List<string> SideMessageData()
        {
            return DefaultSideMessages();
        }

        public static Player PlayerData()
        {
            return DefaultPlayerData();
        }

        public static List<Area> AreasData()
        {

            return DefaultAreas();
        }

        #endregion
        //----------------------------------------------

        //----------------------------------------------
        #region ----------------------------DEFAULTS

        public static Player DefaultPlayerData()
        {
            return new Player()
            {
                //inital player properties
                ID = 6384,
                Name = "Unit #6384",
                AreaID = "SurfRoom001",
                HealthPoints = 100,
                MaxHP = 150,
                StoryTriggers = new List<string>()
                {
                    "Default"
                },
                ItemInventory = new System.Collections.ObjectModel.ObservableCollection<AnyNoun>()
            };
        }

        public static List<string> DefaultPriorityMessages()
        {
            return new List<string>
            {
                "Hello.",
                "These are the default priority messages.",
                "And here is a very long message so that you can feel less lonely in this bare-bones first test of the engine we're using to make this subpar game. Thank you for trying it, by the way. And thank you for reading this message. It means a lot to me that someone would do something like that, and that someone would read this message, especially considering that you have no idea when I'm going to stop talking.",
                "",
                "...Are you actually interested in this text, or do you just want to see where it ends? Are you paying attention to me at all? Is anyone? If no one is, is there a reason I should keep persevering at the things I do? Should I do them just to say I did them? Should I do them because they're something I always dreamed to do? If that's such a good reason, why does my dream pursuit always seem to go so poorly...",
                "I don't know anymore... Why should I keep going? If you've scrolled down this far, you're probably just making sure scrolling works right. Well, it does. No need to keep going down. Unless you're trying to find the bottom... \nWait... \nYou're not listening anyway...",
                "*sigh*",
                "Well, I guess it's time to finish writing this and move on... if there's any reason to...",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "...",
                "...I'm sure I'll find it."
            };
        }

        public static List<string> DefaultSideMessages()
        {
            return new List<string>
            {
                "Hello.",
                "These are the initial side-messages.",
                "",
                "",
                "",
                "",
                "",
                "",
                "...",
                "Is there anything more you're expecting?"
            };
        }

        public static List<Area> DefaultAreas()
        {
            List<Area> defaultAreas = new List<Area>
            {
                new Area
                {
                    AreaID = "SurfRoom001",
                    AreaName = "Surface Room 001",
                    ConnectedAreas = new List<string>{ "SurfRoom002" },
                    LocationInfo = "You woke up in this cold, dark room.",
                    ArtName = "SurfaceRoomArt01",
                    Nouns = new List<AnyNoun>
                    {
                        new AnyObject
                        {
                            Name = $"A small black slab",
                            CanPickUp = true,
                            HealthPoints = 10,
                            ID = 1905,
                            Description = "It has a blinking light, but it won't respond when talked to. Strange.",
                            ActionsOnInteract = new List<GameActionGroup>
                            {
                                new GameActionGroup
                                {
                                    StoryTriggerID = "Default",
                                    ActionsToPerform = new List<GameAction>()
                                    {
                                        new GameAction.ItemGet(
                                        new AnyObject
                                        {
                                            Name = $"Vinyl plastic yellow bird",
                                            CanPickUp = true,
                                            HealthPoints = 10,
                                            ID = 948,
                                            Description = "It is wet for some reason."
                                        }){ ExpireOnCompleted = true }
                                    }
                                }
                            },

                            ActionsOnUse = new List<GameActionGroup>
                            {
                                new GameActionGroup()
                                {
                                    StoryTriggerID = "Default",
                                    ActionsToPerform = new List<GameAction>()
                                    {
                                        new GameAction.HPUp(10, "You find a battery inside, and drain its power.", true){ ExpireOnCompleted = true }
                                    }
                                }
                            }
                        },
                        new AnyObject
                        {
                            Name = $"Twisted metal",
                            CanPickUp = true,
                            HealthPoints = 10,
                            ID = 2634,
                            Description = "Seems to be used to hold papers together.",
                            ActionsOnInteract = new List<GameActionGroup>()
                            {
                                new GameActionGroup()
                                {
                                    StoryTriggerID = "Default",
                                    ActionsToPerform = new List<GameAction>()
                                    {
                                        new GameAction.ItemGet
                                        (new AnyObject
                                        {
                                            Name = $"Papers",
                                            CanPickUp = true,
                                            HealthPoints = 10,
                                            ID = 1239,
                                            Description = "They contain some data about the planet's atmosphere."
                                        }){ ExpireOnCompleted = true }
                                    }
                                }
                            }
                        },
                        new AnyObject
                        {
                            Name = "Switch",
                            ID = 258,
                            CanPickUp = false,
                            HealthPoints = 500,
                            MaxHP = 510,
                            Description = "NO_WINDOW",
                            ActionsOnInteract = new List<GameActionGroup>
                            {
                                new GameActionGroup()
                                {
                                    ActionsToPerform = new List<GameAction>
                                    {
                                        new Dialog.BasicText("You pushed the switch, and apparently something happened."),
                                        new GameAction.TriggerStoryEvent("OpenWarpToSR004_1"){ ExpireOnCompleted = true }
                                    }
                                }
                            }
                        }
                    }
                },
                new Area
                {
                    AreaID = "SurfRoom002",
                    AreaName = "Surface Room 002",
                    ConnectedAreas = new List<string>{ "SurfRoom003", "SurfRoom001" },
                    LocationInfo = "Some room next to the cold, dark room you woke up in.",
                    ArtName = "SurfaceRoomArt01",
                    Nouns = new List<AnyNoun>
                    {
                        new AnyObject
                        {
                            Name = $"Vinyl plastic yellow bird",
                            CanPickUp = true,
                            HealthPoints = 10,
                            ID = 947,
                            Description = "It is wet for some reason.",
                            ActionsOnUse = new List<GameActionGroup>()
                            {
                                new GameActionGroup()
                                {
                                    StoryTriggerID = "Default",
                                    ActionsToPerform = new List<GameAction>
                                    {
                                        new GameAction.MaxHPUp(10){ ExpireOnCompleted = true }
                                    }
                                }
                            }
                        }
                    }
                },
                new Area
                {
                    AreaID = "SurfRoom003",
                    AreaName = "Surface Room 003",
                    ConnectedAreas = new List<string>{ "SurfRoom001", "SurfRoom002", "Quack" },
                    LocationInfo = "A duck-shaped room. (Don't ask why.)",
                    ArtName = "SurfaceRoomArt02",
                    Nouns = new List<AnyNoun>
                    {
                        new AnyObject
                        {
                            Name = $"Crate",
                            CanPickUp = false,
                            HealthPoints = 10,
                            ID = 100,
                            Description = "A crate. It just... sits here. It's too heavy to pick it up."
                        },
                        new AnyObject
                        {
                            Name = "Door",
                            ID = 989,
                            Description = "You go through the door, and find yourself back in the first room.",
                            ViewInfoRestricted = true,
                            ActionsOnInteract = new List<GameActionGroup>
                            {
                                new GameActionGroup()
                                {
                                    StoryTriggerID = "Default",
                                    ActionsToPerform = new List<GameAction>
                                    {
                                        new GameAction.ForceTravel("SurfRoom001")
                                    }
                                }
                            }
                        },
                        new NPC
                        {
                            Name = "Unit #3754 \"Vacchan\"",
                            CanPickUp = false,
                            HealthPoints = 190,
                            ID = 3754,
                            Description = "NO_WINDOW",
                            ItemInventory = new System.Collections.ObjectModel.ObservableCollection<AnyNoun>
                            {
                                new AnyObject()
                                {
                                    Name = "Lime Key",
                                    ID = 624,
                                    Description = "A lime-colored key. You don't know what it's for.",
                                    HealthPoints = 100,
                                    MaxHP = 99,
                                    CanPickUp = true
                                }
                            },
                            ActionsOnInteract = new List<GameActionGroup>
                            {
                                new GameActionGroup()
                                {
                                    StoryTriggerID = "FindVacuumQuest",
                                    ActionsToPerform = new List<GameAction>
                                    {
                                        new GameAction.PerformActionsIf.PlayerOneHasItem(307)
                                        {
                                            CloseOnTrueOrFalse = (false, false),
                                            ActionsOnTrue = new GameActionGroup()
                                            {
                                                ActionsToPerform = new List<GameAction>
                                                {
                                                    new Dialog.YesOrNo()
                                                    {
                                                        CloseOnYesOrNo = (false, false),
                                                        NameHeader = "Vacchan",
                                                        Text = "Hey, you found my vacuum!\nCan I have it back, please? Here, I'll trade you this... Lime Key!",
                                                        ButtonText = ("Yes", "No way!"),
                                                        ActionsOnYes = new GameActionGroup()
                                                        {
                                                            ActionsToPerform = new List<GameAction>
                                                            {
                                                                new Dialog.BasicText("You feel compelled to give Vacchan back her vaccum. After all, it's very special to her, and more importantly you simply must know what that key does."),
                                                                new GameAction.Trade(624, 307),
                                                                new GameAction.AddOrRemoveStoryTriggers(GameAction.AddOrRemoveStoryTriggers.Option.Remove, new List<string>{"FindVacuumQuest"}),
                                                                new GameAction.AddToPriorityMessages(){ TextToAdd = "And there ya go, that was the entire demo" }
                                                            }
                                                        },
                                                        ActionsOnNo = new GameActionGroup()
                                                        {
                                                            ActionsToPerform = new List<GameAction>
                                                            {
                                                                new Dialog.BasicText("...Okay then...\nI guess I'll just keep the key for myself! They'll call me \"Limekeychan\"!")
                                                            }
                                                        }

                                                    }
                                                }
                                            },
                                            ActionsOnFalse = new GameActionGroup()
                                            {
                                                ActionsToPerform = new List<GameAction>
                                                {
                                                    new Dialog.BasicText("Hmm, where could my vacuum be...?"){ NameHeader = "Vacchan" }
                                                }
                                            }
                                        }
                                    }
                                },
                                new GameActionGroup()
                                {
                                    StoryTriggerID = "Default",
                                    ActionsToPerform = new List<GameAction>
                                    {
                                        new GameAction.AddOrRemoveStoryTriggers(GameAction.AddOrRemoveStoryTriggers.Option.Add, new List<string>{"HasNotRecieveVacchanDuck"}){ ExpireOnCompleted = true }
                                    }
                                },
                                new GameActionGroup()
                                {
                                    StoryTriggerID = "HasNotRecieveVacchanDuck",
                                    ActionsToPerform = new List<GameAction>
                                    {
                                        new Dialog.YesOrNo()
                                        {
                                            CloseOnYesOrNo = (false, false),
                                            NameHeader = "Vacchan",
                                            Text = "Do you want a ducky?",
                                            ButtonText = ("Yes", "Heck nah"),
                                            ActionsOnYes = new GameActionGroup()
                                            {
                                                StoryTriggerID = "Default",
                                                ActionsToPerform = new List<GameAction>()
                                                {
                                                    new Dialog.BasicText("Yay! I love giving people duckies!"){ NameHeader = "Vacchan" },
                                                    new Dialog.BasicText("And yet, you must win a dice roll to recieve it!"){ NameHeader = "Vacchan" },
                                                    new GameAction.PerformActionsIf.DiceRollSucceed()
                                                    {
                                                        ActionsOnTrue = new GameActionGroup()
                                                        {
                                                            ActionsToPerform = new List<GameAction>
                                                            {
                                                                new Dialog.BasicText("The die rolled in your favor!"),
                                                                new Dialog.BasicText("Goodie! Now take ducky."){ NameHeader = "Vacchan" },
                                                                new GameAction.ItemGet(new AnyObject()
                                                                {
                                                                    Name = $"Vinyl plastic yellow bird",
                                                                    CanPickUp = true,
                                                                    HealthPoints = 10,
                                                                    ID = 956,
                                                                    Description = "It is wet for some reason.",
                                                                    ActionsOnUse = new List<GameActionGroup>()
                                                                    {
                                                                        new GameActionGroup()
                                                                        {
                                                                            StoryTriggerID = "Default",
                                                                            ActionsToPerform = new List<GameAction>
                                                                            {
                                                                                new GameAction.MaxHPUp(10){ ExpireOnCompleted = true }
                                                                            }
                                                                        }
                                                                    }
                                                                }){ ExpireOnCompleted = true },
                                                                new GameAction.AddOrRemoveStoryTriggers(GameAction.AddOrRemoveStoryTriggers.Option.Remove, new List<string>{ "HasNotRecieveVacchanDuck" }),
                                                                new GameAction.AddOrRemoveStoryTriggers(GameAction.AddOrRemoveStoryTriggers.Option.Add, new List<string>{ "FindVacuumQuest" }),
                                                                new GameAction.AddToPriorityMessages(){ TextToAdd = "It looks like Vacchan lost her vacuum! Maybe you can find it somewhere..." }
                                                            }
                                                        },
                                                        ActionsOnFalse = new GameActionGroup()
                                                        {
                                                            ActionsToPerform = new List<GameAction>
                                                            {
                                                                new Dialog.BasicText("The die did not roll in your favor..."),
                                                                new Dialog.BasicText("Awh, looks like you lost... sorry about that..."){ NameHeader = "Vacchan" }
                                                            }
                                                        }
                                                    }
                                                }
                                            },
                                            ActionsOnNo = new GameActionGroup()
                                            {
                                                StoryTriggerID = "Default",
                                                ActionsToPerform = new List<GameAction>()
                                                {
                                                    new Dialog.BasicText("Oh, okay..."){ NameHeader = "Vacchan" }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        new AnyObject
                        {
                            Name = "Switch",
                            ID = 257,
                            CanPickUp = false,
                            HealthPoints = 500,
                            MaxHP = 510,
                            Description = "NO_WINDOW",
                            ActionsOnInteract = new List<GameActionGroup>
                            {
                                new GameActionGroup()
                                {
                                    ActionsToPerform = new List<GameAction>
                                    {
                                        new Dialog.BasicText("You pushed the switch, and apparently something happened."),
                                        new GameAction.TriggerStoryEvent("RenameVacchan"){ ExpireOnCompleted = true }
                                    }
                                }
                            }
                        }
                    }
                },
                new Area
                {
                    AreaID = "SurfRoom004",
                    AreaName = "Surface Room 004",
                    ConnectedAreas = new List<string>{ "SurfRoom002" },
                    LocationInfo = "Connected to Surface Room 002 through a warp.",
                    ArtName = "SurfaceRoomArt01",
                    Nouns = new List<AnyNoun>
                    {
                        new AnyObject
                        {
                            ID = 504,
                            HealthPoints = 987,
                            MaxHP = 1000,
                            Name = "Dusty Bookshelf",
                            Description = "Vacchan was probably gonna get to cleaning it, but she got distracted or something...",
                            CanPickUp = false,
                            ActionsOnInteract = new List<GameActionGroup>()
                            {
                                new GameActionGroup
                                {
                                    StoryTriggerID = "FindVacuumQuest",
                                    ActionsToPerform = new List<GameAction>
                                    {
                                        new GameAction.ItemGet(new AnyObject()
                                        {
                                            ID = 307,
                                            Name = "Vacuum",
                                            Description = "A powerful yet compact little sucker. Vacchan uses it to keep the colony as clean as possible.",
                                            HealthPoints = 50,
                                            MaxHP = 48,
                                            CanBeWeapon = true,
                                            CanPickUp = true,
                                            UseMessage = "You press a button, and dust flies out. You clearly do not have the P.H.D. required to properly use this instrument."
                                        }){ ExpireOnCompleted = true }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            return defaultAreas;
        }

        public static List<StoryEvent> DefaultStoryEvents()
        {
            List<StoryEvent> defaultStoryEvents = new List<StoryEvent>()
            {
                new StoryEvent("StoryEventTest")
                {
                    EffectAreas = new List<EffectAreaInstructions>()
                    {
                        new EffectAreaInstructions()
                        {
                            AreaID = "SurfRoom003",
                            AreaEffects = new List<AreaEffect>()
                            {
                                new AreaEffect.MoveNoun()
                                {
                                    NounToMoveID = 100,
                                    AreaToMoveToID = "SurfRoom002"
                                }
                            }
                        }
                    }
                },
                new StoryEvent("RenameVacchan")
                {
                    EffectAreas = new List<EffectAreaInstructions>()
                    {
                        new EffectAreaInstructions()
                        {
                            AreaID = "SurfRoom003",
                            AreaEffects = new List<AreaEffect>()
                            {
                                new AreaEffect.EditProperty()
                                {
                                    ObjectID = 3754,
                                    PropertyID = typeof(AnyNoun).GetProperty(nameof(AnyNoun.Name)),
                                    ValueEdited = "Unit #3754 Limekeychan"
                                }
                            }
                        }
                    }
                },
                new StoryEvent("OpenWarpToSR004_1")
                {
                    EffectAreas = new List<EffectAreaInstructions>()
                    {
                        new EffectAreaInstructions()
                        {
                            AreaID = "SurfRoom002",
                            AreaEffects = new List<AreaEffect>()
                            {
                                new AreaEffect.AddNoun()
                                {
                                    NounToAdd = new AnyObject
                                    {
                                        Name = "Warp",
                                        ID = 999,
                                        Description = "You go through the warp, and find yourself in a brand new room.",
                                        ViewInfoRestricted = true,
                                        ActionsOnInteract = new List<GameActionGroup>
                                        {
                                            new GameActionGroup()
                                            {
                                                StoryTriggerID = "Default",
                                                ActionsToPerform = new List<GameAction>
                                                {
                                                    new GameAction.TriggerStoryEvent("UnlockTravel_SR004_1"){ ExpireOnCompleted = true },
                                                    new GameAction.AddToSideMessages(){ TextToAdd = "Congradulations! You found a warp to a new room. Now, you can access it from the Travel interface as well!", ExpireOnCompleted = true  },
                                                    new GameAction.ForceWarp("SurfRoom004")
                                                }
                                            }
                                        }
                                    },
                                }
                            }
                        }
                    }
                },
                new StoryEvent("UnlockTravel_SR004_1")
                {
                    EffectAreas = new List<EffectAreaInstructions>()
                    {
                        new EffectAreaInstructions()
                        {
                            AreaID = "SurfRoom002",
                            AreaEffects = new List<AreaEffect>()
                            {
                                new AreaEffect.UnlockTravel("SurfRoom004")
                            }
                        }
                    }
                }
            };
            return defaultStoryEvents;
        }
        #endregion
        //----------------------------------------------
    }
}
