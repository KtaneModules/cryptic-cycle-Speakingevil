using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;
using KModkit;

public class CrypticCycleScript : MonoBehaviour
{

    public KMAudio Audio;
    public KMBombInfo bomb;
    public List<KMSelectable> keys;
    public GameObject[] dials;
    public GameObject[] dialcanvas;
    public Renderer[] dialbg;
    public Material[] dialcol;
    public TextMesh[] dialText;
    public TextMesh disp;
    public Renderer[] matStore;
    public Renderer[] glitch;
    public MeshRenderer bg;
    public Material legible;
    public Font engalph;

    private int rl
    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!\"£$%^&*()[]{}<>";
    private string[] glyphset = new string[3] { "ABCDEFGHIJKLMNOPQRSTUVWXYZ", "abcdefghijklmnopqrstuvwxyz", "0123456789!\"£$%^&*()[]{}<>" };
    private string[] ciphset = new string[4] {  "GORIYSHQBFLPZATNKVCUJMDEXW", "VENJXDHPSCRGKBMQLYAFTWZIUO", "CMFOVGIANRQWBJDHTZEYKXULPS", "XASDTWRPUJZFYQGEILCKVOHNMB" };
    private string[][] message = new string[2][] {new string[100] { "ADVANCED", "ADDITION", "ALLOCATE", "ALTERING", "BINARIES", "BILLIONS", "BULKHEAD", "BULLETED", "CIPHERED", "CIRCUITS", "COMPUTER", "CONTINUE", "DECRYPTS", "DIVISION", "DISCOVER", "DISPOSAL", "EXAMINED", "EXAMPLES", "EQUATION", "EQUIPPED", "FINISHED", "FINDINGS", "FORTRESS", "FORWARDS", "GAUNTLET", "GAMBLING", "GATHERED", "GLOOMING", "HAZARDED", "HAZINESS", "HUNKERED", "HUNTSMAN", "INDICATE", "INDIGOES", "ILLUSION", "ILLUMINE", "JIGSAWED", "JIMMYING", "JUNCTION", "JUDGMENT", "KILOWATT", "KINETICS", "KNOCKOUT", "KNUCKLED", "LIMITING", "LINEARLY", "LINKAGES", "LABELING", "MONOGRAM", "MONOTONE", "MULTIPLY", "MULLIGAN", "NANOGRAM", "NANOTUBE", "NUMBERED", "NUMERALS", "OCTANGLE", "OCTUPLES", "OBSERVED", "OBSCURED", "PROGRESS", "PROJECTS", "POSITION", "POSITIVE", "QUADRANT", "QUADPLEX", "QUICKEST", "QUINTICS", "REVERSED", "REVOLVED", "ROTATION", "RELATION", "STARTING", "STANDARD", "STOPPING", "STOPWORD", "TRIGGERS", "TRIANGLE", "TOGGLING", "TOGETHER", "UNDERRUN", "UNDERLIE", "ULTIMATE", "ULTRARED", "VICINITY", "VICELESS", "VOLTAGES", "VOLATILE", "WINGDING", "WINNABLE", "WHATEVER", "WHATNOTS", "YELLOWED", "YEASAYER", "YIELDING", "YOURSELF", "ZIPPERED", "ZIGZAGGY", "ZUGZWANG", "ZYMOGRAM" }
                                                 ,new string[100] { "FORWARDS", "JIGSAWED", "HAZARDED", "NUMERALS", "MONOTONE", "QUICKEST", "TOGETHER", "YOURSELF", "DISPOSAL", "HUNKERED", "ILLUSION", "BULLETED", "VOLATILE", "STARTING", "FORTRESS", "STANDARD", "GLOOMING", "MULTIPLY", "ULTRARED", "BILLIONS", "NANOGRAM", "KNUCKLED", "YEASAYER", "JIMMYING", "PROJECTS", "KILOWATT", "QUINTICS", "QUADRANT", "POSITION", "LINEARLY", "ALTERING", "BINARIES", "OBSCURED", "LABELING", "ZUGZWANG", "VOLTAGES", "UNDERLIE", "COMPUTER", "INDICATE", "ZYMOGRAM", "JUNCTION", "CIPHERED", "MULLIGAN", "HUNTSMAN", "REVERSED", "NUMBERED", "POSITIVE", "ZIGZAGGY", "YELLOWED", "OCTUPLES", "ROTATION", "GATHERED", "CIRCUITS", "OBSERVED", "YIELDING", "CONTINUE", "EQUIPPED", "BULKHEAD", "ILLUMINE", "ALLOCATE", "STOPPING", "LIMITING", "TRIGGERS", "LINKAGES", "MONOGRAM", "HAZINESS", "WHATEVER", "DISCOVER", "TOGGLING", "PROGRESS", "NANOTUBE", "FINISHED", "VICELESS", "WINGDING", "EXAMINED", "EXAMPLES", "QUADPLEX", "KNOCKOUT", "DECRYPTS", "UNDERRUN", "OCTANGLE", "RELATION", "ZIPPERED", "EQUATION", "GAUNTLET", "WINNABLE", "ULTIMATE", "ADVANCED", "STOPWORD", "INDIGOES", "KINETICS", "GAMBLING", "ADDITION", "TRIANGLE", "WHATNOTS", "DIVISION", "JUDGMENT", "REVOLVED", "VICINITY", "FINDINGS" } };
    private string[][] operation = new string[9][]
       { new string[9]{ string.Empty, string.Empty, string.Empty, string.Empty, "AND", string.Empty, string.Empty, string.Empty, string.Empty},
            new string[9]{ string.Empty, string.Empty, string.Empty, "XNOR", string.Empty, "XOR", string.Empty, string.Empty, string.Empty},
                new string[9]{ string.Empty, string.Empty, "NOR", string.Empty, "=>", string.Empty, "NOR", string.Empty, string.Empty},
                    new string[9]{ string.Empty, "XOR", string.Empty, "<=", string.Empty, "<=", string.Empty, "XNOR", string.Empty},
                         new string[9]{ "AND", string.Empty, "=>", string.Empty, "OR", string.Empty, "=>", string.Empty, "AND"},
                    new string[9]{ string.Empty, "XNOR", string.Empty, "<=", string.Empty, "<=", string.Empty, "XOR", string.Empty},
                new string[9]{ string.Empty, string.Empty, "NOR", string.Empty, "=>", string.Empty, "NOR", string.Empty, string.Empty},
            new string[9]{ string.Empty, string.Empty, string.Empty, "XOR", string.Empty, "XNOR", string.Empty, string.Empty, string.Empty},
         new string[9]{ string.Empty, string.Empty, string.Empty, string.Empty, "AND", string.Empty, string.Empty, string.Empty, string.Empty}};
    private string[] op = new string[2];
    private string[] keyboards = new string[3];
    private string[] ciphertext = new string[2];
    private string[] logtext = new string[2];
    private string[] logset = new string[8];
    private string[][] logilog = new string[2][] { new string[8], new string[8] };
    private string answer;
    private int ansSet;
    private string[] anslog = new string[8];
    private int[][] rot = new int[2][] { new int[8], new int[8] };
    private int[] labelrot = new int[8];
    private int[] setpick = new int[8];
    private int pressCount;
    private int keyset;
    private bool switching;
    private bool moduleSolved;

