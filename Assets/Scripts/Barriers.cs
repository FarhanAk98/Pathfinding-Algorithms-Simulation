using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Barriers : MonoBehaviour
{
    public Image Block;
    public GameObject Cover, Reset;
    public TMP_Dropdown drp, drp2;
    public static Tiles t = new Tiles();
    public static bool done = false, found = false;
    public static List<Image> path;
    public static int sy, sx, ey, ex, Alg, Dir;
    public static Dijkstras Dig;
    public static AStar Ast;
    public static BreadthFirstSearch bfs;
    public static BestFirstSearch befs;
    Color black, white, red, green, RedPrev, GreenPrev, yellow;
    Image RedImg, GreenImg;
    string color = "Black";
    bool MouseClicked = false;
    float Pos = -85.5f, Posy = -38.25f;
    int i, k, n;
    void Start()
    {
        Dig = this.GetComponent<Dijkstras>();
        Ast = this.GetComponent<AStar>();
        bfs = this.GetComponent<BreadthFirstSearch>();
        befs = this.GetComponent<BestFirstSearch>();
        path = new List<Image>();
        Cover.SetActive(false);
        Reset.SetActive(false);
        Alg = 0;
        Dir = 0;
        i = 0;
        k = 0;
        n = 1;
        while (Pos <= 85.5f)
        {
            Image img = Instantiate(Block, transform.position, Quaternion.identity);
            img.rectTransform.SetParent(this.transform);
            img.rectTransform.position = new Vector3(Pos, Posy, transform.position.z);
            t.ADDT(img, k, i);
            img.name = n.ToString();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) => { ColorChange(img, false); });
            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.PointerDown;
            entry2.callback.AddListener((data) => { ColorChange(img, true); });
            img.GetComponent<EventTrigger>().triggers.Add(entry);
            img.GetComponent<EventTrigger>().triggers.Add(entry2);
            n++;
            i++;
            Pos += 2.25f;
            if (Pos > 85.5f && Posy <= 45f)
            {
                Posy += 2.25f;
                Pos = -85.5f;
                i = 0;
                k++;
            }
        }
        black = new Color32(0, 0, 0, 255);
        white = new Color32(255, 255, 255, 255);
        red = new Color32(255, 0, 0, 255);
        green = new Color32(0, 255, 0, 255);
        yellow = new Color32(255, 255, 0, 255);
        sy = 19;
        sx = 5;
        ey = 19;
        ex = 71;
        t.GetImage(sy, sx).color = red;
        t.GetImage(ey, ex).color = green;
        RedImg = t.GetImage(sy, sx);
        GreenImg = t.GetImage(ey, ex);
        RedPrev = white;
        GreenPrev = white;
    }
    public void SetColor(string col)
    {
        color = "";
        color = col;
    }
    public void Done()
    {
        if (!found)
        {
            t.GetImage(ey, ex).GetComponent<Advance>().Dest = true;
            if (drp2.value == 1)
            {
                t.GetImage(ey, ex).GetComponent<Advance>().visited2 = true;
                t.GetImage(ey, ex).GetComponent<Advance>().Dest = true;
                t.GetImage(ey, ex).GetComponent<Advance>().visited = false;
                t.GetImage(ey, ex).GetComponent<Advance>().yp = -1;
                t.GetImage(ey, ex).GetComponent<Advance>().xp = -1;
                t.GetImage(ey, ex).GetComponent<Advance>().cost = 0;
            }
            Alg = drp.value;
            Dir = drp2.value;
            t.GetImage(sy, sx).GetComponent<Advance>().visited = true;
            t.GetImage(sy, sx).GetComponent<Advance>().visited2 = false;
            t.GetImage(sy, sx).GetComponent<Advance>().yp = -1;
            t.GetImage(sy, sx).GetComponent<Advance>().xp = -1;
            t.GetImage(sy, sx).GetComponent<Advance>().cost = 0;
            if (drp.value == 0)
            {
                if(drp2.value == 1)
                {
                    Dig.Adv2(ey, ex);
                }
                Dig.Adv2(sy, sx);
            }
            else if (drp.value == 1)
            {
                Ast.MinMax(sy, sx, ey, ex);
                if (drp2.value == 1)
                {
                    Ast.Adv2(ey, ex);
                }
                Ast.Adv2(sy, sx);
            }
            else if (drp.value == 2)
            {
                if (drp2.value == 1)
                {
                    bfs.Adv2(ey, ex);
                }
                bfs.Adv2(sy, sx);
            }
            else if (drp.value == 3)
            {
                befs.Adv2(sy, sx);
            }
            done = true;
            Cover.SetActive(true);
            Reset.SetActive(true);
        }
    }
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            MouseClicked = true;
        }
        else
        {
            MouseClicked = false;
        }
        if(drp.value == 3)
        {
            drp2.gameObject.SetActive(false);
            drp2.value = 0;
        }
        else
        {
            drp2.gameObject.SetActive(true);
        }
    }
    public void ColorChange(Image img, bool click)
    {
        if ((MouseClicked || click) && !done && !found)
        {
            if (color.Equals("Black") && img.color != red && img.color != green)
            {
                img.color = black;
            }
            else if (color.Equals("Red") && img.color != green)
            {
                RedImg.color = RedPrev;
                RedPrev = img.color;
                img.color = red;
                RedImg = img;
                sy = t.GetY(img);
                sx = t.GetX(img);
            }
            else if (color.Equals("Green") && img.color != red)
            {
                GreenImg.color = GreenPrev;
                GreenPrev = img.color;
                img.color = green;
                GreenImg = img;
                ey = t.GetY(img);
                ex = t.GetX(img);
            }
            else if(color.Equals("White") && img.color != red && img.color != green)
            {
                img.color = white;
            }
        }
    }
    public void ClearBlack()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<Image>().color == black)
            {
                transform.GetChild(i).GetComponent<Image>().color = white;
            }
        }
    }
    public void ClearScreen()
    {
        done = false;
        found = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            if ((transform.GetChild(i).GetComponent<Advance>().visited || transform.GetChild(i).GetComponent<Advance>().visited2 || transform.GetChild(i).GetComponent<Advance>().start || transform.GetChild(i).GetComponent<Image>().color == yellow) && 
                transform.GetChild(i).GetComponent<Image>().color != red && 
                transform.GetChild(i).GetComponent<Image>().color != green)
            {
                transform.GetChild(i).GetComponent<Image>().color = white;
            }
            if (transform.GetChild(i).GetComponent<Image>().color != red)
            {
                transform.GetChild(i).GetComponent<Advance>().yp = 0;
                transform.GetChild(i).GetComponent<Advance>().xp = 0;
            }
            transform.GetChild(i).GetComponent<Advance>().visited = false;
            transform.GetChild(i).GetComponent<Advance>().visited2 = false;
            transform.GetChild(i).GetComponent<Advance>().Dest = false;
            transform.GetChild(i).GetComponent<Advance>().start = false;
            transform.GetChild(i).GetComponent<Advance>().cost = -1;
        }
        Alg = drp.value;
        Cover.SetActive(false);
        Reset.SetActive(false);
    }
    public class Tiles
    {
        public Image img;
        public int x, y;
        List<Tiles> T = new List<Tiles>();
        public Tiles()
        {
        }
        public Tiles(Image I, int Y, int X)
        {
            img = I;
            x = X;
            y = Y;
        }
        public void ADDT(Image I, int Y, int X)
        {
            I.color = new Color32(255, 255, 255, 255);
            T.Add(new Tiles(I, Y, X));
        }
        public Image GetImage(int Y, int X)
        {
            return GameObject.Find((X + 77 * Y + 1).ToString()).GetComponent<Image>();
        }
        public int GetY(Image img)
        {
            int N = int.Parse(img.name), m = 0;
            if(N % 77 == 0)
            {
                m = 1;
            }
            else
            {
                m = 0;
            }
            return (int)((N / 77) - m);
        }
        public int GetX(Image img)
        {
            int N = int.Parse(img.name);
            if (N % 77 == 0)
            {
                return 76;
            }
            else {
                return (N % 77) - 1;
            }
        }
        public Image Left(int Y, int X)
        {
            if ((X - 1) < 0)
            {
                return null;
            }
            else
            {
                return T[(X - 1) + 77 * Y].img;
            }
        }
        public Image Right(int Y, int X)
        {
            if ((X + 1) > 76)
            {
                return null;
            }
            else
            {
                return T[(X+1) + 77 * Y].img;
            }
        }
        public Image Top(int Y, int X)
        {
            if ((Y + 1) > 38)
            {
                return null;
            }
            else
            {
                return T[X + 77 * (Y+1)].img;
            }
        }
        public Image Bottum(int Y, int X)
        {
            if ((Y - 1) < 0)
            {
                return null;
            }
            else
            {
                return T[X + 77 * (Y - 1)].img;
            }
        }
    }
}
