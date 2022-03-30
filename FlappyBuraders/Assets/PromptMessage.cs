using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlappyBuraders.GlobalStuff;

public class PromptMessage
{
    private readonly string _main;
    private readonly string _sub;
    private readonly string _fmain;
    private readonly string _fsub;

    private PromptMessage(string main, string sub, string fmain, string fsub) {
        this._main = main;
        this._sub = sub;
        this._fmain = fmain;
        this._fsub = fsub;
    }

    public string MainMessage{
        get {
            return _main;
        }   
    }
    public string SubMessage {
        get {
            return _sub;
        }
    }
    public string fMainMessage {
        get {
            return _fmain;
        }
    }
    public string fSubMessage {
        get {
            return _fsub;
        }
    }

    public void Test() {
        
    }

    public static PromptMessage GetRandomReadyMessages() {
        List<PromptMessage> AllMessages = new List<PromptMessage>();
        AllMessages.Add(new PromptMessage("Handa ka na bang madurog!?", "Are you ready to die?!", "DURUGAN NA!", "FIGHT for your Life!"));
        AllMessages.Add(new PromptMessage("Sino'ng maghuhugas ng plato?", "Who will wash the dishes?", "Matalo MAGHUHUGAS!", "The loser will be the slave"));
        AllMessages.Add(new PromptMessage("San kayo maglalunch mamaya?!", "Krispy Kreme or Starbucks?!", "Matalo MANLILIBRE!", "The choice is yours"));
        AllMessages.Add(new PromptMessage("Sino ang tama?!", "Who is the right one?!", "Matira MATIBAY!", "Last man Standing!"));
        AllMessages.Add(new PromptMessage("Gagawa na ba ng assignment?!", "Should you do your homework?!", "MAMAYA NA!", "Do it later!"));
        AllMessages.Add(new PromptMessage("Aral muna o lalandi?!", "Studies or Lovelife?!", "ITULOG MO NA LANG YAN!", "Let's FIGHT first!"));
        AllMessages.Add(new PromptMessage("Papasok o tutulog ulet!?", "Get up or Sleep Again!?", "5MINUTES PA!", "See you in the dream world!"));
        AllMessages.Add(new PromptMessage("Ngayon o mamaya na!?", "Do Now or Later!?", "BUKAS NA LANG!", "Maybe do it next YEAR!"));
        AllMessages.Add(new PromptMessage("Ang matatalo sa labang ito...", "Who speaks the truth!?", "Bayad piso!", "Strength is the Truth!"));
        AllMessages.Add(new PromptMessage("Sinong nagiwang bukas ang plantsa!?", "Who left the flat iron on!?", "SUNUGIN YAN!", "Don't let your house burn!"));
        AllMessages.Add(new PromptMessage("Kukuha na ba ng insurance!?", "Sign the insurance proposal!?", "Protect your Future!", "Protect your future"));
        AllMessages.Add(new PromptMessage("Titigil na ba sa YOSI!?", "Should you stop smoking!?", "TAMA NA YAN!", "Drop it when you lose!"));
        AllMessages.Add(new PromptMessage("Sasama ba sa inuman mamaya?", "Will you drink and drive!?", "LARO NA LANG!", "Stop your foolishness!"));
        AllMessages.Add(new PromptMessage("Ang matatalo dito...", "Should you go to the dentist!?", "BUNUTAN NG NGIPIN!", "Brush your teeth every hour!"));
        AllMessages.Add(new PromptMessage("Bababa ba!?", "Ehhh!?", "BABABA!", "What?"));
        AllMessages.Add(new PromptMessage("Ang mananalo...", "Extraction or Root Canal!?", "LIBRE NA PAMASAHE!", "Bye life"));
        AllMessages.Add(new PromptMessage("To be or not to be!?", "To be or not to be!?", "THAT IS THE QUESTION!", "I don't know!"));
        AllMessages.Add(new PromptMessage("Sino magsasampay ng labada!?", "Who will hang the clothes!?", "Matalo MAGSASAMPAY!", "The loser will be the slave"));
        AllMessages.Add(new PromptMessage("Hawakan mo maigi ang kamay!", "Who will you say 'YES' to!?", "Wag mo pakawalan!", "You deserve better!"));
        AllMessages.Add(new PromptMessage("Magbabayad na ba ng utang!?", "Will you now pay your loan!?", "Sa susunod na SAHOD!!", "Maybe next time!"));
        AllMessages.Add(new PromptMessage("Wala pang 5 minutes!?", "Can you still eat that!?", "PWEDE PA YAN!", "You absolutely can!"));
        AllMessages.Add(new PromptMessage("Kelangan mo nang matulog!", "Can you still eat that!?", "INOM MUNA NG KAPE!", "You absolutely can!"));
        AllMessages.Add(new PromptMessage("Tara kaen na muna tayo!", "Can you still eat that!?", "MATALO HINDI IINOM!", "You absolutely can!"));
        AllMessages.Add(new PromptMessage("Andumi ng sahig nio!", "Can you still eat that!?", "MATALO MAGWAWALIS!", "You absolutely can!"));
        AllMessages.Add(new PromptMessage("Umihi yung aso sa sahig!", "Can you still eat that!?", "MATALO MAGPUPUNAS!", "You absolutely can!"));
        AllMessages.Add(new PromptMessage("May extra kang allowance!", "Can you still eat that!?", "IBILI YAN NG BP!!!", "You absolutely can!"));
        AllMessages.Add(new PromptMessage("Yung sukli mo kanina sa jeep...", "Can you still eat that!?", "IBILI YAN NG BP!!!", "You absolutely can!"));
        AllMessages.Add(new PromptMessage("Nakapulot ka ng 500 pesos!!", "Can you still eat that!?", "IBILI YAN NG BP!!!", "You absolutely can!"));
        AllMessages.Add(new PromptMessage("Malelate ka na!!", "Ehhh!?", "Bahala na si Batman!", "What?"));
        AllMessages.Add(new PromptMessage("Ano ang english ng TUTONG?!", "Ehhh!?", "BAWAL I-GOOGLE!", "What?"));
        AllMessages.Add(new PromptMessage("Ano ang english ng PIKON?!", "Ehhh!?", "BAWAL I-GOOGLE!", "What?"));
        AllMessages.Add(new PromptMessage("Ano ang english ng GIGIL?!", "Ehhh!?", "BAWAL I-GOOGLE!", "What?"));
        AllMessages.Add(new PromptMessage("Lumilindol ng malakas!", "Ehhh!?", "MAGTAGO KA NA SA ILALIM NG LAMESA!", "What?"));
        AllMessages.Add(new PromptMessage("Nasusunog na bahay ng kapitbahay nio!", "Ehhh!?", "HINDE, JOKE LANG!", "What?"));
        AllMessages.Add(new PromptMessage("Bakit ba?!", "Ehhh!?", "WALA LANG!", "What?"));
        AllMessages.Add(new PromptMessage("Baka matalo ka nanaman!", " ", "GALINGAN MO KASI!", " "));
        AllMessages.Add(new PromptMessage("I can show you ZA WARUDO!", " ", "Shining shimmering, splendid!", " "));
        AllMessages.Add(new PromptMessage("Uy malelate ka na sa meeting niyo..", " ", "BAHALA SILA SA BUHAY NILA!!", " "));
        AllMessages.Add(new PromptMessage("Kanino mapupunta ang huling slice ng pizza?!", " ", "KUNG SINO MANALO!", " "));
        AllMessages.Add(new PromptMessage("Sino ang kakaen ng huling piraso ng fries?! ", " ", "BAHALA KAU AKIN NA LANG YAN!", " "));
        AllMessages.Add(new PromptMessage("Bili daw mantika sabi ni nanay!", " ", "Matalo BIBILI!", " "));
        AllMessages.Add(new PromptMessage("Brownout, walang WiFi!", " ", "GE LARO KA NA LANG NETO!", " "));
        AllMessages.Add(new PromptMessage("Salamat sa pagtangkilik nito!", " ", "i-RATE mo yan ng 5 STARS!", " "));
        AllMessages.Add(new PromptMessage("Matatapos na breaktime niyo!", " ", "5 MINUTES PA!", " "));
        AllMessages.Add(new PromptMessage("Kinaen ng pusa yung ulam niyo sa lamesa!", " ", "LAGOT KA SA NANAY MO!", " "));
        AllMessages.Add(new PromptMessage("Omaewa mou...", " ", "Shindeiru", " "));
        AllMessages.Add(new PromptMessage(" ", " ", " ", " "));


        return AllMessages[Random.Range(0, AllMessages.Count - 1)];
    }
    public static PromptMessage GetRandomAccidentDeadMessages() {
        List<PromptMessage> AllMessages = new List<PromptMessage>();
        AllMessages.Add(new PromptMessage(" HAS \r\nBEEN SLAIN!", "Well, it's your fault, really..", "", ""));
        AllMessages.Add(new PromptMessage(" IS \r\nSO DEAD", "Life is short..make the most of it!", "", ""));
        AllMessages.Add(new PromptMessage(" DIED A NOOB", "You could have dodged that!", "", ""));
        AllMessages.Add(new PromptMessage(" DIED IN A PITIFUL MANNER", "Your life is such a waste!", "", ""));
        AllMessages.Add(new PromptMessage(" IS KILLED DUE TO NEGLIGENCE", "This could be prevented!", "", ""));
        AllMessages.Add(new PromptMessage(" DIED BY ACCIDENT", "Stop, look, and listen!", "", ""));
        AllMessages.Add(new PromptMessage(" FORGOT TO JUMP", "Slap your face!", "", ""));
        AllMessages.Add(new PromptMessage(" EXPERIMENTED WITH LIFE", "Conclusion: You Died!", "", ""));
        AllMessages.Add(new PromptMessage(" JUMPED TOO MUCH", "Look above and below!", "", ""));
        AllMessages.Add(new PromptMessage(" COMMITED SUICIDE", "It's not even funny!", "", ""));
        AllMessages.Add(new PromptMessage(" IS A CUTE RETARD", "C'mon, don't give up!", "", ""));
        AllMessages.Add(new PromptMessage(" DOESN'T HAVE INSURANCE", "Better get one now!", "", ""));
        AllMessages.Add(new PromptMessage(" LOVES CHAINSAWS SINCE CHILDHOOD", "What is your problem?!", "", ""));
        AllMessages.Add(new PromptMessage("!?", "!?", "", ""));

        return AllMessages[Random.Range(0, AllMessages.Count - 1)]; 
    }
    public static PromptMessage GetRandomSpecialKillMessages() {
        List<PromptMessage> AllMessages = new List<PromptMessage>();
        AllMessages.Add(new PromptMessage(" DIED A GRUESOME DEATH!", "That is an awesome finish!", "", ""));
        AllMessages.Add(new PromptMessage(" HAD A HEART ATTACK!", "You better watch out for what's next!", "", ""));
        AllMessages.Add(new PromptMessage(" IS SPLIT INTO TWO!", "Tomato juice all over!", "", ""));
        AllMessages.Add(new PromptMessage(" DIED BUT DIDN'T KNOW WHY!", "It happened all so fast!", "", ""));
        AllMessages.Add(new PromptMessage("!?", "!?", "", ""));

        return AllMessages[Random.Range(0, AllMessages.Count - 1)]; 
    }
    public static PromptMessage GetDeadlyHitMessages(int combo) {
        List<PromptMessage> AllMessages = new List<PromptMessage>();
        AllMessages.Add(new PromptMessage("DEADLY HIT!", " ", "", ""));
        AllMessages.Add(new PromptMessage("SUPER DEADLY HIT!", " ", "", ""));
        AllMessages.Add(new PromptMessage("HYPER DEADLY HIT!", " ", "", ""));
        AllMessages.Add(new PromptMessage("MEGA DEADLY HIT!", " ", "", ""));
        AllMessages.Add(new PromptMessage("ULTRA DEADLY HIT!", " ", "", ""));
        AllMessages.Add(new PromptMessage(" ", " ", "", ""));

        if (combo < 4) {
            return AllMessages[0];
        }
        else if (combo < 8) {
            return AllMessages[1];
        }
        else if (combo < 12) {
            return AllMessages[2];
        }
        else if (combo < 16) {
            return AllMessages[3];
        }
        else {
            return AllMessages[4];
        }
        
    }
    public static PromptMessage GetLokiaKillMessage() {
        List<PromptMessage> AllMessages = new List<PromptMessage>();
        AllMessages.Add(new PromptMessage(" DIED A HERO!", "...Or did he really?!", "", ""));
        AllMessages.Add(new PromptMessage(" DIED SAVING THE WORLD!", "...Or did he really?!", "", ""));
        AllMessages.Add(new PromptMessage(" DIED FOR HONOR!", "...Or did he really?!", "", ""));
        AllMessages.Add(new PromptMessage("!?", "!?", "", ""));

        return AllMessages[Random.Range(0, AllMessages.Count - 1)]; 
    }
    public static PromptMessage GetRandomTechnicalKillMessages() {
        List<PromptMessage> AllMessages = new List<PromptMessage>();
        AllMessages.Add(new PromptMessage(" DIDN'T JUMP THE RIGHT TIME!", "You got tight controls!", "", ""));
        AllMessages.Add(new PromptMessage(" IS PUSHED TO DEATH!", "This required lots of skills!", "", ""));
        AllMessages.Add(new PromptMessage(" IS MUTILATED!", "Durog ka manong!", "", ""));
        AllMessages.Add(new PromptMessage(" DIDN'T KNOW WHAT HAPPENED!", "It happened all so fast!", "", ""));
        AllMessages.Add(new PromptMessage(" DIED WIDE EYES OPEN!", "Got surprised!", "", ""));
        AllMessages.Add(new PromptMessage(" DIED ON THE SPOT!", "No need to call the ambulance!", "", ""));
        AllMessages.Add(new PromptMessage("!?", "!?", "", ""));

        return AllMessages[Random.Range(0, AllMessages.Count - 1)]; 
    }
    public static PromptMessage GetRandomCoopKillMessages() {
        List<PromptMessage> AllMessages = new List<PromptMessage>();
        AllMessages.Add(new PromptMessage(" USED ONE HEALTHCARD!", "Buti na lang meron kang HealthCard!", "SO DO NOT DIE!", "You die, you lose!"));
        AllMessages.Add(new PromptMessage("!?", "!?", "", ""));

        return AllMessages[Random.Range(0, AllMessages.Count - 1)]; 
    }
    public static PromptMessage GetRandomHealthCardMessages() {
        List<PromptMessage> AllMessages = new List<PromptMessage>();
        AllMessages.Add(new PromptMessage(" GOT A HEALTHCARD!", "Ayos buti may nakita ka!", "SO DO NOT DIE!", "You die, you lose!"));
        AllMessages.Add(new PromptMessage("!?", "!?", "", ""));

        return AllMessages[Random.Range(0, AllMessages.Count - 1)]; 
    }

