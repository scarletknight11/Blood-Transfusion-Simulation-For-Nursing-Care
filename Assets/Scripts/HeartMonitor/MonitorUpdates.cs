using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonitorUpdates : MonoBehaviour {
	public Text hRate, spO2, pressure, respRate;
	public static MonitorUpdates Instance;
	public float updateGranularity = .1f; // How quickly the monitor updates. Needs to be longer than the length of a frame\, suggested minimum is .1

	public class LabelTween {
		public float length;
		public Text label;
		public float target;
        public string format;
        public float start;
		public LabelTween(float l, Text lbl, float t, string format, float startVal) {
			length = l;
			label = lbl;
			target = t;
            this.format = format;
            start = startVal;
		}
        public LabelTween() {

        }
	}

    public class BPTween : LabelTween {
        public float botStart;
        public float botTarget;

		public BPTween(float l, Text lbl, float t, string format, float startVal, float botStart, float botTar) {
			length = l;
			label = lbl;
			target = t;
            this.format = format;
            start = startVal;
            this.botStart = botStart;
            this.botTarget = botTar;
		}
    }

	void Awake() {
		Instance = this;
	}

	public void UpdateMonitor(string so2, string t, string bp, string hr) {
        UpdateMonitor(so2, t, bp, hr, 2.5f);
	}

    float sp02(string so2in) {
        return float.Parse(so2in.Substring(0, so2in.Length - 1)); // removes % sign at end
    }

    float bpTop(string bp) {
        return float.Parse(bp.Substring(0, bp.IndexOf('/')));
    }

    float bpBot(string bp) {
        return float.Parse(bp.Substring(bp.IndexOf('/') + 1));

    }
	
	public void UpdateMonitor(string so2, string t, string bp, string hr, float seconds) {
        StopCoroutine("MonitorTween");
        StopCoroutine("PressureTween");

		StartCoroutine("MonitorTween", new LabelTween(seconds, spO2, sp02(so2), "{0:0}%",sp02(spO2.text)));
        StartCoroutine("MonitorTween", new LabelTween(seconds, hRate, float.Parse(hr), "{0:0}", float.Parse(hRate.text)));
        StartCoroutine("MonitorTween", new LabelTween(seconds, respRate, float.Parse(t), "{0:0}", float.Parse(hRate.text)));

        float presTop = bpTop(bp);
        float presBot = bpBot(bp);
        float startTop = bpTop(pressure.text);
        float startBot = bpBot(pressure.text);

        BPTween bpt = new BPTween(seconds, pressure, presTop, "{0:0}/{1:0}", startTop, startBot, presBot);

        StartCoroutine("PressureTween", bpt);
	}

    IEnumerator PressureTween(BPTween bt) {
        yield return null;
		
        float t = 0;
        float c = 0f;
        float c2 = 0f;
        float granularity = 8f;

        while (t < bt.length) {
            t += updateGranularity;

            if (t > bt.length) {
                t = bt.length;
			}

            c = Mathf.Lerp(bt.start, bt.target, t / bt.length);
            c2 = Mathf.Lerp(bt.botStart, bt.botTarget, t / bt.length);
            bt.label.text = string.Format(bt.format, c, c2);

            yield return new WaitForSeconds(granularity);
        }

        while (true) { // Fluctuates the heart rate a bit
            float tar = bt.target * Random.Range(.95f, 1.05f);
            float tar2 = bt.botTarget * Random.Range(.95f, 1.05f);
            float lerpSpeed = Random.Range(3f, 5.5f);

            t = 0f;

            float up = granularity * Random.Range(.95f, 1.1f);
            while (t < lerpSpeed) {
                bt.label.text = string.Format(bt.format, Mathf.Lerp(c, tar, t / lerpSpeed), Mathf.Lerp(c2, tar, t / lerpSpeed));
                t += up;
                yield return new WaitForSeconds(up);
            }

            c = tar;
            c2 = tar2;
        }
    }

	IEnumerator MonitorTween(LabelTween lt) {
        yield return null;
		
        float t = 0;
		float start = lt.start;
        float c = 0f;
        float granularity = updateGranularity;

        if (lt.label != hRate) {
            granularity = 8f;
        }

		while(t < lt.length) {
            t += granularity;

			if(t > lt.length) {
				t = lt.length;
			}

			c = Mathf.Lerp(start, lt.target, t/lt.length);
			lt.label.text = string.Format(lt.format,c);

            yield return new WaitForSeconds(granularity);
		}

		while(true) { // Fluctuates the heart rate a bit
			float tar = lt.target * Random.Range(.95f,1.05f);
			float lerpSpeed = Random.Range (3f,5.5f);

			t = 0f;

			while(t < lerpSpeed) {
				lt.label.text = string.Format(lt.format,Mathf.Lerp(c, tar, t/lerpSpeed));

                t += granularity;

                yield return new WaitForSeconds(granularity);
			}

            c = tar;
		}
	}
}
