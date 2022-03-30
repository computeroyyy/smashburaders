using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;
public class BGMS {
        public const string TITLE = "BGM_TITLE";
		public const string SILENT = "BGM_SILENT";
		public const string LOST = "BGM_LOST";
        public const string SUBMERGED = "BGM_SUBMERGED";
        public const string REALTIME = "BGM_REALTIME";
        public const string DISTORTED = "BGM_DISTORTED";
		public const string EXTRACTUM = "BGM_EXTRACTUM";
		public const string TERMINUS = "BGM_TERMINUS";
		public const string RAINING = "BGM_RAINING";
		public const string PRELUDE = "BGM_PRELUDE";
		public const string PILGRIM = "BGM_PILGRIM";
		public const string ECCLESIASTES = "BGM_ECCLESIASTES";
		public const string PSALM6 = "BGM_PSALM6";

    }
    public class SFXS {
        public const string JUMP = "SFX_JUMP";
        public const string TOGGLE = "SFX_TOGGLE";
        public const string READY = "SFX_READY";
        public const string MUNCH = "SFX_MUNCH";
        public const string HIT0 = "SFX_HIT0";
        public const string HIT1 = "SFX_HIT1";
        public const string HIT2 = "SFX_HIT2";
        public const string HIT3 = "SFX_HIT3";
        public const string HIT4 = "SFX_HIT4";
        public const string HIT5 = "SFX_HIT5";
		public const string POP = "SFX_POP";
        public const string SLASH = "SFX_SLASH";
        public const string MECHA = "SFX_MECHA";
        public const string HEALS = "SFX_HEALS";
        public const string TING = "SFX_TING";

        public const string SPECIAL = "SFX_SPECIAL_";
        public const string OHNO = "SFX_OHNO";
        public const string WHOWILLSURVIVE = "SFX_WHOWILLSURVIVE";
        public const string FIGHT = "SFX_FIGHT";
        public const string VICTORY = "SFX_VICTORY";
		public const string EXPLOSION = "SFX_EXPLOSION";
		public const string CHUG = "SFX_CHUG";
		public const string POWERUP = "SFX_POWERUP";
		public const string SHOT = "SFX_SHOT";
		public const string MAGIC = "SFX_MAGIC";
		public const string ICE = "SFX_ICE";
		public const string OLA = "SFX_OLA";
		public const string HEAVYHIT = "SFX_HEAVYHIT";
		public const string MEDIUMHIT = "SFX_MEDIUMHIT";
		public const string HIDE = "SFX_HIDE";
		public const string INSTANT = "SFX_INSTANT";
		public const string SMALLHIT = "SFX_SMALLHIT";
		public const string WINDEXPLODE = "SFX_WINDEXPLODE";
		public const string SPLASH = "SFX_SPLASH";
		public const string DASH = "SFX_DASH";
		public const string HORORO = "SFX_HORORO";
		public const string STATIC = "SFX_STATIC";
		public const string GUNFIRE = "SFX_GUNFIRE";
		public const string GUNHIT = "SFX_GUNHIT";

    }
	public class VOXS {
		public const string VICTORY = "VOX_VICTORY";
		public const string TECHNICALKILL = "VOX_TECHNICALKILL";
		public const string SPECIALKILL = "VOX_SPECIALKILL";
		public const string SURVIVAL = "VOX_SURVIVAL";
		public const string IMPOSSIBLE = "VOX_IMPOSSIBLE";
		public const string GROOT = "VOX_GROOT";
		public const string HIGHSCORE = "VOX_HIGHSCORE";
		public const string ENGAGE = "VOX_ENGAGE";
		public const string FOCUS = "VOX_FOCUS";
		public const string DONTDIE = "VOX_DONTDIE";
		public const string DEFEAT = "VOX_DEFEAT";
		public const string COUNTER = "VOX_COUNTER";
		public const string CHAVAGE = "VOX_CHAVAGE";
		public const string BETTERLUCK = "VOX_BETTERLUCK";
		public const string V4X = "VOX_4X";
		public const string V3X = "VOX_3X";
		public const string V2X = "VOX_2X";
		public const string V1X = "VOX_1X";
		public const string THE_EYE = "VOX_THE_EYE";
		public const string NOOBDED = "VOX_NOOBDED";
		public const string OLA = "VOX_OLA";
		public const string DESTROYED = "VOX_DESTROYED";
		public const string KONOBANGUMI = "VOX_KONOBANGUMI";
		public const string THANKYOU = "VOX_THANKYOU";
		public const string WHOWILLSURVIVE = "VOX_WHOWILLSURVIVE";
		public const string REMATCH = "VOX_REMATCH";
		public const string SHUTDOWN = "VOX_SHUTDOWN";
		public const string FIRSTBLOOD = "VOX_FIRSTBLOOD";
		public const string HAROWARUDO = "VOX_HAROWARUDO";
		public const string DANGER = "VOX_DANGER";

	}