    //Logging
    static int moduleCounter = 1;
    int moduleID;

    private void Awake()
    {
        moduleID = moduleCounter++;
        foreach (KMSelectable key in keys)
        {
            int k = keys.IndexOf(key);
            key.OnInteract += delegate () { KeyPress(k); return false; };
        }
    }

    void Start()
    {
        foreach (Renderer store in matStore)
        {
            store.enabled = false;
        }
        Reset();
    }

    private void KeyPress(int k)
    {
        keys[k].AddInteractionPunch(0.125f);
        if (moduleSolved == false && switching == false)
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            if (k == 26)
            {
                if (pressCount > 0)
                {
                    pressCount--;
                    answer = answer.Remove(answer.Length - 1);
                }
                ansSet -= ansSet % 10;
                ansSet /= 10;
            }
            else if(k == 27)
            {
                keyset = (keyset + 2) % 3;
                StartCoroutine(KeySet());

            }
            else if (k == 28)
            {
                keyset = (keyset + 1) % 3;
                StartCoroutine(KeySet());
            }
            else
            {
                for(int i = 0; i < 3; i++)
                {
                    if (glyphset[i].Contains(keyboards[keyset][k]))
                    {
                        int f = glyphset[i].IndexOf(keyboards[keyset][k]);
                        anslog[pressCount] = glyphset[0][f].ToString();
                        break;
                    }
                }
                pressCount++;
                answer = answer + keyboards[keyset][k];
                ansSet *= 10;
                for(int i = 0; i < 3; i++)
                {
                    if (glyphset[i].Contains(keyboards[keyset][k]))
                    {
                        ansSet += i + 1;
                        break;
                    }
                }
            }
            disp.text = answer;
            if (pressCount == 8)
            {
                int setcode = 0;
                for(int i = 0; i < 8; i++)
                {
                    setcode *= 10;
                    setcode += setpick[i] + 1;
                }
                if (answer == ciphertext[1] && ansSet == setcode)
                {
                    moduleSolved = true;
                    Audio.PlaySoundAtTransform("InputCorrect", transform);
                    disp.color = new Color32(0, 255, 0, 255);
                }
                else
                {
                    GetComponent<KMBombModule>().HandleStrike();
                    disp.color = new Color32(255, 0, 0, 255);
                    Debug.LogFormat("[Cryptic Cycle #{0}]The submitted response was {1} | {2}: Resetting", moduleID, string.Join(string.Empty, anslog), ansSet);
                }
                Reset();
            }
        }
    }

    private void Reset()
    {
        StopAllCoroutines();
        if (moduleSolved == false)
        {
            pressCount = 0;
            ansSet = 0;
            answer = string.Empty;
            r = Random.Range(0, 100);
            string[][] roh = new string[2][] { new string[8], new string[8]};
            List<string>[] ciph = new List<string>[] { new List<string> { }, new List<string> { } };
            List<string>[] log = new List<string>[2] { new List<string> { }, new List<string> { } };
            List<string>[] keysetup = new List<string>[3] { new List<string> { }, new List<string> { }, new List<string> { } };
            List<char> alph = alphabet.ToCharArray().ToList();
            for(int i = 0; i < 78; i++)
            {
                int rand = Random.Range(0, 78 - i);
                keysetup[Mathf.FloorToInt(i / 26)].Add(alph[rand].ToString());
                alph.RemoveAt(rand);
            }
            for(int i = 0; i < 3; i++)
            {
                keyboards[i] = string.Join(string.Empty, keysetup[i].ToArray());
            }
            for (int i = 0; i < 8; i++)
            {
                dialText[i].text = string.Empty;
                rot[1][i] = rot[0][i];
                rot[0][i] = Random.Range(0, 4);
                labelrot[i] = Random.Range(0, 4);
                setpick[i] = Random.Range(0, 3);
                roh[0][i] = ((bomb.GetPortCount() + rot[0][i]) % 2).ToString();
                roh[1][i] = (labelrot[i] % 2).ToString();
                logset[i] = (setpick[i] + 1).ToString();
            }
            int[][] pos = new int[2][] { new int[2], new int[2]};
            for(int i = 0; i < 8; i++)
            {
                switch(rot[0][i])
                {
                    case 0:
                        pos[i % 2][1]--;
                        break;
                    case 1:
                        pos[i % 2][0]++;
                        break;
                    case 2:
                        pos[i % 2][1]++;
                        break;
                    case 3:
                        pos[i % 2][0]--;
                        break;
                }
            }
            for(int i = 0; i < 2; i++)
            {
                op[i] = operation[4 + pos[i][1]][4 + pos[i][0]];
            }
            if(op[1] == op[0])
            {
                op[1] = "NAND";
            }
            bool[][] truth = new bool[8][] { new bool[2], new bool[2], new bool[2], new bool[2], new bool[2], new bool[2], new bool[2], new bool[2] };
            for(int i = 0; i < 8; i++)
            {
                int[] bits = new int[2] { (rot[0][i] + bomb.GetPortCount()) % 2, labelrot[i] % 2 };
                for (int j = 0; j < 2; j++)
                {                  
                    switch (op[j])
                    {
                        case "AND":
                            truth[i][j] = bits[0] == 1 && bits[1] == 1;
                            break;
                        case "NOR":
                            truth[i][j] = bits[0] == 0 && bits[1] == 0;
                            break;
                        case "XOR":
                            truth[i][j] = bits[0] != bits[1];
                            break;
                        case "XNOR":
                            truth[i][j] = bits[0] == bits[1];
                            break;
                        case "=>":
                            truth[i][j] = !(bits[1] == 0 && bits[0] == 1);
                            break;
                        case "<=":
                            truth[i][j] = !(bits[0] == 0 && bits[1] == 1);
                            break;
                        case "OR":
                            truth[i][j] = bits[0] == 1 || bits[1] == 1;
                            break;
                        case "NAND":
                            truth[i][j] = bits[0] == 0 || bits[1] == 0;
                            break;
                    }
                    if (truth[i][j])
                    {
                        logilog[j][i] = "T";
                    }
                    else
                    {
                        logilog[j][i] = "F";
                    }
                }
                for (int j = 0; j < 2; j++)
                {
                    if (truth[i][0] && truth[i][1])
                    {
                        ciph[j].Add(glyphset[setpick[i]][glyphset[0].IndexOf(ciphset[0][glyphset[0].IndexOf(message[j][r][i])])].ToString());
                        log[j].Add(ciphset[0][glyphset[0].IndexOf(message[j][r][i])].ToString());
                    }
                    else if (truth[i][0] && !truth[i][1])
                    {
                        ciph[j].Add(glyphset[setpick[i]][glyphset[0].IndexOf(ciphset[1][glyphset[0].IndexOf(message[j][r][i])])].ToString());
                        log[j].Add(ciphset[1][glyphset[0].IndexOf(message[j][r][i])].ToString());
                    }
                    else if (!truth[i][0] && truth[i][1])
                    {
                        ciph[j].Add(glyphset[setpick[i]][glyphset[0].IndexOf(ciphset[2][glyphset[0].IndexOf(message[j][r][i])])].ToString());
                        log[j].Add(ciphset[2][glyphset[0].IndexOf(message[j][r][i])].ToString());
                    }
                    else
                    {
                        ciph[j].Add(glyphset[setpick[i]][glyphset[0].IndexOf(ciphset[3][glyphset[0].IndexOf(message[j][r][i])])].ToString());
                        log[j].Add(ciphset[3][glyphset[0].IndexOf(message[j][r][i])].ToString());
                    }                  
                }
            }
            ciphertext[0] = string.Join(string.Empty, ciph[0].ToArray());
            ciphertext[1] = string.Join(string.Empty, ciph[1].ToArray());
            logtext[0] = string.Join(string.Empty, log[0].ToArray());
            logtext[1] = string.Join(string.Empty, log[1].ToArray());
            Debug.LogFormat("[Cryptic Cycle #{0}]The encrypted message was {1}", moduleID, logtext[0]);
            Debug.LogFormat("[Cryptic Cycle #{0}]The glyph sets were {1}", moduleID, string.Join(", ", logset));
            Debug.LogFormat("[Cryptic Cycle #{0}]The dial bits were {1}", moduleID, string.Join(", ", roh[0]));
            Debug.LogFormat("[Cryptic Cycle #{0}]The label bits were {1}", moduleID, string.Join(", ", roh[1]));
            Debug.LogFormat("[Cryptic Cycle #{0}]The operators were {1}", moduleID, string.Join(", ", op));
            Debug.LogFormat("[Cryptic Cycle #{0}]The {1} operator returned: {2}", moduleID, op[0], string.Join(", ", logilog[0]));
            Debug.LogFormat("[Cryptic Cycle #{0}]The {1} operator returned: {2}", moduleID, op[1], string.Join(", ", logilog[1]));
            Debug.LogFormat("[Cryptic Cycle #{0}]The deciphered message was {1}", moduleID, message[0][r]);
            Debug.LogFormat("[Cryptic Cycle #{0}]The response word was {1}", moduleID, message[1][r]);
            Debug.LogFormat("[Cryptic Cycle #{0}]The correct response was {1} | {2}", moduleID, logtext[1], string.Join(string.Empty, logset));
        }
        StartCoroutine(DialSet());
    }

    private IEnumerator DialSet()
    {
        switching = true;
        int[] spin = new int[8];
        bool[] set = new bool[8];
        for (int i = 0; i < 8; i++)
        {
            dialbg[i].material = dialcol[0];
            if (moduleSolved == false)
            {
                spin[i] = rot[0][i] - rot[1][i];
            }
            else
            {
                spin[i] = -rot[0][i];
            }
            if (spin[i] < 0)
            {
                spin[i] += 4;
            }
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (spin[j] == 0)
                {
                    if (set[j] == false)
                    {
                        set[j] = true;
                        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, transform);
                        if (moduleSolved == false)
                        {
                            dialcanvas[j].transform.localEulerAngles = new Vector3(0, 0, labelrot[j] * 90);
                            dialbg[j].material = dialcol[1];
                            dialText[j].text = ciphertext[0][j].ToString();
                        }
                        else
                        {
                            switch (j)
                            {
                                case 0:
                                    dialText[j].text = "G";
                                    break;
                                case 3:
                                    dialText[j].text = "D";
                                    break;
                                case 4:
                                    dialText[j].text = "W";
                                    break;
                                case 6:
                                    dialText[j].text = "R";
                                    break;
                                case 7:
                                    dialText[j].text = "K";
                                    break;
                                default:
                                    dialText[j].text = "O";
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    dials[j].transform.localEulerAngles += new Vector3(0, 0, 90);
                    spin[j]--;
                }
            }
            if (i < 7)
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
        if (moduleSolved == true)
        {
            Audio.PlaySoundAtTransform("Reveal", transform);
            for (int i = 0; i < 21; i++)
            {
                foreach(Renderer g in glitch)
                {

                    int rand = Random.Range(0, 2);
                    if(rand == 1 && i < 20)
                    {
                        g.enabled = true;
                    }
                    else
                    {
                        g.enabled = false;
                    }
                }
                yield return new WaitForSeconds(0.05f);
            }
            bg.material = dialcol[4];
            keys[27].transform.localPosition += new Vector3(0, 0, 2);
            keys[28].transform.localPosition += new Vector3(0, 0, 2);
            for (int i = 0; i < 8; i++)
            {
                dialcanvas[i].transform.localEulerAngles = new Vector3(0, 0, 0);
                dials[i].GetComponent<Renderer>().material = dialcol[3];
                dialText[i].GetComponent<Renderer>().material = legible;
                dialText[i].font = engalph;
                dialText[i].color = new Color32(0, 255, 0, 255);
                dialbg[i].material = dialcol[3];
            }
            foreach (KMSelectable key in keys)
            {
                int k = keys.IndexOf(key);
                if (k < 26)
                {
                    TextMesh keytext = key.GetComponentInChildren<TextMesh>();
                    key.GetComponent<Renderer>().material = dialcol[2];
                    keytext.GetComponent<Renderer>().material = legible;
                    keytext.font = engalph;
                    keytext.text = "QWERTYUIOPASDFGHJKLZXCVBNM"[k].ToString();
                    keytext.color = new Color32(255, 255, 255, 255);
                }
            }
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, transform);
            GetComponent<KMBombModule>().HandlePass();
        }
        else
        {
            keyset = 0;
            StartCoroutine(KeySet());
        }
        disp.text = string.Empty;
        disp.color = new Color32(255, 255, 255, 255);
        switching = false;
        yield return null;
    }

    private IEnumerator KeySet()
    {
        switching = true;
        foreach (KMSelectable key in keys)
        {
            int k = keys.IndexOf(key);
            if (k < 26)
            {
                key.transform.localPosition += new Vector3(0, 0, 0.6f);
                key.GetComponent<Renderer>().material = dialcol[2];
                key.GetComponentInChildren<TextMesh>().text = string.Empty;
            }
        }
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.WireSequenceMechanism, transform);
        yield return new WaitForSeconds(1f);
        switching = false;
        foreach (KMSelectable key in keys)
        {
            int k = keys.IndexOf(key);
            if (k < 26)
            {
                key.transform.localPosition -= new Vector3(0, 0, 0.6f);
                key.GetComponent<Renderer>().material = dialcol[0];
                key.GetComponentInChildren<TextMesh>().text = keyboards[keyset][k].ToString();
            }
        }
    }
