using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBuraders.GlobalStuff {
    public class Enums : MonoBehaviour
    {
        public enum GameMode {
            ARCADE, 
            HEAD_TO_HEAD,
            SURVIVAL,
            HOLD_HANDS,
            TRAIN_YOUR_BURADER,
            ONLINE,
            END,
        }
        public enum KillGoal {
            KILL1,
            KILL2,
            KILL3,
            KILL5,
            END
        }
        public enum Stage {
            TOWN_OUTSKIRTS,
            RADIANT_CASTLE,
            MAGICAL_ACADEMY,
            LUSH_FOREST,
            OCEAN_PARADISE,
            STRATOSPHERE,
            INNER_SPACE,
            THE_ARCHITECT,
            RANDOM,
            END
        }
        public enum Rank {
            Atom,
            Molecule,
            Cell,
            Bacteria,
            Algae,
            Tardigrade,
            DustMite,
            Flea,
            Lice,
            Ant,
            FireAnt,
            Termite,
            LadyBug,
            Earthworm,
            Caterpillar,
            Cockroach,
            Frog,
            Turtle,
            Chicken,
            Rat,
            Snake,
            Cow,
            Deer,
            Kangaroo,
            Raven,
            Dog,
            Wolf,
            Bear,
            Hipoppotamus,
            Gorilla,
            Tiger,
            Lion,
            Octopus,
            BabyShark,
            Whale,
            Shark,
            Dolphin,
            Crocodile,
            Anaconda,
            Neanderthal,
            HomoSapien,
            Caveman,
            PreIndustrialAgeMan,
            IndustrialAgeMan,
            AtomicAgeMan,
            SpaceAgeMan,
            SpaceFaring,
            Interstellar,
            WorldBuilder,
            Transsentient,
            Goddess,
            God
        }

        public enum BuraderName {
            //!TIER 1
            OYYY, //hi guys!
            NICOLLO,
            MAYU,
            PAU, //haching, magkakasipon sa paligid ng kalaban, slow move
            SHINMEN,
            POPPINS,
            MARK, //summons Kat na iikot ikot sa kanya, will deal knockback to enemies if hit
            JF,
            T109,
            T110,
            T111,
            T112,
            T113,
            T114,
            T115,
            ENDTIER1,
            
            //!TIER 2
            PLATIMOON,
            BRITTANY, //fades away, became invulnerable for a short time, then appears behind enemy for knockback
            BRYAN, //hacks enemy movement to be same as his, for a very short time
            KYLE, //terrence counterpart, shows waifu in the background, reverse control of enemy
            DOGE,
            ERIC, //will report bugs in the game, random glitch will appear (chainsaw appearing from nowhere, etc)
            T207,
            T208,
            T209,
            T210,
            T211,
            T212,
            T213,
            T214,
            T215,
            ENDTIER2,
            
            //!TIER 3
            LOKIA, 
            MIGS, //gains tremendous weight, obliterate EVERYTHING in its path for a short period of time
            HORORO,
            DINK, //bomb
            CHRISTIAN, //makes clones of enemies, all sharing same hitboxes
            ZANTETSU,
            RIZAL, //call me, enemy will lost control for a while
            IVAN, //transform into a mario plant that shoots fireballs, stun on firebll hit
            T309,
            T310,
            ENDTIER3,

            //!TIER 4 (final bosses)
            MASSIMO, //secret
            DARK_M,
            GIO,
            T404,
            T405,
            //! END
            END,

            PADDING1,
            PADDING2,
            PADDING3,

        }
        public enum Style {
            MELEE,
            RANGED
        }
    }
}