public class AudioManager : MonoBehaviour {

	public BGM[] BGMs;
	public SFX[] SFXs;
	public VOX[] VOXs;
	

	void Start(){
		AudioListener.volume = 1f;
	}
	public static AudioManager instance;

	void Awake () {
		if (instance == null)
			instance = this;
		else {
			Destroy(this.gameObject);
			return;
		}
		
		DontDestroyOnLoad(gameObject);
		
		foreach (BGM bgm in BGMs) {
			bgm.source = gameObject.AddComponent<AudioSource>();
			bgm.source.clip = bgm.clip;
			bgm.source.volume = bgm.volume;
			bgm.source.loop = bgm.loop;
		}
		foreach (SFX sfx in SFXs) {
			sfx.source = gameObject.AddComponent<AudioSource>();
			sfx.source.clip = sfx.clip;
			sfx.source.volume = sfx.volume;
			sfx.source.loop = sfx.loop;
		}
		foreach (VOX vox in VOXs) {
			vox.source = gameObject.AddComponent<AudioSource>();
			vox.source.clip = vox.clip;
			vox.source.volume = 0.5f;
			vox.source.loop = false;
		}
	}

	/// <summary>
	/// play BGM by name
	/// </summary>
	/// <param name="name"></param>
	public void PlayBGM (string name) {
		StopAll();
		BGM s =  Array.Find(BGMs, sound => sound.name == name);
		if (s == null) 
			return;
		s.source.Play();
	}
	public void PlaySFX (string name) {
		SFX s =  Array.Find(SFXs, sound => sound.name == name);
		if (s == null) 
			return;
		s.source.Play();
	}
	public void PlayVOX (string name) {
		VOX s =  Array.Find(VOXs, sound => sound.name == name);
		if (s == null) 
			return;
		s.source.Play();
	}

	/// <summary>
	/// stop sound by name
	/// </summary>
	/// <param name="name"></param>
	public void StopName (string name) {
	BGM s =  Array.Find(BGMs, sound => sound.name == name);
		if (s == null) 
			return;
		s.source.Stop();
	SFX x =  Array.Find(SFXs, sound => sound.name == name);
		if (x == null) 
			return;
		x.source.Stop();
	}
	/// <summary>
	/// stops all sounds
	/// </summary>
	public void StopAll () {
		foreach (BGM s in BGMs)
			s.source.Stop();
	}
	public void MuteAll () {
		foreach (BGM s in BGMs)
			s.source.volume = 0;
	}
	public void UnMuteAll () {
		foreach (BGM s in BGMs)
			s.source.volume = 0.5f;
	}
	public bool IsSFXPlaying (string name) {
	SFX x =  Array.Find(SFXs, sound => sound.name == name);
		if (x != null) {
			if (x.source.isPlaying)
				return true;
			else 
				return false;
		}
		else {
			return false;
		}
			
	}
	public bool IsBGMPlaying (string name) {
	BGM x =  Array.Find(BGMs, sound => sound.name == name);
		if (x != null) {
			if (x.source.isPlaying)
				return true;
			else 
				return false;
		}
		else {
			return false;
		}
			
	}
}