#pragma warning disable 414
    private string TwitchHelpMessage = "!{0} ABCDEFGH [Presses keys in the positions of the letters on a QWERTY keyboard] | !{0} left/right [Cycles between keyboards] | !{0} delete [Deletes last input] !{0} cancel [Deletes all inputs]";
#pragma warning restore 414
    IEnumerator ProcessTwitchCommand(string command)
    {

        if (command.ToLowerInvariant() == "cancel")
        {
            for (int i = 0; i < 8; i++)
            {
                KeyPress(26);
            }
            yield return null;
        }
        else if(command.ToLowerInvariant() == "delete")
        {
            KeyPress(26);
            yield return null;
        }
        else if(command.ToLowerInvariant() == "left")
        {
            KeyPress(27);
            yield return null;
        }
        else if (command.ToLowerInvariant() == "right")
        {
            KeyPress(28);
            yield return null;
        }
        else
        {
            command = command.ToUpperInvariant();
            var word = Regex.Match(command, @"^\s*([A-Z\-]+)\s*$");
            if (!word.Success)
            {
                yield break;
            }
            command = command.Replace(" ", string.Empty);
            foreach (char letter in command)
            {
                KeyPress("QWERTYUIOPASDFGHJKLZXCVBNM".IndexOf(letter));
                yield return new WaitForSeconds(0.125f);
            }
            yield return null;
        }
    }
}
