using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlappyBuraders.GlobalStuff;

public class ProfilesStuff
{
    private readonly string _fullName;
    private readonly string _type;
    private readonly string _specialName;
    private readonly string _specialDescription;
    private readonly string _stuff;

    private ProfilesStuff(string fullName, string type, string specialName, string specialDescription, string stuff) {
        this._fullName = fullName;
        this._type = type;
        this._specialName = specialName;
        this._specialDescription = specialDescription;
        this._stuff = stuff;
    }

    public string FullName { get { return _fullName;} }
    public string Type { get { return _type;} }
    public string SpecialName { get { return _specialName;} }
    public string SpecialDescription { get { return _specialDescription;} }
    public string Stuff { get { return _stuff;} }

    public static ProfilesStuff GetProfiles(Enums.BuraderName yourBurader) {
        switch (yourBurader) {
            case Enums.BuraderName.BRITTANY:
                return new ProfilesStuff("Brittany Erika Joson", "Teleport Melee", "Shadow Flash", 
                "Trained as a professional hide and seeker since childhood, she can conceal everything about her in an instant...then appears out of nowhere to finish you off. Watch your back! \r\n\r\nDamage = **\r\nEffect = ****\r\nDuration = *", 
                "It is a blessing and a curse. You know, to be unnoticed. Makes my job a LOT easier. I can do everything on my own...tho I don't want to be alone...\r\n\r\nsee my arts on FB: @pancakehime012");
            case Enums.BuraderName.BRYAN:
                return new ProfilesStuff("Bryan Solis", "Control", "Life Hackery", 
                "As the Super Hacker of the century, Bryan can hack into almost anything. What better thing to hack than to hack your enemies' movements.\r\n\r\nDamage = N/A\r\nEffect = ******\r\nDuration = **",
                "This is madness! Why do people have to kill each other? If I could just hack into other people's hearts and change them from the inside...wait, I can do that!");
            case Enums.BuraderName.CHRISTIAN:
                return new ProfilesStuff("Donetrius Christian Pujalte", "Obstacle", "You Are Legion", 
                "Kumusta po sila?\r\nKumaen na po sila?\r\nNothing is harder than fighting multiple copies of yourself.\r\n\r\nDamage = *\r\nEffect = ***\r\nDuration = *****", 
                "I arrived very early at the office, in fact, too early, that I heard Ivan and Rizal arguing about something...");
            case Enums.BuraderName.DARK_M:
                return new ProfilesStuff("Dark Massimo", "Hyper Combo", "CAD Barrage",
                "The receiving end of this attack will just wish to never been born.\r\n\r\nDamage = *****\r\nEffect = *****\r\nDuration = *****", 
                "I NEED TO REVISE THE DESIGN OF THIS WORLD...STARTING BY DESTROYING IT");
            case Enums.BuraderName.DINK:
                return new ProfilesStuff("Dink Dragonmire", "Projectile", "Normal Bomb", 
                "For unknown reasons, Dink has a compartment in her clothing that houses unlimited bombs. Careful tho, the explosions can hit her too.\r\n\r\nDamage = *****\r\nEffect = ?\r\nDuration = ****", 
                "I got transported in this weird world - a world where everyone flies! There must be a reason why I'm here. Maybe a clue to find my Prince.");
            case Enums.BuraderName.DOGE:
                return new ProfilesStuff("Doge Akerfeldt", "Obstacle", "Annoying Cream", 
                "Pet owner's only hardship in raising pets...is to clean up their mess. Now try to fill the screen with these.\r\n\r\nDamage = *\r\nEffect = *\r\nDuration = ***************************", 
                "Arf arf! Grr...Arf!\r\n(everyone started to fly after that big boom boom happened. I can also fly now! Everything flies!)");
            case Enums.BuraderName.ERIC:
                return new ProfilesStuff("Ericson Ogot", "Control", 
                "You Got A Bug!", 
                "As a master Q.A. he can spot any bug in you even if it's not there. Goodluck on your controls.\r\n\r\nDamage = *\r\nEffect = ***\r\nDuration = ***", 
                "I know what went wrong. But I don't know how to fix this world. All I can see are errors and mistakes.");
            case Enums.BuraderName.GIO:
                return new ProfilesStuff("GIO Burando", "Control", 
                "HARO WARUDO!", 
                "His power is called Hello World, which by some reason can stop time. While the time is stopped, you can move some objects around you.\r\n\r\nDamage = *\r\nEffect = *****\r\nDuration = *****", 
                "You were expecting Massimo as the final boss?! But it was ME! GIO!");
            case Enums.BuraderName.HORORO:
                return new ProfilesStuff("Hororo Chan", "Teleport", 
                "Screen Breaker", 
                "Hororo had the ability to break in and out of another dimension. S/he can also break walls with the Bat, including fourth walls.\r\n\r\nDamage = **\r\nEffect = ****\r\nDuration = **", 
                "It was just self-defense, really. I do not have any motives to kill all of them... (or do I?) \r\n\r\nWatch my vids on YouTube!");
            case Enums.BuraderName.IVAN:
                return new ProfilesStuff("Ivan Handoko", "Projectile", "Giant Paper Cranes", 
                "Throws paper cranes at enemies that hits like trucks.\r\n\r\nDamage = ***\r\nEffect = **\r\nDuration = ***", 
                "Boss had it right. I believe it was the right thing to do. But something...something just went horribly wrong..");
            case Enums.BuraderName.KYLE:
                return new ProfilesStuff("Kyle Kristofer Gracia", "Control", "Burakku Hooru", 
                "Conjures a compressed gravitational field tight enough to absorb light passing through it...and you.\r\n\r\nDamage = N/A\r\nEffect = ****\r\nDuration = ****", 
                "I don't care about you guys anymore. Not my problem no more. I told you this would happen. No one listened.");
            case Enums.BuraderName.LOKIA:
                return new ProfilesStuff("Loki Odinson't", "?", "Master of Deception", 
                "You think you killed him? Think again.\r\n\r\nDamage = N/A\r\nEffect = ?\r\nDuration = ?", 
                "I was banished from my home-realm and ended up being here in... Urth. I can make a religion out of me here.");
            case Enums.BuraderName.MARK:
                return new ProfilesStuff("Mark Stephen Fernandez", "Melee", 
                "Burning Love", 
                "Summons a miniature version of the Sun behind him that scorches everyone who comes near.\r\n\r\nDamage = *\r\nEffect = *\r\nDuration = *****", 
                "Where is she!? She's been gone since the incident happened...I have to find her..she might be in danger!");
            case Enums.BuraderName.MASSIMO:
                return new ProfilesStuff("Massimo Mercury", "Obstacle", "Architectural Smash", 
                "With his unlimited wealth and architectural genius, he can instantly build huge buildings just to smash it to anyone who dares oppose him.\r\n\r\nDamage = **\r\nEffect = ****\r\nDuration = ****",
                "I will make a Paradise. Eutopia. With my designs, I can make every people's lives much better. Or so I thought. \r\n\r\nNeed an Architect? visit www.mercuriodesignlab.it");
            case Enums.BuraderName.MAYU:
                return new ProfilesStuff("Mayu Vraka", "Control", "Nega-Gravity", 
                "With her heightened powers, she can manipulate gravity of everything within her range. Watch out for falling debris! (or chainsaws)\r\n\r\nDamage = N/A\r\nEffect = ****\r\nDuration = ***", 
                "I have been captured...and they began experimenting on me in a laboratory for weeks. Something happened and I managed to escape..I need to look for my friends. I hope they're alright.");
            case Enums.BuraderName.MIGS:
                return new ProfilesStuff("Miguel Magno", "Melee", "Oh Big!", 
                "You can't tell if he's just so mad at you or just so full. In this state, with no effort he can send you flying like a pancake.\r\n\r\nDamage = ****\r\nEffect = **\r\nDuration = **", 
                "Wah! I thought it was food! But when I took the bite, everything explodes! Good thing I'm still alive!");
            case Enums.BuraderName.NICOLLO:
                return new ProfilesStuff("Nicollo", "Projectile", "Energy Beam", 
                "Charges chi in his forehead then fires it to enemies, dealing huge explosion.\r\n\r\nDamage = ***\r\nEffect = ***\r\nDuration = ***", 
                "Carrot! I was just harvesting my carrots! And all of a sudden, everything was gone. I'm gonna make the one responsible for this pay! For my carrots!");
            case Enums.BuraderName.OYYY:
                return new ProfilesStuff("Mayumi Oyyy", "Melee", "Hana-Onara", 
                "Shogyo mogyo! Her heart is as pure as a newly bloomed flower, but she can use it to destroy the forces of evil.\r\n\r\nDamage = ***\r\nEffect = ***\r\nDuration = **", 
                "The next day I woke up...everyone was dead. There's blood everywhere. I need to find out what happened, what or who did this.");
            case Enums.BuraderName.PAU:
                return new ProfilesStuff("Paulo Dominus", "Melee", "Atomic Sneeze", 
                "It explodes.\r\n It sticks.\r\n It kills.\r\n Wash your hands.\r\n\r\nDamage = **\r\nEffect = ***\r\nDuration = ****", 
                "I used to work for Massimo..until one day I accidentally came over a confidential file containing abominable information..I just had to do something.");
            case Enums.BuraderName.PLATIMOON:
                return new ProfilesStuff("Star Platimoon", "Melee",
                "HOLA HOLA", 
                "Known to be the fastest puncher in town, Platimoon pummels enemies at the rate of your average typing speed! HOLAAA!!!\r\n\r\nDamage = ****\r\nEffect = *\r\nDuration = ***", 
                "How can a Spirit like me be on my own without a user? What the hell is wrong in this world? Where is Godaro?");
            case Enums.BuraderName.POPPINS:
                return new ProfilesStuff("Harry Poppins", "Projectile", "Laser Stick", 
                "He controls his favorite weapon not with his mind..but with his heart. Anyone hit with it will be temporarily stunned and possibly knocked back.\r\n\r\nDamage = *\r\nEffect = ***\r\nDuration = ***", 
                "My ship crashed because of a huge explosion on this planet while my me and crew was passing by. I need to find something I can use to communicate to my home planet.");
            case Enums.BuraderName.JF:
                return new ProfilesStuff("Juan Fidel Sangalang", "Melee", "Terra Smash", 
                "Naturally kind people tend to hold down their anger longer than most people. But once they unleash it, earthquakes happen and buildings get destroyed.\r\n\r\nDamage = ***\r\nEffect = ****\r\nDuration = *", 
                "I know Mayumi's deepest secret. And I'm not going to tell anyone. As long as you do what I told you.");
            case Enums.BuraderName.RIZAL:
                return new ProfilesStuff("Muhammed Rizal", "Teleport", "Call Me", 
                "Your connection will literally be gone when someone calls you while you're playing. And you can't ignore it because it's your boss calling you. GG\r\n\r\nDamage = *\r\nEffect = ****\r\nDuration = **", 
                "I'm ok with Massimo building a better world. But if it means to destroy everything first..I wont allow it.");
            case Enums.BuraderName.SHINMEN:
                return new ProfilesStuff("Shinmen Takezo YT", "Projectile", "Blazing Duet", 
                "Summons his trusty Partner and together they shoot fireballs to the enemy!\r\n\r\nDamage = ***\r\nEffect = *\r\nDuration = ****", 
                "O.M.G. Oyyy! How did you make this, Oyyy? What the heck am I wearing too?! I thought I'd look a lot cooler!\r\n\r\nWatch my vids on YouTube! Lezgo bois!");
            case Enums.BuraderName.ZANTETSU:
                return new ProfilesStuff("Zantetsu Ken", "Melee", "Hishimushimu", 
                "Be careful once he mutters to himself and close his eyes. If you get close enough, you'll lose your head.\r\n\r\nDamage = ****\r\nEffect = ****\r\nDuration = ***", 
                "何。 私はウェイストーンを使用しましたが、この場所を思い出したのを覚えていません。 深い誤解があるはずです。 私はミリアムに戻る必要があります。");
            default:
                return null;
        }
    }
}