    public static PromptMessage GetRandomCoopMessages() {
        List<PromptMessage> AllMessages = new List<PromptMessage>();
        AllMessages.Add(new PromptMessage("AS YOU CAN!", "Kill as many FLIES", "SO DO NOT DIE!", "You die, you lose!"));
        AllMessages.Add(new PromptMessage("!?", "!?", "", ""));

        return AllMessages[Random.Range(0, AllMessages.Count - 1)]; 
    }
    public static PromptMessage GetDangerMessages() {
        List<PromptMessage> AllMessages = new List<PromptMessage>();
        AllMessages.Add(new PromptMessage(" IS IN DANGER!", "Chainsaws will Kill You!", "SO DO NOT DIE!", "You die, you lose!"));
        AllMessages.Add(new PromptMessage(" IS IN DANGER!", "Finish Him!", "SO DO NOT DIE!", "You die, you lose!"));
        AllMessages.Add(new PromptMessage(" IS IN DANGER!", "Tapusin! Tapusin", "SO DO NOT DIE!", "You die, you lose!"));
        AllMessages.Add(new PromptMessage("!?", "!?", "", ""));

        return AllMessages[Random.Range(0, AllMessages.Count - 1)]; 
    }

    public static PromptMessage GetRandomTipsMessages() {
        List<PromptMessage> AllMessages = new List<PromptMessage>();
        AllMessages.Add(new PromptMessage("Getting high score on Survival and Hold Hands earns you large amount of BP!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Special Kills and Technical Kills give bonus BP!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Slow chainsaws always come from the right, and fast vertical ones always come from the left.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Press Jump once when you are fast enough to trigger Break - which stops your movement for a bit.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Time the use of your Special Skill to secure enemy kills!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Colliding on an enemy hard enough will send them flying and giving them a short amount of stun.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Hitting enemies hard gives you bonus special points.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Watch out for the Eye of Doom's Lazur Rainbow Beam!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Don't underestimate the annoying flies. They might be the cause of your untimely death.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Knowing when to use your Special Skill is the key to victory!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("You can fight Buraders you don't own yet in Survival Mode.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("You have unlimited continues in Arcade Mode! You just need to watch some cool ads if you lose frequently.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Bouncing against screen walls gives you more momentum to hit an enemy.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Not all vegetables taste bad - they're just poorly cooked.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("This game autosaves everytime. So don't worry. I got your back(end). ;)", " ", " ", " "));
        AllMessages.Add(new PromptMessage("You gain more Special points more when hitting enemies.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Hitting enemies hard SMASHES them, giving you additional Special points and stunning them for a short while.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Check your stats often in Profiles menu!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Check out the Secret Menu every now and then. You might see someone that interests you.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Bluetooth controllers work in this game! Just figure out the default controls.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("You'll earn more BPs when playing on harder difficulties.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Different Buraders have different amount of Special points needed to use their special skill.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("You gain more BP when playing with a friend!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("This game is originally made to be played with two people. Go find your friend and challenge him to a match!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Grab your friend and fight with him or her in a Head to Head match!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Finishing Arcade mode once with each Burader earns you Stars and large amount of BPs!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("You can kill those annoying flies if you hit them so hard! You'll also get bonus Special Points for doing so.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("You increase your RANK by gaining Stars from finishing Arcade Mode!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("You increase your RANK by gaining Stars from getting High Score from Survival Mode!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("You increase your RANK by gaining Stars from getting High Score from Hold Hands Mode!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("The higher your rank, the more new Buraders will be available for purchase.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Chainsaw damage gets deadlier the more you chain ORA combos before the enemy hits the chainsaw.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Stun duration from being smashed is longer when your HP Bar is empty.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Rate this game 5 STARS on Playstore and you'll win this next match for sure!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("If you find this game interesting, please rate it 5 STARS to show your great support! TYSM!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Check out ANTS SIMULATOR OYYY on Playstore - you'll be glad you did!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Remember to take a break every now and then. But I won't mind if you still play.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("You'll get a huge % boost in your Special Gauge once your HP drops 0%!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Press both direction buttons simultaneously to move in a curve faster. It takes practice to be useful, but it's worth making use of.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("RATE this game 5 STARS on the Store. NOW.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("You'll have 5 Winstreaks in Mobile Legends after you rate this app 5 Stars.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Different Stages have different weird obstacles. Which one is your favorite?", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Watch out for the Jellyfish! It zaps you in contact, stunning you for a second!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("It hurts when you ram into a flying crow.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Those are not caterpillars... they're supposed to be tardigrades :(", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Find this game easy? Change difficulty settings in Settings menu!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Find this game too hard? Change difficulty settings in Settings menu!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("You can resume your progress in Arcade mode anytime, but it will reset once you play other modes.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("God loves you so much he gave up his son so that you'll have everlasting life.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("God loves you so much he gave up his son so that you'll have everlasting life.", " ", " ", " "));
        AllMessages.Add(new PromptMessage(" ", " ", " ", " "));
        
        return AllMessages[Random.Range(0, AllMessages.Count - 1)]; 
    }
    public static PromptMessage GetRandomSubTitle() {
        List<PromptMessage> AllMessages = new List<PromptMessage>();
        AllMessages.Add(new PromptMessage("Previously known as Flappy Buraders. But they're no longer flappy, so...", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Now available in theaters near you.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("A Flappy Bird clone gone mad.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Smash your way to victory!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Also try Flappy Fighters.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Online Battles coming soon!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Challenge your friend in a local match to see who's better!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("And The Random Flies of Cavite City", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Featuring Shinmen Takezo YT and Hororo Chan! And more! Soon!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Rate 5 Stars Now! Or I'll go to your house and turn on the fan.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("And the Evil Eye of Doom!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Not to be confused with Smash Bro. It's way better.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Dragon Ball meets Flappy Bird", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Live Long And Prosper", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Music Composed by nukesheart...many, many years ago (recycled)", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Hi Boss Massimo! If ever you see this game, \r\nknow that you're the most powerful Burader in this game :P.", " ", " ", " "));
        AllMessages.Add(new PromptMessage("A new genre of 1v1 2D Fighting game!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Where everyone can fly!", " ", " ", " "));
        AllMessages.Add(new PromptMessage("Think you can make a better UI? I accept volunteer!", " ", " ", " "));
        AllMessages.Add(new PromptMessage(" ", " ", " ", " "));
        
        return AllMessages[Random.Range(0, AllMessages.Count - 1)]; 
    }
    
}
